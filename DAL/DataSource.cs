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
        //Arrays of data(customers, drones, stations,parcels, dronecharges)
        internal static List<Customer> Customers;
        internal static List<Drone> Drones;
        internal static List<Station> Stations;
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> DronesCharge;
        internal  class Config
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
                        Name = $"Station {r.Next(100,1000)}",
                        Id = r.Next(100000000, 1000000000),
                        ChargeSlots = r.Next(10),
                        Longitude = (double)r.Next(3600)/10,
                        Lattitude = r.Next(360)
                    };                    
                }
                nextStation += 2;



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

                Parcels[0] = new Parcel
                {
                   
                        Id = createParcelNumber++,
                    Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    DroneId = Drones[0].Id,
                    Requested = currentDate,
                    Scheduled = currentDate
                };
                Parcels[1] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    DroneId = Drones[1].Id,
                    Requested = currentDate,
                    Scheduled = currentDate

                };
                Parcels[2] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };
                Parcels[3] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };
                Parcels[4] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };
                Parcels[5] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };
                Parcels[6] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };
                Parcels[7] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };
                Parcels[8] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };
                Parcels[9] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };
                nextParcel += 10;
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
                        if (Drones[i].Status == DroneStatuses.sending)
                        {
                            dalObject.ParcelToDrone(Parcels[i].Id, Drones[i].Id);
                            dalObject.UpdatePickup(Parcels[i].Id);
                        }
                        if (Drones[i].Status == DroneStatuses.maintenance)
                        {
                            dalObject.ChargeDrone(Drones[i].Id, Stations[i % 2].Id);
                        }
                    }
                    nextDrone += 5;

                }
        }
    }
}