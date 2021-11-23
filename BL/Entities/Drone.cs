namespace IBL.BO
{
    public class Dronetolist
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
                "\nID: " + Id + "\nMax weight: " + MaxWeight + "\nBatteryStatus:" + BatteryStatus
                + "\nDroneStatuses:" + Status + "\nParcelInTransfer:" + Parcel +
                "Location" + CurrentLocation + "\n";
                ;
        }
    }
}
