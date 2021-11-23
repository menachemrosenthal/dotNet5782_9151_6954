using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// add a station to the stations array
        /// </summary>
        /// <param name="station">the station for add</param>
        public void AddStation(Station station)
        {
            try
            {
                var exist = DataSource.Stations.Any(x => x.Id == station.Id);
                if (exist)
                    throw new IDAL.AddExistException("Station", station.Id);
                if (station.ChargeSlots < 0)
                    throw new ArgumentException("station cannot have less than 0 charge slots");
                DataSource.Stations.Add(station);
            }
            catch (IDAL.AddExistException ex)
            {
                Console.WriteLine(ex);
                return;
            }
        } 

        /// <summary>
        /// get station
        /// </summary>
        /// <param name="stationId">station id to return</param>
        /// <returns>station object</returns>
        public Station GetStation(int stationId)
        {
            var exist = DataSource.Stations.Any(x => x.Id == stationId);
            if (!exist)
                throw new IDAL.ItemNotFoundException("Station", stationId);

            return DataSource.Stations.FirstOrDefault(x => x.Id == stationId);
        }

        /// <summary>
        /// get list of the stations
        /// </summary>
        /// <returns>station array</returns>
        public IEnumerable<Station> StationList() => DataSource.Stations.ToList();

        public void StationUpdate(Station station)
        {
            int index = DataSource.Stations.IndexOf(station);
            DataSource.Stations[index] = station;
        }

        public IEnumerable<Station> StationsWithFreeSlots()
        {
            List<Station> freeSlotsStations = new();
            foreach (var Station in DataSource.Stations)
                if (Station.ChargeSlots != 0)
                    freeSlotsStations.Add(Station);

            return freeSlotsStations;
        }
    }
}
