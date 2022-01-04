using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;


namespace DalApi
{
    internal partial class DalXml : IDal
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
                XMLTools.SaveListToXMLSerializer(DataSource.Parcels, ParcelPath);
                XMLTools.SaveListToXMLSerializer(DataSource.Customers, CustomerPath);
                XMLTools.SaveListToXMLSerializer(DataSource.Drones, DronePath);
                XMLTools.SaveListToXMLSerializer(DataSource.DronesCharge, DroneChargePath);
                */
                List<DroneCharge> dronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath);
                foreach (var drone in dronesCharge)
                {
                    EndCharge(drone.DroneId);
                }
                //XMLTools.SaveListToXMLSerializer(dronesCharge, DroneChargePath);
            }

            customersRoot = XElement.Load(CustomerPath);
            dronesRoot = XElement.Load(DronePath);
            parcelsRoot = XElement.Load(ParcelPath);
            stationsRoot = XElement.Load(StationPath);
            droneChargesRoot = XElement.Load(DroneChargePath);

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

    }
}
