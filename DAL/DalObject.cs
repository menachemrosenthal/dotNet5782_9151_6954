using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IDAL
{
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// constructor
        /// </summary>
        public DalObject()
        {
            DataSource.Config.Initialize();
        }

        /// <summary>
        /// distance calculation between to geographic points
        /// </summary>
        /// <param name="lat1">user lattiude</param>
        /// <param name="lon1">user longitude</param>
        /// <param name="lat2">object lattiude</param>
        /// <param name="lon2">object longitude</param>
        /// <returns>distance in kilometer</returns>
        public double DistanceCalculate(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist * 1.609344; ;
        }

        /// <summary>
        /// electricity use array
        /// </summary>
        /// <returns>electricityUse array</returns>
        public double[] BatteryUseRquest()
        {
            double[] electricityUse = new double[5];

            electricityUse[0] = DataSource.Config.Free;
            electricityUse[1] = DataSource.Config.CarryingLight;
            electricityUse[2] = DataSource.Config.CarryingMediem;
            electricityUse[3] = DataSource.Config.CarryingHeavy;
            electricityUse[4] = DataSource.Config.ChargePace;

            return electricityUse;
        }
    }
}