using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;


namespace DalApi
{
    internal partial class DalXml : IDal
    {
        public int AddParcel(Parcel parcel)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);
            if (parcels.Any(x => x.Id == parcel.Id))
                throw new DalApi.AddExistException("parcel", parcel.Id);

            parcels.Add(parcel);

            XMLTools.SaveListToXMLSerializer(parcels, ParcelPath);

            return parcel.Id;
        }

        public void UpdatePickup(int parcelId)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);
            var exist = parcels.Any(x => x.Id == parcelId);
            if (!exist)
                throw new ItemNotFoundException("Parcel", parcelId);

            var parcel = parcels.First(x => x.Id == parcelId);
            var index = parcels.IndexOf(parcel);
            parcel.PickedUp = DateTime.Now;
            parcels[index] = parcel;

            XMLTools.SaveListToXMLSerializer(parcels, ParcelPath);
        }

        public void UpdateDelivery(int parcelId)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);

            if (!parcels.Any(x => x.Id == parcelId))
                throw new ItemNotFoundException("Parcel", parcelId);

            var parcel = parcels.First(x => x.Id == parcelId);
            var index = parcels.IndexOf(parcel);
            parcel.Delivered = DateTime.Now;

            parcels[index] = parcel;
            XMLTools.SaveListToXMLSerializer(parcels, ParcelPath);
        }

        public Parcel GetParcel(int parcelId)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);

            if (!parcels.Any(x => x.Id == parcelId))
                throw new ItemNotFoundException("Parcel", parcelId);

            return parcels.FirstOrDefault(x => x.Id == parcelId);
        }

        public IEnumerable<Parcel> ParcelList()
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);

            return from parcel in parcels
                   select parcel;
        }

        public void ParcelDelete(int id)
        {
            Parcel parcel = GetParcel(id);
            if (parcel.DroneId == 0)
            {
                DataSource.Parcels.Remove(parcel);
            }

            else throw new ArgumentException("The parcel is associated");
        }

        public IEnumerable<Parcel> GetParcelsByCondition(Predicate<Parcel> condition)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelPath);

            return from parcel in parcels
                   where condition(parcel)
                   select parcel;
        }
    }
}
