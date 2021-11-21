﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public partial class BL : IBL
    {
        public Customer getCustomer(int CustomerId)
        {
            Customer customer = new();
            // mimush...
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

        public IEnumerable<object> GetCustomerList()
        {
            List<CustomerToList> customers = new();
            foreach (var customer in dal.CustomerList())
            {
                CustomerToList customerToList = new();
                customerToList.Id = customer.Id;
                customerToList.Name = customer.Name;
                customerToList.Phone = customer.Phone;
                customerToList.ParcelsProvidedNum = ProvidedParcels(customer.Id);
                customerToList.ParcelsUnprovidedNum = UnProvidedParcels(customer.Id);
                customerToList.ReceivedParcelsNum = ReceivedParcels(customer.Id);
                customerToList.UnreceivedParcelsNum = UnreceivedParcels(customer.Id);
                customers.Add(customerToList);
            }

            return customers;
        }

        int ProvidedParcels(int customerId)
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

        int UnProvidedParcels(int customerId)
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

        int ReceivedParcels(int customerId)
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

        int UnreceivedParcels(int customerId)
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
    }
}
