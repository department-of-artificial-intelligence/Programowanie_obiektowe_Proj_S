using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model;
using VehicleRentalSystem.Model.Service;
using Xunit;

namespace VehicleRentalSystem.Tests
{
    public class VehicleServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly VehicleService _vehicleService;

        public VehicleServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("VehicleServiceTestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _vehicleService = new VehicleService(_context);
        }

        [Fact]
        public async Task AddVehicle_ShouldIncreaseCount()
        {
            var vehicle = new Vehicle
            {
                Brand = "Toyota",
                Model = "Corolla",
                RegistrationNumber = "ABC123",
                ProdYear = 2020,
                PriceForDay = 100
            };

            await _vehicleService.AddVehicle(vehicle);

            var count = await _vehicleService.GetVehicleCount();
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task DeleteVehicle_ShouldRemoveVehicle_WhenNotRented()
        {
            var vehicle = new Vehicle
            {
                Brand = "Ford",
                Model = "Focus",
                RegistrationNumber = "XYZ789",
                ProdYear = 2019,
                PriceForDay = 80
            };
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var result = await _vehicleService.DeleteVehicle(vehicle.Id);

            Assert.True(result);
            Assert.Equal(0, await _vehicleService.GetVehicleCount());
        }

        [Fact]
        public async Task ServiceVehicle_ShouldUpdateServiceDates()
        {
            var vehicle = new Vehicle
            {
                Brand = "Opel",
                Model = "Astra",
                RegistrationNumber = "OPEL123",
                ProdYear = 2018,
                PriceForDay = 70
            };
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var result = await _vehicleService.ServiceVehicle(vehicle.Id);

            Assert.True(result);
            var updated = await _vehicleService.GetVehicleById(vehicle.Id);
            Assert.NotNull(updated!.LastService);
            Assert.NotNull(updated.NextService);
        }

        [Fact]
        public async Task SearchVehicles_ShouldFindByBrandOrModel()
        {
            var dept = new Department { Name = "TestDept" };
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();

            _context.Vehicles.Add(new Vehicle { Brand = "Mazda", Model = "CX-5", RegistrationNumber = "MAZDA1", ProdYear = 2020, Department = dept });
            _context.Vehicles.Add(new Vehicle { Brand = "Skoda", Model = "Octavia", RegistrationNumber = "SKODA1", ProdYear = 2019, Department = dept });
            await _context.SaveChangesAsync();

            var results = await _vehicleService.SearchVehicles("Mazda");

            Assert.Single(results);
            Assert.Equal("Mazda", results[0].Brand);
        }

        [Fact]
        public async Task GetAvailableAndRentedCounts_ShouldReturnCorrectNumbers()
        {
            _context.Vehicles.Add(new Vehicle { Brand = "VW", Model = "Golf", RegistrationNumber = "VW1", IsRented = false });
            _context.Vehicles.Add(new Vehicle { Brand = "VW", Model = "Passat", RegistrationNumber = "VW2", IsRented = true });
            await _context.SaveChangesAsync();

            var total = await _vehicleService.GetVehicleCount();
            var rented = await _vehicleService.GetRentedVehicleCount();
            var available = await _vehicleService.GetAvailableVehicleCount();

            Assert.Equal(2, total);
            Assert.Equal(1, rented);
            Assert.Equal(1, available);
        }
    }
}
