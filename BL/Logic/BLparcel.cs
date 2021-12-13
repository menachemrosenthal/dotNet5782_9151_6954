﻿using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using BlApi;

namespace BO
{
    public partial class BL : IBL
    {
        /// <summary>
        /// gets parcel and creates bl object
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>created paecel</returns>
        public Parcel GetParcel(int parcelId)
        {
            DO.Parcel p = dal.GetParcel(parcelId);
            Parcel parcel = new()
            {
                Id = p.Id,
                PickedUp = p.PickedUp,
                Priority = (Priorities)p.Priority,
                Requested = p.Requested,
                Scheduled = p.Scheduled,
                Senderid = p.Senderid,
                TargetId = p.TargetId,
                Weight = (WeightCategories)p.Weight,
                Delivered = p.Delivered
            };

            if (drones.Any(x => x.Id == p.DroneId))
            {
                DroneToList d = drones.FirstOrDefault(x => x.Id == p.DroneId);
                parcel.Drone = new()
                {
                    Id = d.Id,
                    CurrentLocation = d.CurrentLocation,
                    BatteryStatus = d.BatteryStatus
                };
            }
            return parcel;
        }

        /// <summary>
        /// updates drone that parcel was delivered
        /// </summary>
        /// <param name="droneId"></param>
        public void ParcelProvisionUpdate(int droneId)
        {
            DroneToList drone = drones.FirstOrDefault(x => x.Id == droneId) ?? throw new KeyNotFoundException(nameof(droneId));

            if (GetDroneSituation(droneId) != "Executing")
                throw new CannotUpdateExeption("drone", droneId, "not executing");

            DO.Parcel parcel = dal.GetParcel(drone.DeliveredParcelId);
            drone.BatteryStatus -= SenderTaregetDistance(parcel) * dal.BatteryUseRequest()[(int)parcel.Weight];
            drone.CurrentLocation = TargetLocation(parcel);
            drone.Status = DroneStatuses.free;
            drone.DeliveredParcelId = 0;
            dal.UpdateDelivery(parcel.Id);
        }

        /// <summary>
        /// gets list of parcels
        /// </summary>
        /// <returns>list of parcels</returns>
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

        /// <summary>
        /// add a parcel
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            DO.Parcel dalParcel = new()
            {
                Senderid = parcel.Senderid,
                TargetId = parcel.TargetId,
                Weight = (DO.WeightCategories)parcel.Weight,
                Priority = (DO.Priorities)parcel.Priority,
                Requested = DateTime.Now,

            };
            dal.AddParcel(dalParcel);
        }

        /// <summary>
        /// get list of usassociated Parceles
        /// </summary>
        /// <returns>list of usassociated Parceles</returns>
        public IEnumerable<ParcelToList> GetNonAssociateParcelList()
        {
            return dal.GetParcelsByCondition(parcel => GetParcelStatus(parcel.Id) == ParcelStatuses.defined)
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

        /// <summary>
        /// gets parcel in transfer
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>parcel in transfer</returns>
        private ParcelInTransfer GetParcelInTransfer(int parcelId)
        {
            ParcelInTransfer parcel = new();
            DO.Parcel dalParcel = dal.GetParcel(parcelId);
            parcel.Id = dalParcel.Id;

            var parcelsStatus = GetParcelStatus(parcelId);
            parcel.Transferred = parcelsStatus != ParcelStatuses.defined && parcelsStatus != ParcelStatuses.associated;
            parcel.Priority = (Priorities)dalParcel.Priority;
            parcel.Receiver = GetCustomerInParcel(dalParcel.TargetId);
            parcel.Sender = GetCustomerInParcel(dalParcel.Senderid);
            parcel.Collection = SenderLocation(dalParcel);
            parcel.Target = TargetLocation(dalParcel);
            parcel.TransportDistance = SenderTaregetDistance(dalParcel);
            return parcel;
        }

        /// <summary>
        /// gets sender location
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns> sender location</returns>
        private Location SenderLocation(DO.Parcel parcel)
            => CustomerLocation(dal.CustomerList().First(x => x.Id == parcel.Senderid));

        /// <summary>
        /// gets Target location
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns> Target location</returns>
        private Location TargetLocation(DO.Parcel parcel)
            => CustomerLocation(dal.CustomerList().First(x => x.Id == parcel.TargetId));

        /// <summary>
        /// sort parcel list
        /// </summary>
        /// <param name="location"></param>
        /// <returns>sort list by "priority" , "weight" , "closest location"</returns>        
        private DO.Parcel ClosestSender(Location location, IEnumerable<DO.Parcel> parcels)
        {
            DO.Parcel closestParcel = dal.ParcelList().FirstOrDefault();
            double diastance = LocationsDistance(location, SenderLocation(closestParcel));

            foreach (var parcel in parcels)
                if (diastance > LocationsDistance(location, SenderLocation(parcel)))
                    closestParcel = parcel;

            return closestParcel;
        }

        /// <summary>
        /// calculates distance between sender and reciever
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>distance between sender and reciever</returns>
        private double SenderTaregetDistance(DO.Parcel parcel)
            => LocationsDistance(SenderLocation(parcel), TargetLocation(parcel));

        /// <summary>
        /// get parcel status
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>parcel status</returns>
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

