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
        public IEnumerable<StationToList> GetBaseStationList()
        {
            List<StationToList> baseStationList = new();
            foreach (var station in dal.StationList())
            {
                StationToList blStation = new()
                {
                    Id = station.Id,
                    Name = station.Name,
                    FreeChargeSlots = station.ChargeSlots,
                    FullChargeSlots = station.ChargeSlots + DronesInStation(station.Id).Count
                };

                baseStationList.Add(blStation);
            }

            return baseStationList;
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
                    station.ChargeSlots = chargeSlots - DronesInStation(stationId).Count;

            dal.StationUpdate(station);
        }
        public string StationToString(int Idstation)
        {
            Station blStation = new();
            IDAL.DO.Station s = new();
            s = dal.GetStation(Idstation);
            blStation.Id = Idstation; blStation.Name = s.Name;
            blStation.ChargeSlots = s.ChargeSlots; 
            blStation.LocationOfStation = StationLocation(s);                        
            blStation.DronesCharging = DronesInStation(Idstation);
            return blStation.ToString();
        }

        List<DroneInCharging> DronesInStation(int stationId)
        {
            List<DroneInCharging> droneCharge = new();

            foreach (var Charge in dal.DroneChargingList())
            {
                if (Charge.StationId == stationId)
                {
                    DroneInCharging d = new();
                    d.Id = Charge.DroneId;                    
                    d.BatteryStatus = Drones.Find(x => x.Id == Charge.DroneId).BatteryStatus;
                    droneCharge.Add(d);
                }
            }

            return droneCharge;
        }

    }
}
