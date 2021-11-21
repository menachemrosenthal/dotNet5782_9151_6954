﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        private CustomerInParcel getCustomerInParcel(int CustomerId)
        {
            CustomerInParcel customer = new();
            IDAL.DO.Customer c = dal.GetCustomer(CustomerId);
            customer.Id = c.Id; customer.Name = c.Name;
            return customer;
        }
        public Customer getCustomer(int CustomerId)
        {
            Customer customer = new();
            IDAL.DO.Customer c1 = dal.GetCustomer(CustomerId);
            customer.Id = c1.Id;customer.Name = c1.Name;customer.Phone = c1.Phone;
            customer.Location.Latitude = c1.Latitude; customer.Location.Longitude = c1.Longitude;
            List<IDAL.DO.Parcel> parcels = (List<IDAL.DO.Parcel>)dal.ParcelList();
            foreach (var par in dal.ParcelList())
            {
                if (par.Senderid == CustomerId)
                {
                    Parcel p1 = new();
                    p1 = getParcel(par.Id);
                    customer.Sended.Add(p1);
                }
            }
            foreach (var par in dal.ParcelList())
            {
                if (par.TargetId == CustomerId)
                {
                    Parcel p2 = new();
                    p2= getParcel(par.Id);
                    customer.Sended.Add(p2);
                }
            }
            return customer;
        }
        Location CustomerLocation(IDAL.DO.Customer customer)
        {
            Location location = new() { Longitude = customer.Longitude, Latitude = customer.Latitude };
            return location;
        }

        IEnumerable<IDAL.DO.Customer> ReceivedCustomersList()
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

        double CustomerClosestStationDistance(int customerId)
        {
            IDAL.DO.Customer customer = new();
            customer = dal.CustomerList().First(x => x.Id == customerId);
            return LocationsDistance(CustomerLocation(customer), StationLocation(ClosestStation(CustomerLocation(customer), dal.StationList())));
        }
    }
}
