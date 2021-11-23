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
        
        private List<DroneToList> drones;
        private IDal dal;

        public BL()
        {
            dal = new DalObject();

            FreeElectricityUse = dal.BatteryUseRquest()[0];
            CarryingLightElectricityUse = dal.BatteryUseRquest()[1];
            CarryingMediemElectricityUse = dal.BatteryUseRquest()[2];
            CarryingHeavyElectricityUse = dal.BatteryUseRquest()[3];
            ChargePace = dal.BatteryUseRquest()[4];

            Random r = new Random();
            drones = new();
            
            //update the dal drone list for bl drone list
            foreach (var Drone in dal.DroneList())
            {
                DroneToList drone = new();
                drone.CurrentLocation = new();
                drone.Id = Drone.Id;
                drone.Model = Drone.Model;
                drone.MaxWeight = (WeightCategories)Drone.MaxWeight;               
                Location stationLocation = new();

                if (!(DroneStatus(drone.Id) == "Free"))
                {
                    drone.Status = DroneStatuses.sending;
                    IDAL.DO.Parcel parcel = new();
                    parcel = dal.ParcelList().First(x => x.DroneId == Drone.Id);                    
                    drone.DeliveredParcelId = parcel.Id;

                    if (DroneStatus(drone.Id) == "Associated")
                        drone.CurrentLocation = StationLocation(ClosestStation(SenderLocation(parcel), dal.StationList()));

                    if (DroneStatus(drone.Id) == "Executing")
                        drone.CurrentLocation = SenderLocation(parcel);

                    int batteryUse = (int)BatteryUseInDelivery(drone, parcel);
                    if (batteryUse < 99)
                        drone.BatteryStatus = r.Next(batteryUse, 99) + 1;
                    drones.Add(drone);
                }

                if (DroneStatus(drone.Id) == "Free")
                {
                    drone.DeliveredParcelId = 0;
                    drone.Status = (DroneStatuses)r.Next(2);                    

                    if (drone.Status == DroneStatuses.maintenance)
                    {                        
                        IDAL.DO.Station station = new();
                        station = dal.StationList().ElementAt(r.Next((int)dal.StationList().LongCount() - 1));
                        drone.CurrentLocation = StationLocation(station);
                        drone.BatteryStatus = r.Next(0, 20);
                        drones.Add(drone);
                        dal.ChargeDrone(drone.Id, station.Id);
                    }

                    else
                    {                        
                        if (ReceivedCustomersList().Count() > 0)
                            drone.CurrentLocation = CustomerLocation(ReceivedCustomersList().ElementAt(r.Next(ReceivedCustomersList().Count() - 1)));
                        drone.BatteryStatus = r.Next((int)FreeElectricityUse * (int)LocationsDistance(drone.CurrentLocation, StationLocation(ClosestStation(drone.CurrentLocation, dal.StationList()))), 99) + 1;
                        drones.Add(drone);
                    }
                }
            }
        }

        public double LocationsDistance(Location l1, Location l2)
        {
             return dal.DistanceCalculate(l1.Latitude, l1.Longitude, l2.Latitude, l2.Longitude);
        }
    }
}


