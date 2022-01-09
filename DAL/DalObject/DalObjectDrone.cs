using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DalApi
{
    internal partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// add a drone to the Drones array
        /// </summary>
        /// <param name="drone">the drone for add</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone)
        {
            var exist = DataSource.Drones.Any(x => x.Id == drone.Id);
            if (exist)
                throw new DalApi.AddExistException("Drone", drone.Id);
            DataSource.Drones.Add(drone);
        }

        /// <summary>
        /// send a drone to station for charge
        /// </summary>
        /// <param name="droneId">drone id</param>
        /// <param name="stationId">station id</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ChargeDrone(int droneId, int stationId)
        {
            var exist = DataSource.Drones.Any(x => x.Id == droneId);
            if (!exist)
                throw new DalApi.ItemNotFoundException("Drone", droneId);


            if (!(exist = DataSource.Stations.Any(x => x.Id == stationId)))
                throw new DalApi.ItemNotFoundException("Station", stationId);

            var station = DataSource.Stations.First(x => x.Id == stationId);
            if (station.ChargeSlots == 0)
                throw new NotFreeChargeSlot("Ther is not free charge slot, please wait");
            

            DataSource.DronesCharge.Add(new() { DroneId = droneId, StationId = stationId, time = DateTime.Now });

            var index = DataSource.Stations.IndexOf(station);
            station.ChargeSlots--;
            DataSource.Stations[index] = station;
        }

        /// <summary>
        /// drone release from chrage
        /// </summary>
        /// <param name="droneId">drone id to release</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public TimeSpan EndCharge(int droneId)
        {
            //drone status update
            var exist = DataSource.DronesCharge.Any(x => x.DroneId == droneId);
            if (!exist)
                throw new ItemNotFoundException("Drone", droneId, "in the drones charge");

            var droneCharge = DataSource.DronesCharge.First(x => x.DroneId == droneId);
            var station = DataSource.Stations.First(x => x.Id == droneCharge.StationId);
            var index = DataSource.Stations.IndexOf(station);
            var time = droneCharge.time;
            station.ChargeSlots++;
            DataSource.Stations[index] = station;
            DataSource.DronesCharge.Remove(droneCharge);
            return DateTime.Now - time;
        }

        /// <summary>
        /// get droone
        /// </summary>
        /// <param name="droneId">drone id to return</param>
        /// <returns>drone object</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            var exist = DataSource.Drones.Any(x => x.Id == droneId);
            if (!exist)
                throw new DalApi.ItemNotFoundException("Drone", droneId);

            return DataSource.Drones.FirstOrDefault(x => x.Id == droneId);
        }


        /// <summary>
        /// get droneCharging
        /// </summary>
        /// <param name="droneId">drone id to return</param>
        /// <returns>drone object</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private DroneCharge GetDroneCharging(int droneId)
        {
            var exist = DataSource.DronesCharge.Any(x => x.DroneId == droneId);
            if (!exist)
                throw new DalApi.ItemNotFoundException("Drone", droneId);

            return DataSource.DronesCharge.FirstOrDefault(x => x.DroneId == droneId);
        }
        /// <summary>
        /// get the drone list
        /// </summary>
        /// <returns>drone list</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> DroneList() => DataSource.Drones.ToList();

        /// <summary>
        /// get the drone charging list
        /// </summary>
        /// <returns>drone list</returns>
        public IEnumerable<DroneCharge> GetDroneChargingList(Predicate<DroneCharge> condition)
            => DataSource.DronesCharge.Where(x => condition(x)).ToList();

        /// <summary>
        /// update drone prioritys
        /// </summary>
        /// <param name="drone">drone for update</param>
        public void DroneUpdate(Drone drone)
        {
            var tmpDrone = DataSource.Drones.FirstOrDefault(x => x.Id == drone.Id);
            int index = DataSource.Drones.IndexOf(tmpDrone);
            DataSource.Drones[index] = drone;
        }

    }
}
