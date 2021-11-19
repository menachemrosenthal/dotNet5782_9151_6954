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
        public void AddParcel(Parcel parcel)
        {
            IDAL.DO.Parcel dalParcel = new();
            dalParcel.Senderid = parcel.Senderid;
            dalParcel.TargetId = parcel.TargetId;
            dalParcel.Weight = (IDAL.DO.WeightCategories)parcel.Weight;
            dalParcel.Priority = (IDAL.DO.Priorities)parcel.Priority;
            dalParcel.Requested = DateTime.Now;
            parcel.Drone = null;
            dal.AddParcel(dalParcel);
        }
    }
}
