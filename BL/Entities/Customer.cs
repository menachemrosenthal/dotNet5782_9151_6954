using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; } 
        public List<Parcel> Sended;
        public List<Parcel> Get;
        public override string ToString()
        {
            return "Customer number: " + Id + "\nName ID: " + Name + "\nPhone ID: " + Phone +
                "\nLocation: " + Location + "List of Parceles to be sent " + Sended +
                "List of Parceles to get" + Get + "\n";
        }
    }
}
