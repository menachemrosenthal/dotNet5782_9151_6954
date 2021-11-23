using System.Collections.Generic;
using System.Linq;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public Customer GetCustomer(int CustomerId)
        {
            IDAL.DO.Customer c1 = dal.GetCustomer(CustomerId);

            Customer customer = new();
            customer.Id = c1.Id;
            customer.Name = c1.Name;
            customer.Phone = c1.Phone;
            customer.Location.Latitude = c1.Latitude;
            customer.Location.Longitude = c1.Longitude;
            foreach (var par in dal.ParcelList())
            {
                if (par.Senderid == CustomerId)
                {
                    Parcel p1 = new();
                    p1 = GetParcel(par.Id);
                    customer.Sended.Add(p1);
                }
                if (par.TargetId == CustomerId)
                {
                    Parcel p2 = new();
                    p2 = GetParcel(par.Id);
                    customer.Sended.Add(p2);
                }
            }
            return customer;
        }

        public void AddCustumer(Customer customer)
        {
            IDAL.DO.Customer dalCustomer = new();
            dalCustomer.Id = customer.Id;
            dalCustomer.Name = customer.Name;
            dalCustomer.Phone = customer.Phone;
            dalCustomer.Latitude = customer.Location.Latitude;
            dalCustomer.Longitude = customer.Location.Longitude;
            dal.AddCustumer(dalCustomer);
        }

        public IEnumerable<CustomerToList> GetCustomerList()
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

        public void CustomerUpdate(Customer customer)
        {
            IDAL.DO.Customer dalCustomer = dal.GetCustomer(customer.Id);

            if (!string.IsNullOrWhiteSpace(customer.Name))
                dalCustomer.Name = customer.Name;
            if (!string.IsNullOrWhiteSpace(customer.Phone))
                dalCustomer.Phone = customer.Phone;
            dal.CustomerUpdate(dalCustomer);
        }

        private double CustomerClosestStationDistance(int customerId)
        {
            IDAL.DO.Customer customer = new();
            customer = dal.CustomerList().First(x => x.Id == customerId);
            return LocationsDistance(CustomerLocation(customer), StationLocation(ClosestStation(CustomerLocation(customer), dal.StationList())));
        }

        private int ProvidedParcels(int customerId)
        {
            int sum = 0;
            foreach (var parcel in dal.ParcelList())
            {
                if (parcel.Senderid == customerId)
                    if (parcel.Delivered != null)
                        sum++;
            }

            return sum;
        }

        private int UnProvidedParcels(int customerId)
        {
            int sum = 0;
            foreach (var parcel in dal.ParcelList())
            {
                if (parcel.Senderid == customerId)
                    if (parcel.Delivered == null)
                        sum++;
            }

            return sum;
        }

        private int ReceivedParcels(int customerId)
        {
            int sum = 0;
            foreach (var parcel in dal.ParcelList())
            {
                if (parcel.TargetId == customerId)
                    if (parcel.Delivered != null)
                        sum++;
            }

            return sum;
        }

        private int UnreceivedParcels(int customerId)
        {
            int sum = 0;
            foreach (var parcel in dal.ParcelList())
            {
                if (parcel.TargetId == customerId)
                    if (parcel.Delivered == null)
                        sum++;
            }

            return sum;
        }

        private CustomerInParcel GetCustomerInParcel(int CustomerId)
        {
            CustomerInParcel customer = new();
            IDAL.DO.Customer c = dal.GetCustomer(CustomerId);
            customer.Id = c.Id; customer.Name = c.Name;
            return customer;
        }

        private Location CustomerLocation(IDAL.DO.Customer customer)
        {
            Location location = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
            return location;
        }

        private IEnumerable<IDAL.DO.Customer> ReceivedCustomersList()
        {
            List<IDAL.DO.Customer> receivedCustomers = new();
            foreach (var Customer in dal.CustomerList())
            {
                if (dal.ParcelList().Any(x => x.TargetId == Customer.Id && x.Delivered != null))
                    receivedCustomers.Add(Customer);
            }
            return receivedCustomers;
        }
    }
}
