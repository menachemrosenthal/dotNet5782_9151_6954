using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public Location CusromerLocation(IDAL.DO.Customer customer)
        {
            Location location = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
            return location;
        }

        public IEnumerable<IDAL.DO.Customer> ReceivedCustomersList()
        {
            List<IDAL.DO.Customer> receivedCustomers = new();
            foreach (var Customer in dal.CustomerList())
            {
                if (dal.ParcelList().Any(x => x.TargetId == Customer.Id && x.Delivered != null))
                    receivedCustomers.Add(Customer);
            }
            return receivedCustomers;
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

        public void CustomerUpdate(Customer customer)
        {
            IDAL.DO.Customer dalCustomer = new();
            dalCustomer = dal.CustomerList().First(x => x.Id == customer.Id);

            if (customer.Name != "")
                dalCustomer.Name = customer.Name;
            if (customer.Phone != "")
                dalCustomer.Phone = customer.Phone;
            dal.CustomerUpdate(dalCustomer);
        }
    }
}
