using IBL.BO;
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
        void AddDrone(DroneToLIst drone,int stationID);
        void DroneNameUpdate(int droneId, string updateName);
        void StationUpdate(int stationId,string nameUpdate,string freeChargeSlots);
        void CustomerUpdate(Customer customer);
        void ChargeDrone(int droneId);
        void ReleaseDrone(int droneId, TimeSpan time);
        void ParcelToDrone(int droneId);
        void ParcelPickedupUptade(int droneId);
        double LocationsDistance(Location l1, Location l2);
        void AddCustumer(Customer customer);
        void AddParcel(Parcel parcel);
        string StationToString(int stationId);
        string DroneToString(int DroneId);
        void parcelProvisionUpdate(int droneId);
    }
}
