using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model;
using VehicleRentalSystem.Model.Extensions;

namespace VehicleRentalSystem.Model.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCustomer(Customer customer)
        {
            Console.WriteLine("\n[INFO] Dodawanie klienta...");

            try
            {
                if (await _context.Customers.AnyAsync(c => c.DriverLicenseNumber == customer.DriverLicenseNumber))
                {
                    throw new ArgumentException("\n[BŁĄD] Taki numer prawo jazdy jest już do kogoś przypisany!");
                }

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                Console.WriteLine($"\n[INFO] Pomyślnie dodano klienta {customer.FirstName} {customer.LastName}!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.Id);
            if (existingCustomer == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono takiego klienta!");
                return false;
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.EmailAddress = customer.EmailAddress;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.DriverLicenseNumber = customer.DriverLicenseNumber;
            existingCustomer.DriverLicenseExpiration = customer.DriverLicenseExpiration;

            await _context.SaveChangesAsync();
            Console.WriteLine($"\n[INFO] Klient [{existingCustomer.Id}] został zaktualizowany.");
            return true;
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Reservations)
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
            {
                Console.WriteLine("\n[BŁĄD] Klient o podanym ID nie został znaleziony!");
                return false;
            }

            if (customer.HasActiveReservations())
            {
                Console.WriteLine("\n[BŁĄD] Nie można usunąć klienta z aktywnymi rezerwacjami!");
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            Console.WriteLine($"\n[INFO] Klient [{customer.Id}] {customer.GetFullName()} został pomyślnie usunięty!");
            return true;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers
                .Include(c => c.Reservations)
                .ToListAsync();
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await _context.Customers
                .Include(r => r.Reservations)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> GetCustomerCount()
        {
            return await _context.Customers.CountAsync();
        }

        public async Task<int> GetActiveCustomerCount()
        {
            return (await _context.Customers
            .Include(c => c.Reservations)
            .ToListAsync())
            .Count(c => c.Reservations.Any(r => r.IsActive()));
        }

        public async Task<Customer?> GetCustomerDetails(int customerId)
        {
            return await _context.Customers
                .Include(c => c.Reservations)
                .ThenInclude(r => r.Vehicle)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }
    }
}