using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    public class DroneInCharging
    {
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
        public override string ToString()
        {
            return "Drone In Charging: " +
                "\nID: " + Id +
                "\n:BatteryStatus "+ BatteryStatus
                ;
        }
    }
}
