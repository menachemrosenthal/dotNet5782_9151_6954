namespace IBL.BO
{
    public class ParcelToList
    {
        public int Id { get; set; }
        public int Senderid { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatuses Status { get; set; }

        public override string ToString()
        {
            return $"\nParcel nuber {Id} \n Sender ID: {Senderid} \n Receiver ID: {TargetId}" +
                $"\n Weight: {Weight} \n Priority: {Priority} \n Status: {Status}";
        }
    }
}
