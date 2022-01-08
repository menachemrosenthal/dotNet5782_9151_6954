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
        public void AddDrone(Drone drone)
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);
            if (drones.Any(x => x.Id == drone.Id))
                throw new DalApi.AddExistException("drone", drone.Id);

            drones.Add(drone);

            XMLTools.SaveListToXMLSerializer(drones, DronePath);
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

            XMLTools.SaveListToXMLSerializer(parcels, ParcelPath);
        }

        public void ChargeDrone(int droneId, int stationId)
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);
            if (!drones.Any(x => x.Id == droneId))
                throw new DalApi.ItemNotFoundException("drone", droneId);

            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            if (!stations.Any(x => x.Id == stationId))
                throw new DalApi.ItemNotFoundException("Station", stationId);

            List<DroneCharge> droneCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath);
            droneCharge.Add(new() { DroneId = droneId, StationId = stationId, time = DateTime.Now });

            var station = stations.First(x => x.Id == stationId);
            if (station.ChargeSlots == 0)
                throw new NotFreeChargeSlot("Ther is not free charge slot, please wait");

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

            var droneCharge = dronesCharge.FirstOrDefault(x => x.DroneId == droneId);
            var station = stations.FirstOrDefault(x => x.Id == droneCharge.StationId);
            var index = stations.IndexOf(station);
            var time = droneCharge.time;
            station.ChargeSlots++;
            stations[index] = station;
            dronesCharge.Remove(droneCharge);

            XMLTools.SaveListToXMLSerializer(dronesCharge, DroneChargePath);
            XMLTools.SaveListToXMLSerializer(stations, StationPath);

            return time - DateTime.Now;
        }

        public Drone GetDrone(int droneId)
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);
            if (!drones.Any(x => x.Id == droneId))
                throw new DalApi.ItemNotFoundException("Drone", droneId);

            return drones.FirstOrDefault(x => x.Id == droneId);
        }

        public IEnumerable<Drone> DroneList()
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(DronePath);

            return from drone in drones
                   select drone;
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

        public IEnumerable<DroneCharge> GetDroneChargingList(Predicate<DroneCharge> condition)
        {
            List<DroneCharge> dronesCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath);

            return from droneCharge in dronesCharge
                   where condition(droneCharge)
                   select droneCharge;
        }
    }
}
