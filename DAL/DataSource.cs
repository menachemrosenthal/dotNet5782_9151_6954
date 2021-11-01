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
                        ChargeSlots = r.Next(2,8),
                        Longitude = (double)r.Next(31748768, 31810806) / 1000000,
                        Lattitude = (double)r.Next(34663817, 35223456) / 1000000
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
                        Longitude = (double)r.Next(31748768, 31810806) / 1000000,
                        Lattitude = (double)r.Next(34663817, 35223456) / 1000000
                    };
                }
                nextCustomer += 10;

                //initialize drones

                Drones[nextDrone++] = new Drone
                {
                    Id = r.Next(100),
                    Model = $"Ferari",
                    Battery = r.Next(50, 100),
                    MaxWeight = (WeightCategories)2,
                    Status = (DroneStatuses)2,
                };

                Drones[nextDrone++] = new Drone
                {
                    Id = r.Next(100),
                    Model = $"Mercedes",
                    Battery = r.Next(50, 100),
                    MaxWeight = (WeightCategories)2,
                    Status = (DroneStatuses)2,
                };

                Drones[nextDrone++] = new Drone
                {
                    Id = r.Next(100),
                    Model = $"Mitsubishi",
                    Battery = r.Next(50, 100),
                    MaxWeight = (WeightCategories)0,
                    Status = (DroneStatuses)0,
                };

                Drones[nextDrone++] = new Drone
                {
                    Id = r.Next(100),
                    Model = "Toyota",
                    Battery = r.NextDouble() * 100,
                    MaxWeight = (WeightCategories)0,
                    Status = (DroneStatuses)1,
                };

                DronesCharge[nextDroneCharge++] = new DroneCharge
                {
                    DroneId = Drones[3].Id,
                    StationId = Stations[1].Id
                };
                Stations[1].ChargeSlots--;

                //initialize parcels
                Parcels[0] = new Parcel
                {

                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[9].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    DroneId = Drones[0].Id,
                    Requested = currentDate,
                    Scheduled = currentDate,
                    PickedUp = currentDate
                };

                Parcels[1] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[8].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    DroneId = Drones[1].Id,
                    Requested = currentDate,
                    Scheduled = currentDate,
                    PickedUp = currentDate
                };

                Parcels[2] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[7].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };

                Parcels[3] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[6].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    DroneId = Drones[2].Id,
                    Requested = currentDate,
                    Scheduled = currentDate
                };

                Parcels[4] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[5].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };

                Parcels[5] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[4].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };

                Parcels[6] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[3].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };

                Parcels[7] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[2].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };

                Parcels[8] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[1].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };

                Parcels[9] = new Parcel
                {
                    Id = createParcelNumber++,
                    Senderid = DataSource.Customers[0].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                };
                nextParcel += 10;
            }
        }
    }
}