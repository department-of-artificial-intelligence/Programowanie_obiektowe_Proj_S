using Microsoft.EntityFrameworkCore;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.Services;
using Xunit;

namespace WypozyczalniaSamochodow.Tests
{
    public class CustomerServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerService _service;
        private readonly Branch _testBranch;

        public CustomerServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CustomerServiceTestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _service = new CustomerService(_context);

            _testBranch = new Branch { Name = "Test", City = "Warszawa", Address = "ul. Test 1", ContactNumber = "123456789" };
            _context.Branches.Add(_testBranch);
            _context.SaveChanges();
        }

        [Fact]
        public void GetCustomerByBranch_ShouldReturnCustomersForBranch()
        {
            var customer1 = new Customer { FirstName = "Jan", LastName = "Kowalski", LicenseNumber = "ABC123", Email = "jan@test.pl", PhoneNumber = "123456789", BranchId = _testBranch.Id };
            var customer2 = new Customer { FirstName = "Anna", LastName = "Nowak", LicenseNumber = "XYZ789", Email = "anna@test.pl", PhoneNumber = "987654321", BranchId = _testBranch.Id };
            _context.Customers.AddRange(customer1, customer2);
            _context.SaveChanges();

            var result = _service.GetCustomerByBranch(_testBranch.Id).ToList();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.FirstName == "Jan");
            Assert.Contains(result, c => c.FirstName == "Anna");
        }

        [Fact]
        public void AddCustomer_WithValidData_ShouldAddCustomer()
        {
            var customer = new Customer
            {
                FirstName = "Piotr",
                LastName = "Wiśniewski",
                LicenseNumber = "DEF456",
                Email = "piotr@test.pl",
                PhoneNumber = "555666777"
            };

            _service.AddCustomer(customer, _testBranch.Id);

            var result = _context.Customers.FirstOrDefault(c => c.FirstName == "Piotr");
            Assert.NotNull(result);
            Assert.Equal(0, result.LoyaltyPoints);
            Assert.Equal(_testBranch.Id, result.BranchId);
        }

        [Fact]
        public void AddCustomer_WithNullCustomer_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.AddCustomer(null!, _testBranch.Id));
        }

        [Fact]
        public void AddCustomer_WithInvalidEmail_ShouldThrowException()
        {
            var customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test",
                LicenseNumber = "TEST123",
                Email = "invalidemail",
                PhoneNumber = "123456789"
            };

            var exception = Assert.Throws<ArgumentException>(() => _service.AddCustomer(customer, _testBranch.Id));
            Assert.Equal("Nieprawidłowy adres email", exception.Message);
        }

        [Fact]
        public void AddCustomer_WithNonExistentBranch_ShouldThrowException()
        {
            var customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test",
                LicenseNumber = "TEST123",
                Email = "test@test.pl",
                PhoneNumber = "123456789"
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _service.AddCustomer(customer, 999));
            Assert.Equal("Oddział o podanym ID nie istnieje", exception.Message);
        }

        [Fact]
        public void UpdateCustomer_WithValidData_ShouldUpdateCustomer()
        {
            var customer = new Customer
            {
                FirstName = "Stary",
                LastName = "Klient",
                LicenseNumber = "OLD123",
                Email = "old@test.pl",
                PhoneNumber = "111222333",
                BranchId = _testBranch.Id
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customer.FirstName = "Nowy";
            customer.Email = "new@test.pl";

            _service.UpdateCustomer(customer);

            var result = _context.Customers.Find(customer.Id);
            Assert.NotNull(result);
            Assert.Equal("Nowy", result.FirstName);
            Assert.Equal("new@test.pl", result.Email);
        }

        [Fact]
        public void UpdateCustomer_WithNonExistentCustomer_ShouldThrowException()
        {
            var customer = new Customer
            {
                Id = 999,
                FirstName = "Test",
                LastName = "Test",
                LicenseNumber = "TEST123",
                Email = "test@test.pl",
                PhoneNumber = "123456789"
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _service.UpdateCustomer(customer));
            Assert.Equal("Klient nie istnieje", exception.Message);
        }

        [Fact]
        public void RemoveCustomer_WithoutActiveRentals_ShouldRemoveCustomer()
        {
            var customer = new Customer
            {
                FirstName = "Do usunięcia",
                LastName = "Klient",
                LicenseNumber = "DEL123",
                Email = "del@test.pl",
                PhoneNumber = "999888777",
                BranchId = _testBranch.Id
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            var customerId = customer.Id;

            _service.RemoveCustomer(customerId, _testBranch.Id);

            var result = _context.Customers.Find(customerId);
            Assert.Null(result);
        }

        [Fact]
        public void RemoveCustomer_WithActiveRentals_ShouldThrowException()
        {
            var customer = new Customer
            {
                FirstName = "Z wypożyczeniem",
                LastName = "Klient",
                LicenseNumber = "RENT123",
                Email = "rent@test.pl",
                PhoneNumber = "777888999",
                BranchId = _testBranch.Id
            };
            var rental = new Rental
            {
                CustomerId = customer.Id,
                BranchId = _testBranch.Id,
                IsCompleted = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Days = 1,
                Cost = 100
            };
            customer.Rentals.Add(rental);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            var exception = Assert.Throws<InvalidOperationException>(() => _service.RemoveCustomer(customer.Id, _testBranch.Id));

            Assert.Equal("Nie można usunąć klienta posiadającego aktywne wypożyczenia", exception.Message);
        }

        [Fact]
        public void SearchCustomers_WithLastName_ShouldReturnMatchingCustomers()
        {
            var customer1 = new Customer { FirstName = "Jan", LastName = "Kowalski", LicenseNumber = "ABC123", Email = "jan@test.pl", PhoneNumber = "123456789", BranchId = _testBranch.Id };
            var customer2 = new Customer { FirstName = "Anna", LastName = "Nowak", LicenseNumber = "XYZ789", Email = "anna@test.pl", PhoneNumber = "987654321", BranchId = _testBranch.Id };
            _context.Customers.AddRange(customer1, customer2);
            _context.SaveChanges();

            var result = _service.SearchCustomers(_testBranch.Id, "Kowalski", null, null).ToList();

            Assert.Single(result);
            Assert.Equal("Jan", result[0].FirstName);
        }

        [Fact]
        public void SearchCustomers_WithMinPoints_ShouldReturnMatchingCustomers()
        {
            var customer1 = new Customer { FirstName = "Jan", LastName = "Kowalski", LicenseNumber = "ABC123", Email = "jan@test.pl", PhoneNumber = "123456789", LoyaltyPoints = 50, BranchId = _testBranch.Id };
            var customer2 = new Customer { FirstName = "Anna", LastName = "Nowak", LicenseNumber = "XYZ789", Email = "anna@test.pl", PhoneNumber = "987654321", LoyaltyPoints = 10, BranchId = _testBranch.Id };
            _context.Customers.AddRange(customer1, customer2);
            _context.SaveChanges();

            var result = _service.SearchCustomers(_testBranch.Id, null, null, 30).ToList();

            Assert.Single(result);
            Assert.Equal("Jan", result[0].FirstName);
        }
    }
}