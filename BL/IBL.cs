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
        double LocationsDistance(Location l1, Location l2);
        void AddDrone(DroneToLIst drone,int stationID);
        void DroneNameUpdate(int droneId, string updateName);
        void StationUpdate(int stationId,string nameUpdate,string freeChargeSlots);
        void CustomerUpdate(Customer customer);
        void ChargeDrone(int droneId);
        void ReleaseDrone(int droneId, string time);
    }
}
