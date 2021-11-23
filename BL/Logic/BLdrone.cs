using BL;
using BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public IEnumerable<DroneToList> GetDroneList() => drones.Select(x => x);

        public void ParcelPickedupUptade(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));
            if (DroneStatus(droneId) == "Associated")
            {
                double distance = LocationsDistance(drone.CurrentLocation, SenderLocation(dal.GetParcel(drone.DeliveredParcelId)));
                drone.BatteryStatus -= distance * FreeElectricityUse;
                drone.CurrentLocation = SenderLocation(dal.GetParcel(drone.DeliveredParcelId));
                dal.UpdatePickup(drone.DeliveredParcelId);
                return;
            }
            else
            {
                throw new CannotUpdateExeption("drone", droneId, "drone is unassociated");
            }
        }

        public void AddDrone(DroneToList drone, int stationID)
        {
            if (drones.Any(x => x.Id == drone.Id))
                throw new DuplicateItemException($"DronID: {drone.Id} exists already.");
            if (!dal.StationList().Any(x => x.Id == stationID))
                throw new KeyNotFoundException(nameof(stationID));

            Random r = new Random();
            drone.BatteryStatus = r.Next(20, 40);
            drone.CurrentLocation = new();
            drone.CurrentLocation = StationLocation(dal.GetStation(stationID));
            drone.Status = DroneStatuses.maintenance;

            IDAL.DO.Drone daldrone = new();
            daldrone.Id = drone.Id;
            daldrone.Model = drone.Model;
            daldrone.MaxWeight = (IDAL.DO.WeightCategories)drone.MaxWeight;

            drones.Add(drone);
            dal.AddDrone(daldrone);
            dal.ChargeDrone(drone.Id, stationID);
        }

        public void DroneNameUpdate(int droneId, string updateName)
        {
            IDAL.DO.Drone drone = dal.DroneList().FirstOrDefault(x => x.Id == droneId);
            if (drone.Id == 0)
                throw new KeyNotFoundException(nameof(droneId));
            drone.Model = updateName;
            dal.DroneUpdate(drone);
        }

        public void ChargeDrone(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));
            if (DroneStatus(droneId) != "Free")
                throw new CannotUpdateExeption("drone", droneId, "drone is not free to charge");

            int index = drones.IndexOf(drone);

            //possible station location
            Location location = StationLocation(ClosestStation(drone.CurrentLocation, dal.StationsWithFreeSlots()));

            double distance = LocationsDistance(drone.CurrentLocation, location);

            if (FreeElectricityUse * distance <= drone.BatteryStatus)
            {
                drone.CurrentLocation = location;
                drone.BatteryStatus -= FreeElectricityUse * distance;
                drone.Status = DroneStatuses.maintenance;
                drones[index] = drone;
                dal.ChargeDrone(droneId, ClosestStation(location, dal.StationList()).Id);

                return;
            }
            else
            {
                throw new CannotUpdateExeption("drone", droneId, "not enough battery to reach station");
            }
        }

        public void ReleaseDrone(int droneId, TimeSpan time)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));

            if (drone.Status != DroneStatuses.maintenance)
                throw new CannotUpdateExeption("drone", droneId, "not in maintenance to be released");

            double timeToDouble = time.TotalMinutes;
            timeToDouble /= 60;
            drone.BatteryStatus = ChargePace * timeToDouble;
            if (drone.BatteryStatus > 100)
                drone.BatteryStatus = 100;

            drone.Status = DroneStatuses.free;
            dal.EndCharge(droneId);
            int index = drones.IndexOf(drone);
            drones[index] = drone;
        }

        public void ParcelToDrone(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));

            if (DroneStatus(droneId) != "Free")
                throw new ArgumentException("Drone must be free", nameof(droneId));

            List<IDAL.DO.Parcel> parcels = dal.ParcelList().Where(x => x.DroneId == 0).ToList();
            int weight = (int)drone.MaxWeight;
            parcels
                .OrderByDescending(x => x.Priority)
                .ThenByDescending(x => x.Weight)
                .ThenBy(x => LocationsDistance(SenderLocation(x), drone.CurrentLocation))
                //.Select(x => new { Parcel = x, Distance = LocationsDistance(SenderLocation(x), drone.CurrentLocation) })
                .ToList()
                .ForEach(parcel =>
                {
                    if (weight >= (int)parcel.Weight)
                    {
                        if (drone.BatteryStatus >= BatteryUseInDelivery(drone, parcel))
                        {
                            drone.Status = DroneStatuses.sending;
                            drone.DeliveredParcelId = parcel.Id;
                            dal.ParcelToDrone(parcel.Id, drone.Id);
                            drones[drones.IndexOf(drone)] = drone;
                            return;
                        }
                    }
                });
            throw new UselessDroneException($"Couldn't find any match parcel for dron id: {droneId}");
        }

        public Drone GetDrone(int droneId)
        {
            DroneToList d = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));

            Drone drone = new();
            drone.Id = droneId;
            drone.Model = d.Model; drone.MaxWeight = d.MaxWeight;
            drone.Status = d.Status; drone.BatteryStatus = d.BatteryStatus;
            drone.CurrentLocation = d.CurrentLocation;
            if (drone.Status == DroneStatuses.sending)
            {
                drone.Parcel = GetParcelInTransfer(d.DeliveredParcelId);
            }
            return drone;
        }

        //                   **************        Auxiliary functions          ***************           //

        /// <summary>
        /// cheking drone status
        /// </summary>
        /// <param name="id">drone id for check</param>
        /// <returns>"Free" or "Associated" or "Executing"</returns>
        private string DroneStatus(int id)
        {
            if (drones.Any(x => x.Id == id && x.Status == DroneStatuses.maintenance))
                return "Maintenece";
            if (!dal.ParcelList().Any(x => x.DroneId == id))
                return "Free";
            else
            {
                IDAL.DO.Parcel parcel = new();
                parcel = dal.ParcelList().First(x => x.DroneId == id);
                if (parcel.PickedUp == null)
                    return "Associated";
                if (parcel.Delivered == null)
                    return "Executing";
            }
            return "Free";
        }

        private double BatteryUseInDelivery(DroneToList drone, IDAL.DO.Parcel parcel)
        {
            double BatteryUse = LocationsDistance(drone.CurrentLocation, SenderLocation(parcel)) * FreeElectricityUse;
            BatteryUse += SenderTaregetDistance(parcel) * dal.BatteryUseRquest()[(int)parcel.Weight];
            BatteryUse += CustomerClosestStationDistance(parcel.TargetId) * FreeElectricityUse;

            return BatteryUse;
        }
    }
}
