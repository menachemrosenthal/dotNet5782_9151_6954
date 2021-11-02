using IDAL.DO;
using System;

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
        public void AddStation(Station station)
        {
            int nextStation = DataSource.Config.nextStation++;
            DataSource.Stations[nextStation] = new Station();
            DataSource.Stations[nextStation] = station;
        }


        /// <summary>
        /// add a drone to the drones array
        /// </summary>
        /// <param name="drone">the drone for add</param>
        public void AddDrone(Drone drone)
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
        public void Addcustumer(Customer customer)
        {
            int nc = DataSource.Config.nextCustomer;
            DataSource.Customers[nc] = new();
            DataSource.Config.nextCustomer++;
            DataSource.Customers[nc] = customer;
        }


        /// <summary>
        /// add a parcel to the parcels array
        /// </summary>
        /// <param name="parcel">the parcel to add</param>
        public void AddParcel(Parcel parcel)
        {
            parcel.Id = DataSource.Config.createParcelNumber++;
            int nextParcel = DataSource.Config.nextParcel++;
            DataSource.Parcels[nextParcel] = new();
            DataSource.Parcels[nextParcel] = parcel;
        }



        /// <summary>
        /// connect parcel to drone
        /// </summary>
        /// <param name="parcelId">parcel number to connect</param>
        /// <param name="droneId">drone id</param>
        public void ParcelToDrone(int parcelId, int droneId)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
            {
                if (parcelId == DataSource.Parcels[i].Id)
                {
                    DataSource.Parcels[i].DroneId = droneId;
                    DataSource.Parcels[i].Scheduled = DateTime.Now;
                }
            }
        }


        /// <summary>
        /// the time of pickup a parcel by drone update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdatePickup(int parcelId)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
            {
                if (parcelId == DataSource.Parcels[i].Id)
                    DataSource.Parcels[i].PickedUp = DateTime.Now;

                //the drone status update to sending
                for (int j = 0; j < DataSource.Config.nextDrone; j++)
                    if (DataSource.Parcels[i].DroneId == DataSource.Drones[j].Id)
                        DataSource.Drones[j].Status = DroneStatuses.sending;
            }
        }


        /// <summary>
        /// parcel delivery time update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdateDelivery(int parcelId)
        {
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
            {
                if (parcelId == DataSource.Parcels[i].Id)
                {
                    DataSource.Parcels[i].Delivered = DateTime.Now;

                    //the drone status uodate to free
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
        public void ChargeDrone(int droneId, int stationId)
        {

            for (int i = 0; i < DataSource.Config.nextDrone; i++)
            {
                if (droneId == DataSource.Drones[i].Id)
                {
                    DataSource.Drones[i].Status = DroneStatuses.maintenance;
                    //create drone charge object
                    DataSource.DronesCharge[DataSource.Config.nextDroneCharge++] = new() { DroneId = droneId, StationId = stationId };
                }
            }

            //charge slots update
            for (int i = 0; i < DataSource.Config.nextStation; i++)
            {
                if (stationId == DataSource.Stations[i].Id)
                    DataSource.Stations[i].ChargeSlots--;
            }
        }


        /// <summary>
        /// drone release from chrage
        /// </summary>
        /// <param name="droneId">drone id to release</param>
        public void EndCharge(int droneId)
        {
            //drone status update
            for (int i = 0; i < DataSource.Config.nextDrone; i++)
            {
                if (droneId == DataSource.Drones[i].Id)
                { 
                    DataSource.Drones[i].Status = DroneStatuses.free;
                    break;
                }
            }

            //charge slots update
            for (int i = 0; i < DataSource.Config.nextDroneCharge; i++)
            {
                if (DataSource.DronesCharge[i].DroneId == droneId)
                {
                    for (int j = 0; j < DataSource.Config.nextStation; j++)
                    {
                        if (DataSource.DronesCharge[i].StationId == DataSource.Stations[j].Id)
                        { 
                            DataSource.Stations[j].ChargeSlots++;
                            break;
                        }
                    }
                    break;
                }
            }
        }


        /// <summary>
        /// get station
        /// </summary>
        /// <param name="stationId">station id to return</param>
        /// <returns>station object</returns>
        public Station GetStation(int stationId)
        {
            int i = 0;
            for (; i < DataSource.Config.nextStation; i++)
            {
                if (stationId == DataSource.Stations[i].Id)
                    break;
            }
            return DataSource.Stations[i];
        }


        /// <summary>
        /// get droone
        /// </summary>
        /// <param name="droneId">drone id to return</param>
        /// <returns>drone object</returns>
        public Drone GetDrone(int droneId)
        {
            int i = 0;
            for (; i < DataSource.Config.nextDrone; i++)
            {
                if (droneId == DataSource.Drones[i].Id)
                    break;
            }
            return DataSource.Drones[i];
        }


        /// <summary>
        /// get customer
        /// </summary>
        /// <param name="customerId">customer id to return</param>
        /// <returns>customer object</returns>
        public Customer GetCustomer(int customerId)
        {
            int i = 0;
            for (; i < DataSource.Config.nextCustomer; i++)
            {
                if (customerId == DataSource.Customers[i].Id)
                    break;
            }
            return DataSource.Customers[i];
        }


        /// <summary>
        /// get parcel
        /// </summary>
        /// <param name="parcelId">parcel id to return</param>
        /// <returns>parcel object</returns>
        public Parcel GetParcel(int parcelId)
        {
            int i = 0;
            for (; i < DataSource.Config.nextParcel; i++)
            {
                if (parcelId == DataSource.Parcels[i].Id)
                    break;
            }
            return DataSource.Parcels[i];
        }


        /// <summary>
        /// get list of the stations
        /// </summary>
        /// <returns>station array</returns>
        public Station[] StationList()
        {
            Station[] stationList = new Station[DataSource.Config.nextStation];
            for (int i = 0; i < DataSource.Config.nextStation; i++)
            {
                stationList[i] = new Station();
                stationList[i] = DataSource.Stations[i];
            }
            return stationList;
        }


        /// <summary>
        /// get list of the customers
        /// </summary>
        /// <returns>customer array</returns>
        public Customer[] CustomerList()
        {
            Customer[] customerList = new Customer[DataSource.Config.nextCustomer];
            for (int i = 0; i < DataSource.Config.nextCustomer; i++)
            {
                customerList[i] = new Customer();
                customerList[i] = DataSource.Customers[i];
            }
            return customerList;
        }


        /// <summary>
        /// get list of the parcels
        /// </summary>
        /// <returns>parcel array</returns>
        public Parcel[] ParcelList()
        {
            Parcel[] parcelList = new Parcel[DataSource.Config.nextParcel];
            for (int i = 0; i < DataSource.Config.nextParcel; i++)
            {
                parcelList[i] = new Parcel();
                parcelList[i] = DataSource.Parcels[i];
            }
            return parcelList;
        }


        /// <summary>
        /// get the drone list
        /// </summary>
        /// <returns>drone list</returns>
        public Drone[] DroneList()
        {
            Drone[] droneList = new Drone[DataSource.Config.nextDrone];
            for (int i = 0; i < DataSource.Config.nextDrone; i++)
            {
                droneList[i] = new Drone();
                droneList[i] = DataSource.Drones[i];
            }
            return droneList;
        }


        public double DistanceCalculate(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist * 1.609344; ;
        }
    }
}