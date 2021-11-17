using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public void AddDrone(DroneToLIst drone, int stationID)
        {
            Random r = new Random();
            drone.BatteryStatus = (double)r.Next(20,40);
            drone.CurrentLocation.Longitude = dal.GetStation(stationID).Longitude;
            drone.CurrentLocation.Latitude = dal.GetStation(stationID).Latitude;
            drone.Status = Enums.DroneStatuses.maintenance;
            IDAL.DO.Drone daldrone = new();
            daldrone.Id = drone.Id; daldrone.Model = drone.Model;
            daldrone.MaxWeight = (IDAL.DO.WeightCategories)drone.MaxWeight;
            dal.AddDrone(daldrone);
        }

        /// <summary>
        /// cheking drone status
        /// </summary>
        /// <param name="id">drone id for check</param>
        /// <returns>"Free" or "Associated" or "Executing"</returns>
        public string DroneStatus(int id)
        {
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

        public void DroneNameUpdate(int droneId, string updateName)
        {
            IDAL.DO.Drone drone = new();
            drone = dal.DroneList().First(x => x.Id == droneId);
            drone.Model = updateName;
            dal.DroneUpdate(drone);
        }

        public void ChargeDrone(int droneId)
        {            
            if (DroneStatus(droneId) == "Free")
            {
                DroneToList drone = new();
                drone = Drones.Find(x => x.Id == droneId);
                int index = Drones.IndexOf(drone);

                //possible station location
                Location location = new();
                location = StationLocation(ClosestStation(drone.CurrentLocation, dal.StationsWithFreeSlots()));

                double distance = LocationsDistance(drone.CurrentLocation, location);

                if (FreeElectricityUse*distance >= drone.BatteryStatus)
                {
                    drone.CurrentLocation = location;
                    drone.BatteryStatus -= FreeElectricityUse * distance;
                    drone.Status = Enums.DroneStatuses.maintenance;
                    Drones[index] = drone;
                    dal.ChargeDrone(droneId, ClosestStation(location, dal.StationList()).Id);

                    return;
                }
            }

            //throw ...
        }

        public void ReleaseDrone(int droneId, string time)
        {
            DroneToList drone = new();
            drone = Drones.Find(x=>x.Id == droneId);
            if(drone.Status == Enums.DroneStatuses.maintenance)
            {
                double timeToDouble = int.Parse(time.Substring(0, 2));
                timeToDouble += double.Parse(time.Substring(3, 2)) / 100 * 1.66666667;               
                drone.BatteryStatus = ChargePace * timeToDouble;
                drone.Status = Enums.DroneStatuses.free;
                dal.EndCharge(droneId);
                int index = Drones.IndexOf(drone);
                Drones[index] = drone;                
            }
            //else
                //throw....
        } 
    }
}
