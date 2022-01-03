using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

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
        internal static XElement dronesRoot;
        internal static XElement parcelsRoot;
        internal static XElement stationsRoot;
        internal static XElement droneChargesRoot;
        internal static XElement customersRoot;
        internal static XElement configRoot;
        internal static string cPath;
        internal static string dPath;
        internal static string pPath;
        internal static string sPath;
        internal static string dchPath;
        internal static string configPath;

        DalXml()
        {
            
            cPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\CustomerXml.xml";
            dPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\DroneXml.xml";
            pPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\ParcelXml.xml";
            sPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\StationXml.xml";
            dchPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\DroneChargeXml.xml";
            configPath= @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\ConfigXml.xml";

            if (true/*files dont exist*/)
            {
                customersRoot = new XElement("Customers");
                dronesRoot = new XElement("Drones");
                parcelsRoot = new XElement("Parcels");
                stationsRoot = new XElement("Stations");
                droneChargesRoot = new XElement("DroneCharge");

                DataSource.Config.Initialize();
                XmlSerializer x = new(DataSource.Customers.GetType());
                FileStream fs = new(cPath, FileMode.Create);
                x.Serialize(fs, DataSource.Customers);
                x = new XmlSerializer(DataSource.Drones.GetType());
                fs = new FileStream(dPath, FileMode.Create);
                x.Serialize(fs, DataSource.Drones);
                x = new XmlSerializer(DataSource.Parcels.GetType());
                fs = new FileStream(pPath, FileMode.Create);
                x.Serialize(fs, DataSource.Parcels);
                x = new XmlSerializer(DataSource.Stations.GetType());
                fs = new FileStream(sPath, FileMode.Create);
                x.Serialize(fs, DataSource.Stations);
                x = new XmlSerializer(DataSource.DronesCharge.GetType());
                fs = new FileStream(dchPath, FileMode.Create);
                x.Serialize(fs, DataSource.DronesCharge);

            }
                
            customersRoot = XElement.Load(cPath);
            dronesRoot = XElement.Load(dPath);
            parcelsRoot = XElement.Load(pPath);
            stationsRoot = XElement.Load(sPath);
            droneChargesRoot = XElement.Load(dchPath);

        }

        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(Drone drone)
        {
            XElement dr;
            dr = dronesRoot.Elements().FirstOrDefault(x => int.Parse(x.Attribute("Id").Value) == drone.Id);
            if (!dr.IsEmpty)
                throw new DalApi.AddExistException("Drone", drone.Id);
            XmlSerializer x = new(drone.GetType());
            StreamWriter st = new (dPath);
            x.Serialize(st, drone);
        }

        public void AddCustumer(Customer customer)
        {
            XElement c;
            c = customersRoot.Elements().FirstOrDefault(x => int.Parse(x.Attribute("Id").Value) == customer.Id);
            if (!c.IsEmpty)
                throw new DalApi.AddExistException("Drone", customer.Id);
            XmlSerializer x = new(customer.GetType());
            StreamWriter st = new(sPath);
            x.Serialize(st, customer);
        }

        public int AddParcel(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public void ParcelToDrone(int parcelId, int droneId)
        {
            XElement p,d;
            p = parcelsRoot.Elements().FirstOrDefault(x => int.Parse(x.Attribute("Id").Value) == parcelId);
            if (!p.IsEmpty)
                throw new ItemNotFoundException("Parcel", parcelId);
            d = dronesRoot.Elements().FirstOrDefault(x => int.Parse(x.Attribute("Id").Value) == droneId);
            if (!d.IsEmpty)
                throw new ItemNotFoundException("Drone", droneId);
            p.Element("DroneId").Value = droneId.ToString();
        }

        /// <summary>
        /// the time of pickup a parcel by drone update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdatePickup(int parcelId)
        {
            XElement p;
            p = parcelsRoot.Elements().FirstOrDefault(x => int.Parse(x.Attribute("Id").Value) == parcelId);
            if (!p.IsEmpty)
                throw new ItemNotFoundException("Parcel", parcelId);

            p.Element("PickedUp").Value = DateTime.Now.ToString();
        }

        /// <summary>
        /// parcel delivery time update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdateDelivery(int parcelId)
        {
            XElement p;
            p = parcelsRoot.Elements().FirstOrDefault(x => int.Parse(x.Attribute("Id").Value) == parcelId);
            if (!p.IsEmpty)
                throw new ItemNotFoundException("Parcel", parcelId);

            p.Element("Delivered").Value = DateTime.Now.ToString();
        }

        /// <summary>
        /// send a drone to station for charge
        /// </summary>
        /// <param name="droneId">drone id</param>
        /// <param name="stationId">station id</param>
        public void ChargeDrone(int droneId, int stationId)
        {
            XElement d,s;
            d = dronesRoot.Elements().FirstOrDefault(x => int.Parse(x.Attribute("Id").Value) == droneId);
            if (!d.IsEmpty)
                throw new ItemNotFoundException("Drone", droneId);
            s = stationsRoot.Elements().FirstOrDefault(x => int.Parse(x.Attribute("Id").Value) == stationId);
            if (!s.IsEmpty)
                throw new ItemNotFoundException("Station", stationId);
            DroneCharge dronecharge = new() { DroneId = droneId, StationId = stationId, time = DateTime.Now };
            XmlSerializer x = new(dronecharge.GetType());
            StreamWriter st = new(dchPath);
            x.Serialize(st, dronecharge);
            int c = int.Parse(s.Element("ChargeSlots").Value) - 1;
            s.Element("ChargeSlots").Value = c.ToString();
        }


        /// <summary>
        /// drone release from chrage
        /// </summary>
        /// <param name="droneId">drone id to release</param>
        public TimeSpan EndCharge(int droneId)
        {
            XElement d,s;
            d = dronesRoot.Elements().FirstOrDefault(x => int.Parse(x.Attribute("Id").Value) == droneId);
            if (!d.IsEmpty)
                throw new ItemNotFoundException("Drone", droneId);
            //drone status update
            s = stationsRoot.Elements().FirstOrDefault(x => x.Attribute("Id").Value == d.Attribute("Id").Value);
            string time = Convert.ToDateTime(d.Attribute("time").Value).ToString();
            d.Remove();
            int c = int.Parse(s.Element("ChargeSlots").Value) + 1;
            s.Element("ChargeSlots").Value = c.ToString();
            DateTime dt = Convert.ToDateTime(time);
            return dt - DateTime.Now;
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
