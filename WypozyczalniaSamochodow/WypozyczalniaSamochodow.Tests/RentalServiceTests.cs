using Microsoft.EntityFrameworkCore;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.Services;
using Xunit;

namespace WypozyczalniaSamochodow.Tests
{
    public class RentalServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly RentalService _service;
        private readonly Branch _testBranch;
        private readonly Car _testCar;
        private readonly Customer _testCustomer;

        public RentalServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RentalServiceTestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _service = new RentalService(_context);

            _testBranch = new Branch { Name = "Test", City = "Warszawa", Address = "ul. Test 1", ContactNumber = "123456789" };
            _context.Branches.Add(_testBranch);
            _context.SaveChanges();

            _testCar = new Car
            {
                Brand = "Toyota",
                Model = "Corolla",
                ProductionYear = 2020,
                Power = 120,
                EngineVolume = 1.6,
                AvgConsumption = 6.5,
                Gearbox = "manualna",
                FuelType = "benzyna",
                PricePerDay = 150,
                BranchId = _testBranch.Id,
                IsAvailable = true
            };
            _context.Cars.Add(_testCar);
            _context.SaveChanges();

            _testCustomer = new Customer
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                LicenseNumber = "ABC123",
                Email = "jan@test.pl",
                PhoneNumber = "123456789",
                BranchId = _testBranch.Id,
                LoyaltyPoints = 50
            };
            _context.Customers.Add(_testCustomer);
            _context.SaveChanges();
        }

        [Fact]
        public void GetActiveRentals_ShouldReturnOnlyActiveRentals()
        {
            var rental1 = new Rental { CarId = _testCar.Id, CustomerId = _testCustomer.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2), Days = 2, Cost = 300, IsCompleted = false };
            var rental2 = new Rental { CarId = _testCar.Id, CustomerId = _testCustomer.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today.AddDays(-5), EndDate = DateTime.Today.AddDays(-3), Days = 2, Cost = 300, IsCompleted = true };
            _context.Rentals.AddRange(rental1, rental2);
            _context.SaveChanges();

            var result = _service.GetActiveRentals(_testBranch.Id).ToList();

            Assert.Single(result);
            Assert.False(result[0].IsCompleted);
        }

        [Fact]
        public void GetCompletedRentals_ShouldReturnOnlyCompletedRentals()
        {
            var rental1 = new Rental { CarId = _testCar.Id, CustomerId = _testCustomer.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2), Days = 2, Cost = 300, IsCompleted = false };
            var rental2 = new Rental { CarId = _testCar.Id, CustomerId = _testCustomer.Id, BranchId = _testBranch.Id, StartDate = DateTime.Today.AddDays(-5), EndDate = DateTime.Today.AddDays(-3), Days = 2, Cost = 300, IsCompleted = true, CompletedDate = DateTime.Now };
            _context.Rentals.AddRange(rental1, rental2);
            _context.SaveChanges();

            var result = _service.GetCompletedRentals(_testBranch.Id).ToList();

            Assert.Single(result);
            Assert.True(result[0].IsCompleted);
        }

        [Fact]
        public void RentCar_WithValidData_ShouldCreateRental()
        {
            var rental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
                Days = 2,
                Cost = 300
            };

            _service.RentCar(rental);

            var result = _context.Rentals.FirstOrDefault(r => r.CarId == _testCar.Id);
            Assert.NotNull(result);
            Assert.Equal(2, result.Days);
            Assert.Equal(300, result.Cost);
        }

        [Fact]
        public void RentCar_WithNonExistentCar_ShouldThrowException()
        {
            var rental = new Rental
            {
                CarId = 999,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
                Days = 2,
                Cost = 300
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _service.RentCar(rental));
            Assert.Equal("Samochód nie istnieje w tym oddziale", exception.Message);
        }

        [Fact]
        public void RentCar_WithNonExistentCustomer_ShouldThrowException()
        {
            var rental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = 999,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
                Days = 2,
                Cost = 300
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _service.RentCar(rental));
            Assert.Equal("Klient nie istnieje w tym oddziale", exception.Message);
        }

        [Fact]
        public void RentCar_WithOverlappingDates_ShouldThrowException()
        {
            var existingRental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
                Days = 2,
                Cost = 300,
                IsCompleted = false
            };
            _context.Rentals.Add(existingRental);
            _context.SaveChanges();

            _context.Entry(_testCar).Collection(c => c.Rentals).Load(); // odświeżenie samochodu z bazy

            var newRental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today.AddDays(2),
                EndDate = DateTime.Today.AddDays(4),
                Days = 2,
                Cost = 300
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _service.RentCar(newRental));
            Assert.Equal("Samochód jest już zarezerwowany w tym okresie", exception.Message);
        }

        [Fact]
        public void RentCar_StartingToday_ShouldSetCarAsUnavailable()
        {
            var rental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                Days = 2,
                Cost = 300
            };

            _service.RentCar(rental);

            _context.Entry(_testCar).Reload();
            Assert.False(_testCar.IsAvailable);
        }

        [Fact]
        public void RentCar_StartingInFuture_ShouldKeepCarAvailable()
        {
            var rental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today.AddDays(5),
                EndDate = DateTime.Today.AddDays(7),
                Days = 2,
                Cost = 300
            };

            _service.RentCar(rental);

            _context.Entry(_testCar).Reload();
            Assert.True(_testCar.IsAvailable);
        }

        [Fact]
        public void ReturnCar_WithValidRental_ShouldCompleteRental()
        {
            var rental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today.AddDays(-2),
                EndDate = DateTime.Today.AddDays(1),
                Days = 3,
                Cost = 450,
                IsCompleted = false
            };
            _context.Rentals.Add(rental);
            _context.SaveChanges();

            var initialPoints = _testCustomer.LoyaltyPoints;

            _service.ReturnCar(rental.Id, _testBranch.Id);

            _context.Entry(rental).Reload();
            Assert.True(rental.IsCompleted);
            Assert.NotNull(rental.CompletedDate);
            Assert.False(rental.IsCancelledBeforeStart);

            _context.Entry(_testCustomer).Reload();
            Assert.Equal(initialPoints + 3, _testCustomer.LoyaltyPoints);
        }

        [Fact]
        public void ReturnCar_BeforeStartDate_ShouldCancelWithZeroCost()
        {
            var rental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today.AddDays(5),
                EndDate = DateTime.Today.AddDays(7),
                Days = 2,
                Cost = 300,
                IsCompleted = false
            };
            _context.Rentals.Add(rental);
            _context.SaveChanges();

            _service.ReturnCar(rental.Id, _testBranch.Id);

            _context.Entry(rental).Reload();
            Assert.True(rental.IsCompleted);
            Assert.True(rental.IsCancelledBeforeStart);
            Assert.Equal(0, rental.Cost);
            Assert.NotNull(rental.CompletedDate);
        }

        [Fact]
        public void ReturnCar_WithNonExistentRental_ShouldThrowException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _service.ReturnCar(999, _testBranch.Id));
            Assert.Equal("Brak aktywnego wypożyczenia o podanym ID", exception.Message);
        }

        [Fact]
        public void RentCarWithPoints_WithEnoughPoints_ShouldCreateRental()
        {
            var rental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Days = 1,
                Cost = 0
            };
            var initialPoints = _testCustomer.LoyaltyPoints;

            _service.RentCarWithPoints(rental);

            var result = _context.Rentals.FirstOrDefault(r => r.Cost == 0 && r.CustomerId == _testCustomer.Id);
            Assert.NotNull(result);

            _context.Entry(_testCustomer).Reload();
            Assert.Equal(initialPoints - 20, _testCustomer.LoyaltyPoints);

            _context.Entry(_testCar).Reload();
            Assert.False(_testCar.IsAvailable);
        }

        [Fact]
        public void RentCarWithPoints_WithoutEnoughPoints_ShouldThrowException()
        {
            _testCustomer.LoyaltyPoints = 10;
            _context.SaveChanges();

            var rental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Days = 1,
                Cost = 0
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _service.RentCarWithPoints(rental));
            Assert.Contains("wymagane 20", exception.Message);
        }

        [Fact]
        public void RentCarWithPoints_WithOverlappingDates_ShouldThrowException()
        {
            var existingRental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                Days = 2,
                Cost = 300,
                IsCompleted = false
            };
            _context.Rentals.Add(existingRental);
            _context.SaveChanges();

            _context.Entry(_testCar).Collection(c => c.Rentals).Load();

            var newRental = new Rental
            {
                CarId = _testCar.Id,
                CustomerId = _testCustomer.Id,
                BranchId = _testBranch.Id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Days = 1,
                Cost = 0
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _service.RentCarWithPoints(newRental));
            Assert.Equal("Samochód jest niedostępny w tym okresie", exception.Message);
        }
    }
}