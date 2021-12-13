using IDAL.DO;
using System;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDal
    {
        /// <summary>
        /// add a station to the stations array
        /// </summary>
        /// <param name="station">the station for add</param>
        void AddStation(Station station);

        /// <summary>
        /// add a drone to the Drones array
        /// </summary>
        /// <param name="drone">the drone for add</param>
        void AddDrone(Drone drone);

        /// <summary>
        /// add a customer to the Customers array array
        /// </summary>
        /// <param name="customer">the customer for add</param>
        void AddCustumer(Customer customer);

        /// <summary>
        /// add a parcel to the parcels array
        /// </summary>
        /// <param name="parcel">the parcel to add</param>
        int AddParcel(Parcel parcel);

        /// <summary>
        /// connect parcel to drone
        /// </summary>
        /// <param name="parcelId">parcel number to connect</param>
        /// <param name="droneId">drone id</param>
        void ParcelToDrone(int parcelId, int droneId);

        /// <summary>
        /// the time of pickup a parcel by drone update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        void UpdatePickup(int parcelId);

        /// <summary>
        /// parcel delivery time update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
         void UpdateDelivery(int parcelId);

        /// <summary>
        /// send a drone to station for charge
        /// </summary>
        /// <param name="droneId">drone id</param>
        /// <param name="stationId">station id</param>
        void ChargeDrone(int droneId, int stationId);

        /// <summary>
        /// drone release from chrage
        /// </summary>
        /// <param name="droneId">drone id to release</param>
        TimeSpan EndCharge(int droneId);

        /// <summary>
        /// get station
        /// </summary>
        /// <param name="stationId">station id to return</param>
        /// <returns>station object</returns>
        Station GetStation(int stationId);

        /// <summary>
        /// get droone
        /// </summary>
        /// <param name="droneId">drone id to return</param>
        /// <returns>drone object</returns>
        Drone GetDrone(int droneId);

        /// <summary>
        /// get customer
        /// </summary>
        /// <param name="customerId">customer id to return</param>
        /// <returns>customer object</returns>
        Customer GetCustomer(int customerId);

        /// <summary>
        /// get parcel
        /// </summary>
        /// <param name="parcelId">parcel id to return</param>
        /// <returns>parcel object</returns>
        Parcel GetParcel(int parcelId);

        /// <summary>
        /// get list of the stations
        /// </summary>
        /// <returns>station array</returns>
        IEnumerable<Station> StationList();

        /// <summary>
        /// get list of the Customers
        /// </summary>
        /// <returns>customer array</returns>
        IEnumerable<Customer> CustomerList();

        /// <summary>
        /// get list of the parcels
        /// </summary>
        /// <returns>parcel array</returns>
        IEnumerable<Parcel> ParcelList();

        /// <summary>
        /// get the drone list
        /// </summary>
        /// <returns>drone list</returns>
        IEnumerable<Drone> DroneList();

        /// <summary>
        /// distance calculation between to geographic points
        /// </summary>
        /// <param name="lat1">user lattiude</param>
        /// <param name="lon1">user longitude</param>
        /// <param name="lat2">object lattiude</param>
        /// <param name="lon2">object longitude</param>
        /// <returns>distance in kilometer</returns>
        double DistanceCalculate(double lat1, double lon1, double lat2, double lon2);

        /// <summary>
        /// electricity use array
        /// </summary>
        /// <returns>electricityUse array</returns>
        double[] BatteryUseRequest();

        /// <summary>
        /// update drone prioritys
        /// </summary>
        /// <param name="drone">drone for update</param>
        void DroneUpdate(Drone drone);

        /// <summary>
        /// update station priority
        /// </summary>
        /// <param name="station">station for update</param>
        void StationUpdate(Station station);

        /// <summary>
        /// update customer priority
        /// </summary>
        /// <param name="customer">customer for update</param>
        void CustomerUpdate(Customer customer);

        /// <summary>
        /// get stations with free charge slots
        /// </summary>
        /// <returns>stations with free charge slots list</returns>
        //IEnumerable<Station> StationsWithFreeSlots();

        /// <summary>
        /// get the drone charging list
        /// </summary>
        /// <returns>drone list</returns>
        public IEnumerable<DroneCharge> GetDroneChargingList(Predicate<DroneCharge> condition);

        IEnumerable<Station> GetStationsByCondition(Predicate<Station> condition);

        IEnumerable<Customer> GetCustomersByCondition(Predicate<Customer> condition);

        IEnumerable<Parcel> GetParcelsByCondition(Predicate<Parcel> condition);
    }
}
