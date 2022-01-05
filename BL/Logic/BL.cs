using BlApi;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace BO
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

        //static readonly BL instance = new();
        //internal static BL Instance { get { return instance; } }
        public static BL Instance { get { return Nested.instance; } }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void EventRegistration(EventHandler e, string obj)
        {
            if (obj == "Drone")
                DroneChanged += e;
            if (obj == "Station")
                StationChanged += e;
            if (obj == "Customer")
                CustomerChanged += e;
            if (obj == "Parcel")
                ParcelChanged += e;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void EventDelete(EventHandler e, string obj)
        {
            if (obj == "Drone")
                DroneChanged -= e;
            if (obj == "Station")
                StationChanged -= e;
            if (obj == "Customer")
                CustomerChanged -= e;
            if (obj == "Parcel")
                ParcelChanged -= e;
        }

        /// <summary>
        /// running the events
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void EventsAction()
        {
            if (StationChanged != null)
                StationChanged(this, EventArgs.Empty);
            if (DroneChanged != null)
                DroneChanged(this, EventArgs.Empty);
            if (CustomerChanged != null)
                CustomerChanged(this, EventArgs.Empty);
            if (ParcelChanged != null)
                ParcelChanged(this, EventArgs.Empty);
        }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly BL instance = new BL();
        }

        private BL()
        {
            dal = DalFactory.GetDal("DalXml");

            FreeElectricityUse = dal.BatteryUseRequest()[0];
            CarryingLightElectricityUse = dal.BatteryUseRequest()[1];
            CarryingMediemElectricityUse = dal.BatteryUseRequest()[2];
            CarryingHeavyElectricityUse = dal.BatteryUseRequest()[3];
            ChargePace = dal.BatteryUseRequest()[4];

            Random r = new();
            drones = new();

            //update the dal drone list for bl drone list
            foreach (DalApi.Drone dalDrone in dal.DroneList())
            {
                DroneToList drone = new();
                drone.CurrentLocation = new();
                drone.Id = dalDrone.Id;
                drone.Model = dalDrone.Model;
                drone.MaxWeight = (WeightCategories)dalDrone.MaxWeight;
                Location stationLocation = new();
                drones.Add(drone);

                //the drone is associated to parcel
                if (GetDroneSituation(drone.Id) is not ("Free" or "Maintenance"))
                {
                    drone.Status = DroneStatuses.sending;
                    DalApi.Parcel parcel = dal.ParcelList().FirstOrDefault(x => x.DroneId == dalDrone.Id);
                    drone.DeliveredParcelId = parcel.Id;

                    if (GetDroneSituation(drone.Id) == "Associated")
                        drone.CurrentLocation = StationLocation(ClosestStation(SenderLocation(parcel), dal.StationList()));

                    if (GetDroneSituation(drone.Id) == "Executing")
                        drone.CurrentLocation = SenderLocation(parcel);

                    int batteryUse = (int)BatteryUseInDelivery(drone, parcel);
                    if (batteryUse < 99)
                        drone.BatteryStatus = r.Next(batteryUse, 99) + 1;
                }

                else
                {
                    drone.DeliveredParcelId = 0;
                    //randome status between free and maintenance
                    drone.Status = (DroneStatuses)r.Next(2);


                    if (GetDroneSituation(drone.Id) == "Maintenance")
                    {
                        DalApi.Station station = new();
                        station = dal.StationList().ElementAt(r.Next((int)dal.StationList().LongCount() - 1));
                        drone.CurrentLocation = StationLocation(station);
                        drone.BatteryStatus = r.Next(0, 20);
                        dal.ChargeDrone(drone.Id, station.Id);
                    }

                    //is not maintenance or associated to parcel
                    else
                    {
                        if (ReceivedCustomersList().Any())
                        {
                            drone.CurrentLocation = CustomerLocation(ReceivedCustomersList().ElementAt(r.Next(ReceivedCustomersList().Count() - 1)));
                        }

                        drone.BatteryStatus = r.Next((int)FreeElectricityUse * (int)LocationsDistance(drone.CurrentLocation,
                                                    StationLocation(ClosestStation(drone.CurrentLocation, dal.StationList()))), 99)
                                                    + 1;
                    }
                }
            }
        }

        
        private double LocationsDistance(Location l1, Location l2)
             => dal.DistanceCalculate(l1.Latitude, l1.Longitude, l2.Latitude, l2.Longitude);
    }
}


