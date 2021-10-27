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
        internal static Customer[] Customers = new Customer[100];
        internal static Drone[] Drones = new Drone[10];
        internal static Station[] Stations = new Station[5];
        internal static Parcel[] Parcels = new Parcel[1000];
        internal  class Config
        {
            internal static int nextCustomer = 0;
            internal static int nextDrone = 0;
            internal static int nextStation = 0;
            internal static int nextParcel = 0;
            internal static int craeteParcelNumber = 11;
            internal static void Initialize()
            {
                Random r = new Random();
                DateTime currentDate = DateTime.Now;

                for (int i = 0; i < 2; i++)
                {
                    Stations[i] = new Station
                    {
                        Name = r.Next(100,1000),
                        Id = r.Next(100000000, 1000000000),
                        ChargeSlots = r.Next(10),
                        Longitude = r.Next(360),
                        Lattitude = r.Next(360)
                    };
                }

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

                for (int i = 0; i < 10; i++)
                {
                    Customers[i] = new Customer
                    {
                        Id = r.Next(100000000, 1000000000),
                        Name = $"person {i}",
                        Phone = string.Format("{00:##-#######}", r.Next(100000000, 100000000)),
                        Longitude = r.Next(360),
                        Lattitude = r.Next(360)
                    };
                }
                nextCustomer += 10;

                for (int i = 0; i < 10; i++)
                {
                    Parcels[i] = new Parcel
                    {
                        Id = r.Next(100000000, 1000000000),
                        Senderid = DataSource.Customers[r.Next(Config.nextCustomer - 1)].Id,
                        TargetId = r.Next(100000000, 1000000000),
                        Weight = (WeightCategories)r.Next(3),
                        Proirity = (Priorities)r.Next(3),
                        DroneId = r.Next(100000000, 1000000000),
                        Requested = currentDate,
                        Scheduled = currentDate,
                        PickedUp = currentDate,
                        Delivered = currentDate
                    };
                }
                nextParcel += 10;

            }
        }
    }
}