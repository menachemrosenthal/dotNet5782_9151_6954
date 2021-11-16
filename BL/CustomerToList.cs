using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        int ParcelsProvidedNum { get; set; }
        int ParcelsUnprovidedNum { get; set; }
        int ReceivedParcelsNum { get; set; } 
        int UnreceivedParcelsNum { get; set; }
    }
}
