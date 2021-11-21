using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    public class ParcelInTransfer
    {
        public int Id { get; set; }
        public bool Transferred { get; set; }
        public Priorities Priority { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Receiver { get; set; }
        public Location Collection { get; set; }
        public Location Target { get; set; }
        public double TransportDistance { get; set; }
        public override string ToString()
        {
            return "Parcel number: " + Id +
                "\nTransferred: " + Transferred + "\nPriority: " + Priority + "\nSender: " + Sender +
                "\nReceiver: " + Receiver + "\nCollection: " + Collection +
                "\nTarget: " + Target + "\nTransportDistance: " + TransportDistance + "\n";
        }
    }
}
