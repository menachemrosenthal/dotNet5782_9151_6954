using IBL.BO;
using System;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        void AddStation(Station station);        
        void AddDrone(DroneToList drone,int stationID);
        Drone GetDrone(int DroneId);
        void DroneNameUpdate(int droneId, string updateName);
        void StationUpdate(int stationId,string nameUpdate,string freeChargeSlots);
        void CustomerUpdate(Customer customer);
        void ChargeDrone(int droneId);
        void ReleaseDrone(int droneId, TimeSpan time);
        void ParcelToDrone(int droneId);
        void ParcelPickedupUptade(int droneId);        
        void AddCustumer(Customer customer);
        Customer GetCustomer(int CustomerId);
        void AddParcel(Parcel parcel);
        Parcel GetParcel(int parcelId);
        Station GetStation(int StationId);
        void ParcelProvisionUpdate(int droneId);
        IEnumerable<StationToList> GetBaseStationList();
        IEnumerable<DroneToList> GetDroneList();
        IEnumerable<CustomerToList> GetCustomerList();
        IEnumerable<ParcelToList> GetParcelList();
        IEnumerable<ParcelToList> GetNonAssociateParcelList();
        IEnumerable<StationToList> GetFreeChargingSlotsStationList();
    }
}
