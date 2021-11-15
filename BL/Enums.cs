using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Enums
    {
        public enum WeightCategories { light, medium, heavy }
        public enum Priorities { ragular, fast, urgent }
        public enum DroneStatuses { free, maintenance, sending }
        public enum ParcelStatuses {defined, associated, collected, provided }
    }
}
