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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="st">station to add into the array stations</param>
        public static void Addstation(Station st)
        {
            int nst = DataSource.Config.nextStation++;
            DataSource.Stations[nst] = new Station();
            DataSource.Stations[nst] = st;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr">drone to add into the array drones</param>
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
            DataSource.Customers[nc] = cu;
        }
        public static void AddParcel(Parcel pa)
        {
            pa.Id = DataSource.Config.createParcelNumber++;
            int np = DataSource.Config.nextParcel++;
            DataSource.Parcels[np] = new();
            DataSource.Parcels[np] = pa;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="did"></param>
        public static void ParcelToDrone(int pid, int did)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
                if (pid == DataSource.Parcels[i].Id)
                {
                    DataSource.Parcels[i].DroneId = did;
                    DataSource.Parcels[i].Scheduled = DateTime.Now;
                }
        }
        public static void UpdatePickup(int id)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
            { 
                if (id == DataSource.Parcels[i].Id)
                    DataSource.Parcels[i].PickedUp = DateTime.Now;

                for (int j = 0; j < DataSource.Config.nextDrone; j++)
                    if (DataSource.Parcels[i].DroneId == DataSource.Drones[j].Id)
                        DataSource.Drones[j].Status = DroneStatuses.sending;
            }
        }
        public static void UpdateDelivery(int id)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
            {
                if (id == DataSource.Parcels[i].Id)
                {
                    DataSource.Parcels[i].Delivered = DateTime.Now;

                    for (int j = 0; j < DataSource.Config.nextDrone; j++)                    
                        if (DataSource.Parcels[i].DroneId == DataSource.Drones[j].Id)
                            DataSource.Drones[j].Status = DroneStatuses.free;                                            
                }
            }
        }
        public static void ChargeDrone(int did, int sid)
        {
            for (int i = 0; i < DataSource.Config.nextDrone; i++)
            {
                if (did == DataSource.Drones[i].Id)
                {
                    DataSource.Drones[i].Status = DroneStatuses.maintenance;
                    DataSource.DronesCharge[DataSource.Config.nextDroneCharge] = new(){ DroneId = did, StationId = sid };
                    DataSource.Config.nextDroneCharge += 1;
                }
            }

            for (int i = 0; i < DataSource.Config.nextStation; i++)
            {
                if (sid == DataSource.Stations[i].Id)
                    DataSource.Stations[i].ChargeSlots -= 1;
            }
        }
        public static void EndCharge(int did)
        {
            for (int i = 0; i < DataSource.Config.nextDrone; i++)
            {
                if (did == DataSource.Drones[i].Id)
                {
                    DataSource.Drones[i].Status = DroneStatuses.free;
                    //delete drg
                    break;
                }
            }

            for (int i = 0; i < DataSource.Config.nextDroneCharge; i++)
            {
                if (DataSource.DronesCharge[i].DroneId == did)
                {
                    for (int j = 0; j < DataSource.Config.nextStation; j++)
                    {
                        if (DataSource.DronesCharge[i].StationId == DataSource.Stations[j].Id)
                            DataSource.Stations[j].ChargeSlots += 1; break;
                    }
                    break;
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
        public static Station[] StationList()
        {
            Station[] stationList = new Station[DataSource.Config.nextStation];
            for (int i = 0; i < DataSource.Config.nextStation; i++)
            {
                stationList[i] = new Station();
                stationList[i] = DataSource.Stations[i];
            }
            return stationList;
        }

        public static Customer[] CustomerList()
        {
            Customer[] customerList = new Customer[DataSource.Config.nextCustomer];
            for (int i = 0; i < DataSource.Config.nextCustomer; i++)
            {
                customerList[i] = new Customer();
                customerList[i] = DataSource.Customers[i];
            }
            return customerList;
        }

        public static Parcel[] ParcelList()
        {
            Parcel[] parcelList = new Parcel[DataSource.Config.nextParcel];
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
            {
                parcelList[i] = new Parcel();
                parcelList[i] = DataSource.Parcels[i];
            }
            return parcelList;
        }

        public static Drone[] DroneList()
        {
            Drone[] droneList = new Drone[DataSource.Config.nextDrone];
            for (int i = 0; i < DataSource.Config.nextDrone; i++)
            {
                droneList[i] = new Drone();
                droneList[i] = DataSource.Drones[i];
            }
            return droneList;
        }
    }
}
