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
        Drone GetDrone(int droneId);
        Customer GetCustomer(int customerId);
        Parcel GetParcel(int parcelId);
        IEnumerable<Station> StationList();
        IEnumerable<Customer> CustomerList();
        IEnumerable<Parcel> ParcelList();
        IEnumerable<Drone> DroneList();
        double DistanceCalculate(double lat1, double lon1, double lat2, double lon2);

    }
}
