using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.CompilerServices;


namespace DalApi
{
    internal partial class DalXml : IDal
    {
        public void AddStation(Station station)
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            if (stations.Any(x => x.Id == station.Id))
                throw new DalApi.AddExistException("station", station.Id);

            stations.Add(station);

            XMLTools.SaveListToXMLSerializer(stations, StationPath);
        }

        public Station GetStation(int stationId)
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);
            if (!stations.Any(x => x.Id == stationId))
                throw new DalApi.ItemNotFoundException("Station", stationId);

            return stations.FirstOrDefault(x => x.Id == stationId);
        }

        public IEnumerable<Station> StationList()
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

            return from station in stations
                   select station;
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

        public IEnumerable<Station> GetStationsByCondition(Predicate<Station> condition)
        {
            List<Station> stations = XMLTools.LoadListFromXMLSerializer<Station>(StationPath);

            return from station in stations
                   where condition(station)
                   select station;
        }
    }
}
