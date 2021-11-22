using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using IDAL.DO;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public Parcel GetParcel(int parcelId)
        {
            IDAL.DO.Parcel p = dal.GetParcel(parcelId);
            Parcel parcel = new();
            parcel.Id = p.Id; 
            parcel.PickedUp = p.PickedUp;
            parcel.Priority = (Priorities)p.Priority;
            parcel.Requested = p.Requested; parcel.Scheduled = p.Scheduled;
            parcel.Senderid = p.Senderid; parcel.TargetId = p.TargetId;
            parcel.Weight = (WeightCategories)p.Weight;
            parcel.Delivered = p.Delivered;
            if (drones.Any(x => x.Id == p.DroneId))
            {
                DroneToList d = new(); d = drones.Find(x => x.Id == p.DroneId);
                parcel.Drone = new(); parcel.Drone.Id = d.Id;
                parcel.Drone.CurrentLocation = new();
                parcel.Drone.CurrentLocation = d.CurrentLocation;
                parcel.Drone.BatteryStatus = d.BatteryStatus;
            }
            return parcel;
        }

        private ParcelInTransfer GetParcelInTransfer(int parcelId)
        {
            ParcelInTransfer parcel = new();
            IDAL.DO.Parcel dalParcel = dal.GetParcel(parcelId);
            parcel.Id = dalParcel.Id;

            var parcelsStatus = GetParcelStatus(parcelId);
            parcel.Transferred = parcelsStatus != ParcelStatuses.defined && parcelsStatus != ParcelStatuses.associated;

            parcel.Priority = (Priorities)dalParcel.Priority;
            parcel.Receiver = GetCustomerInParcel(dalParcel.TargetId);
            parcel.Sender = GetCustomerInParcel(dalParcel.Senderid);
            parcel.Collection = SenderLocation(dalParcel);
            parcel.Target = TargetLocation(dalParcel);
            return parcel;
        }

        public void ParcelProvisionUpdate(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));

            if (DroneStatus(droneId) != "Executing")
            {
                throw new CannotUpdateExeption("drone", droneId, "not executing");
            }

            IDAL.DO.Parcel parcel = dal.GetParcel(drone.DeliveredParcelId);
            drone.BatteryStatus -= SenderTaregetDistance(parcel) * dal.BatteryUseRquest()[(int)parcel.Weight];
            drone.CurrentLocation = TargetLocation(parcel);
            drone.Status = DroneStatuses.free;
            dal.UpdateDelivery(parcel.Id);
        }

        public IEnumerable<ParcelToList> GetParcelList()
        {
            return dal.ParcelList()
                .Select(parcel => new ParcelToList
                {
                    Id = parcel.Id,
                    Senderid = parcel.Senderid,
                    TargetId = parcel.TargetId,
                    Weight = (WeightCategories)parcel.Weight,
                    Priority = (Priorities)parcel.Priority,
                    Status = GetParcelStatus(parcel.Id)
                });
        }

        public IEnumerable<ParcelToList> GetNonAssociateParcelList()
        {
            return dal.ParcelList()
                .Where(parcel => GetParcelStatus(parcel.Id) == ParcelStatuses.defined)
                .Select(parcel => new ParcelToList
                {
                    Id = parcel.Id,
                    Senderid = parcel.Senderid,
                    TargetId = parcel.TargetId,
                    Weight = (WeightCategories)parcel.Weight,
                    Priority = (Priorities)parcel.Priority,
                    Status = GetParcelStatus(parcel.Id)
                });
        }

        private Location SenderLocation(IDAL.DO.Parcel parcel)
        {
            return CustomerLocation(dal.CustomerList().First(x => x.Id == parcel.Senderid));
        }

        private Location TargetLocation(IDAL.DO.Parcel parcel)
        {
            return CustomerLocation(dal.CustomerList().First(x => x.Id == parcel.TargetId));
        }

        /// <summary>
        /// sort parcel list
        /// </summary>
        /// <param name="location"></param>
        /// <returns>sort list by "priority" , "weight" , "closest location"</returns>

        private IDAL.DO.Parcel ClosestSender(Location location, IEnumerable<IDAL.DO.Parcel> parcels)
        {
            IDAL.DO.Parcel closestParcel = new();
            closestParcel = dal.ParcelList().First();
            double diastance = LocationsDistance(location, SenderLocation(closestParcel));

            foreach (var parcel in parcels)
            {
                if (diastance > LocationsDistance(location, SenderLocation(parcel)))
                    closestParcel = parcel;
            }

            return closestParcel;
        }

        private double SenderTaregetDistance(IDAL.DO.Parcel parcel)
        {
            return LocationsDistance(SenderLocation(parcel), TargetLocation(parcel));
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

        private ParcelStatuses GetParcelStatus(int parcelId)
        {
            if (dal.GetParcel(parcelId).Scheduled == null)
                return ParcelStatuses.defined;
            if (dal.GetParcel(parcelId).PickedUp == null)
                return ParcelStatuses.associated;
            if (dal.GetParcel(parcelId).Delivered == null)
                return ParcelStatuses.collected;
            return ParcelStatuses.provided;
        }
    }
}

