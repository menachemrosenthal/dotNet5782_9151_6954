using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location LocationOfStation { get; set; }
        public int ChargeSlots { get; set; }

        public List<DroneInCharging> DronesCharging;
        public override string ToString()
        {
            return "\nStation: " + Name +
                "\nID: " + Id +
                "\nCharge slots: " + ChargeSlots +
                "Location"+LocationOfStation +
                "List of Drones In Charging:"+DronesCharging+"\n";                
        }
    }
}
