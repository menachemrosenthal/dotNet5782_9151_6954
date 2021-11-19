using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

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
            IBL.IBL bl = new BL();
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
                            Add();
                            break;
                        case MainMenu.update:
                            Update();
                            break;
                        case MainMenu.display:
                            Display();
                            break;
                        case MainMenu.lists:
                            Lists();
                            break;
                        case MainMenu.exit:
                            flag = false;
                            break;
                        default:
                            break;
                    }

                    void Add()
                    {
                        Console.WriteLine("\nPick one of the following add options:\n"
                        + " Base station, press 1\n Drone, press 2\n Customer press 3\n Parcel, press 4\n");

                        Enum.TryParse(Console.ReadLine(), out AddMenu choice);

                        switch (choice)
                        {
                            case AddMenu.baseStation: AddStation(bl);
                                break;
                            case AddMenu.drone: AddDrone(bl);
                                break;
                            case AddMenu.customer: AddCustomer(bl);
                                break;
                            case AddMenu.parcel: AddParcel(bl);
                                break;
                            default:
                                break;
                        }
                        return;
                    }

                    static void Update()
                    {
                        Console.WriteLine("\nPick one of the following update options:\n"
                            + " Drone name, press 1\n Station, press 2\n Customer, press 3\n Sending drone to charge, press 4\n"
                            + " Release drone from charge, press 5\n Associating parcel to drone, press 6\n"
                            + " Parcel picked-up by drone, press 7\n Parcel provision by drone, press 8\n");

                        Enum.TryParse(Console.ReadLine(), out UpdateMenu choice);

                        switch (choice)
                        {
                            case UpdateMenu.droneName:
                                break;
                            case UpdateMenu.station:
                                break;
                            case UpdateMenu.customer:
                                break;
                            case UpdateMenu.droneToCharge:
                                break;
                            case UpdateMenu.releaseDrone:
                                break;
                            case UpdateMenu.parcelToDrone:
                                break;
                            case UpdateMenu.parcelPickedup:
                                break;
                            case UpdateMenu.parcelProvision:
                                break;
                            default:
                                break;
                        }
                        return;
                    }

                    static void Display()
                    {
                        Console.WriteLine("\nPick one of the following object display options:\n"
                            + " Base station, press 1\n Drone, press 2\n Customer, press 3\n Parcel, press 4\n");

                        Enum.TryParse(Console.ReadLine(), out DisplayMenu choice);

                        switch (choice)
                        {
                            case DisplayMenu.baseStation:
                                break;
                            case DisplayMenu.drone:
                                break;
                            case DisplayMenu.customer:
                                break;
                            case DisplayMenu.parcel:
                                break;
                            default:
                                break;
                        }
                        return;
                    }

                    static void Lists()
                    {
                        Console.WriteLine("\n\nPick one of the list display options:\n"
                            + " Base stations, press 1\n Drones, press 2\n Customers, press 3\n Parcels, press 4\n"
                            + " Non drone associate parcels, press 5\n Unoccupied charge slots base stations, press 6\n");

                        Enum.TryParse(Console.ReadLine(), out ListsMenu choice);

                        switch (choice)
                        {
                            case ListsMenu.baseStations:
                                break;
                            case ListsMenu.drones:
                                break;
                            case ListsMenu.customers:
                                break;
                            case ListsMenu.parcels:
                                break;
                            case ListsMenu.nonDroneParcels:
                                break;
                            case ListsMenu.unoccupiedSlotsBaseStations:
                                break;
                            default:
                                break;
                        }
                        return;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private static void AddParcel(IBL.IBL bl)
        {
            Parcel parcel = new();
            Console.WriteLine("\nENTER sender ID");
            _ = int.TryParse(Console.ReadLine(), out int senderId);
            Console.WriteLine("\nENTER Target ID");
            _ = int.TryParse(Console.ReadLine(), out int TargetId);
            Console.WriteLine("\nENTER Weight: light, medium or heavy");
            _ = Enum.TryParse(Console.ReadLine(), out Enums.WeightCategories weight);
            Console.WriteLine("\nENTER proirity: ragular, fast, urgent");
            _ = Enum.TryParse(Console.ReadLine(), out Enums.Priorities priority);
            
            parcel.Requested = DateTime.Now;

            parcel.Senderid = senderId; parcel.TargetId = TargetId; 
            parcel.Weight = weight; parcel.Priority = priority;
            bl.AddParcel(parcel);
        }

        public static void AddStation(IBL.IBL bl)
        {

            Station station = new();

            Console.WriteLine("ENTER Id");
            _ = int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nENTER Name");
            station.Name = Console.ReadLine();
            Console.WriteLine("\nENTER Charge slots");
            _ = int.TryParse(Console.ReadLine(), out int chargeSlots);
            Console.WriteLine("\nENTER longitude");
            _ = double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("\nENTER latitude");
            _ = double.TryParse(Console.ReadLine(), out double latitude);

            station.Id = id; station.ChargeSlots = chargeSlots; station.LocationOfStation.Longitude = longitude;
            station.LocationOfStation.Latitude = latitude;
            bl.AddStation(station);            
        }

        public static void AddDrone(IBL.IBL bl)
        {
            Drone drone = new();

            Console.WriteLine("ENTER Id\n");
            _ = int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nENTER Model");
            drone.Model = Console.ReadLine();
            Console.WriteLine("\nENTER MaxWeight: light, medium or heavy");
            _ = Enum.TryParse(Console.ReadLine(), out Enums.WeightCategories maxWeight);
            Console.WriteLine("ENTER station ID to charge\n");
            _ = int.TryParse(Console.ReadLine(), out int stationId);

            drone.Id = id; drone.MaxWeight = maxWeight;
            bl.AddDrone(drone, stationId);
        }
        public static void AddCustomer(IBL.IBL bl)
        {
            Customer customer = new();

            Console.WriteLine("\nENTER Id");
            _ = int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("\nENTER Name");
            customer.Name = Console.ReadLine();
            Console.WriteLine("\nENTER phone");
            customer.Phone = Console.ReadLine();
            Console.WriteLine("\nENTER longitude");
            _ = double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("\nENTER latitude");
            _ = double.TryParse(Console.ReadLine(), out double lattitude);

            customer.Id = id; customer.Location.Longitude = longitude; 
            customer.Location.Latitude = lattitude;
            bl.AddCustumer(customer);

        }
    }
}

