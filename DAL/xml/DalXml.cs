using System;
using System.Collections.Generic;
using System.Linq;
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

        string
        CustomerPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\CustomerXml.xml",
        DronePath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\DroneXml.xml",
        ParcelPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\ParcelXml.xml",
        StationPath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\StationXml.xml",
        DroneChargePath = @"C:\Users\User\source\repos\dotNet5782_9151_6954\New folder (2)\DAL\xml\DroneChargeXml.xml";

        DalXml()
        {
            if (true/*files dont exist*/)
            {
                customersRoot = new XElement("Customers");
                dronesRoot = new XElement("Drones");
                parcelsRoot = new XElement("Parcels");
                stationsRoot = new XElement("Stations");
                droneChargesRoot = new XElement("DroneCharge");
                /*
                DataSource.Config.Initialize();
                XmlSerializer x = new(DataSource.Customers.GetType());
                FileStream fs = new(CustomerPath, FileMode.Create);
                x.Serialize(fs, DataSource.Customers);
                x = new XmlSerializer(DataSource.Drones.GetType());
                fs = new FileStream(DronePath, FileMode.Create);
                x.Serialize(fs, DataSource.Drones);
                x = new XmlSerializer(DataSource.Parcels.GetType());
                fs = new FileStream(ParcelPath, FileMode.Create);
                x.Serialize(fs, DataSource.Parcels);
                x = new XmlSerializer(DataSource.Stations.GetType());
                fs = new FileStream(StationPath, FileMode.Create);
                x.Serialize(fs, DataSource.Stations);
                x = new XmlSerializer(DataSource.DronesCharge.GetType());
                fs = new FileStream(DroneChargePath, FileMode.Create);
                x.Serialize(fs, DataSource.DronesCharge);
               */
            }

            customersRoot = XElement.Load(CustomerPath);
            dronesRoot = XElement.Load(DronePath);
            parcelsRoot = XElement.Load(ParcelPath);
            stationsRoot = XElement.Load(StationPath);
            droneChargesRoot = XElement.Load(DroneChargePath);

        }

        public void AddStation(Station station)
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            if (stations.Any(x => x.Id == station.Id))
                throw new DalApi.AddExistException("station", station.Id);

            stations.Add(station);

            XMLTools.SaveListToXMLSerializer(stations, StationPath);
        }
        
        public void AddDrone(Drone drone)
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);
            if (drones.Any(x => x.Id == drone.Id))
                throw new DalApi.AddExistException("drone", drone.Id);

            drones.Add(drone);

            XMLTools.SaveListToXMLSerializer(drones, DronePath);
        }

        public void AddCustumer(Customer customer)
        {
            List<Customer> customers = XMLTools.LoadListFromXMLSerializer<Customer>(CustomerPath);
            if (customers.Any(x => x.Id == customer.Id))
                throw new DalApi.AddExistException("station", customer.Id);

            customers.Add(customer);

            XMLTools.SaveListToXMLSerializer(customers, CustomerPath);

        }

        public int AddParcel(Parcel parcel)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);
            if (parcels.Any(x => x.Id == parcel.Id))
                throw new DalApi.AddExistException("parcel", parcel.Id);

            parcels.Add(parcel);

            XMLTools.SaveListToXMLSerializer(parcels, ParcelPath);

            return parcel.Id;
        }

        public void ParcelToDrone(int parcelId, int droneId)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);
            if (!parcels.Any(x => x.Id == parcelId))
                throw new DalApi.ItemNotFoundException("parcel", parcelId);
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);
            if (!drones.Any(x => x.Id == droneId))
                throw new DalApi.ItemNotFoundException("drone", droneId);
            var parcel = parcels.First(x => x.Id == parcelId);
            var index = parcels.IndexOf(parcel);
            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            parcels[index] = parcel;
            XMLTools.SaveListToXMLSerializer(parcels, ParcelPath);
        }

        /// <summary>
        /// the time of pickup a parcel by drone update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdatePickup(int parcelId)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);

            if (!parcels.Any(x => x.Id == parcelId))
                throw new ItemNotFoundException("Parcel", parcelId);
            
            var parcel = parcels.First(x => x.Id == parcelId);
            var index = parcels.IndexOf(parcel);
            parcel.PickedUp = DateTime.Now;
            DataSource.Parcels[index] = parcel;
            XMLTools.SaveListToXMLSerializer(parcels, ParcelPath);
        }

        /// <summary>
        /// parcel delivery time update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdateDelivery(int parcelId)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);

            if (!parcels.Any(x => x.Id == parcelId))
                throw new ItemNotFoundException("Parcel", parcelId);

            var parcel = parcels.First(x => x.Id == parcelId);
            var index = parcels.IndexOf(parcel);
            parcel.Delivered = DateTime.Now;
            DataSource.Parcels[index] = parcel;
            XMLTools.SaveListToXMLSerializer(parcels, ParcelPath);
        }

        /// <summary>
        /// send a drone to station for charge
        /// </summary>
        /// <param name="droneId">drone id</param>
        /// <param name="stationId">station id</param>
        public void ChargeDrone(int droneId, int stationId)
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);
            if (!drones.Any(x => x.Id == droneId))
                throw new DalApi.ItemNotFoundException("drone", droneId);

            if (!(exist = DataSource.Stations.Any(x => x.Id == stationId)))
                throw new DalApi.ItemNotFoundException("Station", stationId);

            DataSource.DronesCharge.Add(new() { DroneId = droneId, StationId = stationId, time = DateTime.Now });

            var station = DataSource.Stations.First(x => x.Id == stationId);
            var index = DataSource.Stations.IndexOf(station);
            station.ChargeSlots--;
            DataSource.Stations[index] = station;
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
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            if (!stations.Any(x => x.Id == stationId))
                throw new DalApi.ItemNotFoundException("Station", stationId);

            return stations.FirstOrDefault(x => x.Id == stationId);
        }

        public Drone GetDrone(int droneId)
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);
            if (!drones.Any(x => x.Id == droneId))
                throw new DalApi.ItemNotFoundException("Drone", droneId);

            return drones.FirstOrDefault(x => x.Id == droneId);
        }

        public Customer GetCustomer(int customerId)
        {
            List<Customer> customers = XMLTools.LoadListFromXMLSerializer<Customer>(CustomerPath);

            return customers.FirstOrDefault(x => x.Id == customerId);
        }

        public Parcel GetParcel(int parcelId)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);

            if (!parcels.Any(x => x.Id == parcelId))
                throw new ItemNotFoundException("Parcel", parcelId);

            return parcels.FirstOrDefault(x => x.Id == parcelId);
        }

        public IEnumerable<Station> StationList()
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

            return from station in stations
                   select station;
        }

        public IEnumerable<Customer> CustomerList()
        {
            return from customer in customersRoot.Elements()
                   select new Customer()
                   {
                       Name = customer.Element("Name").Value,
                       Id = int.Parse(customer.Element("Id").Value),
                       Phone = customer.Element("Phone").Value,
                       Latitude = double.Parse(customer.Element("Latitude").Value),
                       Longitude = double.Parse(customer.Element("Longitude").Value)
                   };
        }

        public IEnumerable<Parcel> ParcelList()
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);

            return from parcel in parcels
                   select parcel;
        }

        public IEnumerable<Drone> DroneList()
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);

            return from drone in drones
                   select drone;
        }

        public double DistanceCalculate(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist * 1.609344;
        }

        public void ParcelDelete(int id)
        {
            Parcel parcel = GetParcel(id);
            if (parcel.DroneId == 0)
            {
                DataSource.Parcels.Remove(parcel);
            }

            else throw new ArgumentException("The parcel is associated");
        }

        public double[] BatteryUseRequest()
        {
            double[] electricityUse = new double[5];

            electricityUse[0] = DataSource.Config.Free;
            electricityUse[1] = DataSource.Config.CarryingLight;
            electricityUse[2] = DataSource.Config.CarryingMediem;
            electricityUse[3] = DataSource.Config.CarryingHeavy;
            electricityUse[4] = DataSource.Config.ChargePace;

            return electricityUse;
        }

        public void DroneUpdate(Drone drone)
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);
            if (!drones.Any(x => x.Id == drone.Id))
                throw new DalApi.ItemNotFoundException("Drone", drone.Id);

            var tmpDrone = drones.FirstOrDefault(x => x.Id == drone.Id);
            int index = drones.IndexOf(tmpDrone);
            drones[index] = drone;

            XMLTools.SaveListToXMLSerializer(drones, DronePath);
        }

        public void StationUpdate(Station station)
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            if (!stations.Any(x => x.Id == station.Id))
                throw new DalApi.ItemNotFoundException("Drone", station.Id);

            var tmpStation = stations.FirstOrDefault(x => x.Id == station.Id);
            int index = stations.IndexOf(tmpStation);
            stations[index] = station;

            XMLTools.SaveListToXMLSerializer(stations, DronePath);
        }

        public void CustomerUpdate(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> GetDroneChargingList(Predicate<DroneCharge> condition)
        {
            List<DroneCharge> dronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath);

            return from droneCharge in dronesCharge
                   select droneCharge;
        }

        public IEnumerable<Station> GetStationsByCondition(Predicate<Station> condition)
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

            return from station in stations
                   where condition(station)
                   select station;
        }

        public IEnumerable<Customer> GetCustomersByCondition(Predicate<Customer> condition)
        {
            List<Customer> customers = XMLTools.LoadListFromXMLSerializer<Customer>(CustomerPath);

            return from customer in customers
                   where condition(customer)
                   select customer;
        }

        public IEnumerable<Parcel> GetParcelsByCondition(Predicate<Parcel> condition)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);

            return from parcel in parcels
                   where condition(parcel)
                   select parcel;
        }
    }
}
