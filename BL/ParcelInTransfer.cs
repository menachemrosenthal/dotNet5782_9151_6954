using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    class ParcelInTransfer
    {
        public int Id { get; set; }
        public bool waitingOrTransferred { get; set; }
        public Priorities Priority { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Receiver { get; set; }
        public Location Collection { get; set; }
        public Location Target { get; set; }
        public double TransportDistance { get; set; }
    }
}
