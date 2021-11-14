using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    interface IDal
    {
        void AddStation(Station station);
        void AddDrone(Drone drone);
        void AddCustumer(Customer customer);
        void AddParcel(Parcel parcel);
        void ParcelToDrone(int parcelId, int droneId);
        void UpdatePickup(int parcelId);
        void UpdateDelivery(int parcelId);
        void ChargeDrone(int droneId, int stationId);
        void EndCharge(int droneId);
        Station GetStation(int stationId);

    }
}
