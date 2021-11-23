using System;

namespace IBL.BO
{
    public class Parcel
    {
        public int Id { get; set; }
        public int Senderid { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel Drone { get; set; }
        public DateTime? Requested { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Delivered { get; set; }
        public override string ToString()
        {
            return "Parcel number: " + Id + "\n Sender ID: " + Senderid + "\n Target ID: " + TargetId +
                "\n Weight: " + Weight + "\n Priority: " + Priority + "\n Drone: " + Drone +
                "\n Requested: " + Requested + "\n Schedeled: " + Scheduled +
                "\n Picked up: " + PickedUp + "\n Delivered: " + Delivered + "\n";
        }
    }
}
