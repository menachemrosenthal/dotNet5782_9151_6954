using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public Parcel getParcel(int parcelId)
        {
            Parcel parcel = new();
            IDAL.DO.Parcel p = dal.GetParcel(parcelId);
            parcel.Id = p.Id; parcel.PickedUp = p.PickedUp;
            parcel.Priority = (Enums.Priorities)p.Priority;
            parcel.Requested = p.Requested; parcel.Scheduled = p.Scheduled;
            parcel.Senderid = p.Senderid; parcel.TargetId = p.TargetId;
            parcel.Weight = (Enums.WeightCategories)p.Weight;
            parcel.Delivered = p.Delivered;
            DroneToList d = new(); d = Drones.Find(x => x.Id == p.DroneId);
            parcel.Drone.Id = d.Id; parcel.Drone.CurrentLocation = d.CurrentLocation;
            parcel.Drone.BatteryStatus = d.BatteryStatus;
            return parcel;
        }
        public void parcelProvisionUpdate(int droneId)
        {
            if (DroneStatus(droneId) == "Executing")
            {
                DroneToList drone = new();
                drone = Drones.Find(x => x.Id == droneId);
                IDAL.DO.Parcel parcel = new();
                parcel = dal.ParcelList().First(x => x.Id == drone.DeliveredParcelId);
                drone.BatteryStatus -= SenderTaregetDistance(parcel) * dal.BatteryUseRquest()[(int)parcel.Weight];
                drone.CurrentLocation = TargetLocation(parcel);
                drone.Status = Enums.DroneStatuses.free;
                dal.UpdateDelivery(parcel.Id);
            }


            //throw...


        }

        public IEnumerable<ParcelToList> getParcelList()
        {
            List<ParcelToList> parcels = new();
            foreach (var parcel in dal.ParcelList())
            {
                ParcelToList parcelToList = new()
                {
                    Id = parcel.Id,
                    Senderid = parcel.Senderid,
                    TargetId = parcel.TargetId,
                    Weight = (Enums.WeightCategories)parcel.Weight,
                    Priority = (Enums.Priorities)parcel.Priority,
                    Status = GetParcelStatus(parcel.Id)
                };
                parcels.Add(parcelToList);
            }
            return parcels;
        }

        public IEnumerable<ParcelToList> GetNonAssociateParcelList()
        {
            List<ParcelToList> parcels = new();
            foreach (var parcel in dal.ParcelList())
            {
                if (GetParcelStatus(parcel.Id) == Enums.ParcelStatuses.defined)
                {
                    ParcelToList parcelToList = new()
                    {
                        Id = parcel.Id,
                        Senderid = parcel.Senderid,
                        TargetId = parcel.TargetId,
                        Weight = (Enums.WeightCategories)parcel.Weight,
                        Priority = (Enums.Priorities)parcel.Priority,
                        Status = GetParcelStatus(parcel.Id)
                    };
                    parcels.Add(parcelToList);
                }
            }
            return parcels;
        }

        Location SenderLocation(IDAL.DO.Parcel parcel)
        {
            return CustomerLocation(dal.CustomerList().First(x => x.Id == parcel.Senderid));
        }

        Location TargetLocation(IDAL.DO.Parcel parcel)
        {
            return CustomerLocation(dal.CustomerList().First(x => x.Id == parcel.TargetId));
        }

        /// <summary>
        /// sort parcel list
        /// </summary>
        /// <param name="location"></param>
        /// <returns>sort list by "priority" , "weight" , "closest location"</returns>
        List<IDAL.DO.Parcel> SortParcels(Location location)
        {
            List<IDAL.DO.Parcel> parcels = new();
            List<IDAL.DO.Parcel> temp = new();
            foreach (var parcel in dal.ParcelList())
            {
                parcels.Add(parcel);
            }

            //sort by closet location
            while (parcels.Count != 0)
            {
                temp.Add(ClosestSender(location, parcels));
                parcels.Remove(ClosestSender(location, parcels));
            }

            //sort by weight
            while (temp.Count != 0)
            {
                if (temp.Any(x => x.Weight == WeightCategories.heavy))
                {
                    parcels.Add(temp.First(x => x.Weight == WeightCategories.heavy));
                    temp.Remove(temp.First(x => x.Weight == WeightCategories.heavy));
                    continue;
                }

                if (temp.Any(x => x.Weight == WeightCategories.medium))
                {
                    parcels.Add(temp.First(x => x.Weight == WeightCategories.medium));
                    temp.Remove(temp.First(x => x.Weight == WeightCategories.medium));
                    continue;
                }

                if (temp.Any(x => x.Weight == WeightCategories.light))
                {
                    parcels.Add(temp.First(x => x.Weight == WeightCategories.light));
                    temp.Remove(temp.First(x => x.Weight == WeightCategories.light));
                    continue;
                }
            }

            //sort by priority
            while (parcels.Count != 0)
            {
                if (parcels.Any(x => x.Priority == Priorities.urgent))
                {
                    temp.Add(parcels.First(x => x.Priority == Priorities.urgent));
                    parcels.Remove(parcels.First(x => x.Priority == Priorities.urgent));
                    continue;
                }

                if (parcels.Any(x => x.Priority == Priorities.fast))
                {
                    temp.Add(parcels.First(x => x.Priority == Priorities.fast));
                    parcels.Remove(parcels.First(x => x.Priority == Priorities.fast));
                    continue;
                }

                if (parcels.Any(x => x.Priority == Priorities.ragular))
                {
                    temp.Add(parcels.First(x => x.Priority == Priorities.ragular));
                    parcels.Remove(parcels.First(x => x.Priority == Priorities.ragular));
                    continue;
                }
            }

            return temp;
        }

        IDAL.DO.Parcel ClosestSender(Location location, IEnumerable<IDAL.DO.Parcel> parcels)
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

        double SenderTaregetDistance(IDAL.DO.Parcel parcel)
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

        Enums.ParcelStatuses GetParcelStatus(int parcelId)
        {           
            if (dal.GetParcel(parcelId).Scheduled == null)
                return Enums.ParcelStatuses.defined;
            if (dal.GetParcel(parcelId).PickedUp == null)
                return Enums.ParcelStatuses.associated;
            if (dal.GetParcel(parcelId).Delivered == null)
                return Enums.ParcelStatuses.collected;
            return Enums.ParcelStatuses.provided;
        }
    }
}

