﻿using IDAL;
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
            foreach (var Drone in dal.DroneList())
            {
                drone.Id = Drone.Id;
                drone.Model = Drone.Model;
                drone.MaxWeight = (Enums.WeightCategories)Drone.MaxWeight;
                Location customerLocation = new();

                //the drone is associated to parcel
                if (dal.ParcelList().Any(x => x.DroneId == Drone.Id))
                {
                    drone.Status = Enums.DroneStatuses.sending;

                    IDAL.DO.Parcel parcel = new();
                    parcel = dal.ParcelList().First(x => x.DroneId == Drone.Id);

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