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
        /// <summary>
        /// constructor
        /// </summary>
        public DalObject()
        {
            DataSource.Config.Initialize();
        }


        /// <summary>
        /// add a station to the stations array
        /// </summary>
        /// <param name="station">the station for add</param>
        public static void AddStation(Station station)
        {
            int nst = DataSource.Config.nextStation++;
            DataSource.Stations[nst] = new Station();
            DataSource.Stations[nst] = station;
        }


        /// <summary>
        /// add a drone to the drones array
        /// </summary>
        /// <param name="drone">the drone for add</param>
        public static void AddDrone(Drone drone)
        {
            int ndr = DataSource.Config.nextDrone;
            DataSource.Drones[ndr] = new();
            DataSource.Config.nextDrone++;
            DataSource.Drones[ndr] = drone;
        }


        /// <summary>
        /// add a customer to the customers array array
        /// </summary>
        /// <param name="customer">the customer for add</param>
        public static void Addcustumer(Customer customer)
        {
            int nc = DataSource.Config.nextCustomer;
            DataSource.Customers[nc] = new();
            DataSource.Config.nextCustomer++;
            DataSource.Customers[nc] = customer;
        }


        /// <summary>
        /// add a parcel to the parcels array
        /// </summary>
        /// <param name="parcel"></param>
        public static void AddParcel(Parcel parcel)
        {
            parcel.Id = DataSource.Config.createParcelNumber++;
            int nextParcel = DataSource.Config.nextParcel++;
            DataSource.Parcels[nextParcel] = new();
            DataSource.Parcels[nextParcel] = parcel;
        }


        /// <summary>
        /// connecting a drone to parcel
        /// </summary>
        /// <param name="parcelId">the parcel number</param>
        /// <param name="droneId">drone id</param>
        public static void ParcelToDrone(int parcelId, int droneId)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
                if (parcelId == DataSource.Parcels[i].Id)
                {
                    DataSource.Parcels[i].DroneId = droneId;
                    DataSource.Parcels[i].Scheduled = DateTime.Now;
                }
        }


        /// <summary>
        /// the time of pickup a parcel by drone update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public static void UpdatePickup(int parcelId)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
            { 
                if (parcelId == DataSource.Parcels[i].Id)
                    DataSource.Parcels[i].PickedUp = DateTime.Now;

                for (int j = 0; j < DataSource.Config.nextDrone; j++)
                    if (DataSource.Parcels[i].DroneId == DataSource.Drones[j].Id)
                        DataSource.Drones[j].Status = DroneStatuses.sending;
            }
        }


        /// <summary>
        /// parcel delivery time update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public static void UpdateDelivery(int parcelId)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
            {
                if (parcelId == DataSource.Parcels[i].Id)
                {
                    DataSource.Parcels[i].Delivered = DateTime.Now;

                    for (int j = 0; j < DataSource.Config.nextDrone; j++)                    
                        if (DataSource.Parcels[i].DroneId == DataSource.Drones[j].Id)
                            DataSource.Drones[j].Status = DroneStatuses.free;                                            
                }
            }
        }


        /// <summary>
        /// send a drone to station for charge
        /// </summary>
        /// <param name="droneId">drone id</param>
        /// <param name="stationId">station id</param>
        public static void ChargeDrone(int droneId, int stationId)
        {
            for (int i = 0; i < DataSource.Config.nextDrone; i++)
            {
                if (droneId == DataSource.Drones[i].Id)
                {
                    DataSource.Drones[i].Status = DroneStatuses.maintenance;
                    DataSource.DronesCharge[DataSource.Config.nextDroneCharge] = new() { DroneId = droneId, StationId = stationId };
                    DataSource.Config.nextDroneCharge += 1;
                }
            }

            for (int i = 0; i < DataSource.Config.nextStation; i++)
            {
                if (stationId == DataSource.Stations[i].Id)
                    DataSource.Stations[i].ChargeSlots -= 1;
            }
        }


        /// <summary>
        /// drone release from chrage
        /// </summary>
        /// <param name="droneId">drone id to release</param>
        public static void EndCharge(int droneId)
        {
            for (int i = 0; i < DataSource.Config.nextDrone; i++)
            {
                if (droneId == DataSource.Drones[i].Id)
                {
                    DataSource.Drones[i].Status = DroneStatuses.free;
                    //delete drg
                    break;
                }
            }

            for (int i = 0; i < DataSource.Config.nextDroneCharge; i++)
            {
                if (DataSource.DronesCharge[i].DroneId == droneId)
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        public static Customer[] customerList()
        {
            Customer[] customerList = new Customer[DataSource.Config.nextCustomer];
            for (int i = 0; i < DataSource.Config.nextCustomer; i++)
            {
                customerList[i] = new Customer();
                customerList[i] = DataSource.Customers[i];
            }
            return customerList;
        }

        public static Parcel[] parcelList()
        {
            Parcel[] parcelList = new Parcel[DataSource.Config.nextParcel];
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
            {
                parcelList[i] = new Parcel();
                parcelList[i] = DataSource.Parcels[i];
            }
            return parcelList;
        }

        public static Drone[] droneList()
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
