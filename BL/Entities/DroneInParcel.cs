namespace BO
{
    public class DroneInParcel
    {
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
        public Location CurrentLocation { get; set; }
        public override string ToString()
        {
            return "Id: " + Id +
                " BatteryStatus: " + BatteryStatus + "\nCurrent Location:\n" + CurrentLocation;
        }
    }
}
