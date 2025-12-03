using CarsRental.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Logic
{
    public class CustomerService : ICustomerService
    {
        private List<Customer> _customers = new List<Customer>();
        private int _customerCounter = 0;

        public void AddCustomer(Customer customer)
        {
            Console.WriteLine("Dodawanie klienta...");

            if (customer == null)
            {
                Console.WriteLine("Nie można dodać pustych danych!\n");
                return;
            }

            _customerCounter++;
            customer.Id = _customerCounter;
            _customers.Add(customer);

            Console.WriteLine($"Dodano {customer.FirstName} {customer.LastName}\n");
        }

        public void UpdateCustomer(Customer customer)
        {
            Console.WriteLine("Aktualizowanie danych klienta...");
            if (customer == null)
            {
                Console.WriteLine("Pola nie mogą być puste!");
                return;
            }

            var existingCustomer = GetCustomer(customer.Id);
            if (existingCustomer == null)
            {
                Console.WriteLine("Nie znaleziono takiego klienta");
                return;
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.EmailAddress = customer.EmailAddress;
            existingCustomer.PhoneNumber = customer.PhoneNumber;

            Console.WriteLine($"Zaktualizowano klienta [{customer.Id}] {customer.FirstName} {customer.LastName}");
        }

        public void RemoveCustomer(int id)
        {
            Console.WriteLine("Usuwanie pojazdu...");

            Customer? customerToRemove = GetCustomer(id);
            if (customerToRemove != null)
            {
                _customers.Remove(customerToRemove);
                Console.WriteLine($"Usunięto [{customerToRemove.Id}] {customerToRemove.FirstName} {customerToRemove.LastName} z wypożyczalni\n");
            }
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers;
        }

        public Customer? GetCustomer(int id)
        {
            var cus = _customers.Find(c => c.Id == id);
            if (cus == null)
            {
                Console.WriteLine($"Nie znaleziono klienta ID({id})\n");
                return null;
            }
            return cus;
        }

        public int GetCustomersCount()
        {
            return _customers.Count;
        }
    }
}
