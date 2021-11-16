using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace IBL.BO
{
    public partial class BLdrone : IBL
    {
        public void AddDrone(Drone drone, int stationID)
        {
            Random r = new Random();
            drone.BatteryStatus = (double)r.Next(20,40);
            drone.CurrentLocation.Longitude = dal.GetStation(stationID).Longitude;
            drone.CurrentLocation.Latittude = dal.GetStation(stationID).Lattitude;
            drone.Status = Enums.DroneStatuses.maintenance;
            IDAL.DO.Drone daldrone = new();
            daldrone.Id = drone.Id; daldrone.Model = drone.Model;
            daldrone.MaxWeight = (IDAL.DO.WeightCategories)drone.MaxWeight;
            dal.AddDrone(daldrone);
        }
    }
}
