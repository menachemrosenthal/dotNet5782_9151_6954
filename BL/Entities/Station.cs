
using System.Collections.Generic;

namespace BO
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
            string s = "";
            foreach (var drone in DronesCharging)
            {
                s += "\n" + drone.ToString();
            }
            return "\nStation: " + Name +
                "\n ID: " + Id +
                "\n Charge slots: " + ChargeSlots +
                "\n Location" + LocationOfStation + s;
        }
    }
}
