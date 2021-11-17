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
        List<DroneToList> Drones = new();
        IDal dal = new DalObject();

        public BL()
        {
            FreeElectricityUse = dal.ElectricityUseRquest()[0];
            CarryingLightElectricityUse = dal.ElectricityUseRquest()[1];
            CarryingMediemElectricityUse = dal.ElectricityUseRquest()[2];
            CarryingHeavyElectricityUse = dal.ElectricityUseRquest()[3];
            ChargePace = dal.ElectricityUseRquest()[4];

            Random r = new Random();
            DroneToList drone = new();
            foreach (var Drone in dal.DroneList())
            {
                drone.Id = Drone.Id;
                drone.Model = Drone.Model;
                drone.MaxWeight = (Enums.WeightCategories)Drone.MaxWeight;
                Location customerLocation = new();
                Location stationLocation = new();

                if (!(DroneStatus(drone.Id) == "Free"))
                {
                    drone.Status = Enums.DroneStatuses.sending;
                    IDAL.DO.Parcel parcel = new();
                    parcel = dal.ParcelList().First(x => x.DroneId == Drone.Id);
                    customerLocation = CusromerLocation(dal.CustomerList().First(x => x.Id == parcel.Senderid));
                    drone.ParcelInTransferId = parcel.Id;

                    if (DroneStatus(drone.Id) == "Associated")
                        drone.CurrentLocation = StationLocation(ClosestStation(customerLocation,dal.StationList()));

                    if (DroneStatus(drone.Id) == "Executing")
                        drone.CurrentLocation = customerLocation;

                    drone.BatteryStatus = r.Next((int)CarryingHeavyElectricityUse *
                            ((int)LocationsDistance(drone.CurrentLocation, TargetLocation(parcel))
                            + (int)LocationsDistance(StationLocation(ClosestStation(TargetLocation(parcel),dal.StationList())), TargetLocation(parcel))), 99) + 1;

                    Drones.Add(drone);

                }

                if (DroneStatus(drone.Id) == "Free")
                {
                    drone.ParcelInTransferId = 0;
                    drone.Status = (Enums.DroneStatuses)r.Next(2);

                    if (drone.Status == Enums.DroneStatuses.maintenance)
                    {
                        drone.CurrentLocation = StationLocation(dal.StationList().ElementAt(r.Next((int)dal.StationList().LongCount())));
                        drone.BatteryStatus = r.Next(0, 20);
                        Drones.Add(drone);
                        dal.ChargeDrone(drone.Id, dal.StationList().First(x => StationLocation(x) == drone.CurrentLocation).Id);
                    }

                    else
                    {
                        drone.CurrentLocation = CusromerLocation(ReceivedCustomersList().ElementAt(r.Next((int)ReceivedCustomersList().LongCount())));
                        drone.BatteryStatus = r.Next((int)FreeElectricityUse * (int)LocationsDistance(drone.CurrentLocation, StationLocation(ClosestStation(drone.CurrentLocation, dal.StationList()))), 99) + 1;
                        Drones.Add(drone);
                    }
                }                         
            }
        }

        public double LocationsDistance(Location l1, Location l2)
        {
            return dal.DistanceCalculate(l1.Latitude, l1.Longitude, l2.Latitude, l2.Longitude);
        }
    }

    
};


