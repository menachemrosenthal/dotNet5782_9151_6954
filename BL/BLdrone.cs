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
        public void AddDrone(Drone drone, int stationID)
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
    }
}
