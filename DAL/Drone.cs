using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public double Battery { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public override string ToString()
            {
                return "Drone: " + Model +
                    "\nID: " + Id + "\nBattery: " + Battery + "\nMax weight: " + MaxWeight + "\nStatus: " + Status + "\n";
            }
        }
    }
}

