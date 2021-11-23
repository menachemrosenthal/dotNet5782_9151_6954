using IBL.BO;
using System;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        /// <summary>
        /// add a station
        /// </summary>
        /// <param name="station"></param>
        void AddStation(Station station);

        /// <summary>
        /// add a drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="stationID"></param>
        void AddDrone(DroneToList drone,int stationID);
<<<<<<< HEAD

        /// <summary>
        /// gets drone and creates bl object
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>created drone</returns>
        Drone GetDrone(int DroneId);

        /// <summary>
        /// update name of drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="updateName"></param>
=======
        Drone GetDrone(int DroneId);
>>>>>>> b39bb1742696245ade238d89e653049547755891
        void DroneNameUpdate(int droneId, string updateName);

        /// <summary>
        /// update name or charge slots of station
        /// </summary>
        /// <param name="stationId">station id for update</param>
        /// <param name="nameUpdate">new name</param>
        /// <param name="chargSlots">num of charge slots</param>
        void StationUpdate(int stationId,string nameUpdate,string freeChargeSlots);

        /// <summary>
        /// update customer name or phone num
        /// </summary>
        /// <param name="customer">customer for update</param>
        void CustomerUpdate(Customer customer);

        /// <summary>
        /// updates drone to charging state
        /// </summary>
        /// <param name="droneId"></param>
        void ChargeDrone(int droneId);

        /// <summary>
        /// releases drone from chatge
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="time"></param>
        void ReleaseDrone(int droneId, TimeSpan time);

        /// <summary>
        /// updates parcel to a drone
        /// </summary>
        /// <param name="droneId"></param>
        void ParcelToDrone(int droneId);

        /// <summary>
        /// updates that parcel was picked up
        /// </summary>
        /// <param name="droneId"></param>
        void ParcelPickedupUptade(int droneId);

        /// <summary>
        /// add a Custumer
        /// </summary>
        /// <param name="Custumer"></param>
        void AddCustumer(Customer customer);

        /// <summary>
        /// gets Customer and creates bl object
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>created Customer</returns>
        Customer GetCustomer(int CustomerId);

        /// <summary>
        /// add a parcel
        /// </summary>
        /// <param name="parcel"></param>
        void AddParcel(Parcel parcel);

        /// <summary>
        /// gets parcel and creates bl object
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>created paecel</returns>
        Parcel GetParcel(int parcelId);

        /// <summary>
        /// get station
        /// </summary>
        /// <param name="StationId">id station to get</param>
        /// <returns>station</returns>
        Station GetStation(int StationId);

        /// <summary>
        /// updates drone that parcel was delivered
        /// </summary>
        /// <param name="droneId"></param>
        void ParcelProvisionUpdate(int droneId);

        /// <summary>
        /// gets list of stations
        /// </summary>
        /// <returns>list of stations</returns>
        IEnumerable<StationToList> GetBaseStationList();

        /// <summary>
        /// get drone list
        /// </summary>
        /// <returns>IEnumerable of drone list</returns>
        IEnumerable<DroneToList> GetDroneList();

        /// <summary>
        /// gets list of customer
        /// </summary>
        /// <returns>list of customer</returns>
        IEnumerable<CustomerToList> GetCustomerList();

        /// <summary>
        /// gets list of parcels
        /// </summary>
        /// <returns>list of parcels</returns>
        IEnumerable<ParcelToList> GetParcelList();

        /// <summary>
        /// get list of usassociated Parceles
        /// </summary>
        /// <returns>list of usassociated Parceles</returns>
        IEnumerable<ParcelToList> GetNonAssociateParcelList();

        /// <summary>
        /// gets list of stations with free charge slots
        /// </summary>
        /// <returns>list of stations</returns>
        IEnumerable<StationToList> GetFreeChargingSlotsStationList();
    }
}
