using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IBL.BO
{
    public partial class BL:IBL
    {
        public Location SenderLocation(IDAL.DO.Parcel parcel)
        {
            return CusromerLocation(dal.CustomerList().First(x => x.Id == parcel.Senderid));
        }

        public Location TargetLocation(IDAL.DO.Parcel parcel)
        {
            return CusromerLocation(dal.CustomerList().First(x => x.Id == parcel.TargetId));
        }
    }
}
