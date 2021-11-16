using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;


namespace IBL.BO
{
    class DroneToList
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatuses Status { get; set; }
        public int ParcelInTransferId { get; set; }
        public Location CurrentLocation { get; set; }
    }
}
