using System;
using System.Collections.Generic;
using System.Linq;
using BlApi;

namespace BO
{
    public partial class BL : IBL
    {        
        public event EventHandler DroneChanged;

        /// <summary>
        /// get drone list
        /// </summary>
        /// <returns>IEnumerable of drone list</returns>
        public IEnumerable<DroneToList> GetDroneList() => drones.ToList();

        /// <summary>
        /// updates that parcel was picked up
        /// </summary>
        /// <param name="droneId"></param>
        public void ParcelPickedupUptade(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));
            if (GetDroneSituation(droneId) == "Associated")
            {
                double distance = LocationsDistance(drone.CurrentLocation, SenderLocation(dal.GetParcel(drone.DeliveredParcelId)));
                drone.BatteryStatus -= distance * FreeElectricityUse;
                drone.CurrentLocation = SenderLocation(dal.GetParcel(drone.DeliveredParcelId));
                dal.UpdatePickup(drone.DeliveredParcelId);

                if (DroneChanged != null)
                    DroneChanged(this, EventArgs.Empty);

                if (ParcelChanged != null)
                    ParcelChanged(this, EventArgs.Empty);

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

            Random r = new();
            drone.BatteryStatus = r.Next(20, 40);
            drone.CurrentLocation = new();
            drone.CurrentLocation = StationLocation(dal.GetStation(stationID));
            drone.Status = DroneStatuses.maintenance;

            DalApi.Drone daldrone = new();
            daldrone.Id = drone.Id;
            daldrone.Model = drone.Model;
            daldrone.MaxWeight = (DalApi.WeightCategories)drone.MaxWeight;

            drones.Add(drone);
            dal.AddDrone(daldrone);
            dal.ChargeDrone(drone.Id, stationID);

            if (DroneChanged != null)
                DroneChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// update name of drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="updateName"></param>
        public void DroneNameUpdate(int droneId, string updateName)
        {
            if (!dal.DroneList().Any(x => x.Id == droneId))
                throw new KeyNotFoundException(nameof(droneId));
            DalApi.Drone drone = dal.DroneList().FirstOrDefault(x => x.Id == droneId);
            DroneToList blDrone = this.drones.FirstOrDefault(x => x.Id == droneId);
            drone.Model = updateName;
            blDrone.Model = updateName;
            dal.DroneUpdate(drone);

            int index = drones.IndexOf(blDrone);
            drones[index] = blDrone;

            if (DroneChanged != null)
                DroneChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// updates drone to charging state
        /// </summary>
        /// <param name="droneId"></param>
        public void ChargeDrone(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));
            if (GetDroneSituation(droneId) != "Free")
                throw new CannotUpdateExeption("drone", droneId, "drone is not free to charge");

            int index = drones.IndexOf(drone);

            //possible station location
            Location location = StationLocation(ClosestStation(drone.CurrentLocation, dal.GetStationsByCondition(x => x.ChargeSlots > 0)));

            double distance = LocationsDistance(drone.CurrentLocation, location);

            if (FreeElectricityUse * distance <= drone.BatteryStatus)
            {
                drone.CurrentLocation = location;
                drone.BatteryStatus -= FreeElectricityUse * distance;
                drone.Status = DroneStatuses.maintenance;
                drones[index] = drone;
                dal.ChargeDrone(droneId, ClosestStation(location, dal.StationList()).Id);

                if (DroneChanged != null)
                    DroneChanged(this, EventArgs.Empty);
                if (StationChanged != null)
                    StationChanged(this, EventArgs.Empty);
                return;
            }
            else
                throw new CannotUpdateExeption("drone", droneId, "not enough battery to reach station");
        }

        /// <summary>
        /// releases drone from chatge
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="time"></param>
        public void ReleaseDrone(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));

            if (drone.Status != DroneStatuses.maintenance)
                throw new CannotUpdateExeption("drone", droneId, "not in maintenance to be released");

            var time = dal.EndCharge(droneId);
            double timeToDouble = time.TotalMinutes;
            timeToDouble /= 60;
            drone.BatteryStatus += ChargePace * timeToDouble;
            if (drone.BatteryStatus > 100)
                drone.BatteryStatus = 100;

            drone.Status = DroneStatuses.free;

            int index = drones.IndexOf(drone);
            drones[index] = drone;

            if (DroneChanged != null)
                DroneChanged(this, EventArgs.Empty);

            if (StationChanged != null)
                StationChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// updates parcel to a drone
        /// </summary>
        /// <param name="droneId"></param>
        public void ParcelToDrone(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));

            if (GetDroneSituation(droneId) != "Free")
                throw new ArgumentException("Drone must be free", nameof(droneId));

            List<DalApi.Parcel> parcels = dal.GetParcelsByCondition(x => x.DroneId == 0).ToList();
            int weight = (int)drone.MaxWeight;
            parcels
                .OrderByDescending(x => x.Priority)
                .ThenByDescending(x => x.Weight)
                .ThenBy(x => LocationsDistance(SenderLocation(x), drone.CurrentLocation));
            //.Select(x => new { Parcel = x, Distance = LocationsDistance(SenderLocation(x), drone.CurrentLocation) })


            foreach (var parcel in parcels)
            {
                if (weight >= (int)parcel.Weight &&
                    drone.BatteryStatus >= BatteryUseInDelivery(drone, parcel))
                {
                    drone.Status = DroneStatuses.sending;
                    drone.DeliveredParcelId = parcel.Id;
                    dal.ParcelToDrone(parcel.Id, drone.Id);
                    drones[drones.IndexOf(drone)] = drone;

                    if (DroneChanged != null)
                        DroneChanged(this, EventArgs.Empty);

                    if (ParcelChanged != null)
                        ParcelChanged(this, EventArgs.Empty);

                    return;
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

            if (drone.Status == DroneStatuses.sending)
                drone.Parcel = GetParcelInTransfer(d.DeliveredParcelId);

            return drone;
        }

        /// <summary>
        /// drone list by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns>drone list by condition</returns>
        public IEnumerable<DroneToList> GetDronesByCondition(Predicate<DroneToList> condition)
                => drones.Where(x => condition(x));

        /// <summary>
        /// cheking drone status
        /// </summary>
        /// <param name="id">drone id for check</param>
        /// <returns>"Free" or "Associated" or "Executing"</returns>
        public string GetDroneSituation(int id)
        {
            if (drones.Any(x => x.Id == id && x.Status == DroneStatuses.maintenance))
                return "Maintenance";

            if (dal.ParcelList().Any(x => x.DroneId == id && x.Delivered == null))
                return dal.ParcelList().Any(x => x.DroneId == id && x.PickedUp == null) ? "Associated" : "Executing";

            return "Free";
        }

        /// <summary>
        /// calculates amount of battery use needed for delivery
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="parcel"></param>
        /// <returns>amount of battery use needed for delivery</returns>
        private double BatteryUseInDelivery(DroneToList drone, DalApi.Parcel parcel)
        {
            double BatteryUse = LocationsDistance(drone.CurrentLocation, SenderLocation(parcel)) * FreeElectricityUse
            + SenderTaregetDistance(parcel) * dal.BatteryUseRequest()[(int)parcel.Weight]
            + CustomerClosestStationDistance(parcel.TargetId) * FreeElectricityUse;

            return BatteryUse;
        }
    }
}
