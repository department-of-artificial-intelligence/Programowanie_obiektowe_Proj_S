using Microsoft.EntityFrameworkCore;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Extensions;
using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Services
{
    public class CustomerService(ApplicationDbContext context) : ICustomerService
    {
        private readonly ApplicationDbContext _context = context;

        public IEnumerable<Customer> GetCustomerByBranch(int branchId)
        {
            return _context.Customers
                .Include(c => c.Rentals)
                .Where(c => c.BranchId == branchId)
                .ToList();
        }

        public void AddCustomer(Customer customer, int branchId)
        {
            if (customer is null) throw new ArgumentNullException(nameof(customer));

            customer.ValidateCustomer();

            bool branchExists = _context.Branches.Any(b => b.Id == branchId);
            if (!branchExists) throw new InvalidOperationException("Oddział o podanym ID nie istnieje");

            customer.BranchId = branchId;
            customer.LoyaltyPoints = 0;

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer is null) throw new ArgumentNullException(nameof(customer));

            customer.ValidateCustomer();

            var existingCustomer = _context.Customers.FirstOrDefault(c => c.Id == customer.Id);
            if (existingCustomer is null) throw new InvalidOperationException("Klient nie istnieje");

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.LicenseNumber = customer.LicenseNumber;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;

            _context.SaveChanges();
        }

        public void RemoveCustomer(int customerId, int branchId)
        {
            var customer = _context.Customers
                .Include(c => c.Rentals)
                .FirstOrDefault(c => c.Id == customerId && c.BranchId == branchId);

            if (customer is null) throw new InvalidOperationException("Klient o takim ID nie istnieje w tym oddziale");

            if (customer.HasActiveRentals()) throw new InvalidOperationException("Nie można usunąć klienta posiadającego aktywne wypożyczenia");

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public IEnumerable<Customer> SearchCustomers(int branchId, string? lastName, string? licenseNumber, int? minPoints)
        {
            var query = _context.Customers
                .Include(c => c.Rentals)
                .Where(c => c.BranchId == branchId);

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(c => c.LastName.Contains(lastName));

            if (!string.IsNullOrWhiteSpace(licenseNumber))
                query = query.Where(c => c.LicenseNumber.Contains(licenseNumber));

            if (minPoints.HasValue)
                query = query.Where(c => c.LoyaltyPoints >= minPoints.Value);

            return query.ToList();
        }

    }
}
