using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model;
using VehicleRentalSystem.Model.Service;
using Xunit;

namespace VehicleRentalSystem.Tests
{
    public class CustomerServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CustomerServiceTestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _customerService = new CustomerService(_context);
        }

        [Fact]
        public async Task AddCustomer_ShouldIncreaseCount()
        {
            var customer = new Customer
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                EmailAddress = "jan@example.com",
                PhoneNumber = "123456789"
            };

            await _customerService.AddCustomer(customer);

            var count = await _customerService.GetCustomerCount();
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task UpdateCustomer_ShouldModifyData()
        {
            var customer = new Customer
            {
                FirstName = "Anna",
                LastName = "Nowak",
                EmailAddress = "anna@example.com"
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customer.FirstName = "Anna Maria";
            var result = await _customerService.UpdateCustomer(customer);

            Assert.True(result);
            var updated = await _customerService.GetCustomerById(customer.Id);
            Assert.Equal("Anna Maria", updated!.FirstName);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldRemoveCustomer_WhenNoActiveReservations()
        {
            var customer = new Customer
            {
                FirstName = "Piotr",
                LastName = "Zieliński"
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var result = await _customerService.DeleteCustomer(customer.Id);

            Assert.True(result);
            Assert.Equal(0, await _customerService.GetCustomerCount());
        }

        [Fact]
        public async Task GetAllCustomers_ShouldReturnAll()
        {
            _context.Customers.Add(new Customer { FirstName = "Adam", LastName = "Test" });
            _context.Customers.Add(new Customer { FirstName = "Ewa", LastName = "Test" });
            await _context.SaveChangesAsync();

            var customers = await _customerService.GetAllCustomers();

            Assert.Equal(2, customers.Count);
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnCustomer()
        {
            var customer = new Customer { FirstName = "Karol", LastName = "Nowy" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var found = await _customerService.GetCustomerById(customer.Id);

            Assert.NotNull(found);
            Assert.Equal("Karol", found!.FirstName);
        }

        [Fact]
        public async Task GetActiveCustomerCount_ShouldReturnCorrectNumber()
        {
            var customer = new Customer { FirstName = "Active", LastName = "Client" };
            var reservation = new Reservation
            {
                Customer = customer,
                Status = ActualStatus.Potwierdzona,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2)
            };

            _context.Customers.Add(customer);
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            var count = await _customerService.GetActiveCustomerCount();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task GetCustomerDetails_ShouldIncludeReservationsAndVehicle()
        {
            var vehicle = new Vehicle { Brand = "Toyota", Model = "Corolla" };
            var customer = new Customer { FirstName = "Detail", LastName = "Test" };
            var reservation = new Reservation
            {
                Customer = customer,
                Vehicle = vehicle,
                Status = ActualStatus.Potwierdzona,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3)
            };

            _context.Vehicles.Add(vehicle);
            _context.Customers.Add(customer);
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            var details = await _customerService.GetCustomerDetails(customer.Id);

            Assert.NotNull(details);
            Assert.NotEmpty(details!.Reservations);
            Assert.NotNull(details.Reservations.First().Vehicle);
        }
    }
}
