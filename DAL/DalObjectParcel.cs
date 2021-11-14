﻿using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// add a parcel to the parcels array
        /// </summary>
        /// <param name="parcel">the parcel to add</param>
        public void AddParcel(Parcel parcel)
        {
            parcel.Id = DataSource.Config.CreateParcelNumber;
            DataSource.Parcels.Add(parcel);
        }

        /// <summary>
        /// connect parcel to drone
        /// </summary>
        /// <param name="parcelId">parcel number to connect</param>
        /// <param name="droneId">drone id</param>
        public void ParcelToDrone(int parcelId, int droneId)
        {
            try
            {
                var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Parcel", parcelId);

                if (!(exist = DataSource.Drones.Any(x => x.Id == droneId)))

                    throw new IDAL.ItemNotFoundException("Drone", droneId);

                var parcel = DataSource.Parcels.First(x => x.Id == parcelId);
                var index = DataSource.Parcels.IndexOf(parcel);
                parcel.DroneId = droneId;
                parcel.Scheduled = DateTime.Now;
                DataSource.Parcels[index] = parcel;
            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
                return;
            }

        }

        /// <summary>
        /// the time of pickup a parcel by drone update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdatePickup(int parcelId)
        {
            try
            {
                var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Parcel", parcelId);

                var parcel = DataSource.Parcels.First(x => x.Id == parcelId);
                var index = DataSource.Parcels.IndexOf(parcel);
                parcel.PickedUp = DateTime.Now;
                DataSource.Parcels[index] = parcel;

            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        /// <summary>
        /// parcel delivery time update
        /// </summary>
        /// <param name="parcelId">parcel id</param>
        public void UpdateDelivery(int parcelId)
        {
            try
            {
                var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Parcel", parcelId);

                var parcel = DataSource.Parcels.First(x => x.Id == parcelId);
                var index = DataSource.Parcels.IndexOf(parcel);
                parcel.Delivered = DateTime.Now;
                DataSource.Parcels[index] = parcel;

            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        /// <summary>
        /// get parcel
        /// </summary>
        /// <param name="parcelId">parcel id to return</param>
        /// <returns>parcel object</returns>
        public Parcel? GetParcel(int parcelId)
        {
            try
            {
                var exist = DataSource.Parcels.Any(x => x.Id == parcelId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Parcel", parcelId);

                return DataSource.Parcels.FirstOrDefault(x => x.Id == parcelId);
            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        /// <summary>
        /// get list of the parcels
        /// </summary>
        /// <returns>parcel array</returns>
        public IEnumerable<Parcel> ParcelList() => DataSource.Parcels.ToList();
    }
}
