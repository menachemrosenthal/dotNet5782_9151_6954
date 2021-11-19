using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public void AddStation(Station station)
        {
            IDAL.DO.Station dalStation = new();
            dalStation.Id = station.Id;
            dalStation.Name = station.Name;
            dalStation.Latitude = station.LocationOfStation.Latitude;
            dalStation.Longitude = station.LocationOfStation.Longitude;
            station.DronesCharging = null;
            dal.AddStation(dalStation);
        }

        Location StationLocation(IDAL.DO.Station station)
        {
            Location location = new() { Longitude = station.Longitude, Latitude = station.Latitude };
            return location;
        }

        IDAL.DO.Station ClosestStation(Location location,IEnumerable<IDAL.DO.Station> stations)
        {
            IDAL.DO.Station station = new();
            station = dal.StationList().First();
            double diastance = LocationsDistance(location, StationLocation(station));

            foreach (var Station in stations)
            {
                if (diastance > LocationsDistance(location, StationLocation(Station)))
                    station = Station;
            }

            return station;
        }

        public void StationUpdate(int stationId, string nameUpdate, string freeChargeSlots)
        {
            //if (!dal.StationList().Any(x => x.Id == station.Id))
            IDAL.DO.Station station = new();
            station = dal.StationList().First(x => x.Id == station.Id);
            if (nameUpdate!= "")
                station.Name = nameUpdate;
            if (freeChargeSlots != "")
                if (int.TryParse((freeChargeSlots), out int chargeSlots))
                    station.ChargeSlots = chargeSlots;

            dal.StationUpdate(station);
        }
        public string StationToString(int Idstation)
        {
            Station blStation = new();
            IDAL.DO.Station s = new();
            s = dal.GetStation(Idstation);
            blStation.Id = Idstation; blStation.Name = s.Name;
            blStation.ChargeSlots = s.ChargeSlots; blStation.LocationOfStation.Longitude = s.Longitude;
            blStation.LocationOfStation.Latitude = s.Latitude;
            IEnumerable<IDAL.DO.DroneCharge> drones = dal.DroneChargingList();
            foreach (var droneCharge in drones)
            {
                if (droneCharge.StationId == Idstation)
                {
                    DroneInCharging d = new();
                    d.Id = droneCharge.DroneId;
                    // how do i get battery status?!
                    blStation.DronesCharging.Add(d);
                }
            }
            return blStation.ToString();
        }
    }
}
