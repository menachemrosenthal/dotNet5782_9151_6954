
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ChargeSlots { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public override string ToString()
            {
                return "Station: " + Name +
                    "\nID: " + Id + "\nCharge slots: " + ChargeSlots + "\nLongitude: " + Longitude + "\nLattitude: " + Latitude + "\n";
            }
        }
    }



