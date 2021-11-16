using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public static double FreeElectricityUse { get; set; }
        public static double CarryingLightElectricityUse { get; set; }
        public static double CarryingMediemElectricityUse { get; set; }
        public static double CarryingHeavyElectricityUse { get; set; }
        public static double ChargePace { get; set; }
        List<Drone> Drones = new();
        IDal dal = new DalObject();

        public BL()
        {
            dal = new DalObject();
            FreeElectricityUse = dal.ElectricityUseRquest()[0];
            CarryingLightElectricityUse = dal.ElectricityUseRquest()[1];
            CarryingMediemElectricityUse = dal.ElectricityUseRquest()[2];
            CarryingHeavyElectricityUse = dal.ElectricityUseRquest()[3];
            ChargePace = dal.ElectricityUseRquest()[4];

            Drone drone = new();
            Location droneLocation = new();
            foreach (var Drone in dal.DroneList())
            {
                drone.Id = Drone.Id;
                drone.Model = Drone.Model;
                drone.MaxWeight = (Enums.WeightCategories)Drone.MaxWeight;

                if (dal.ParcelList().Any(x => x.DroneId == Drone.Id && x.PickedUp == null))
                {
                    IDAL.DO.Parcel parcel = new();
                    parcel = dal.ParcelList().First(x => x.DroneId == Drone.Id);
                    Location customerLocation = new();                    

                    drone.Status = Enums.DroneStatuses.sending;
                    droneLocation.Longitude = dal.StationList().First().Longitude;
                    droneLocation.Latittude = dal.StationList().First().Lattitude;
                    foreach (var Station in dal.StationList())
                    {
                       
                    }
                }
                    

            }
        }
    }
}
