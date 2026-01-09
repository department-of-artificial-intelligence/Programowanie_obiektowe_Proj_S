using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model.Service
{
    public interface ICustomerService
    {
        Task AddCustomer(Customer customer);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int customerId);
        Task<Customer?> GetCustomerById(int customerId);
        Task<List<Customer>> GetAllCustomers();
        Task<int> GetCustomerCount();
        Task<int> GetActiveCustomerCount();
        Task<Customer?> GetCustomerDetails(int customerId);
    }
}

