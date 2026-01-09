using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model;
using VehicleRentalSystem.Model.Service;
using Xunit;

namespace VehicleRentalSystem.Tests
{
    public class EmployeeServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeServiceTestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _employeeService = new EmployeeService(_context);
        }

        [Fact]
        public async Task HireEmployee_ShouldAddEmployee()
        {
            _context.Database.EnsureDeleted(); 
            _context.Database.EnsureCreated();

            var department = new Department { Name = "TestDept" };
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                DepartmentId = department.Id,
                Title = JobTitle.Pracownik
            };

            await _employeeService.HireEmployee(employee);

            var count = await _employeeService.GetEmployeeCount();
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnSortedListWithDepartments()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var department1 = new Department { Name = "IT" };
            var department2 = new Department { Name = "HR" };
            _context.Departments.AddRange(department1, department2);
            await _context.SaveChangesAsync();

            var emp1 = new Employee
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                DepartmentId = department1.Id,
                Title = JobTitle.Pracownik
            };

            var emp2 = new Employee
            {
                FirstName = "Anna",
                LastName = "Nowak",
                DepartmentId = department2.Id,
                Title = JobTitle.Dyrektor
            };

            _context.Employees.AddRange(emp1, emp2);
            await _context.SaveChangesAsync();

            var employees = await _employeeService.GetAllEmployees();

            Assert.Equal(2, employees.Count);

            Assert.Equal(JobTitle.Pracownik, employees.First().Title);
            Assert.Equal(JobTitle.Dyrektor, employees.Last().Title);

            Assert.NotNull(employees[0].Department);
            Assert.NotNull(employees[1].Department);
        }


        [Fact]
        public async Task FireEmployee_ShouldRemoveEmployee()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var department = new Department { Name = "HR" };
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                FirstName = "Anna",
                LastName = "Nowak",
                DepartmentId = department.Id,
                Title = JobTitle.Pracownik
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            await _employeeService.FireEmployee(employee.Id);

            var count = await _employeeService.GetEmployeeCount();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task RankupEmployee_ShouldPromoteEmployee()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var department = new Department { Name = "IT" };
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                FirstName = "Piotr",
                LastName = "Zieliński",
                DepartmentId = department.Id,
                Title = JobTitle.Pracownik
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            await _employeeService.RankupEmployee(employee.Id);

            var promoted = await _employeeService.GetEmployee(employee.Id);
            Assert.NotNull(promoted);
            Assert.NotEqual(JobTitle.Pracownik, promoted!.Title);
        }

        [Fact]
        public async Task GetManagementCount_ShouldReturnCorrectNumber()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var department = new Department { Name = "Management" };
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            _context.Employees.Add(new Employee
            {
                FirstName = "Adam",
                LastName = "Dyrektor",
                DepartmentId = department.Id,
                Title = JobTitle.Dyrektor
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Ewa",
                LastName = "Kierownik",
                DepartmentId = department.Id,
                Title = JobTitle.Kierownik
            });

            await _context.SaveChangesAsync();

            var count = await _employeeService.GetManagementCount();
            Assert.Equal(2, count);
        }
    }
}
