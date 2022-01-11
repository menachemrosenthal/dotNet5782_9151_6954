using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using BlApi;

namespace BO
{
    public partial class BL : IBL
    {
        private static object BlLock = new();
            
        /// <summary>
        /// get drone list
        /// </summary>
        /// <returns>IEnumerable of drone list</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDroneList() { lock (BlLock) { return drones.ToList(); } }

        /// <summary>
        /// updates that parcel was picked up
        /// </summary>
        /// <param name="droneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ParcelPickedupUptade(int droneId)
        {
            lock (dal)
            {
                DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));
                if (GetDroneSituation(droneId) == "Associated")
                {
                    double distance = LocationsDistance(drone.CurrentLocation, SenderLocation(dal.GetParcel(drone.DeliveredParcelId)));
                    drone.BatteryStatus -= distance * FreeElectricityUse;
                    drone.CurrentLocation = SenderLocation(dal.GetParcel(drone.DeliveredParcelId));
                    dal.UpdatePickup(drone.DeliveredParcelId);
                    drones[drones.IndexOf(drone)] = drone;
                    return;
                }
                else
                {
                    throw new CannotUpdateExeption("drone", droneId, "drone is unassociated");
                }
            }
        }

        /// <summary>
        /// add a drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="stationID"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(DroneToList drone, int stationID)
        {
            lock (dal)
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
            }
        }

        /// <summary>
        /// update name of drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="updateName"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneNameUpdate(int droneId, string updateName)
        {
            lock (dal)
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
            }
        }

        /// <summary>
        /// updates drone to charging state
        /// </summary>
        /// <param name="droneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ChargeDrone(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));
            if (GetDroneSituation(droneId) != "Free")
                throw new CannotUpdateExeption("drone", droneId, "drone is not free to charge");

            int index = drones.IndexOf(drone);

            lock (dal)
            {
                //station for charge
                DalApi.Station station = StationForCharging(droneId);

                double distance = LocationsDistance(drone.CurrentLocation, StationLocation(station));

                if (drone.BatteryStatus - (FreeElectricityUse * distance) > 0)
                {
                    drone.CurrentLocation = StationLocation(station);
                    drone.BatteryStatus -= FreeElectricityUse * distance;
                    drones[index] = drone;
                    try
                    {
                        dal.ChargeDrone(droneId, station.Id);
                        drone.Status = DroneStatuses.maintenance;
                    }
                    catch (Exception ex)
                    {
                        throw new NotFreeChargeSlot("There is not free charge slot please wait", ex);
                    }
                    drones[index] = drone;
                    return;
                }
                else
                    throw new CannotUpdateExeption("drone", droneId, "not enough battery to reach station");
            }
        }

        /// <summary>
        /// releases drone from chatge
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="time"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDrone(int droneId)
        {
            lock (dal)
            {
                DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));

                if (drone.Status != DroneStatuses.maintenance)
                    throw new CannotUpdateExeption("drone", droneId, "not in maintenance to be released");

                var time = dal.EndCharge(droneId);
                double timeToDouble = time.TotalMinutes;
                timeToDouble *= 60;
                drone.BatteryStatus += ChargePace * timeToDouble;
                if (drone.BatteryStatus > 100)
                    drone.BatteryStatus = 100;

                drone.Status = DroneStatuses.free;

                int index = drones.IndexOf(drone);
                drones[index] = drone;
            }
        }

        /// <summary>
        /// updates parcel to a drone
        /// </summary>
        /// <param name="droneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ParcelToDrone(int droneId)
        {

            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));

            if (GetDroneSituation(droneId) != "Free")
                throw new ArgumentException("Drone must be free", nameof(droneId));

            lock (dal)
            {
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

                        return;
                    }
                };
                throw new UselessDroneException($"Couldn't find any match parcel for dron id: {droneId}");
            }
        }

        /// <summary>
        /// gets drone and creates bl object
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>created drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            lock (dal)
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
        }

        /// <summary>
        /// drone list by condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns>drone list by condition</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDronesByCondition(Predicate<DroneToList> condition)
        { lock (dal) { return drones.Where(x => condition(x)); } }

        /// <summary>
        /// cheking drone status
        /// </summary>
        /// <param name="id">drone id for check</param>
        /// <returns>"Free" or "Associated" or "Executing"</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string GetDroneSituation(int id)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == id);
            //if (drones.Any(x => x.Id == id && x.Status == DroneStatuses.maintenance))
            if (drone.Status == DroneStatuses.maintenance)
                return "Maintenance";

            lock (dal)
            {
                DalApi.Parcel parcel = dal.ParcelList().FirstOrDefault(x => x.DroneId == id);
                if (dal.ParcelList().Any(x => x.DroneId == id && x.Delivered == null))
                    return dal.ParcelList().Any(x => x.DroneId == id && x.PickedUp == null) ? "Associated" : "Executing";
            }
            return "Free";
        }

        /// <summary>
        /// calculates amount of battery use needed for delivery
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="parcel"></param>
        /// <returns>amount of battery use needed for delivery</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal double BatteryUseInDelivery(DroneToList drone, DalApi.Parcel parcel)
        {
            lock (dal)
            {
                double BatteryUse = LocationsDistance(drone.CurrentLocation, SenderLocation(parcel)) * FreeElectricityUse
                + SenderTaregetDistance(parcel) * dal.BatteryUseRequest()[(int)parcel.Weight]
                + CustomerClosestStationDistance(parcel.TargetId) * FreeElectricityUse;

                return BatteryUse;
            }
        }

        public void StartSimulator(int droneId, Action update, Func<bool> finish)
        {
            new Simulator(droneId, update, finish, this);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetSimulatorDrone()
        {
            return Simulator.Drone;
        }

        /// <summary>
        /// try to associated parcel to the drone and return string
        /// </summary>
        /// <param name="droneId">drone for association parcel to it</param>
        /// <returns>"No Parcels for delivery" or "The drone was associated successfully" or "Not enough battery"</returns>
         [MethodImpl(MethodImplOptions.Synchronized)]
        internal string SimulatorParcelToDrone(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId);
            List<DalApi.Parcel> parcels;

            lock (BlLock)
                parcels = dal.GetParcelsByCondition(x => GetParcelStatus(x.Id) == ParcelStatuses.defined).ToList();

            lock (dal)
            {
                if (!parcels.Any())
                    return "No match Parcel for delivery";

                int weight = (int)drone.MaxWeight;
                parcels
                    .OrderByDescending(x => x.Priority)
                    .ThenByDescending(x => x.Weight)
                    .ThenBy(x => LocationsDistance(SenderLocation(x), drone.CurrentLocation));

                foreach (var parcel in parcels)
                {
                    if (weight >= (int)parcel.Weight &&
                        drone.BatteryStatus >= BatteryUseInDelivery(drone, parcel))
                    {
                        drone.Status = DroneStatuses.sending;
                        drone.DeliveredParcelId = parcel.Id;
                        dal.ParcelToDrone(parcel.Id, drone.Id);
                        drones[drones.IndexOf(drone)] = drone;

                        return "Is associating";
                    }
                };
            }

            if (drone.BatteryStatus < 100)
                return "Not enough battery";

            return "No match Parcel for delivery";
        }

        internal DalApi.Station StationForCharging(int droneId)
        {
            Drone drone = GetDrone(droneId);
            double batteryUse;

            lock (dal)
            {
                var stations = dal.GetStationsByCondition(x => x.ChargeSlots > 0);

                DalApi.Station stationToCharge = ClosestStation(drone.CurrentLocation, stations);
                batteryUse = LocationsDistance(drone.CurrentLocation, StationLocation(stationToCharge))
                             * FreeElectricityUse;

                if (drone.BatteryStatus - batteryUse > 0)
                    return stationToCharge;

                return ClosestStation(drone.CurrentLocation, dal.StationList());
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal string SimulatorChargeDrone(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));
            if (GetDroneSituation(droneId) != "Free")
                return "drone is not free to charge";

            int index = drones.IndexOf(drone);

            //station for charge
            DalApi.Station station = StationForCharging(droneId);

            double distance = LocationsDistance(drone.CurrentLocation, StationLocation(station));

            lock (dal)
            {
                if (FreeElectricityUse * distance - drone.BatteryStatus > 0)
                {
                    drone.CurrentLocation = StationLocation(station);
                    drone.BatteryStatus -= FreeElectricityUse * distance;
                    drones[index] = drone;
                    try
                    {
                        dal.ChargeDrone(droneId, station.Id);
                        drone.Status = DroneStatuses.maintenance;                        
                        return "Drone is charging";
                    }
                    catch (Exception ex)
                    {                        
                        return "Not free charge slot";
                    }
                }
                else
                    return "not enough battery";
            }
        }
    }
}
