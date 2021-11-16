using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    class StationToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FreeChargeSlots { get; set; }
        public int FullChargeSlots { get; set; }

    }
}
