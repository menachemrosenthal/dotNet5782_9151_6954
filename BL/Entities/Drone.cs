namespace IBL.BO
{
    public class Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatuses Status { get; set; }
        public ParcelInTransfer Parcel { get; set; }
        public Location CurrentLocation { get; set; }
        public override string ToString()
        {
            return "Drone: " + Model +
                "\n ID: " + Id + "\n Max weight: " + MaxWeight + "\n Battery status: " + BatteryStatus
                + "\n Drone status: " + Status + "\n Parcel in transfer: \n" + Parcel +
                " Drone current Location: " + CurrentLocation + "\n";
                ;
        }
    }
}
