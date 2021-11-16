using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;

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
            bool flag = true;

            Console.WriteLine("Welcome to Drone Deliveries!");

            while (flag)
            {
                Console.WriteLine("\n\nPick one of the following options:\n"
                    + "For add menu press 1"
                    + "For Update menu press 2"
                    + "For Object display menu press 3"
                     + "For List disply menu press 4\n");

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

                    static void Add()
                    {
                        Console.WriteLine("\nPick one of the following add options:\n"
                        + "Base station, press 1\n Drone, press 2/n Customer press 3\n Parcel, press 4\n");

                        Enum.TryParse(Console.ReadLine(), out AddMenu choice);

                        switch (choice)
                        {
                            case AddMenu.baseStation: AddStation();
                                break;
                            case AddMenu.drone:
                                break;
                            case AddMenu.customer:
                                break;
                            case AddMenu.parcel:
                                break;
                            default:
                                break;
                        }
                        return;
                    }

                    static void Update()
                    {
                        Console.WriteLine("\nPick one of the following update options:\n"
                            + "Drone name, press 1\n Station, press 2\n Customer, press 3\n Sending drone to charge, press 4\n"
                            + "Release drone from charge, press 5\n Associating parcel to drone, press 6\n"
                            + "Parcel picked-up by drone, press 7\n Parcel provision by drone, press 8\n");

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
                            + "Base station, press 1\n Drone, press 2\n Customer, press 3\n Parcel, press 4\n");

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
                            + "Base stations, press 1\n Drones, press 2\n Customers, press 3\n Parcels, press 4\n"
                            + "Non drone associate parcels, press 5\n Unoccupied charge slots base stations, press 6\n");

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
        public static void AddStation()
        {

        }

    }
}
