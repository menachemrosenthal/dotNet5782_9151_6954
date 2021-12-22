using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DalApi
{
    internal partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// add a customer to the Customers array array
        /// </summary>
        /// <param name="customer">the customer for add</param>
        public void AddCustumer(Customer customer)
        {
            var exist = DataSource.Customers.Any(x => x.Id == customer.Id);
            if (exist)
                throw new DalApi.AddExistException("Customer", customer.Id);

            DataSource.Customers.Add(customer);
        }

        /// <summary>
        /// get customer
        /// </summary>
        /// <param name="customerId">customer id to return</param>
        /// <returns>customer object</returns>
        public Customer GetCustomer(int customerId)
        {
            var exist = DataSource.Customers.Any(x => x.Id == customerId);
            if (!exist)
                throw new DalApi.ItemNotFoundException("Customer", customerId);

            return DataSource.Customers.FirstOrDefault(x => x.Id == customerId);

        }

        /// <summary>
        /// get list of the Customers
        /// </summary>
        /// <returns>customer array</returns>
        public IEnumerable<Customer> CustomerList() => DataSource.Customers.ToList();

        /// <summary>
        /// update customer priority
        /// </summary>
        /// <param name="customer">customer for update</param>
        public void CustomerUpdate(Customer customer)
        {
            Customer tmpCustomer= GetCustomer(customer.Id);
            int index = DataSource.Customers.IndexOf(tmpCustomer);
            DataSource.Customers[index] = customer;
        }

        public IEnumerable<Customer> GetCustomersByCondition(Predicate<Customer> condition)
            => DataSource.Customers.Where(x => condition(x));
    }
}
