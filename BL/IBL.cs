using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public interface IBL
    {
        void AddStation(Station station);
        void AddDrone(Drone drone,int stationID);
    }
}
