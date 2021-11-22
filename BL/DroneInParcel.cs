

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    public class DroneInParcel
    {
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
        public Location CurrentLocation { get; set; }
        public override string ToString()
        {
            return "\n Id: " + Id +
                "\n BatteryStatus: " + BatteryStatus + "\n CurrentLocation: " + CurrentLocation;
                ;
        }
    }
}
