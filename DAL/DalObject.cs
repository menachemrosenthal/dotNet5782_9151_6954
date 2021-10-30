using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Config.Initialize();
        }
        public static void Addstation(Station st)
        {
            int nst = DataSource.Config.nextStation;
            DataSource.Stations[nst] = new Station();
            DataSource.Config.nextStation++;
            DataSource.Stations[nst] = st;
        }
        public static void AddDrone(Drone dr) 
        {
            int ndr = DataSource.Config.nextDrone;
            DataSource.Drones[ndr] = new();
            DataSource.Config.nextDrone++;
            DataSource.Drones[ndr] = dr;
        }
        public static void Addcustumer(Customer cu)
        {
            int nc = DataSource.Config.nextCustomer;
            DataSource.Customers[nc] = new();
            DataSource.Config.nextCustomer++;
            DataSource.Customers[nc] =cu;
        }
        public static void Addparcel(Parcel pa)
        {
            int np = DataSource.Config.nextParcel;
            DataSource.Customers[np] = new();
            DataSource.Config.nextParcel++;
            DataSource.Parcels[np] = pa;
        }
        public void addParcel()
        {
            int npcl = DataSource.Config.nextParcel;
            DataSource.Parcels[npcl] = new Parcel();

            Console.WriteLine("Enter ID\n");
            DataSource.Parcels[npcl].Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter sender ID\n");
            DataSource.Parcels[npcl].Senderid = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter target ID\n");
            DataSource.Parcels[npcl].TargetId = int.Parse(Console.ReadLine());

            Console.WriteLine("Weight between 0-2\n");
            DataSource.Parcels[npcl].Weight = (WeightCategories)int.Parse(Console.ReadLine());

            DataSource.Parcels[npcl].DroneId = 0;

            DateTime currentDate = DateTime.Now;
            DataSource.Parcels[npcl].Requested = currentDate;
        }
        public static void parceltodrone(int pid,int did)
        {
            for(int i = 0; i < DataSource.Config.nextParcel; i++)
                if (pid == DataSource.Parcels[i].Id)
                    DataSource.Parcels[i].DroneId = did;
        }
        public static void Updatepickup(DateTime dt,int id)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
                if (id == DataSource.Parcels[i].Id)
                    DataSource.Parcels[i].PickedUp = dt;
        }
        public static void Updatedelivery(DateTime dt, int id)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
                if (id == DataSource.Parcels[i].Id)
                    DataSource.Parcels[i].Delivered = dt;
        }
        public static void Chargedrone(int did,int sid)
        {
            for (int i = 0; i < DataSource.Config.nextDrone; i++)
            {
                if (did == DataSource.Drones[i].Id)
                {
                    DataSource.Drones[i].Status = DroneStatuses.maintenance;
                    DroneCharge drg = new() { DroneId = did, StationId = sid };

                }
            }
        }
        public static void Endcharge(int did, int sid)
        {
            for (int i = 0; i < DataSource.Config.nextDrone; i++)
            {
                if (did == DataSource.Drones[i].Id)
                {
                    DataSource.Drones[i].Status = DroneStatuses.free;
                    //delete drg
                }
            }

        }
        public static Station GetStation(int id)
        {
            int i = 0;
            for (; i < DataSource.Config.nextStation; i++)
            {
                if (id == DataSource.Stations[i].Id)
                    break;
            }
            return DataSource.Stations[i];
        }
        public static Drone GetDrone(int id)
        {
            int i = 0;
            for (; i < DataSource.Config.nextDrone; i++)
            {
                if (id == DataSource.Drones[i].Id)
                    break;
            }
            return DataSource.Drones[i];
        }
        public static Customer GetCostumer(int id)
        {
            int i = 0;
            for (; i < DataSource.Config.nextCustomer; i++)
            {
                if (id == DataSource.Customers[i].Id)
                    break;
            }
            return DataSource.Customers[i];
        }
        public static Parcel GetParcel(int id)
        {
            int i = 0;
            for (; i < DataSource.Config.nextParcel; i++)
            {
                if (id == DataSource.Parcels[i].Id)
                    break;
            }
            return DataSource.Parcels[i];
        }
    }
}
