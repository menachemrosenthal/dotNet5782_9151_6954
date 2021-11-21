using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;


namespace IBL.BO
{
    public class DroneToList
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatuses Status { get; set; }
        public int DeliveredParcelId { get; set; }
        public Location CurrentLocation { get; set; }
        public override string ToString()
        {
            return $"\nDrone: {Model} \n ID: {Id} \n Maximum weight {MaxWeight} " +
                $"\n Battery: {BatteryStatus} \n Status: {Status.ToString()} " +
                $"\n Delivered Parcle ID: {DeliveredParcelId} \n {CurrentLocation} ";
        }
    }
}
