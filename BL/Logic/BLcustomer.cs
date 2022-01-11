using System;
using System.Collections.Generic;
using System.Linq;
using BlApi;
using System.Runtime.CompilerServices;

namespace BO
{
    public partial class BL : IBL
    {
        /// <summary>
        /// when changing happens or customer was added
        /// </summary>
        private event EventHandler CustomerChanged;

        /// <summary>
        /// gets Customer and creates bl object
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>BL Customer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int CustomerId)
        {
            lock (dal)
            {
                DalApi.Customer dalCustomer = dal.GetCustomer(CustomerId);

                Customer customer = new()
                {
                    Id = dalCustomer.Id,
                    Name = dalCustomer.Name,
                    Phone = dalCustomer.Phone,
                    Location = CustomerLocation(dalCustomer),
                    Sended = new(),
                    Get = new()
                };

                foreach (var par in dal.ParcelList())
                {
                    if (par.Senderid == CustomerId)
                    {
                        ParcelInCustomer parcel = new()
                        {
                            Id = par.Id,
                            Weight = (WeightCategories)par.Weight,
                            Priority = (Priorities)par.Priority,
                            status = GetParcelStatus(par.Id),
                            Customer = GetCustomerInParcel(par.TargetId)
                        };

                        customer.Sended.Add(parcel);
                    }

                    if (par.TargetId == CustomerId)
                    {
                        ParcelInCustomer parcel = new()
                        {
                            Id = par.Id,
                            Weight = (WeightCategories)par.Weight,
                            Priority = (Priorities)par.Priority,
                            status = GetParcelStatus(par.Id),
                            Customer = GetCustomerInParcel(par.Senderid)
                        };

                        customer.Get.Add(parcel);
                    }
                }
                return customer;
            }
        }

        /// <summary>
        /// add a Custumer
        /// </summary>
        /// <param name="Custumer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustumer(Customer customer)
        {
            lock (dal)
            {
                if (customer.Location.Longitude is < 34.5 or > 35.9
                    || customer.Location.Latitude is < 31.589844 or > 32.801705)
                {
                    throw new ArgumentException("location was out Out Of range");
                }

                DalApi.Customer dalCustomer = new()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Latitude = customer.Location.Latitude,
                    Longitude = customer.Location.Longitude
                };
                dal.AddCustumer(dalCustomer);                
            }
        }

        /// <summary>
        /// gets list of customer
        /// </summary>
        /// <returns>list of customer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetCustomerList()
        {
            lock (dal)
            {
                return dal.CustomerList().Select(x =>
                    new CustomerToList
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Phone = x.Phone,
                        ParcelsProvidedNum = ProvidedParcels(x.Id),
                        ParcelsUnprovidedNum = UnProvidedParcels(x.Id),
                        ReceivedParcelsNum = ReceivedParcels(x.Id),
                        UnreceivedParcelsNum = UnreceivedParcels(x.Id)
                    });
            }
        }

        /// <summary>
        /// update customer name or phone num
        /// </summary>
        /// <param name="customer">customer for update</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CustomerUpdate(Customer customer)
        {
            lock (dal)
            {
                DalApi.Customer dalCustomer = dal.GetCustomer(customer.Id);

                if (!string.IsNullOrWhiteSpace(customer.Name))
                    dalCustomer.Name = customer.Name;

                if (!string.IsNullOrWhiteSpace(customer.Phone))
                    dalCustomer.Phone = customer.Phone;

                dal.CustomerUpdate(dalCustomer);                
            }
        }

        /// <summary>
        /// calculates distance to closest station
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private double CustomerClosestStationDistance(int customerId)
        {
            lock (dal)
            {
                DalApi.Customer customer = dal.CustomerList().FirstOrDefault(x => x.Id == customerId);
                return LocationsDistance(CustomerLocation(customer), StationLocation(ClosestStation(CustomerLocation(customer), dal.StationList())));
            }
        }

        /// <summary>
        /// gets number of provided parcels to customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>number of provided parcels</returns>
        private int ProvidedParcels(int customerId)
        {
            lock (dal)
            {
                int sum = 0;
                foreach (var parcel in dal.GetParcelsByCondition(x => x.Senderid == customerId && x.Delivered != null))
                    sum++;

                return sum;
            }
        }

        /// <summary>
        /// gets number of unprovided parcels to customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>number of unprovided parcels</returns>
        private int UnProvidedParcels(int customerId)
        {
            lock (dal)
            {
                int sum = 0;
                foreach (var parcel in dal.GetParcelsByCondition(x => x.Senderid == customerId && x.Delivered == null))
                    sum++;

                return sum;
            }
        }

        /// <summary>
        /// gets number of Received parcels to customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>number of Received parcels</returns>
        private int ReceivedParcels(int customerId)
        {
            lock (dal)
            {
                int sum = 0;
                foreach (var parcel in dal.GetParcelsByCondition(x => x.TargetId == customerId && x.Delivered != null))
                    sum++;

                return sum;
            }
        }

        /// <summary>
        /// gets number of unReceived parcels to customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>number of unReceived parcels</returns>
        private int UnreceivedParcels(int customerId)
        {
            lock (dal)
            {
                int sum = 0;
                foreach (var parcel in dal.GetParcelsByCondition(x => x.TargetId == customerId && x.Delivered == null))
                    sum++;

                return sum;
            }
        }

        /// <summary>
        /// gets CustomerInParcel by id
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns>CustomerInParcel</returns>
        private CustomerInParcel GetCustomerInParcel(int CustomerId)
        {
            lock (dal)
            {
                DalApi.Customer dalCustomer = dal.GetCustomer(CustomerId);
                CustomerInParcel customer = new()
                {
                    Id = dalCustomer.Id,
                    Name = dalCustomer.Name
                };
                return customer;
            }
        }

        /// <summary>
        /// gets customer location
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>customer location</returns>
        private Location CustomerLocation(DalApi.Customer customer)
        {
            lock (dal)
            {
                Location location = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
                return location;
            }
        }

        /// <summary>
        /// list of customers that recieved rarcels
        /// </summary>
        /// <returns>list of customers</returns>
        private IEnumerable<DalApi.Customer> ReceivedCustomersList()
        {
            lock (dal)
            {
                return dal.GetCustomersByCondition
                   (x => dal.ParcelList().Any(y => x.Id == y.TargetId && y.Delivered != null)).ToList();
            }
        }
    }
}
