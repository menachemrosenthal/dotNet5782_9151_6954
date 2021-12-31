using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Xml.Linq;

namespace DalApi
{
    internal class DalXml : IDal
    {
        public static DalXml Instance { get { return Nested.instance; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly DalXml instance = new();
        }
        DalXml()
        {
            XElement customersRoot;
            string cPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\CustomerXml.xml";
            customersRoot = new XElement("Customers");
            customersRoot.Save(cPath);
            XElement dronesRoot;
            string dPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\DroneXml.xml";
            dronesRoot = new XElement("Drones");
            dronesRoot.Save(dPath);
            XElement parcelsRoot;
            string pPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\ParcelXml.xml";
            parcelsRoot = new XElement("Parcels");
            parcelsRoot.Save(pPath);
            XElement stationsRoot;
            string sPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\StationXml.xml";
            stationsRoot = new XElement("Stations");
            stationsRoot.Save(sPath);
        }

        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(Drone drone)
        {
            throw new NotImplementedException();
        }

        public void AddCustumer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public int AddParcel(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public void ParcelToDrone(int parcelId, int droneId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePickup(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void UpdateDelivery(int parcelId)
        {
            throw new NotImplementedException();
        }

        public void ChargeDrone(int droneId, int stationId)
        {
            throw new NotImplementedException();
        }

        public TimeSpan EndCharge(int droneId)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int stationId)
        {
            throw new NotImplementedException();
        }

        public Drone GetDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Parcel GetParcel(int parcelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> StationList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> CustomerList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> ParcelList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> DroneList()
        {
            throw new NotImplementedException();
        }

        public double DistanceCalculate(double lat1, double lon1, double lat2, double lon2)
        {
            throw new NotImplementedException();
        }

        public void ParcelDelete(int id)
        {
            throw new NotImplementedException();
        }

        public double[] BatteryUseRequest()
        {
            throw new NotImplementedException();
        }

        public void DroneUpdate(Drone drone)
        {
            throw new NotImplementedException();
        }

        public void StationUpdate(Station station)
        {
            throw new NotImplementedException();
        }

        public void CustomerUpdate(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> GetDroneChargingList(Predicate<DroneCharge> condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStationsByCondition(Predicate<Station> condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomersByCondition(Predicate<Customer> condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcelsByCondition(Predicate<Parcel> condition)
        {
            throw new NotImplementedException();
        }
    }
}
