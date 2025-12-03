using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsRental.Model;

namespace CarsRental.Logic
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void RemoveCustomer(int CustomerId);
        Customer? GetCustomer(int CustomerId);
        List<Customer> GetAllCustomers();
        int GetCustomersCount();
    }
}
