using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public Names Name { get; set; }
            public string Phone { get; set; }


            public double Longitude { get; set; }
            public double Lattitude { get; set; }

        }
    }
}


