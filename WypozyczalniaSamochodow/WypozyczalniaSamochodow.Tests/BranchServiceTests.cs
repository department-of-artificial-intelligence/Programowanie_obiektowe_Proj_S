using Microsoft.EntityFrameworkCore;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Model;
using WypozyczalniaSamochodow.Services;
using Xunit;

namespace WypozyczalniaSamochodow.Tests
{
    public class BranchServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly BranchService _service;

        public BranchServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BranchServiceTestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _service = new BranchService(_context);
        }

        [Fact]
        public void GetAllBranches_ShouldReturnAllBranches()
        {
            _context.Branches.AddRange(
                new Branch { Name = "Oddział 1", City = "Warszawa", Address = "ul. Testowa 1", ContactNumber = "123456789" },
                new Branch { Name = "Oddział 2", City = "Kraków", Address = "ul. Testowa 2", ContactNumber = "987654321" }
            );
            _context.SaveChanges();

            var result = _service.GetAllBranches().ToList();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, b => b.Name == "Oddział 1");
            Assert.Contains(result, b => b.Name == "Oddział 2");
        }

        [Fact]
        public void GetBranchById_WithValidId_ShouldReturnBranch()
        {
            var branch = new Branch { Name = "Test", City = "Warszawa", Address = "ul. Test 1", ContactNumber = "123456789" };
            _context.Branches.Add(branch);
            _context.SaveChanges();

            var result = _service.GetBranchById(branch.Id);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
            Assert.Equal("Warszawa", result.City);
        }

        [Fact]
        public void GetBranchById_WithInvalidId_ShouldThrowException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _service.GetBranchById(999));
            Assert.Equal("Oddział o podanym ID nie istnieje", exception.Message);
        }

        [Fact]
        public void AddBranch_WithValidData_ShouldAddBranch()
        {
            var branch = new Branch
            {
                Name = "Nowy Oddział",
                City = "Gdańsk",
                Address = "ul. Morska 10",
                ContactNumber = "555666777"
            };

            _service.AddBranch(branch);

            var result = _context.Branches.FirstOrDefault(b => b.Name == "Nowy Oddział");
            Assert.NotNull(result);
            Assert.Equal("Gdańsk", result.City);
            Assert.Equal("ul. Morska 10", result.Address);
        }

        [Fact]
        public void AddBranch_WithNullBranch_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.AddBranch(null!));
        }

        [Fact]
        public void AddBranch_WithEmptyName_ShouldThrowException()
        {
            var branch = new Branch
            {
                Name = "",
                City = "Warszawa",
                Address = "ul. Test 1",
                ContactNumber = "123456789"
            };

            var exception = Assert.Throws<ArgumentException>(() => _service.AddBranch(branch));
            Assert.Equal("Nazwa oddziału jest wymagana", exception.Message);
        }

        [Fact]
        public void AddBranch_WithInvalidContactNumber_ShouldThrowException()
        {
            var branch = new Branch
            {
                Name = "Test",
                City = "Warszawa",
                Address = "ul. Test 1",
                ContactNumber = "12345"
            };

            var exception = Assert.Throws<ArgumentException>(() => _service.AddBranch(branch));
            Assert.Equal("Numer kontaktowy musi składać się z 9 cyfr", exception.Message);
        }

        [Fact]
        public void UpdateBranch_WithValidData_ShouldUpdateBranch()
        {
            var branch = new Branch { Name = "Stary", City = "Warszawa", Address = "ul. Stara 1", ContactNumber = "123456789" };
            _context.Branches.Add(branch);
            _context.SaveChanges();

            branch.Name = "Nowy";
            branch.City = "Kraków";

            _service.UpdateBranch(branch);

            var result = _context.Branches.Find(branch.Id);
            Assert.NotNull(result);
            Assert.Equal("Nowy", result.Name);
            Assert.Equal("Kraków", result.City);
        }

        [Fact]
        public void UpdateBranch_WithNullBranch_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.UpdateBranch(null!));
        }

        [Fact]
        public void UpdateBranch_WithNonExistentId_ShouldThrowException()
        {
            var branch = new Branch { Id = 999, Name = "Test", City = "Test", Address = "Test", ContactNumber = "123456789" };

            var exception = Assert.Throws<InvalidOperationException>(() => _service.UpdateBranch(branch));
            Assert.Equal("Oddział nie istnieje", exception.Message);
        }

        [Fact]
        public void RemoveBranch_WithoutActiveRentals_ShouldRemoveBranch()
        {
            var branch = new Branch { Name = "Do usunięcia", City = "Warszawa", Address = "ul. Test 1", ContactNumber = "123456789" };
            _context.Branches.Add(branch);
            _context.SaveChanges();
            var branchId = branch.Id;

            _service.RemoveBranch(branchId);

            var result = _context.Branches.Find(branchId);
            Assert.Null(result);
        }

        [Fact]
        public void RemoveBranch_WithActiveRentals_ShouldThrowException()
        {
            var branch = new Branch { Name = "Z wypożyczeniami", City = "Warszawa", Address = "ul. Test 1", ContactNumber = "123456789" };
            var rental = new Rental
            {
                BranchId = branch.Id,
                IsCompleted = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Days = 1,
                Cost = 100
            };
            branch.Rentals.Add(rental);
            _context.Branches.Add(branch);
            _context.SaveChanges();

            var exception = Assert.Throws<InvalidOperationException>(() => _service.RemoveBranch(branch.Id));
            Assert.Equal("Nie można usunąć oddziału posiadającego aktywne wypożyczenia", exception.Message);
        }

        [Fact]
        public void RemoveBranch_WithNonExistentId_ShouldThrowException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _service.RemoveBranch(999));
            Assert.Equal("Oddział nie istnieje", exception.Message);
        }
    }
}