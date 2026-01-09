using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model;
using VehicleRentalSystem.Model.Service;
using Xunit;

namespace VehicleRentalSystem.Tests
{
    public class DepartmentServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly DepartmentService _departmentService;

        public DepartmentServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("DepartmentServiceTestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _departmentService = new DepartmentService(_context);
        }

        [Fact]
        public async Task AddDepartment_ShouldIncreaseCount()
        {
            var department = new Department
            {
                Name = "Oddział Katowice",
                City = "Katowice",
                StreetAddress = "ul. Mickiewicza 10"
            };

            await _departmentService.AddDepartment(department);

            var count = await _departmentService.GetDepartmentCount();
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task UpdateDepartment_ShouldModifyData()
        {
            var department = new Department
            {
                Name = "Oddział Kraków",
                City = "Kraków",
                StreetAddress = "ul. Długa 5"
            };
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            department.Name = "Oddział Kraków Centrum";
            var result = await _departmentService.UpdateDepartment(department);

            Assert.True(result);
            var updated = await _departmentService.GetDepartment(department.Id);
            Assert.Equal("Oddział Kraków Centrum", updated!.Name);
        }

        [Fact]
        public async Task GetAllDepartments_ShouldReturnAll()
        {
            _context.Departments.Add(new Department { Name = "Warszawa", City = "Warszawa", StreetAddress = "Marszałkowska 1" });
            _context.Departments.Add(new Department { Name = "Łódź", City = "Łódź", StreetAddress = "Piotrkowska 100" });
            await _context.SaveChangesAsync();

            var departments = await _departmentService.GetAllDepartments();

            Assert.Equal(2, departments.Count);
        }

        [Fact]
        public async Task GetDepartment_ShouldReturnDepartment()
        {
            var department = new Department { Name = "Poznań", City = "Poznań", StreetAddress = "Święty Marcin 20" };
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            var found = await _departmentService.GetDepartment(department.Id);

            Assert.NotNull(found);
            Assert.Equal("Poznań", found!.City);
        }

        [Fact]
        public async Task DeleteDepartment_ShouldRemoveDepartment_WhenNoEmployeesOrVehicles()
        {
            var department = new Department { Name = "Gdańsk", City = "Gdańsk", StreetAddress = "Długa 50" };
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            var result = await _departmentService.DeleteDepartment(department.Id);

            Assert.True(result);
            Assert.Equal(0, await _departmentService.GetDepartmentCount());
        }

        [Fact]
        public async Task DeleteDepartment_ShouldFail_WhenEmployeesExist()
        {
            var department = new Department { Name = "Wrocław", City = "Wrocław", StreetAddress = "Rynek 1" };
            var employee = new Employee { FirstName = "Jan", LastName = "Nowak", Department = department };

            _context.Departments.Add(department);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var result = await _departmentService.DeleteDepartment(department.Id);

            Assert.False(result);
            Assert.Equal(1, await _departmentService.GetDepartmentCount());
        }

        [Fact]
        public async Task DeleteDepartment_ShouldFail_WhenVehiclesExist()
        {
            var department = new Department { Name = "Szczecin", City = "Szczecin", StreetAddress = "Plac Grunwaldzki 2" };
            var vehicle = new Vehicle { Brand = "Ford", Model = "Focus", Department = department };

            _context.Departments.Add(department);
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            var result = await _departmentService.DeleteDepartment(department.Id);

            Assert.False(result);
            Assert.Equal(1, await _departmentService.GetDepartmentCount());
        }
    }
}
