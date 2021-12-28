namespace BO
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
                " , Transferred: " + Transferred + " , Priority: " + Priority + " ,\nSender: " + Sender +
                "\nReceiver: " + Receiver + "\nCollection location: " + Collection +
                "\nTarget location: " + Target + "\n Transport distance: " + TransportDistance + "\n";
        }
    }
}
