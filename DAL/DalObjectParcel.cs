using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DalApi
{
    internal partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// add a parcel to the parcels array
        /// </summary>
        /// <param name="parcel">the parcel to add</param>
        public int AddParcel(Parcel parcel)
        {
            parcel.Id = DataSource.Config.CreateParcelNumber;
            DataSource.Parcels.Add(parcel);
            return parcel.Id;
        }

        /// <summary>
        /// connect parcel to drone
        /// </summary>
        /// <param name="parcelId">parcel number to connect</param>
        /// <param name="droneId">drone id</param>
        public void ParcelToDrone(int parcelId, int droneId)
        {
            var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
            if (!exist)
                throw new ItemNotFoundException("Parcel", parcelId);

            if (!(exist = DataSource.Drones.Any(x => x.Id == droneId)))

                throw new ItemNotFoundException("Drone", droneId);

            var parcel = DataSource.Parcels.First(x => x.Id == parcelId);
            var index = DataSource.Parcels.IndexOf(parcel);
            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            DataSource.Parcels[index] = parcel;
        }

        /// <summary>
        /// the time of pickup a parcel by drone update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdatePickup(int parcelId)
        {
            var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
            if (!exist)
                throw new ItemNotFoundException("Parcel", parcelId);

            var parcel = DataSource.Parcels.First(x => x.Id == parcelId);
            var index = DataSource.Parcels.IndexOf(parcel);
            parcel.PickedUp = DateTime.Now;
            DataSource.Parcels[index] = parcel;
        }

        /// <summary>
        /// parcel delivery time update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdateDelivery(int parcelId)
        {
            var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
            if (!exist)
                throw new ItemNotFoundException("Parcel", parcelId);

            var parcel = DataSource.Parcels.First(x => x.Id == parcelId);
            var index = DataSource.Parcels.IndexOf(parcel);
            parcel.Delivered = DateTime.Now;
            DataSource.Parcels[index] = parcel;
        }

        /// <summary>
        /// get parcel
        /// </summary>
        /// <param name="parcelId">parcel id to return</param>
        /// <returns>parcel object</returns>
        public Parcel GetParcel(int parcelId)
        {
            var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
            if (!exist)
                throw new DalApi.ItemNotFoundException("Parcel", parcelId);

            return DataSource.Parcels.FirstOrDefault(x => x.Id == parcelId);
        }

        /// <summary>
        /// get list of the parcels
        /// </summary>
        /// <returns>parcel array</returns>
        public IEnumerable<Parcel> ParcelList() => DataSource.Parcels.ToList();

        public IEnumerable<Parcel> GetParcelsByCondition(Predicate<Parcel> condition)
            => DataSource.Parcels.Where(x => condition(x)).ToList();
    }
}
