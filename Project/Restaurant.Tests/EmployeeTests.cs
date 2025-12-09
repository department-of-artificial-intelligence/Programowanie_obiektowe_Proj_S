using RestaurantManagement.Models;
using RestaurantManagement.Models.Enums;

namespace RestaurantManagement.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void EmployeeCreation_ShouldSetAllProperties()
        {
            // Arrange
            // Arrange
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");
            var hiredDate = DateTime.Now;
            // Act

            // Act
            var employee = new Employee
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                PhoneNumber = "123456789",
                Email = "jan.kowalski@restaurant.com",
                DateOfBirth = new DateTime(1990, 5, 15),
                Address = address,
                EmployeeType = EmployeeType.Kelner,
                Salary = 3500,
                HiredOn = hiredDate
            // Assert
            };

            // Assert
            Assert.Equal("Jan", employee.FirstName);
            Assert.Equal("Kowalski", employee.LastName);
            Assert.Equal(EmployeeType.Kelner, employee.EmployeeType);
            Assert.Equal(3500, employee.Salary);
            Assert.Equal(hiredDate, employee.HiredOn);
            Assert.Null(employee.FiredOn);
        }

        [Fact]
        public void Employee_ShouldInheritFromPerson()
        {
            // Arrange
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");
            var employee = new Employee
            {
                FirstName = "Anna",
                LastName = "Nowak",
                PhoneNumber = "987654321",
                Email = "anna@restaurant.com",
                DateOfBirth = new DateTime(1992, 8, 20),
                Address = address,
                EmployeeType = EmployeeType.Kucharz,
                Salary = 4500,
                HiredOn = DateTime.Now
            };

            Assert.IsAssignableFrom<Person>(employee);
            Assert.Equal("Anna Nowak", employee.FullName);
        }

        [Fact]
        public void Employee_ToString_Test()
        {
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");
            var employee = new Employee
            {
                FirstName = "Piotr",
                LastName = "Wiśniewski",
                PhoneNumber = "111222333",
                Email = "piotr@restaurant.com",
                DateOfBirth = new DateTime(1988, 3, 10),
                Address = address,
                EmployeeType = EmployeeType.Szef,
                Salary = 8000,
                HiredOn = DateTime.Now
            };

            var result = employee.ToString();

            Assert.Contains("Piotr", result);
            Assert.Contains("Wiśniewski", result);
        }

        [Fact]
        public void Employee_FiredOn_Can_Be_Set()
        {
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");
            var employee = new Employee
            {
                FirstName = "Maria",
                LastName = "Kowalczyk",
                PhoneNumber = "555666777",
                Email = "maria@restaurant.com",
                DateOfBirth = new DateTime(1995, 11, 5),
                Address = address,
                EmployeeType = EmployeeType.Barman,
                Salary = 3800,
                HiredOn = DateTime.Now
            };

            employee.FiredOn = DateTime.Now;

            Assert.NotNull(employee.FiredOn);
        }

        [Fact]
        public void Employee_Salary_Can_Be_Modified()
        {
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");
            var employee = new Employee
            {
                FirstName = "Tomasz",
                LastName = "Nowak",
                PhoneNumber = "999888777",
                Email = "tomasz@restaurant.com",
                DateOfBirth = new DateTime(1987, 6, 15),
                Address = address,
                EmployeeType = EmployeeType.Kelner,
                Salary = 3000,
                HiredOn = DateTime.Now
            };

            employee.Salary = 3500;

            Assert.Equal(3500, employee.Salary);
        }

        [Fact]
        public void Employee_FullName_Test()
        {
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");
            var employee = new Employee
            {
                FirstName = "Marcin",
                LastName = "Jankowski",
                PhoneNumber = "333444555",
                Email = "marcin@restaurant.com",
                DateOfBirth = new DateTime(1992, 10, 8),
                Address = address,
                EmployeeType = EmployeeType.Menadżer,
                Salary = 7000,
                HiredOn = DateTime.Now
            };

            Assert.Equal("Marcin Jankowski", employee.FullName);
        }
    }
}

