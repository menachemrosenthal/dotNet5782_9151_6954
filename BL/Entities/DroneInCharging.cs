namespace IBL.BO
{
    public class DroneInCharging
    {
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
        public override string ToString()
        {
            return " Drone In Charging: " +
                "\n  ID: " + Id +
                " , BatteryStatus: " + BatteryStatus;
                
        }
    }
}
