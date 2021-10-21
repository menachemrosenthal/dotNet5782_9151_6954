using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DP
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int Senderid { get; set; }
            public int TargetId { get; set; }
            public string Weight { get; set; }
            public string Proirity { get; set; }
            public int DroindId { get; set; }
            public string Requested { get; set; }
            public string Scheduled { get; set; }
            public string PickedUp { get; set; }
            public string Delivered { get; set; }
        }
    }
}
