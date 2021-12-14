﻿namespace DO
{
    public struct Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public override string ToString()
        {
            return "Customer: " + Name +
                "\nID: " + Id + "\nPhone nember: " + Phone + "\nLongitude: " + Longitude + "\nLattitude: " + Latitude + "\n";
        }
    }
}
