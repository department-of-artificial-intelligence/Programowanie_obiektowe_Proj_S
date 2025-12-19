using Microsoft.EntityFrameworkCore;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.Services;
using Xunit;

namespace WypozyczalniaSamochodow.Tests
{
    public class CarServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CarService _service;
        private readonly Branch _testBranch;

        public CarServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CarServiceTestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _service = new CarService(_context);

            _testBranch = new Branch { Name = "Test", City = "Warszawa", Address = "ul. Test 1", ContactNumber = "123456789" };
            _context.Branches.Add(_testBranch);
            _context.SaveChanges();
        }

        [Fact]
        public void GetCarByBranch_ShouldReturnCarsForBranch()
        {
            var car1 = new Car { Brand = "Toyota", Model = "Corolla", ProductionYear = 2020, Power = 120, EngineVolume = 1.6, AvgConsumption = 6.5, Gearbox = "manualna", FuelType = "benzyna", PricePerDay = 150, BranchId = _testBranch.Id };
            var car2 = new Car { Brand = "Honda", Model = "Civic", ProductionYear = 2021, Power = 140, EngineVolume = 1.8, AvgConsumption = 7.0, Gearbox = "automatyczna", FuelType = "benzyna", PricePerDay = 180, BranchId = _testBranch.Id };
            _context.Cars.AddRange(car1, car2);
            _context.SaveChanges();

            var result = _service.GetCarByBranch(_testBranch.Id).ToList();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Brand == "Toyota");
            Assert.Contains(result, c => c.Brand == "Honda");
        }

        [Fact]
        public void GetCarByBranch_WithNoCars_ShouldReturnEmptyList()
        {
            var result = _service.GetCarByBranch(_testBranch.Id).ToList();

            Assert.Empty(result);
        }

        [Fact]
        public void AddCar_WithValidData_ShouldAddCar()
        {
            var car = new Car
            {
                Brand = "BMW",
                Model = "X5",
                ProductionYear = 2022,
                Power = 300,
                EngineVolume = 3.0,
                AvgConsumption = 9.5,
                Gearbox = "automatyczna",
                FuelType = "diesel",
                PricePerDay = 400
            };

            _service.AddCar(car, _testBranch);

            var result = _context.Cars.FirstOrDefault(c => c.Brand == "BMW");

            Assert.NotNull(result);
            Assert.True(result.IsAvailable);
            Assert.Equal(_testBranch.Id, result.BranchId);
            Assert.Equal("X5", result.Model);
        }

        [Fact]
        public void AddCar_WithNullCar_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.AddCar(null!, _testBranch));
        }

        [Fact]
        public void AddCar_WithNonExistentBranch_ShouldThrowException()
        {
            var car = new Car
            {
                Brand = "Test",
                Model = "Test",
                ProductionYear = 2020,
                Power = 100,
                EngineVolume = 1.5,
                AvgConsumption = 6.0,
                Gearbox = "manualna",
                FuelType = "benzyna",
                PricePerDay = 100
            };

            var fakeBranch = new Branch { Id = 999 };
            var exception = Assert.Throws<InvalidOperationException>(() => _service.AddCar(car, fakeBranch));

            Assert.Equal("Oddział o podanym ID nie istnieje", exception.Message);
        }

        [Fact]
        public void AddCar_WithInvalidPower_ShouldThrowException()
        {
            var car = new Car
            {
                Brand = "Test",
                Model = "Test",
                ProductionYear = 2020,
                Power = -50,
                EngineVolume = 1.5,
                AvgConsumption = 6.0,
                Gearbox = "manualna",
                FuelType = "benzyna",
                PricePerDay = 100
            };

            var exception = Assert.Throws<ArgumentException>(() => _service.AddCar(car, _testBranch));
            Assert.Equal("Moc pojazdu musi być większa od 0 i mniejsza od 2000 KM", exception.Message);
        }

        [Fact]
        public void UpdateCar_WithValidData_ShouldUpdateCar()
        {
            var car = new Car
            {
                Brand = "Stary",
                Model = "Model",
                ProductionYear = 2020,
                Power = 100,
                EngineVolume = 1.5,
                AvgConsumption = 6.0,
                Gearbox = "manualna",
                FuelType = "benzyna",
                PricePerDay = 100,
                BranchId = _testBranch.Id
            };
            _context.Cars.Add(car);
            _context.SaveChanges();

            car.Brand = "Nowy";
            car.Power = 150;

            _service.UpdateCar(car);

            var result = _context.Cars.Find(car.Id);
            Assert.NotNull(result);
            Assert.Equal("Nowy", result.Brand);
            Assert.Equal(150, result.Power);
        }

        [Fact]
        public void UpdateCar_WithNonExistentCar_ShouldThrowException()
        {
            var car = new Car
            {
                Id = 999,
                Brand = "Test",
                Model = "Test",
                ProductionYear = 2020,
                Power = 100,
                EngineVolume = 1.5,
                AvgConsumption = 6.0,
                Gearbox = "manualna",
                FuelType = "benzyna",
                PricePerDay = 100
            };

            var exception = Assert.Throws<InvalidOperationException>(() => _service.UpdateCar(car));
            Assert.Equal("Samochód nie istnieje", exception.Message);
        }

        [Fact]
        public void RemoveCar_WithoutActiveRentals_ShouldRemoveCar()
        {
            var car = new Car
            {
                Brand = "Do usunięcia",
                Model = "Model",
                ProductionYear = 2020,
                Power = 100,
                EngineVolume = 1.5,
                AvgConsumption = 6.0,
                Gearbox = "manualna",
                FuelType = "benzyna",
                PricePerDay = 100,
                BranchId = _testBranch.Id
            };
            _context.Cars.Add(car);
            _context.SaveChanges();
            var carId = car.Id;

            _service.RemoveCar(carId, _testBranch.Id);

            var result = _context.Cars.Find(carId);
            Assert.Null(result);
        }

        [Fact]
        public void RemoveCar_WithActiveRentals_ShouldThrowException()
        {
            var car = new Car
            {
                Brand = "Z wypożyczeniem",
                Model = "Model",
                ProductionYear = 2020,
                Power = 100,
                EngineVolume = 1.5,
                AvgConsumption = 6.0,
                Gearbox = "manualna",
                FuelType = "benzyna",
                PricePerDay = 100,
                BranchId = _testBranch.Id
            };
            var rental = new Rental
            {
                CarId = car.Id,
                BranchId = _testBranch.Id,
                IsCompleted = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Days = 1,
                Cost = 100
            };
            car.Rentals.Add(rental);
            _context.Cars.Add(car);
            _context.SaveChanges();

            var exception = Assert.Throws<InvalidOperationException>(() => _service.RemoveCar(car.Id, _testBranch.Id));
            Assert.Equal("Nie można usunąć samochodu posiadającego aktywne wypożyczenia", exception.Message);
        }

        [Fact]
        public void SearchCars_WithBrand_ShouldReturnMatchingCars()
        {
            var car1 = new Car { Brand = "Toyota", Model = "Corolla", ProductionYear = 2020, Power = 120, EngineVolume = 1.6, AvgConsumption = 6.5, Gearbox = "manualna", FuelType = "benzyna", PricePerDay = 150, BranchId = _testBranch.Id };
            var car2 = new Car { Brand = "Honda", Model = "Civic", ProductionYear = 2021, Power = 140, EngineVolume = 1.8, AvgConsumption = 7.0, Gearbox = "automatyczna", FuelType = "benzyna", PricePerDay = 180, BranchId = _testBranch.Id };
            _context.Cars.AddRange(car1, car2);
            _context.SaveChanges();

            var result = _service.SearchCars(_testBranch.Id, "Toyota", null, null, null).ToList();

            Assert.Single(result);
            Assert.Equal("Toyota", result[0].Brand);
        }

        [Fact]
        public void SearchCars_WithMinPower_ShouldReturnMatchingCars()
        {
            var car1 = new Car { Brand = "Toyota", Model = "Corolla", ProductionYear = 2020, Power = 120, EngineVolume = 1.6, AvgConsumption = 6.5, Gearbox = "manualna", FuelType = "benzyna", PricePerDay = 150, BranchId = _testBranch.Id };
            var car2 = new Car { Brand = "Honda", Model = "Civic", ProductionYear = 2021, Power = 200, EngineVolume = 1.8, AvgConsumption = 7.0, Gearbox = "automatyczna", FuelType = "benzyna", PricePerDay = 180, BranchId = _testBranch.Id };
            _context.Cars.AddRange(car1, car2);
            _context.SaveChanges();

            var result = _service.SearchCars(_testBranch.Id, null, 150, null, null).ToList();

            Assert.Single(result);
            Assert.Equal("Honda", result[0].Brand);
        }
    }
}