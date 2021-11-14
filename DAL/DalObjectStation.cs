using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
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
        public Station? GetStation(int stationId)
        {
            try
            {
                var exist = DataSource.Stations.Any(x => x.Id == stationId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Station", stationId);

                return DataSource.Stations.FirstOrDefault(x => x.Id == stationId);
            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        /// <summary>
        /// get list of the stations
        /// </summary>
        /// <returns>station array</returns>
        public IEnumerable<Station> StationList() => DataSource.Stations.ToList();


    }
}
