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
            return " Parcel number: " + Id +
                "\n Transferred: " + Transferred + "\n Priority: " + Priority + "\n Sender: " + Sender +
                "\n Receiver: " + Receiver + "\n Collection location: " + Collection +
                "\n Target location: " + Target + "\n Transport distance: " + TransportDistance + "\n";
        }
    }
}
