using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                var exist = DataSource.Stations.Any(x => x.Id == station.Id);
                if (exist)
                    throw new IDAL.AddExistException("Station", station.Id, "station is alredy exist");
                DataSource.Stations.Add(station);
            }
            catch (IDAL.AddExistException ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }


        /// <summary>
        /// add a drone to the Drones array
        /// </summary>
        /// <param name="drone">the drone for add</param>
        public void AddDrone(Drone drone)
        {
            try
            {
                var exist = DataSource.Drones.Any(x => x.Id == drone.Id);
                if (exist)
                    throw new IDAL.AddExistException("Drone", drone.Id, "drone is alredy exist");
                DataSource.Drones.Add(drone);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return;
            }

        }


        /// <summary>
        /// add a customer to the Customers array array
        /// </summary>
        /// <param name="customer">the customer for add</param>
        public void AddCustumer(Customer customer)
        {
            try
            {
                var exist = DataSource.Customers.Any(x => x.Id == customer.Id);
                if (exist)
                    throw new IDAL.AddExistException("Customer", customer.Id, "customer is alredy exist");
                DataSource.Customers.Add(customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

        }


        /// <summary>
        /// add a parcel to the parcels array
        /// </summary>
        /// <param name="parcel">the parcel to add</param>
        public void AddParcel(Parcel parcel)
        {
            parcel.Id = DataSource.Config.CreateParcelNumber;
            DataSource.Parcels.Add(parcel);
        }


        /// <summary>
        /// connect parcel to drone
        /// </summary>
        /// <param name="parcelId">parcel number to connect</param>
        /// <param name="droneId">drone id</param>
        public void ParcelToDrone(int parcelId, int droneId)
        {
            try
            {
                var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Parcel", parcelId, "not found");

                if (!(exist = DataSource.Drones.Any(x => x.Id == droneId)))

                    throw new IDAL.ItemNotFoundException("Drone", droneId, "not found");

                var parcel = DataSource.Parcels.First(x => x.Id == parcelId);
                var index = DataSource.Parcels.IndexOf(parcel);
                parcel.DroneId = droneId;
                parcel.Scheduled = DateTime.Now;
                DataSource.Parcels[index] = parcel;
            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
                return;
            }

        }


        /// <summary>
        /// the time of pickup a parcel by drone update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdatePickup(int parcelId)
        {
            try
            {
                var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Parcel", parcelId, "not found");

                var parcel = DataSource.Parcels.First(x => x.Id == parcelId);
                var index = DataSource.Parcels.IndexOf(parcel);
                parcel.PickedUp = DateTime.Now;
                DataSource.Parcels[index] = parcel;

            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine((ex);
                return;
            }
        }


        /// <summary>
        /// parcel delivery time update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdateDelivery(int parcelId)
        {
            try
            {
                var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Parcel", parcelId, "not found");

                var parcel = DataSource.Parcels.First(x => x.Id == parcelId);
                var index = DataSource.Parcels.IndexOf(parcel);
                parcel.Delivered = DateTime.Now;
                DataSource.Parcels[index] = parcel;

            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// send a drone to station for charge
        /// </summary>
        /// <param name="droneId">drone id</param>
        /// <param name="stationId">station id</param>
        public void ChargeDrone(int droneId, int stationId)
        {
            var exist = DataSource.Drones.Any(x => x.Id == droneId);
            if (exist)
            {
                if (exist = DataSource.Stations.Any(x => x.Id == stationId))
                {
                    DataSource.DronesCharge.Add(new() { DroneId = droneId, StationId = stationId });

                    var station = DataSource.Stations.First(x => x.Id == stationId);
                    var index = DataSource.Stations.IndexOf(station);
                    station.ChargeSlots--;
                    DataSource.Stations[index] = station;
                }
            }
        }

        /// <summary>
        /// drone release from chrage
        /// </summary>
        /// <param name="droneId">drone id to release</param>
        public void EndCharge(int droneId)
        {
            //drone status update
            var exist = DataSource.DronesCharge.Any(x => x.DroneId == droneId);
            if (exist)
            {
                var droneCharge = DataSource.DronesCharge.First(x => x.DroneId == droneId);
                var station = DataSource.Stations.First(x => x.Id == droneCharge.StationId);
                var index = DataSource.Stations.IndexOf(station);
                station.ChargeSlots++;
                DataSource.Stations[index] = station;
                DataSource.DronesCharge.Remove(droneCharge);
            }
        }


        /// <summary>
        /// get station
        /// </summary>
        /// <param name="stationId">station id to return</param>
        /// <returns>station object</returns>
        public Station GetStation(int stationId) => DataSource.Stations.FirstOrDefault(x => x.Id == stationId);


        /// <summary>
        /// get droone
        /// </summary>
        /// <param name="droneId">drone id to return</param>
        /// <returns>drone object</returns>
        public Drone GetDrone(int droneId) => DataSource.Drones.FirstOrDefault(x => x.Id == droneId);


        /// <summary>
        /// get customer
        /// </summary>
        /// <param name="customerId">customer id to return</param>
        /// <returns>customer object</returns>
        public Customer GetCustomer(int customerId) => DataSource.Customers.FirstOrDefault(x => x.Id == customerId);


        /// <summary>
        /// get parcel
        /// </summary>
        /// <param name="parcelId">parcel id to return</param>
        /// <returns>parcel object</returns>
        public Parcel GetParcel(int parcelId) => DataSource.Parcels.FirstOrDefault(x => x.Id == parcelId);


        /// <summary>
        /// get list of the stations
        /// </summary>
        /// <returns>station array</returns>
        public IEnumerable<Station> StationList() => DataSource.Stations.ToList();


        /// <summary>
        /// get list of the Customers
        /// </summary>
        /// <returns>customer array</returns>
        public IEnumerable<Customer> CustomerList() => DataSource.Customers.ToList();


        /// <summary>
        /// get list of the parcels
        /// </summary>
        /// <returns>parcel array</returns>
        public IEnumerable<Parcel> ParcelList() => DataSource.Parcels.ToList();


        /// <summary>
        /// get the drone list
        /// </summary>
        /// <returns>drone list</returns>
        public IEnumerable<Drone> DroneList() => DataSource.Drones.ToList();


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