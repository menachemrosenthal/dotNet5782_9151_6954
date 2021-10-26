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
        internal static Customer[] customers = new Customer[100];
        internal static Drone[]    drones    = new Drone[10];
        internal static Station[]  stations  = new Station[5];
        internal static Parcel[]   parcels   = new Parcel[1000];

        internal class Config
        {
            internal static int nextCustomer = 0;
            internal static int nextDrone = 0;
            internal static int nextStation = 0;
            internal static int nextParcel = 0;

            internal static int craeteParcelNumber = 11;

            internal void Initialize() 
            {
                Random  r = new Random();

                for(int i = 0; i < 2; i++)
                {
                    stations[i] = new Station
                    {
                        Name = 'a' + i,
                        Id = r.Next(100000000,1000000000),
                        ChargeSlots = r.Next(10),
                        Longitude = r.Next(360),
                        Lattitude = r.Next(360)
                    };
                }
                for (int i = 0; i < 5; i++)
                {
                    drones[i] = new Drone
                    {
                        Id = r.Next(100),
                        Model = (Names)r.Next(9),
                        Battery = 50 + i,
<<<<<<< HEAD
                        MaxWeight = (WeightCategories)r.Next(3),
                        Status = DroneStatuses.free
=======
                        MaxWeight = WeightCategories,
                        Status=DroneStatuses.free
>>>>>>> acba88d482c6d5275b8550f181aceecba03c1f32
                    };
                }
                nextDrone += 5;

                for (int i = 0; i < 10; i++)
                {
                    customers[i] = new Customer
                    {
                        Id = r.Next(100000000,1000000000),
                        Name = "abc" + i,
<<<<<<< HEAD
                        Phone= string.Format("{00:##-#######}",r.Next(100000000,100000000)),
                        Longitude = r.Next(360),
                        Lattitude = r.Next(360)
                    };
                }
                nextCustomer += 10;

=======
                        Phone = @"{058}-{667788+i}",
                        Longitude = 31.78 + 200 * i,
                        Lattitude = 35.20 + 300 * i
                    };
                }
                for (int i = 0; i < 10; i++)
                {
                    parcels[i] = new Parcel
                    {
                        Id = 222222251 + i,
                        Senderid { get; set; }
                        TargetId { get; set; }
                        Weight { get; set; }
                        Proirity { get; set; }
                        DroindId { get; set; }
                        Requested { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
        };
                }
>>>>>>> acba88d482c6d5275b8550f181aceecba03c1f32




            }
        }
    }
}
