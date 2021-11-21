using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

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
            return "Parcel number: " + Id + "\nSender ID: " + Senderid + "\nTarget ID: " + TargetId +
                "\nWeight: " + Weight + "\nPriority: " + Priority + "\nDrone: " + Drone +
                "\nRequested: " + Requested + "\nSchedeled: " + Scheduled +
                "\nPicked up: " + PickedUp + "\nDelivered: " + Delivered + "\n";
        }
    }
}
