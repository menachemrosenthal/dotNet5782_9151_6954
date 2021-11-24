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
        public List<ParcelInCustomer> Sended;
        public List<ParcelInCustomer> Get;
        public override string ToString()
        {
            string s = "";
            foreach (var parcel in Sended)
            {
                s += parcel.ToString();
            }
            string a = "";
            foreach (var parcel in Get)
            {
                a += parcel.ToString();
            }
            return "Customer: " + Name + "\n ID: " + Id + "\n Phone: " + Phone +
                "\n Location: " + Location + "\n List of Parceles to be sent:\n" + s +
                "\n List of Parceles to get:\n" + a + "\n";
        }
    }
}
