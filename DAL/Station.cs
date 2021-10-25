using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public int ChargeSlots { get; set; }


            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            
        }
    }
}


