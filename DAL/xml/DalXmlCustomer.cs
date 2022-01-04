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
        public void AddCustumer(Customer customer)
        {
            customersRoot.Add(createCustomer(customer));
        }

        public XElement createCustomer(Customer customer)
        {
            XElement id = new XElement("Id", customer.Id);
            XElement name = new XElement("Name", customer.Name);
            XElement phone = new XElement("Phone", customer.Name);
            XElement longitude = new XElement("Longitude", customer.Longitude);
            XElement latitude = new XElement("Latitude", customer.Latitude);
            XElement xeCustomer = new XElement("student", id, name, phone, longitude, latitude);

            return xeCustomer;
        }

        public Customer GetCustomer(int customerId)
        {
            try
            {
                return
                (from c in customersRoot.Elements()
                 where int.Parse(c.Element("Id").Value) == customerId
                 select new Customer()
                 {
                     Id = int.Parse(c.Element("Id").Value),
                     Name = c.Element("Name").Value,
                     Phone = c.Element("Phone").Value,
                     Longitude = double.Parse(c.Element("Longitude").Value),
                     Latitude = double.Parse(c.Element("Latitude").Value)

                 }).FirstOrDefault();
            }
            catch { return default; }
        }

        public IEnumerable<Customer> CustomerList()
        {
            return from customer in customersRoot.Elements()
                   select new Customer()
                   {
                       Name = customer.Element("Name").Value,
                       Id = int.Parse(customer.Element("Id").Value),
                       Phone = customer.Element("Phone").Value,
                       Latitude = double.Parse(customer.Element("Latitude").Value),
                       Longitude = double.Parse(customer.Element("Longitude").Value)
                   };
        }

        public void CustomerUpdate(Customer customer)
        {

            XElement e = (from c in customersRoot.Elements()
                          where int.Parse(c.Element("Id").Value) == customer.Id
                          select c).FirstOrDefault();
            if (e != null)
            {

                e.Element("Id").Value = customer.Id.ToString();
                e.Element("Name").Value = customer.Name.ToString();
                e.Element("Phone").Value = customer.Phone.ToString();
                e.Element("Latitude").Value = customer.Latitude.ToString();
                e.Element("Longitude").Value = customer.Longitude.ToString();
                customersRoot.Save(CustomerPath);
            }

        }

        public IEnumerable<Customer> GetCustomersByCondition(Predicate<Customer> condition)
        {
            List<Customer> customers = XMLTools.LoadListFromXMLSerializer<Customer>(CustomerPath);

            return from customer in customers
                   where condition(customer)
                   select customer;
        }
    }
}
