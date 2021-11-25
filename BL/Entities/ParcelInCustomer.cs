namespace IBL.BO
{
    public class ParcelInCustomer
    {
        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatuses status { get; set; }
        public CustomerInParcel Customer { get; set; }

        public override string ToString()
        {
            return "  Parcel ID: " + Id + "\n  Weight: " + Weight
                    + "\n  Priority: " + Priority + "\n  Status: " + status
                    + "\n  Customer: " + Customer;
        }
    }
}
