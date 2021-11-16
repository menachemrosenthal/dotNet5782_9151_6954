using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public partial class BLdrone : IBL
    {
        public static double FreeElectricityUse { get; set; }
        public static double CarryingLightElectricityUse { get; set; }
        public static double CarryingMediemElectricityUse { get; set; }
        public static double CarryingHeavyElectricityUse { get; set; }
        public static double ChargePace { get; set; }
        List<Drone> Drones = new();
        IDal dal = new DalObject();

        public BLdrone()
        {
            dal = new DalObject();
            FreeElectricityUse = dal.ElectricityUseRquest()[0];
            CarryingLightElectricityUse = dal.ElectricityUseRquest()[1];
            CarryingMediemElectricityUse = dal.ElectricityUseRquest()[2];
            CarryingHeavyElectricityUse = dal.ElectricityUseRquest()[3];
            ChargePace = dal.ElectricityUseRquest()[4];

            Random r = new Random();
            Drone drone = new();
            foreach (var Drone in dal.DroneList())
            {
                drone.Id = Drone.Id;
                drone.Model = Drone.Model;
                drone.MaxWeight = (Enums.WeightCategories)Drone.MaxWeight;
                Location customerLocation = new();
                Location stationLocation = new();

                //the drone is associated to parcel
                if (dal.ParcelList().Any(x => x.DroneId == Drone.Id))
                {
                    drone.Status = Enums.DroneStatuses.sending;

                    IDAL.DO.Parcel parcel = new();
                    parcel = dal.ParcelList().First(x => x.DroneId == Drone.Id);

                    //the drone is sending
                    if (parcel.Delivered == null)
                    {
                        //the customer that parcel belong to
                        customerLocation.Longitude = dal.CustomerList().First(x => x.Id == parcel.Senderid).Longitude;
                        customerLocation.Latitude = dal.CustomerList().First(x => x.Id == parcel.Senderid).Latitude;

                        //if the parcel not yet pickedup
                        if (parcel.PickedUp == null)
                        {
                            drone.CurrentLocation.Longitude = dal.StationList().First().Longitude;
                            drone.CurrentLocation.Latitude = dal.StationList().First().Latitude;

                            //search for the closest station to the customer
                            foreach (var Station in dal.StationList())
                            {
                                Location location = new() { Longitude = Station.Longitude, Latitude = Station.Latitude };
                                if (LocationsDistance(drone.CurrentLocation, customerLocation) > LocationsDistance(customerLocation, location))
                                    drone.CurrentLocation = location;
                            }
                        }
                        //the parcel has alredy pickedup
                        else
                        {
                            //not yet delivered
                            if (parcel.Delivered == null)
                                drone.CurrentLocation = customerLocation;
                        }
                        Location targetLocation = new();

                        targetLocation.Longitude = dal.CustomerList().First(x => x.Id == parcel.TargetId).Longitude;
                        targetLocation.Latitude = dal.CustomerList().First(x => x.Id == parcel.TargetId).Latitude;
                        stationLocation.Longitude = dal.StationList().First().Longitude;
                        stationLocation.Latitude = dal.StationList().First().Latitude;
                        foreach (var Station in dal.StationList())
                        {
                            Location location = new() { Longitude = Station.Longitude, Latitude = Station.Latitude };
                            if (LocationsDistance(targetLocation, stationLocation) > LocationsDistance(targetLocation, location))
                                stationLocation = location;
                        }
                        drone.BatteryStatus = r.Next((int)CarryingHeavyElectricityUse * 
                            ((int)LocationsDistance(drone.CurrentLocation, targetLocation)
                            + (int)LocationsDistance(stationLocation, targetLocation)), 99) + 1;

                        Drones.Add(drone);
                    }
                    //the drone is free
                    else
                    {
                        drone.Status = (Enums.DroneStatuses)r.Next(2);
                        if (drone.Status == Enums.DroneStatuses.maintenance)
                        {
                            int index = r.Next((int)dal.StationList().LongCount());
                            drone.CurrentLocation.Longitude = dal.StationList().ElementAt(index).Longitude;
                            drone.CurrentLocation.Latitude = dal.StationList().ElementAt(index).Latitude;
                            drone.BatteryStatus = r.Next(0, 20);
                            Drones.Add(drone);
                        }
                        else
                        {
                            List<IDAL.DO.Customer> geters = new();
                            foreach (var customer in dal.CustomerList())
                            {
                                if (dal.ParcelList().Any(x => x.TargetId == customer.Id))
                                    geters.Add(customer);
                            }

                        }
                    }


                }

                else
                {

                }
            }
        }

        public double LocationsDistance(Location l1, Location l2)
        {
            return dal.DistanceCalculate(l1.Latitude, l1.Longitude, l2.Latitude, l2.Longitude);
        }
    }
}

;