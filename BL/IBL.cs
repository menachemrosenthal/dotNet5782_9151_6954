﻿using IBL.BO;
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
        double LocationsDistance(Location l1, Location l2);
        void AddDrone(Drone drone,int stationID);
    }
}