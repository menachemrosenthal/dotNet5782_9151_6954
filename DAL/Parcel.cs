using System;


    namespace DalApi
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int Senderid { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public int DroneId { get; set; }
            public DateTime? Requested { get; set; }
            public DateTime? Scheduled { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }
            public override string ToString()
            {
                return "Parcel number: " + Id + "\nSender ID: " + Senderid + "\nTarget ID: " + TargetId +
                    "\nWeight: " + Weight + "\nPriority: " + Priority + "\nDrone ID: " + DroneId +
                    "\nRequested: " + Requested + "\nSchedeled: " + Scheduled +
                    "\nPicked up: " + PickedUp + "\nDelivered: " + Delivered + "\n";
            }
        }
    }

