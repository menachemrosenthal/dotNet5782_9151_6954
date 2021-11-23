using System.Collections.Generic;
using System.Linq;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public IEnumerable<StationToList> GetBaseStationList() => dal.StationList().Select(x =>
                new StationToList
                {
                    Id = x.Id,
                    Name = x.Name,
                    FreeChargeSlots = x.ChargeSlots,
                    FullChargeSlots = x.ChargeSlots + DronesInStation(x.Id).Count
                }
            );

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

        public void AddStation(Station station)
        {
            IDAL.DO.Station dalStation = new();
            dalStation.Id = station.Id;
            dalStation.Name = station.Name;
            dalStation.ChargeSlots = station.ChargeSlots;
            dalStation.Latitude = station.LocationOfStation.Latitude;
            dalStation.Longitude = station.LocationOfStation.Longitude;
            station.DronesCharging = null;
            dal.AddStation(dalStation);
        }

        public void StationUpdate(int stationId, string nameUpdate, string freeChargeSlots)
        {
            IDAL.DO.Station station = dal.GetStation(stationId);

            if (!string.IsNullOrWhiteSpace(nameUpdate))
                station.Name = nameUpdate;
            if (!string.IsNullOrWhiteSpace(freeChargeSlots) && int.TryParse(freeChargeSlots, out int chargeSlots))
                station.ChargeSlots = chargeSlots - DronesInStation(stationId).Count;
            dal.StationUpdate(station);
        }

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

        private Location StationLocation(IDAL.DO.Station station)
        {
            return new() { Longitude = station.Longitude, Latitude = station.Latitude };
        }

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
