using BL;
using BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        /// <summary>
        /// get drone list
        /// </summary>
        /// <returns>IEnumerable of drone list</returns>
        public IEnumerable<DroneToList> GetDroneList() => drones.Select(x => x);

        /// <summary>
        /// updates that parcel was picked up
        /// </summary>
        /// <param name="droneId"></param>
        public void ParcelPickedupUptade(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));
            if (GetDroneStatus(droneId) == "Associated")
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

        /// <summary>
        /// add a drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="stationID"></param>
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

        /// <summary>
        /// update name of drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="updateName"></param>
        public void DroneNameUpdate(int droneId, string updateName)
        {
            IDAL.DO.Drone drone = dal.DroneList().FirstOrDefault(x => x.Id == droneId);
            DroneToList d = this.drones.FirstOrDefault(x => x.Id == droneId);
            if (drone.Id == 0)
                throw new KeyNotFoundException(nameof(droneId));
            drone.Model = updateName;
            d.Model = updateName;
            dal.DroneUpdate(drone);

            int index = this.drones.IndexOf(d);
            this.drones[index] = d;

        }

        /// <summary>
        /// updates drone to charging state
        /// </summary>
        /// <param name="droneId"></param>
        public void ChargeDrone(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));
            if (GetDroneStatus(droneId) != "Free")
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

        /// <summary>
        /// releases drone from chatge
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="time"></param>
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

        /// <summary>
        /// updates parcel to a drone
        /// </summary>
        /// <param name="droneId"></param>
        public void ParcelToDrone(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));

            if (GetDroneStatus(droneId) != "Free")
                throw new ArgumentException("Drone must be free", nameof(droneId));

            List<IDAL.DO.Parcel> parcels = dal.ParcelList().Where(x => x.DroneId == 0).ToList();
            int weight = (int)drone.MaxWeight;
            parcels
                .OrderByDescending(x => x.Priority)
                .ThenByDescending(x => x.Weight)
                .ThenBy(x => LocationsDistance(SenderLocation(x), drone.CurrentLocation))
                .Select(x => new { Parcel = x, Distance = LocationsDistance(SenderLocation(x), drone.CurrentLocation) })
                .ToList();
            
                foreach(var parcel in parcels)
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
                };
            throw new UselessDroneException($"Couldn't find any match parcel for dron id: {droneId}");
        }

        /// <summary>
        /// gets drone and creates bl object
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>created drone</returns>
        public Drone GetDrone(int droneId)
        {
            DroneToList d = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));


            Drone drone = new()
            {
                Model = d.Model,
                MaxWeight = d.MaxWeight,
                Status = d.Status,
                BatteryStatus = d.BatteryStatus,
                CurrentLocation = d.CurrentLocation,
                Id = droneId
            };

            drone.CurrentLocation = d.CurrentLocation;

            if (drone.Status == DroneStatuses.sending)
            {
                drone.Parcel = GetParcelInTransfer(d.DeliveredParcelId);
            }

            return drone;
        }

        /// <summary>
        /// cheking drone status
        /// </summary>
        /// <param name="id">drone id for check</param>
        /// <returns>"Free" or "Associated" or "Executing"</returns>
        private string GetDroneStatus(int id)
        {
            if (drones.Any(x => x.Id == id && x.Status == DroneStatuses.maintenance))
                return "Maintenece";

            if (dal.ParcelList().Any(x => x.DroneId == id && x.Delivered == null))
            {
                IDAL.DO.Parcel parcel = dal.ParcelList().FirstOrDefault(x => x.DroneId == id && x.PickedUp == null);
                if (dal.ParcelList().Any(x => x.DroneId == id && x.PickedUp == null))
                    return "Associated";

                return "Executing";
            }
            return "Free";
        }

        /// <summary>
        /// calculates amount of battery use needed for delivery
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="parcel"></param>
        /// <returns>amount of battery use needed for delivery</returns>
        private double BatteryUseInDelivery(DroneToList drone, IDAL.DO.Parcel parcel)
        {
            double BatteryUse = LocationsDistance(drone.CurrentLocation, SenderLocation(parcel)) * FreeElectricityUse;
            BatteryUse += SenderTaregetDistance(parcel) * dal.BatteryUseRquest()[(int)parcel.Weight];
            BatteryUse += CustomerClosestStationDistance(parcel.TargetId) * FreeElectricityUse;

            return BatteryUse;
        }
    }
}
