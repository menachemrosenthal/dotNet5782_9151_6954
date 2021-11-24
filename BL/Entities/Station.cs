using System.Collections.Generic;

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
                "\n ID: " + Id +
                "\n Charge slots: " + ChargeSlots +
                "\n Location" + LocationOfStation;                             
        }
    }
}
