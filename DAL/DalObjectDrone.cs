﻿using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// add a drone to the Drones array
        /// </summary>
        /// <param name="drone">the drone for add</param>
        public void AddDrone(Drone drone)
        {
            try
            {
                var exist = DataSource.Drones.Any(x => x.Id == drone.Id);
                if (exist)
                    throw new IDAL.AddExistException("Drone", drone.Id);
                DataSource.Drones.Add(drone);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return;
            }

        }

        /// <summary>
        /// send a drone to station for charge
        /// </summary>
        /// <param name="droneId">drone id</param>
        /// <param name="stationId">station id</param>
        public void ChargeDrone(int droneId, int stationId)
        {
            try
            {
                var exist = DataSource.Drones.Any(x => x.Id == droneId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Drone", droneId);


                if (!(exist = DataSource.Stations.Any(x => x.Id == stationId)))
                    throw new IDAL.ItemNotFoundException("Station", stationId);

                DataSource.DronesCharge.Add(new() { DroneId = droneId, StationId = stationId });

                var station = DataSource.Stations.First(x => x.Id == stationId);
                var index = DataSource.Stations.IndexOf(station);
                station.ChargeSlots--;
                DataSource.Stations[index] = station;
            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        /// <summary>
        /// drone release from chrage
        /// </summary>
        /// <param name="droneId">drone id to release</param>
        public void EndCharge(int droneId)
        {
            try
            {
                //drone status update
                var exist = DataSource.DronesCharge.Any(x => x.DroneId == droneId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Drone", droneId, "in the drones charge");

                var droneCharge = DataSource.DronesCharge.First(x => x.DroneId == droneId);
                var station = DataSource.Stations.First(x => x.Id == droneCharge.StationId);
                var index = DataSource.Stations.IndexOf(station);
                station.ChargeSlots++;
                DataSource.Stations[index] = station;
                DataSource.DronesCharge.Remove(droneCharge);
            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
                return;
            }

        }

        /// <summary>
        /// get droone
        /// </summary>
        /// <param name="droneId">drone id to return</param>
        /// <returns>drone object</returns>
        public Drone? GetDrone(int droneId)
        {
            try
            {
                var exist = DataSource.Drones.Any(x => x.Id == droneId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Drone", droneId);

                return DataSource.Drones.FirstOrDefault(x => x.Id == droneId);
            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        /// <summary>
        /// get the drone list
        /// </summary>
        /// <returns>drone list</returns>
        public IEnumerable<Drone> DroneList() => DataSource.Drones.ToList();


    }
}
