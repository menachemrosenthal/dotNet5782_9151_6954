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
            //int nextStation = DataSource.Config.nextStation++;
            //DataSource.Stations[nextStation] = new Station();
            //DataSource.Stations[nextStation] = station;
            //DataSource.Config.nextStation++;
            DataSource.Stations.Add(station);
        }


        /// <summary>
        /// add a drone to the Drones array
        /// </summary>
        /// <param name="drone">the drone for add</param>
        public void AddDrone(Drone drone)
        {
            //int ndr = DataSource.Config.nextDrone;
            //DataSource.Drones[ndr] = new();
            //DataSource.Drones[ndr] = drone;
            //DataSource.Config.nextDrone++;
            DataSource.Drones.Add(drone);
        }


        /// <summary>
        /// add a customer to the Customers array array
        /// </summary>
        /// <param name="customer">the customer for add</param>
        public void Addcustumer(Customer customer)
        {
            //int nc = DataSource.Config.nextCustomer;
            //DataSource.Customers[nc] = new();
            //DataSource.Config.nextCustomer++;
            DataSource.Customers.Add(customer);
        }


        /// <summary>
        /// add a parcel to the parcels array
        /// </summary>
        /// <param name="parcel">the parcel to add</param>
        public void AddParcel(Parcel parcel)
        {
            parcel.Id = DataSource.Config.createParcelNumber++;
            //int nextParcel = DataSource.Config.nextParcel++;
            //DataSource.Parcels[nextParcel] = new();
            DataSource.parcels.Add(parcel);
            //DataSource.Config.nextParcel++;
        }



        /// <summary>
        /// connect parcel to drone
        /// </summary>
        /// <param name="parcelId">parcel number to connect</param>
        /// <param name="droneId">drone id</param>
        public void ParcelToDrone(int parcelId, int droneId)
        {
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (parcelId == DataSource.parcels[i].Id)
                {
                    //DataSource.Parcels[i].DroneId = droneId;
                    //DataSource.Parcels[i].Scheduled = DateTime.Now;
                    Parcel temp;
                    temp = DataSource.parcels[i];
                    temp.DroneId = droneId;
                    DataSource.parcels[i] = temp;
                }
            }
        }


        /// <summary>
        /// the time of pickup a parcel by drone update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdatePickup(int parcelId)
        {
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (parcelId == DataSource.parcels[i].Id)
                {
                    Parcel temp1;
                    temp1 = DataSource.parcels[i];
                    temp1.PickedUp = DateTime.Now;
                    DataSource.parcels[i] = temp1;
                }
                   // DataSource.Parcels[i].PickedUp = DateTime.Now;

                //drone status update to sending
                for (int j = 0; j < DataSource.Drones.Count; j++)
                    if (DataSource.parcels[i].DroneId == DataSource.Drones[j].Id)
                    {
                        Drone temp2;
                        temp2 = DataSource.Drones[j];
                        temp2.Status= DroneStatuses.sending;
                        DataSource.Drones[j] = temp2;
                    }
                        //DataSource.Drones[j].Status = DroneStatuses.sending;
            }
        }


        /// <summary>
        /// parcel delivery time update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdateDelivery(int parcelId)
        {
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (parcelId == DataSource.parcels[i].Id)
                {
                    Parcel temp;
                    temp = DataSource.parcels[i];
                    temp.Delivered = DateTime.Now;
                    DataSource.parcels[i] = temp;
                    //DataSource.parcels[i].Delivered = DateTime.Now;

                    //drone status uodate to free
                    for (int j = 0; j < DataSource.Drones.Count; j++)
                        if (DataSource.parcels[i].DroneId == DataSource.Drones[j].Id)
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

            for (int i = 0; i < DataSource.Drones.Count; i++)
            {
                if (droneId == DataSource.Drones[i].Id)
                {   //drone status update
                    DataSource.Drones[i].Status = DroneStatuses.maintenance;
                    //create drone charge object
                    DroneCharge temp= new() { DroneId = droneId, StationId = stationId };
                    DataSource.DronesCharge.Add(temp);
                }
            }

            //charge slots update
            for (int i = 0; i < DataSource.Stations.Count; i++)
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
            for (int i = 0; i < DataSource.Drones.Count; i++)
            {
                if (droneId == DataSource.Drones[i].Id)
                { 
                    DataSource.Drones[i].Status = DroneStatuses.free;
                    break;
                }
            }

            //charge slots update
            for (int i = 0; i < DataSource.DronesCharge.Count; i++)
            {
                //found the correct drone charge object
                if (DataSource.DronesCharge[i].DroneId == droneId)
                {
                    for (int j = 0; j < DataSource.Stations.Count; j++)
                    {
                        //found the correct station
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
            for (; i < DataSource.Stations.Count; i++)
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

            for (; i < DataSource.Drones.Count; i++)
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

            for (; i < DataSource.Customers.Count; i++)
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

            for (; i < DataSource.parcels.Count; i++)
            {
                if (parcelId == DataSource.parcels[i].Id)
                    break;
            }
            return DataSource.parcels[i];
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
        /// get list of the Customers
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

        /// <summary>
        /// distance calculation between to geographic points
        /// </summary>
        /// <param name="lat1">user lattiude</param>
        /// <param name="lon1">user longitude</param>
        /// <param name="lat2">object lattiude</param>
        /// <param name="lon2">object longitude</param>
        /// <returns>distance in kilometer</returns>
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