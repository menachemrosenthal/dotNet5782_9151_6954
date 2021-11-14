using IDAL.DO;
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
        /// add a customer to the Customers array array
        /// </summary>
        /// <param name="customer">the customer for add</param>
        public void AddCustumer(Customer customer)
        {
            try
            {
                var exist = DataSource.Customers.Any(x => x.Id == customer.Id);
                if (exist)
                    throw new IDAL.AddExistException("Customer", customer.Id);
                DataSource.Customers.Add(customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

        }

        /// <summary>
        /// get customer
        /// </summary>
        /// <param name="customerId">customer id to return</param>
        /// <returns>customer object</returns>
        public Customer? GetCustomer(int customerId)
        {
            try
            {
                var exist = DataSource.Customers.Any(x => x.Id == customerId);
                if (!exist)
                    throw new IDAL.ItemNotFoundException("Customer", customerId);

                return DataSource.Customers.FirstOrDefault(x => x.Id == customerId);
            }
            catch (IDAL.ItemNotFoundException ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        /// <summary>
        /// get list of the Customers
        /// </summary>
        /// <returns>customer array</returns>
        public IEnumerable<Customer> CustomerList() => DataSource.Customers.ToList();
    }
}
