﻿using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IBL
    {
        void AddStation(Station station);        
        void AddDrone(Drone drone,int stationID);
        Drone getDrone(int DroneId);
        void DroneNameUpdate(int droneId, string updateName);
        void StationUpdate(int stationId,string nameUpdate,string freeChargeSlots);
        void CustomerUpdate(Customer customer);
        void ChargeDrone(int droneId);
        void ReleaseDrone(int droneId, TimeSpan time);
        void ParcelToDrone(int droneId);
        void ParcelPickedupUptade(int droneId);
        double LocationsDistance(Location l1, Location l2);
        void AddCustumer(Customer customer);
        Customer getCustomer(int CustomerId);
        void AddParcel(Parcel parcel);
        Parcel getParcel(int parcelId);
        string StationToString(int stationId);
        void parcelProvisionUpdate(int droneId);
    }
}
