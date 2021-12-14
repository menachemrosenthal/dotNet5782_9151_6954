namespace BO
{
    public class CustomerToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int ParcelsProvidedNum { get; set; }
        public int ParcelsUnprovidedNum { get; set; }
        public int ReceivedParcelsNum { get; set; }
        public int UnreceivedParcelsNum { get; set; }

        public override string ToString()
        {
            return $"\nCustomer: {Name} \n ID: {Id} \n Phone: {Phone} " +
                $"\n Parcels provided: {ParcelsProvidedNum} \n Parcels unprovided {ParcelsUnprovidedNum} " +
                $"\n Parcels received: {ReceivedParcelsNum} \n Parcels unreceived {UnreceivedParcelsNum}";
        }
    }
}
