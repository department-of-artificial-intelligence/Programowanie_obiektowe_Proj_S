using Microsoft.EntityFrameworkCore;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.Services;
using Xunit;

namespace WypozyczalniaSamochodow.Tests
{
    public class RaportServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly RaportService _service;
        private readonly Branch _testBranch;

        public RaportServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RaportServiceTestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _service = new RaportService(_context);

            _testBranch = new Branch { Name = "Test", City = "Warszawa", Address = "ul. Test 1", ContactNumber = "123456789" };
            _context.Branches.Add(_testBranch);
            _context.SaveChanges();
        }

        [Fact]
        public void GetTotalRevenue_ShouldReturnSumOfCompletedRentalsCost()
        {
            var car1 = new Car { Brand = "Toyota", Model = "Corolla", ProductionYear = 2020, Power = 120, EngineVolume = 1.6, AvgConsumption = 6.5, Gearbox = "manualna", FuelType = "benzyna", PricePerDay = 150, BranchId = _testBranch.Id };
            var car2 = new Car { Brand = "Honda", Model = "Civic", ProductionYear = 2021, Power = 140, EngineVolume = 1.8, AvgConsumption = 7.0, Gearbox = "automatyczna", FuelType = "benzyna", PricePerDay = 200, BranchId = _testBranch.Id };
            var customer1 = new Customer { FirstName = "Jan", LastName = "Kowalski", LicenseNumber = "ABC123", Email = "jan@test.pl", PhoneNumber = "123456789", BranchId = _testBranch.Id };
            var customer2 = new Customer { FirstName = "Anna", LastName = "Nowak", LicenseNumber = "XYZ789", Email = "anna@test.pl", PhoneNumber = "987654321", BranchId = _testBranch.Id };

            _context.Cars.AddRange(car1, car2);
            _context.Customers.AddRange(customer1, customer2);
            _context.SaveChanges();

            var rental1 = new Rental { CarId = car1.Id, CustomerId = customer1.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today.AddDays(-5), EndDate = DateTime.Today.AddDays(-3), Days = 2, Cost = 300, IsCompleted = true };
            var rental2 = new Rental { CarId = car2.Id, CustomerId = customer2.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today.AddDays(-10), EndDate = DateTime.Today.AddDays(-8), Days = 2, Cost = 400, IsCompleted = true };
            var rental3 = new Rental { CarId = car1.Id, CustomerId = customer1.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2), Days = 2, Cost = 300, IsCompleted = false };
            _context.Rentals.AddRange(rental1, rental2, rental3);
            _context.SaveChanges();

            var result = _service.GetTotalRevenue(_testBranch.Id);

            Assert.Equal(700, result);
        }

        [Fact]
        public void GetTotalRevenue_WithNoCompletedRentals_ShouldReturnZero()
        {
            var result = _service.GetTotalRevenue(_testBranch.Id);

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetMostRentedCar_ShouldReturnCarWithMostRentals()
        {
            var car1 = new Car { Brand = "Toyota", Model = "Corolla", ProductionYear = 2020, Power = 120, EngineVolume = 1.6, AvgConsumption = 6.5, Gearbox = "manualna", FuelType = "benzyna", PricePerDay = 150, BranchId = _testBranch.Id };
            var car2 = new Car { Brand = "Honda", Model = "Civic", ProductionYear = 2021, Power = 140, EngineVolume = 1.8, AvgConsumption = 7.0, Gearbox = "automatyczna", FuelType = "benzyna", PricePerDay = 200, BranchId = _testBranch.Id };
            var customer1 = new Customer { FirstName = "Jan", LastName = "Kowalski", LicenseNumber = "ABC123", Email = "jan@test.pl", PhoneNumber = "123456789", BranchId = _testBranch.Id };
            var customer2 = new Customer { FirstName = "Anna", LastName = "Nowak", LicenseNumber = "XYZ789", Email = "anna@test.pl", PhoneNumber = "987654321", BranchId = _testBranch.Id };

            _context.Cars.AddRange(car1, car2);
            _context.Customers.AddRange(customer1, customer2);
            _context.SaveChanges();

            var rental1 = new Rental { CarId = car1.Id, CustomerId = customer1.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today.AddDays(-5), EndDate = DateTime.Today.AddDays(-3), Days = 2, Cost = 300, IsCompleted = true, Car = car1 };
            var rental2 = new Rental { CarId = car1.Id, CustomerId = customer2.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today.AddDays(-10), EndDate = DateTime.Today.AddDays(-8), Days = 2, Cost = 300, IsCompleted = true, Car = car1 };
            var rental3 = new Rental { CarId = car2.Id, CustomerId = customer1.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today.AddDays(-15), EndDate = DateTime.Today.AddDays(-13), Days = 2, Cost = 400, IsCompleted = true, Car = car2 };
            _context.Rentals.AddRange(rental1, rental2, rental3);
            _context.SaveChanges();

            var result = _service.GetMostRentedCar(_testBranch.Id);

            Assert.NotNull(result);
            Assert.Equal(car1.Id, result.Id);
            Assert.Equal("Toyota", result.Brand);
        }

        [Fact]
        public void GetMostRentedCar_WithNoRentals_ShouldReturnNull()
        {
            var result = _service.GetMostRentedCar(_testBranch.Id);

            Assert.Null(result);
        }

        [Fact]
        public void GetAverageDailyRevenue_ShouldCalculateCorrectly()
        {
            var car1 = new Car { Brand = "Toyota", Model = "Corolla", ProductionYear = 2020, Power = 120, EngineVolume = 1.6, AvgConsumption = 6.5, Gearbox = "manualna", FuelType = "benzyna", PricePerDay = 150, BranchId = _testBranch.Id };
            var car2 = new Car { Brand = "Honda", Model = "Civic", ProductionYear = 2021, Power = 140, EngineVolume = 1.8, AvgConsumption = 7.0, Gearbox = "automatyczna", FuelType = "benzyna", PricePerDay = 200, BranchId = _testBranch.Id };
            var customer1 = new Customer { FirstName = "Jan", LastName = "Kowalski", LicenseNumber = "ABC123", Email = "jan@test.pl", PhoneNumber = "123456789", BranchId = _testBranch.Id };
            var customer2 = new Customer { FirstName = "Anna", LastName = "Nowak", LicenseNumber = "XYZ789", Email = "anna@test.pl", PhoneNumber = "987654321", BranchId = _testBranch.Id };

            _context.Cars.AddRange(car1, car2);
            _context.Customers.AddRange(customer1, customer2);
            _context.SaveChanges();

            var rental1 = new Rental { CarId = car1.Id, CustomerId = customer1.Id, BranchId = _testBranch.Id, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 3), Days = 2, Cost = 300, IsCompleted = true };
            var rental2 = new Rental { CarId = car2.Id, CustomerId = customer2.Id, BranchId = _testBranch.Id, StartDate = new DateTime(2024, 1, 5), EndDate = new DateTime(2024, 1, 7), Days = 2, Cost = 400, IsCompleted = true };
            _context.Rentals.AddRange(rental1, rental2);
            _context.SaveChanges();

            var result = _service.GetAverageDailyRevenue(_testBranch.Id);

            Assert.Equal(100, result);
        }

        [Fact]
        public void GetAverageDailyRevenue_WithNoRentals_ShouldReturnZero()
        {
            var result = _service.GetAverageDailyRevenue(_testBranch.Id);

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetBestCustomer_ShouldReturnCustomerWithHighestTotalCost()
        {
            var car1 = new Car { Brand = "Toyota", Model = "Corolla", ProductionYear = 2020, Power = 120, EngineVolume = 1.6, AvgConsumption = 6.5, Gearbox = "manualna", FuelType = "benzyna", PricePerDay = 150, BranchId = _testBranch.Id };
            var car2 = new Car { Brand = "Honda", Model = "Civic", ProductionYear = 2021, Power = 140, EngineVolume = 1.8, AvgConsumption = 7.0, Gearbox = "automatyczna", FuelType = "benzyna", PricePerDay = 200, BranchId = _testBranch.Id };
            var customer1 = new Customer { FirstName = "Jan", LastName = "Kowalski", LicenseNumber = "ABC123", Email = "jan@test.pl", PhoneNumber = "123456789", BranchId = _testBranch.Id };
            var customer2 = new Customer { FirstName = "Anna", LastName = "Nowak", LicenseNumber = "XYZ789", Email = "anna@test.pl", PhoneNumber = "987654321", BranchId = _testBranch.Id };

            _context.Cars.AddRange(car1, car2);
            _context.Customers.AddRange(customer1, customer2);
            _context.SaveChanges();

            var rental1 = new Rental { CarId = car1.Id, CustomerId = customer1.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today.AddDays(-5), EndDate = DateTime.Today.AddDays(-3), Days = 2, Cost = 300, IsCompleted = true, Customer = customer1 };
            var rental2 = new Rental { CarId = car1.Id, CustomerId = customer1.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today.AddDays(-10), EndDate = DateTime.Today.AddDays(-8), Days = 2, Cost = 300, IsCompleted = true, Customer = customer1 };
            var rental3 = new Rental { CarId = car2.Id, CustomerId = customer2.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today.AddDays(-15), EndDate = DateTime.Today.AddDays(-13), Days = 2, Cost = 400, IsCompleted = true, Customer = customer2 };
            _context.Rentals.AddRange(rental1, rental2, rental3);
            _context.SaveChanges();

            var result = _service.GetBestCustomer(_testBranch.Id);

            Assert.NotNull(result);
            Assert.Equal(customer1.Id, result.Id);
            Assert.Equal("Jan", result.FirstName);
        }

        [Fact]
        public void GetBestCustomer_WithNoRentals_ShouldReturnNull()
        {
            var result = _service.GetBestCustomer(_testBranch.Id);

            Assert.Null(result);
        }
    }
}