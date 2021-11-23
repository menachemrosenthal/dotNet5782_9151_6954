namespace IBL.BO
{
    public class StationToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FreeChargeSlots { get; set; }
        public int FullChargeSlots { get; set; }
        public override string ToString()
        {
            return $"\nBase Station: {Name} \n ID: {Id} \n Free Charge slots: {FreeChargeSlots} " +
            $"\n Full Charge slots: {FullChargeSlots}";
        }
    }
}
