﻿using BlApi;
using BO;
using System;

namespace ConsoleUI_BL
{
    class Program
    {
        public enum MainMenu { add = 1, update, display, lists, exit }
        public enum AddMenu { baseStation = 1, drone, customer, parcel }
        public enum UpdateMenu { droneName = 1, station, customer, droneToCharge, releaseDrone, parcelToDrone, parcelPickedup, parcelProvision }
        public enum DisplayMenu { baseStation = 1, drone, customer, parcel }
        public enum ListsMenu { baseStations = 1, drones, customers, parcels, nonDroneParcels, unoccupiedSlotsBaseStations }
       
        static void Main(string[] args)
        {
            IBL bl = new BL.BlFactory();

            bool flag = true;

            Console.WriteLine("Welcome to Drone Deliveries!");

            while (flag)
            {
                Console.WriteLine("\n\nPick one of the following options:\n For add menu press 1\n"
                 + " For Update menu press 2\n For Object display menu press 3\n For List disply menu press 4\n");

                Enum.TryParse(Console.ReadLine(), out MainMenu choice);
                try
                {
                    switch (choice)
                    {
                        case MainMenu.add:
                            Add(bl);
                            break;
                        case MainMenu.update:
                            Update(bl);
                            break;
                        case MainMenu.display:
                            Display(bl);
                            break;
                        case MainMenu.lists:
                            Lists(bl);
                            break;
                        case MainMenu.exit:
                            flag = false;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void Add(BlApi.IBL bl)
        {
            Console.WriteLine("\nPick one of the following add options:\n"
            + " Base station, press 1\n Drone, press 2\n Customer press 3\n Parcel, press 4\n");

            Enum.TryParse(Console.ReadLine(), out AddMenu choice);

            switch (choice)
            {
                case AddMenu.baseStation:
                    AddStation(bl);
                    break;
                case AddMenu.drone:
                    AddDrone(bl);
                    break;
                case AddMenu.customer:
                    AddCustomer(bl);
                    break;
                case AddMenu.parcel:
                    AddParcel(bl);
                    break;
                default:
                    break;
            }
            return;
        }

        private static void Update(BlApi.IBL bl)
        {
            Console.WriteLine("\nPick one of the following update options:\n"
                + " Drone name, press 1\n Station, press 2\n Customer, press 3\n Sending drone to charge, press 4\n"
                + " Release drone from charge, press 5\n Associating parcel to drone, press 6\n"
                + " Parcel picked-up by drone, press 7\n Parcel provision by drone, press 8\n");

            Enum.TryParse(Console.ReadLine(), out UpdateMenu choice);

            switch (choice)
            {
                case UpdateMenu.droneName:
                    DroneNameUpdate(bl);
                    break;
                case UpdateMenu.station:
                    StationUpdate(bl);
                    break;
                case UpdateMenu.customer:
                    CustomerUpdate(bl);
                    break;
                case UpdateMenu.droneToCharge:
                    ChargeDrone(bl);
                    break;
                case UpdateMenu.releaseDrone:
                    ReleaseDrone(bl);
                    break;
                case UpdateMenu.parcelToDrone:
                    ParcelToDrone(bl);
                    break;
                case UpdateMenu.parcelPickedup:
                    ParcelPickedupUptade(bl);
                    break;
                case UpdateMenu.parcelProvision:
                    parcelProvision(bl);
                    break;
                default:
                    break;
            }
            return;
        }

        private static void Display(BlApi.IBL bl)
        {
            Console.WriteLine("\nPick one of the following object display options:\n"
                + " Base station, press 1\n Drone, press 2\n Customer, press 3\n Parcel, press 4\n");

            Enum.TryParse(Console.ReadLine(), out DisplayMenu choice);

            switch (choice)
            {
                case DisplayMenu.baseStation:
                    DisplayBaseStation(bl);
                    break;
                case DisplayMenu.drone:
                    DisplayDrone(bl);
                    break;
                case DisplayMenu.customer:
                    DisplayCustomer(bl);
                    break;
                case DisplayMenu.parcel:
                    DisplayParcel(bl);
                    break;
                default:
                    break;
            }
            return;
        }

        private static void Lists(BlApi.IBL bl)
        {
            Console.WriteLine("\n\nPick one of the list display options:\n"
                + " Base stations, press 1\n Drones, press 2\n Customers, press 3\n Parcels, press 4\n"
                + " Non drone associate parcels, press 5\n Unoccupied charge slots base stations, press 6\n");

            Enum.TryParse(Console.ReadLine(), out ListsMenu choice);

            switch (choice)
            {
                case ListsMenu.baseStations:
                    PrintBaseStationList(bl);
                    break;
                case ListsMenu.drones:
                    PrintDroneList(bl);
                    break;
                case ListsMenu.customers:
                    PrintCusromerList(bl);
                    break;
                case ListsMenu.parcels:
                    PrintParcelList(bl);
                    break;
                case ListsMenu.nonDroneParcels:
                    PrintNonAssociateParcelList(bl);
                    break;
                case ListsMenu.unoccupiedSlotsBaseStations:
                    PrintFreeChargingSlotsStationList(bl);
                    break;
                default:
                    break;
            }
            return;
        }

        private static void PrintFreeChargingSlotsStationList(BlApi.IBL bl)
        {
            foreach (var station in bl.GetFreeChargingSlotsStationList())
                Console.WriteLine(station);
        }

        private static void PrintNonAssociateParcelList(BlApi.IBL bl)
        {
            foreach (var parcel in bl.GetNonAssociateParcelList())
                Console.WriteLine(parcel);
        }

        private static void PrintParcelList(BlApi.IBL bl)
        {
            foreach (var parcel in bl.GetParcelList())
                Console.WriteLine(parcel);
        }

        private static void PrintCusromerList(BlApi.IBL bl)
        {
            foreach (var customer in bl.GetCustomerList())
            {
                Console.WriteLine(customer.ToString());
            }
        }

        private static void PrintDroneList(BlApi.IBL bl)
        {
            foreach (var drone in bl.GetDroneList())
                Console.WriteLine(drone.ToString());
        }

        private static void PrintBaseStationList(BlApi.IBL bl)
        {
            foreach (var station in bl.GetBaseStationList())
                Console.WriteLine(station.ToString());
        }

        private static void DisplayParcel(BlApi.IBL bl)
        {
            BO.Parcel parcel = new();
            Console.WriteLine("ENTER Parcel id");
            _ = int.TryParse(Console.ReadLine(), out int ParcelId);
            parcel = bl.GetParcel(ParcelId);
            Console.WriteLine(parcel);
        }

        private static void DisplayCustomer(BlApi.IBL bl)
        {
            BO.Customer customer = new();
            Console.WriteLine("ENTER Customer id");
            _ = int.TryParse(Console.ReadLine(), out int CustomerId);
            //customer = bl.getCustomer(CustomerId);
            Console.WriteLine(bl.GetCustomer(CustomerId));
        }

        private static void parcelProvision(BlApi.IBL bl)
        {
            Console.WriteLine("\nENTER Drone id");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            bl.ParcelProvisionUpdate(droneId);
        }

        private static void DisplayDrone(BlApi.IBL bl)
        {
            BO.Drone drone = new();
            Console.WriteLine("ENTER Drone id");
            _ = int.TryParse(Console.ReadLine(), out int DroneId);
            drone = bl.GetDrone(DroneId);
            Console.WriteLine(drone);
        }

        private static void DisplayBaseStation(BlApi.IBL bl)
        {
            BO.Station station = new();
            Console.WriteLine("ENTER Station id");
            _ = int.TryParse(Console.ReadLine(), out int stationId);

            Console.WriteLine(bl.GetStation(stationId));

        }

        private static void ParcelPickedupUptade(BlApi.IBL bl)
        {
            Console.WriteLine("ENTER Drone id");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            bl.ParcelPickedupUptade(droneId);
        }

        private static void ParcelToDrone(BlApi.IBL bl)
        {
            Console.WriteLine("\nENTER Drone id");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            bl.ParcelToDrone(droneId);
        }

        private static void ReleaseDrone(BlApi.IBL bl)
        {
            Console.WriteLine("\nENTER Drone id");
            _ = int.TryParse(Console.ReadLine(), out int droneId);
            Console.WriteLine("ENTER charging time 00:00");
            _ = TimeSpan.TryParse(Console.ReadLine(), out TimeSpan time);

            bl.ReleaseDrone(droneId);
        }

        private static void ChargeDrone(BlApi.IBL bl)
        {
            Console.WriteLine("\nENTER Drone id");
            _ = int.TryParse(Console.ReadLine(), out int droneId);

            bl.ChargeDrone(droneId);
        }

        private static void CustomerUpdate(BlApi.IBL bl)
        {
            Console.WriteLine("\nENTER Customer id");
            _ = int.TryParse(Console.ReadLine(), out int customerId);
            Console.WriteLine("\nENTER the new name or press enter");
            string newName = Console.ReadLine();
            Console.WriteLine("\nENTER the new phone number");
            string newPhoneNum = Console.ReadLine();

            Customer customer = new() { Id = customerId, Name = newName, Phone = newPhoneNum };
            bl.CustomerUpdate(customer);
        }

        private static void StationUpdate(BlApi.IBL bl)
        {
            Console.WriteLine("/nENTER Station id");
            _ = int.TryParse(Console.ReadLine(), out int stationId);
            Console.WriteLine("/nENTER a new name or press enter");
            string nameUpdate = Console.ReadLine();
            Console.WriteLine("/nENTER number of free charge slots or press enter");
            string freeChargSlots = Console.ReadLine();

            bl.StationUpdate(stationId, nameUpdate, freeChargSlots);
        }

        private static void DroneNameUpdate(BlApi.IBL bl)
        {
            Console.WriteLine("\nENTER Drone id");
            _ = int.TryParse(Console.ReadLine(), out int droneId);
            Console.WriteLine("\nETER the new name");
            string updateName = Console.ReadLine();

            bl.DroneNameUpdate(droneId, updateName);
        }


        private static void AddParcel(BlApi.IBL bl)
        {
            Parcel parcel = new();
            Console.WriteLine("\nENTER sender ID");
            _ = int.TryParse(Console.ReadLine(), out int senderId);
            Console.WriteLine("\nENTER Target ID");
            _ = int.TryParse(Console.ReadLine(), out int TargetId);
            Console.WriteLine("\nENTER Weight: light, medium or heavy");
            _ = Enum.TryParse(Console.ReadLine(), out WeightCategories weight);
            Console.WriteLine("\nENTER proirity: ragular, fast, urgent");
            _ = Enum.TryParse(Console.ReadLine(), out Priorities priority);

            parcel.Requested = DateTime.Now;

            parcel.Senderid = senderId;
            parcel.TargetId = TargetId;
            parcel.Weight = weight;
            parcel.Priority = priority;
            bl.AddParcel(parcel);
        }

        public static void AddStation(BlApi.IBL bl)
        {

            Station station = new();

            Console.WriteLine("ENTER Id");
            _ = int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nENTER Name");
            station.Name = Console.ReadLine();
            Console.WriteLine("\nENTER Charge slots");
            _ = int.TryParse(Console.ReadLine(), out int chargeSlots);
            Console.WriteLine("\nENTER longitude  from 34.5 to 35.9");
            _ = double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("\nENTER latitude from 31.589844 to  32.801705");
            _ = double.TryParse(Console.ReadLine(), out double lattitude);

            station.Id = id; station.ChargeSlots = chargeSlots;
            station.LocationOfStation = new()
            {
                Longitude = longitude,
                Latitude = lattitude
            };
            bl.AddStation(station);
        }

        public static void AddDrone(BlApi.IBL bl)
        {
            DroneToList drone = new();

            Console.WriteLine("ENTER Id\n");
            _ = int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nENTER Model");
            drone.Model = Console.ReadLine();
            Console.WriteLine("\nENTER MaxWeight: light, medium or heavy");
            _ = Enum.TryParse(Console.ReadLine(), out WeightCategories maxWeight);
            Console.WriteLine("ENTER station ID to charge\n");
            _ = int.TryParse(Console.ReadLine(), out int stationId);

            drone.Id = id; drone.MaxWeight = maxWeight;
            bl.AddDrone(drone, stationId);
        }

        public static void AddCustomer(BlApi.IBL bl)
        {
            Customer customer = new();

            Console.WriteLine("\nENTER Id");
            _ = int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nENTER Name");
            customer.Name = Console.ReadLine();
            Console.WriteLine("\nENTER phone");
            customer.Phone = Console.ReadLine();
            Console.WriteLine("\nENTER longitude  from 34.5 to 35.9");
            _ = double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("\nENTER latitude from 31.589844 to  32.801705");
            _ = double.TryParse(Console.ReadLine(), out double lattitude);

            customer.Id = id;
            customer.Location = new()
            {
                Longitude = longitude,
                Latitude = lattitude
            };
            bl.AddCustumer(customer);

        }
    }
}

