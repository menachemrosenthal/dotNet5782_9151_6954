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
                " , BatteryStatus: "+ BatteryStatus
                ;
        }
    }
}
