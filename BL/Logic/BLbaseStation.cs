using System;
using System.Collections.Generic;
using System.Linq;
using BlApi;

namespace BO
{
    public partial class BL : IBL
    {
        /// <summary>
        /// gets list of stations
        /// </summary>
        /// <returns>list of stations</returns>
        public IEnumerable<StationToList> GetBaseStationList()
            => dal.StationList().Select(x =>
                new StationToList
                {
                    Id = x.Id,
                    Name = x.Name,
                    FreeChargeSlots = x.ChargeSlots,
                    FullChargeSlots = x.ChargeSlots + DronesInStation(x.Id).Count
                }
            );

        /// <summary>
        /// gets list of stations with free charge slots
        /// </summary>
        /// <returns>list of stations</returns>
        public IEnumerable<StationToList> GetFreeChargingSlotsStationList()
            => GetStationsByCondition(x => x.FreeChargeSlots > 0);

        /// <summary>
        /// add a station
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            if (station.LocationOfStation.Longitude < 34.5 ||
               station.LocationOfStation.Longitude > 35.9)
                throw new ArgumentOutOfRangeException("The longitude was out of range");

            if (station.LocationOfStation.Latitude < 31.589844 ||
            station.LocationOfStation.Latitude > 32.801705)
                throw new ArgumentOutOfRangeException("The latitude was out Of range");

            DalApi.Station dalStation = new()
            {
                Id = station.Id,
                Name = station.Name,
                ChargeSlots = station.ChargeSlots,
                Latitude = station.LocationOfStation.Latitude,
                Longitude = station.LocationOfStation.Longitude,
            };
            station.DronesCharging = null;
            dal.AddStation(dalStation);
        }

        /// <summary>
        /// update name or charge slots of station
        /// </summary>
        /// <param name="stationId">station id for update</param>
        /// <param name="nameUpdate">new name</param>
        /// <param name="chargSlots">num of charge slots</param>
        public void StationUpdate(int stationId, string nameUpdate, string chargSlots)
        {
            DalApi.Station station = dal.GetStation(stationId);

            if (!string.IsNullOrWhiteSpace(nameUpdate))
                station.Name = nameUpdate;
            if (!string.IsNullOrWhiteSpace(chargSlots) && int.TryParse(chargSlots, out int freeChargeSlots))
            {
                if (freeChargeSlots < 0)
                    throw new ArgumentOutOfRangeException("The charge slots must be positive value");
                station.ChargeSlots = freeChargeSlots - DronesInStation(stationId).Count;
            }
            dal.StationUpdate(station);
        }

        /// <summary>
        /// get station
        /// </summary>
        /// <param name="StationId">id station to get</param>
        /// <returns>station</returns>
        public Station GetStation(int StationId)
        {
            DalApi.Station dalStation = dal.GetStation(StationId);
            Station station = new()
            {
                Id = dalStation.Id,
                Name = dalStation.Name,
                ChargeSlots = dalStation.ChargeSlots,
                LocationOfStation = StationLocation(dalStation),
                DronesCharging = DronesInStation(StationId)
            };
            return station;
        }

        public IEnumerable<StationToList> GetStationsByCondition(Predicate<StationToList> condition)
                => GetBaseStationList().Where(x => condition(x));

        /// <summary>
        /// list of drones charging in station
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns>list of drones in charging</returns>
        private List<DroneInCharging> DronesInStation(int stationId)
        {
            return dal.GetDroneChargingList(x => x.StationId == stationId).Select(charge => new DroneInCharging()
            {
                Id = charge.DroneId,
                BatteryStatus = drones.Find(x => x.Id == charge.DroneId).BatteryStatus
            }).ToList();
        }

        /// <summary>
        /// gets location of station
        /// </summary>
        /// <param name="station"></param>
        /// <returns>location of station</returns>
        private Location StationLocation(DalApi.Station station)
        {
            return new() { Longitude = station.Longitude, Latitude = station.Latitude };
        }

        /// <summary>
        /// calculates closest station to location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="stations"></param>
        /// <returns>closest station</returns>
        private DalApi.Station ClosestStation(Location location, IEnumerable<DalApi.Station> stations)
        {
            DalApi.Station station = dal.StationList().First();
            double diastance = LocationsDistance(location, StationLocation(station));

            foreach (var Station in stations)
                if (diastance > LocationsDistance(location, StationLocation(Station)))
                    station = Station;

            return station;
        }
    }
}
