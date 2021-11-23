using System;
using System.Collections.Generic;
using System.Linq;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        /// <summary>
        /// gets list of stations
        /// </summary>
        /// <returns>list of stations</returns>
        public IEnumerable<StationToList> GetBaseStationList() => dal.StationList().Select(x =>
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
        {
            return dal.StationList().Where(x => x.ChargeSlots > 0).Select(station => new StationToList()
            {
                Id = station.Id,
                Name = station.Name,
                FreeChargeSlots = station.ChargeSlots,
                FullChargeSlots = station.ChargeSlots + DronesInStation(station.Id).Count
            });
        }

        /// <summary>
        /// add a station
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            if ((station.LocationOfStation.Longitude < 34955762 / 1000000 ||
               station.LocationOfStation.Longitude > 34959020 / 1000000) &&
                  (station.LocationOfStation.Latitude < 31589844 / 1000000 ||
                  station.LocationOfStation.Latitude > 32801705 / 1000000))
                throw new ArgumentException("location was out Out Of range");
            IDAL.DO.Station dalStation = new();
            dalStation.Id = station.Id;
            dalStation.Name = station.Name;
            dalStation.ChargeSlots = station.ChargeSlots;
            dalStation.Latitude = station.LocationOfStation.Latitude;
            dalStation.Longitude = station.LocationOfStation.Longitude;
            station.DronesCharging = null;
            dal.AddStation(dalStation);
        }

        /// <summary>
        /// update station
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="nameUpdate"></param>
        /// <param name="freeChargeSlots"></param>
        public void StationUpdate(int stationId, string nameUpdate, string freeChargeSlots)
        {
            IDAL.DO.Station station = dal.GetStation(stationId);

            if (!string.IsNullOrWhiteSpace(nameUpdate))
                station.Name = nameUpdate;
            if (!string.IsNullOrWhiteSpace(freeChargeSlots) && int.TryParse(freeChargeSlots, out int chargeSlots))
                station.ChargeSlots = chargeSlots - DronesInStation(stationId).Count;
            dal.StationUpdate(station);
        }

        /// <summary>
        /// gets Station and creates bl object
        /// </summary>
        /// <param name="StationId"></param>
        /// <returns>created Station</returns>
        public Station GetStation(int StationId)
        {
            IDAL.DO.Station dalStation = dal.GetStation(StationId);
            Station station = new();
            station.Id = dalStation.Id;
            station.Name = dalStation.Name;
            station.ChargeSlots = dalStation.ChargeSlots;
            station.LocationOfStation = StationLocation(dalStation);
            station.DronesCharging = DronesInStation(StationId);
            return station;
        }

        /// <summary>
        /// list of drones charging in station
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns>list of drones in charging</returns>
        private List<DroneInCharging> DronesInStation(int stationId)
        {
            List<DroneInCharging> droneCharge = new();

            foreach (var Charge in dal.DroneChargingList())
            {
                if (Charge.StationId == stationId)
                {
                    DroneInCharging d = new();
                    d.Id = Charge.DroneId;
                    d.BatteryStatus = drones.Find(x => x.Id == Charge.DroneId).BatteryStatus;
                    droneCharge.Add(d);
                }
            }

            return droneCharge;
        }

        /// <summary>
        /// gets location of station
        /// </summary>
        /// <param name="station"></param>
        /// <returns>location of station</returns>
        private Location StationLocation(IDAL.DO.Station station)
        {
            return new() { Longitude = station.Longitude, Latitude = station.Latitude };
        }

        /// <summary>
        /// calculates closest station to location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="stations"></param>
        /// <returns>closest station</returns>
        private IDAL.DO.Station ClosestStation(Location location, IEnumerable<IDAL.DO.Station> stations)
        {
            IDAL.DO.Station station = dal.StationList().First();
            double diastance = LocationsDistance(location, StationLocation(station));

            foreach (var Station in stations)
            {
                if (diastance > LocationsDistance(location, StationLocation(Station)))
                    station = Station;
            }

            return station;
        }
    }
}
