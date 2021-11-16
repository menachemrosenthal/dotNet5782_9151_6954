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
    }
}
