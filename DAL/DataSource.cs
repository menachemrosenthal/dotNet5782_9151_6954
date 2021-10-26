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
        internal static Drone[]    drones = new Drone[10];
        internal static Station[]  stations = new Station[5];
        internal static Parcel[]   parcels = new Parcel[1000];

        internal class Config
        {
            internal static int nextCustomer = 0;
            internal static int nextDrone = 0;
            internal static int nextStation = 0;
            internal static int nextParcel = 0;

            internal static int craeteParcelNumber = 11;

            public void Initialize() 
            {
                for(int i = 0; i < 2; i++)
                {
                    stations[i] = new Station
                    {
                        Name = 'a' + i,
                        Id = 111 + 5 * i,
                        ChargeSlots = 5 + i,
                        Longitude = 31.78 + 20 * i,
                        Lattitude = 35.20 + 30 * i
                    };
                }
                for (int i = 0; i < 5; i++)
                {
                    drones[i] = new Drone
                    {
                        Id = 10 + i,
                        Model = "ferari" + i,
                        Battery = 50 + i,
                        MaxWeight = WeightCategories,
                        Status=DroneStatuses.free
                    };
                }
                for (int i = 0; i < 10; i++)
                {
                    customers[i] = new Customer
                    {
                        Id = 2222 + i,
                        Name = "abc" + i,
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
                        Senderid
                        TargetId
                        Weight
                        Proirity
                        DroindId
                        Requested
                        Scheduled
                        PickedUp
                        Delivered
        };
                }




            }
        }
    }
}
