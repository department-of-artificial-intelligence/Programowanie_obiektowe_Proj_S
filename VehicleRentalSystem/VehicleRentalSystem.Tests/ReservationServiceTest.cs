using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model;
using VehicleRentalSystem.Model.Service;
using Xunit;

namespace VehicleRentalSystem.Tests
{
    public class ReservationServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ReservationService _reservationService;
        private readonly VehicleService _vehicleService;

        public ReservationServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ReservationServiceTestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _vehicleService = new VehicleService(_context);
            _reservationService = new ReservationService(_context, _vehicleService);
        }

        [Fact]
        public async Task CreateReservation_ShouldAddReservation()
        {
            var customer = new Customer
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                DriverLicenseNumber = "ABC123",
                DriverLicenseExpiration = DateTime.Now.AddYears(1)
            };
            var department = new Department { Name = "Warszawa", City = "Warszawa", StreetAddress = "Marszałkowska 1" };
            var vehicle = new Vehicle
            {
                Brand = "Toyota",
                Model = "Corolla",
                PriceForDay = 100,
                Department = department,
                IsRented = false
            };

            _context.Customers.Add(customer);
            _context.Departments.Add(department);
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(5);

            var reservation = await _reservationService.CreateReservation(customer.Id, vehicle.Id, startDate, endDate);

            Assert.NotNull(reservation);
            Assert.Equal(1, await _reservationService.GetReservationCount());
        }

        [Fact]
        public async Task CompleteReservation_ShouldChangeStatusAndFreeVehicle()
        {
            var customer = new Customer { FirstName = "Anna", LastName = "Nowak", DriverLicenseNumber = "XYZ123", DriverLicenseExpiration = DateTime.Now.AddYears(1) };
            var department = new Department { Name = "Kraków", City = "Kraków", StreetAddress = "Długa 5" };
            var vehicle = new Vehicle { Brand = "Ford", Model = "Focus", PriceForDay = 120, Department = department, IsRented = false };

            _context.Customers.Add(customer);
            _context.Departments.Add(department);
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var reservation = await _reservationService.CreateReservation(customer.Id, vehicle.Id, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));

            var result = await _reservationService.CompleteReservation(reservation!.Id);

            Assert.True(result);
            var updated = await _reservationService.GetReservation(reservation.Id);
            Assert.Equal(ActualStatus.Zakończona, updated!.Status);
            Assert.False(updated.Vehicle!.IsRented);
        }

        [Fact]
        public async Task CancelReservation_ShouldChangeStatusAndFreeVehicle()
        {
            var customer = new Customer { FirstName = "Piotr", LastName = "Zieliński", DriverLicenseNumber = "LMN456", DriverLicenseExpiration = DateTime.Now.AddYears(1) };
            var department = new Department { Name = "Łódź", City = "Łódź", StreetAddress = "Piotrkowska 100" };
            var vehicle = new Vehicle { Brand = "BMW", Model = "X3", PriceForDay = 200, Department = department, IsRented = false };

            _context.Customers.Add(customer);
            _context.Departments.Add(department);
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var reservation = await _reservationService.CreateReservation(customer.Id, vehicle.Id, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

            var result = await _reservationService.CancelReservation(reservation!.Id);

            Assert.True(result);
            var updated = await _reservationService.GetReservation(reservation.Id);
            Assert.Equal(ActualStatus.Anulowana, updated!.Status);
            Assert.False(updated.Vehicle!.IsRented);
        }

        [Fact]
        public async Task CalculateCost_ShouldReturnDiscountedPrice()
        {
            var customer = new Customer { FirstName = "Karol", LastName = "Test", DriverLicenseNumber = "DDD111", DriverLicenseExpiration = DateTime.Now.AddYears(1) };
            var department = new Department { Name = "Gdańsk", City = "Gdańsk", StreetAddress = "Długa 50" };
            var vehicle = new Vehicle { Brand = "Audi", Model = "A4", PriceForDay = 150, Department = department, IsRented = false };

            _context.Customers.Add(customer);
            _context.Departments.Add(department);
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var reservation = await _reservationService.CreateReservation(customer.Id, vehicle.Id, DateTime.Now.AddDays(1), DateTime.Now.AddDays(10));

            var cost = await _reservationService.CalculateCost(reservation!.Id);

            var expected = 9 * (vehicle.PriceForDay * 0.85);
            Assert.Equal(expected, cost);
        }

        [Fact]
        public async Task GetReservationCounts_ShouldReturnCorrectNumbers()
        {
            var customer = new Customer { FirstName = "Test", LastName = "Client", DriverLicenseNumber = "AAA999", DriverLicenseExpiration = DateTime.Now.AddYears(1) };
            var department = new Department { Name = "Poznań", City = "Poznań", StreetAddress = "Święty Marcin 20" };
            var vehicle = new Vehicle { Brand = "Opel", Model = "Astra", PriceForDay = 80, Department = department, IsRented = false };

            _context.Customers.Add(customer);
            _context.Departments.Add(department);
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var res1 = await _reservationService.CreateReservation(customer.Id, vehicle.Id, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            await _reservationService.CancelReservation(res1!.Id);

            var res2 = await _reservationService.CreateReservation(customer.Id, vehicle.Id, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));
            await _reservationService.CompleteReservation(res2!.Id);

            Assert.Equal(2, await _reservationService.GetReservationCount());
            Assert.Equal(0, await _reservationService.GetActiveReservationCount());
            Assert.Equal(1, await _reservationService.GetCompletedReservationCount());
            Assert.Equal(1, await _reservationService.GetCanceledReservationCount());
        }
    }
}
