using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using DAL;

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
        CustomerPath = @"C:\Users\Itzic\source\repos\dotNet5782_9151_6954\DAL\xml\CustomerXml.xml",
        DronePath = @"C:\Users\Itzic\source\repos\dotNet5782_9151_6954\DAL\xml\DroneXml.xml",
        ParcelPath = @"C:\Users\Itzic\source\repos\dotNet5782_9151_6954\DAL\xml\ParcelXml.xml",
        StationPath = @"C:\Users\Itzic\source\repos\dotNet5782_9151_6954\DAL\xml\StationXml.xml",
        DroneChargePath = @"C:\Users\Itzic\source\repos\dotNet5782_9151_6954\DAL\xml\DroneChargeXml.xml";

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
                */
                XmlSerializer x = new(DataSource.DronesCharge.GetType());
                FileStream fs = new(DroneChargePath, FileMode.Create);
                x.Serialize(fs, DataSource.DronesCharge);
                fs.Close();
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
                throw new DalApi.AddExistException("customer", customer.Id);

            customers.Add(customer);

            XMLTools.SaveListToXMLSerializer(customers, CustomerPath);
        }

        public int AddParcel(Parcel parcel)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);
            if (parcels.Any(x => x.Id == parcel.Id))
                throw new DalApi.AddExistException("parcel", parcel.Id);

            parcels.Add(parcel);

            XMLTools.SaveListToXMLSerializer(parcels, StationPath);

            return parcel.Id;
        }

        public void ParcelToDrone(int parcelId, int droneId)
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);

            var exist = parcels.Any(x => x.Id == parcelId);
            if (!exist)
                throw new ItemNotFoundException("Parcel", parcelId);

            if (!(exist = drones.Any(x => x.Id == droneId)))
                throw new ItemNotFoundException("Drone", droneId);

            var parcel = parcels.FirstOrDefault(x => x.Id == parcelId);
            var index = parcels.IndexOf(parcel);
            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            parcels[index] = parcel;
            XMLTools.SaveListToXMLSerializer(parcels, StationPath);
        }

        public void UpdatePickup(int parcelId)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);
            var exist = parcels.Any(x => x.Id == parcelId);
            if (!exist)
                throw new ItemNotFoundException("Parcel", parcelId);

            var parcel = parcels.First(x => x.Id == parcelId);
            var index = parcels.IndexOf(parcel);
            parcel.PickedUp = DateTime.Now;
            parcels[index] = parcel;
            XMLTools.SaveListToXMLSerializer(parcels, ParcelPath);
        }

        public void UpdateDelivery(int parcelId)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);
            var exist = parcels.Any(x => x.Id == parcelId);
            if (!exist)
                throw new ItemNotFoundException("Parcel", parcelId);

            var parcel = parcels.First(x => x.Id == parcelId);
            var index = parcels.IndexOf(parcel);
            parcel.Delivered = DateTime.Now;
            parcels[index] = parcel;
            XMLTools.SaveListToXMLSerializer(parcels, ParcelPath);
        }

        public void ChargeDrone(int droneId, int stationId)
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);
            var exist = drones.Any(x => x.Id == droneId);
            if (!exist)
                throw new DalApi.ItemNotFoundException("Drone", droneId);

            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            if (!(exist = stations.Any(x => x.Id == stationId)))
                throw new DalApi.ItemNotFoundException("Station", stationId);

            List<DroneCharge> droneCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath);
            droneCharge.Add(new() { DroneId = droneId, StationId = stationId, time = DateTime.Now });

            var station = stations.First(x => x.Id == stationId);
            var index = stations.IndexOf(station);
            station.ChargeSlots--;
            stations[index] = station;

            XMLTools.SaveListToXMLSerializer(droneCharge, DroneChargePath);
            XMLTools.SaveListToXMLSerializer(stations, StationPath);
        }

        public TimeSpan EndCharge(int droneId)
        {
            List<DroneCharge> dronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath);
            var exist = dronesCharge.Any(x => x.DroneId == droneId);
            if (!exist)
                throw new ItemNotFoundException("Drone", droneId, "in the drones charge");

            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

            var droneCharge = dronesCharge.First(x => x.DroneId == droneId);
            var station = stations.First(x => x.Id == droneCharge.StationId);
            var index = stations.IndexOf(station);
            var time = droneCharge.time;
            station.ChargeSlots++;
            stations[index] = station;
            dronesCharge.Remove(droneCharge);

            XMLTools.SaveListToXMLSerializer(dronesCharge, DroneChargePath);
            XMLTools.SaveListToXMLSerializer(stations, StationPath);

            return time - DateTime.Now;
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
