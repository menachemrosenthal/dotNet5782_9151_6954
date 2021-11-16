using IDAL.DO;
using System;
using System.Collections.Generic;
namespace IDAL
{
    class DataSource
    {
        //Arrays of data(Customers, Drones, stations,parcels, dronecharges)
        internal static List<Customer> Customers = new();
        internal static List<Drone> Drones = new();
        internal static List<Station> Stations = new();
        internal static List<Parcel> Parcels = new();
        internal static List<DroneCharge> DronesCharge = new();

        internal class Config
        {
            private static int _createParcelNumber = 1;
            internal static int CreateParcelNumber => _createParcelNumber++;

            public static double Free { get; set; }
            public static double CarryingLight { get; set; }
            public static double CarryingMediem { get; set; }
            public static double CarryingHeavy { get; set; }
            public static double ChargePace { get; set; }
            /// <summary>
            /// starting a project, initialize the data
            /// </summary>
            internal static void Initialize()
            {
                Random r = new Random();
                DateTime currentDate = DateTime.Now;

                //initialize stations
                Stations = new List<Station> {
                    new Station
                    {
                        Name = $"Station {r.Next(100, 1000)}",
                        Id = r.Next(100000000, 1000000000),
                        ChargeSlots = r.Next(2, 8),
                        Longitude = (double)r.Next(31748768, 31810806) / 1000000,
                        Latitude = (double)r.Next(34663817, 35223456) / 1000000
                    },
                    new Station
                    {
                        Name = $"Station {r.Next(100, 1000)}",
                        Id = r.Next(100000000, 1000000000),
                        ChargeSlots = r.Next(2, 8),
                        Longitude = (double)r.Next(31748768, 31810806) / 1000000,
                        Latitude = (double)r.Next(34663817, 35223456) / 1000000
                    }
                };

                //initialize Customers
                Customer customer = new();
                for (int i = 0; i < 10; i++)
                {
                    customer.Id = r.Next(100000000, 1000000000);
                    customer.Name = $"person {i}";
                    customer.Phone = string.Format("0{0:###-#######}", r.Next(500000000, 599999999));
                    customer.Longitude = (double)r.Next(31748768, 31810806) / 1000000;
                    customer.Latitude = (double)r.Next(34663817, 35223456) / 1000000;

                    Customers.Add(customer);
                }

                //initialize Drones

                Drone drone = new();

                drone.Id = r.Next(100); drone.Model = $"Ferari"; drone.MaxWeight = (WeightCategories)2;
                Drones.Add(drone);

                drone.Id = r.Next(100); drone.Model = $"Mercedes"; drone.MaxWeight = (WeightCategories)2;
                Drones.Add(drone);

                drone.Id = r.Next(100); drone.Model = $"Mitsubishi"; drone.MaxWeight = (WeightCategories)0;
                Drones.Add(drone);

                drone.Id = r.Next(100); drone.Model = "Toyota"; drone.MaxWeight = (WeightCategories)0;
                Drones.Add(drone);


                DroneCharge droneCharge = new();
                droneCharge.DroneId = Drones[3].Id; droneCharge.StationId = Stations[1].Id;
                DronesCharge.Add(droneCharge);

                var station = Stations[1];
                station.ChargeSlots--;

                //initialize parcels
                Parcels.Add(new Parcel
                {

                    Id = CreateParcelNumber,
                    Senderid = DataSource.Customers[9].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    DroneId = Drones[0].Id,
                    Requested = currentDate,
                    Scheduled = currentDate,
                    PickedUp = currentDate
                });

                Parcels.Add(new Parcel
                {
                    Id = CreateParcelNumber,
                    Senderid = DataSource.Customers[8].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    DroneId = Drones[1].Id,
                    Requested = currentDate,
                    Scheduled = currentDate,
                    PickedUp = currentDate
                });

                Parcels.Add(new Parcel
                {
                    Id = CreateParcelNumber,
                    Senderid = DataSource.Customers[7].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                });

                Parcels.Add(new Parcel
                {
                    Id = CreateParcelNumber,
                    Senderid = DataSource.Customers[6].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    DroneId = Drones[2].Id,
                    Requested = currentDate,
                    Scheduled = currentDate
                });

                Parcels.Add(new Parcel
                {
                    Id = CreateParcelNumber,
                    Senderid = DataSource.Customers[5].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                });

                Parcels.Add(new Parcel
                {
                    Id = CreateParcelNumber,
                    Senderid = DataSource.Customers[4].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                });

                Parcels.Add(new Parcel
                {
                    Id = CreateParcelNumber,
                    Senderid = DataSource.Customers[3].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                });

                Parcels.Add(new Parcel
                {
                    Id = CreateParcelNumber,
                    Senderid = DataSource.Customers[2].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                });

                Parcels.Add(new Parcel
                {
                    Id = CreateParcelNumber,
                    Senderid = DataSource.Customers[1].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                });

                Parcels.Add(new Parcel
                {
                    Id = CreateParcelNumber,
                    Senderid = DataSource.Customers[0].Id,
                    TargetId = r.Next(100000000, 1000000000),
                    Weight = (WeightCategories)r.Next(3),
                    Priority = (Priorities)r.Next(3),
                    Requested = currentDate
                });
            }
        }
    }
}