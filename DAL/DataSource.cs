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
        //Arrays of data(Customers, Drones, stations,parcels, dronecharges)
        internal static List<Customer> Customers;
        internal static List<Drone> Drones;
        internal static List<Station> Stations;
        internal static List<Parcel> parcels;
        internal static List<DroneCharge> DronesCharge;

        internal class Config
        {
            internal static int createParcelNumber = 1;

            /// <summary>
            /// starting a project, initialize the data
            /// </summary>
            internal static void Initialize()
            {
                Random r = new Random();
                DateTime currentDate = DateTime.Now;

                //initialize stations
                Station station = new();
                for (int i = 0; i < 2; i++)
                {
                    station.Name = $"Station {r.Next(100, 1000)}";
                    station.Id = r.Next(100000000, 1000000000);
                    station.ChargeSlots = r.Next(2, 8);
                    station.Longitude = (double)r.Next(31748768, 31810806) / 1000000;
                    station.Lattitude = (double)r.Next(34663817, 35223456) / 1000000;

                    Stations.Add(station);
                }

                //initialize Customers
                Customer customer = new();
                for (int i = 0; i < 10; i++)
                {
                    customer.Id = r.Next(100000000, 1000000000);
                    customer.Name = $"person {i}";
                    customer.Phone = string.Format("0{0:###-#######}", r.Next(500000000, 599999999));
                    customer.Longitude = (double)r.Next(31748768, 31810806) / 1000000;
                    customer.Lattitude = (double)r.Next(34663817, 35223456) / 1000000;

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

                station = Stations[1];
                station.ChargeSlots--;
                Stations[1] = station;


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