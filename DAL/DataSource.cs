using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DalObject
{
    class DataSource
    {
        //Arrays of data(customers, drones, stations, parcels, dronecharges)
        internal static Customer[] Customers = new Customer[100];
        internal static Drone[] Drones = new Drone[10];
        internal static Station[] Stations = new Station[5];
        internal static Parcel[] Parcels = new Parcel[1000];
        internal static DroneCharge[] DronesCharge = new DroneCharge[10];
        internal class Config
        {
            //pointers to next free element in arrays
            internal static int nextCustomer = 0;
            internal static int nextDrone = 0;
            internal static int nextStation = 0;
            internal static int nextParcel = 0;
            internal static int nextDroneCharge = 0;
            internal static int createParcelNumber = 1;

            /// <summary>
            /// starting a project, initialize the data
            /// </summary>
            internal static void Initialize()
            {
                Random r = new Random();
                DateTime currentDate = DateTime.Now;

                //initialize stations
                for (int i = 0; i < 2; i++)
                {
                    Stations[i] = new Station
                    {
                        Name = $"Station {r.Next(100, 1000)}",
                        Id = r.Next(100000000, 1000000000),
                        ChargeSlots = r.Next(10),
                        Longitude = r.Next(360),
                        Lattitude = r.Next(360)
                    };
                }
                nextStation += 2;

                //initialize drones
                for (int i = 0; i < 5; i++)
                {
                    Drones[i] = new Drone
                    {
                        Id = r.Next(100),
                        Model = $"Ferari {i}",
                        Battery = 50 + i,
                        MaxWeight = (WeightCategories)r.Next(3),
                        Status = (DroneStatuses)r.Next(3),
                    };
                }
                nextDrone += 5;

                //initialize customers
                for (int i = 0; i < 10; i++)
                {
                    Customers[i] = new Customer
                    {
                        Id = r.Next(100000000, 1000000000),
                        Name = $"person {i}",
                        Phone = string.Format("0{0:###-#######}", r.Next(500000000, 599999999)),
                        Longitude = r.Next(360),
                        Lattitude = r.Next(360)
                    };
                }
                nextCustomer += 10;

                //initialize parcels
                for (int i = 0; i < 10; i++)
                {
                    int choice = r.Next(0, 4);
                    if (choice == 0)
                    {
                        Parcels[i] = new Parcel
                        {
                            Id = createParcelNumber++,
                            Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                            TargetId = r.Next(100000000, 1000000000),
                            Weight = (WeightCategories)r.Next(3),
                            Priority = (Priorities)r.Next(3),
                            DroneId = 0,
                            Requested = currentDate
                        };
                    }
                    if (choice == 1)
                    {
                        Parcels[i] = new Parcel
                        {
                            Id = createParcelNumber++,
                            Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                            TargetId = r.Next(100000000, 1000000000),
                            Weight = (WeightCategories)r.Next(3),
                            Priority = (Priorities)r.Next(3),
                            DroneId = DataSource.Drones[r.Next(Config.nextDrone - 1)].Id,
                            Requested = currentDate,
                        };
                        DalObject.ParcelToDrone(Parcels[i].Id, Parcels[i].DroneId);
                    }
                    if (choice == 2)
                    {
                        Parcels[i] = new Parcel
                        {
                            Id = createParcelNumber++,
                            Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                            TargetId = r.Next(100000000, 1000000000),
                            Weight = (WeightCategories)r.Next(3),
                            Priority = (Priorities)r.Next(3),
                            DroneId = DataSource.Drones[r.Next(Config.nextDrone - 1)].Id,
                            Requested = currentDate,
                            Scheduled = currentDate,
                        };
                        DalObject.ParcelToDrone(Parcels[i].Id, Parcels[i].DroneId);
                        DalObject.UpdatePickup(Parcels[i].Id);
                    }
                    if (choice == 3)
                    {
                        Parcels[i] = new Parcel
                        {
                            Id = createParcelNumber++,
                            Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                            TargetId = r.Next(100000000, 1000000000),
                            Weight = (WeightCategories)r.Next(3),
                            Priority = (Priorities)r.Next(3),
                            DroneId = DataSource.Drones[r.Next(Config.nextDrone - 1)].Id,
                            Requested = currentDate,
                        };
                        DalObject.ParcelToDrone(Parcels[i].Id, Parcels[i].DroneId);
                        DalObject.UpdatePickup(Parcels[i].Id);
                        DalObject.UpdateDelivery(Parcels[i].Id);
                    }
                }
                nextParcel += 10;
            }
        }
    }
}