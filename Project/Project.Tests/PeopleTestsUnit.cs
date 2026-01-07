using Xunit;
using Project.Model;
using System;

namespace Project.Tests
{
    public class PersonTests
    {
        [Fact]
        public void Employee_ShouldCalculateInfoCorrectly()
        {
            Employee emp = new Employee(1, "Jan", "Kowalski", "jan@firma.pl", "ul. Jasna 1", "+48123456789", 5000m, EmployeePosition.Manager);

            string info = emp.GetInfo();

            Assert.Contains("[EMPLOYEE #1]", info);
            Assert.Contains("Jan Kowalski", info);
            Assert.Contains("Manager", info);
        }

        [Fact]
        public void Customer_ShouldInitializeWithZeroWalletBalance()
        {
            Customer cust = new Customer(101, "Anna", "Nowak", "anna@poczta.pl", "ul. Polna 4", "+48987654321", "Warszawa", "Mazowieckie", "00-001");

            Assert.Equal(0, cust.WalletBalance);
        }

        [Fact]
        public void PhoneNumber_ShouldThrowException_WhenFormatIsInvalid()
        {
            Employee emp = new Employee(1, "Jan", "Kowalski", "jan@firma.pl", "ul. Jasna 1", "+48123456789", 5000m, EmployeePosition.Manager);

            Assert.Throws<ArgumentException>(() => emp.PhoneNumber = "123-456-789");
        }

        [Fact]
        public void Salary_ShouldThrowException_WhenValueIsNegative()
        {
            Employee emp = new Employee(1, "Jan", "Kowalski", "jan@firma.pl", "ul. Jasna 1", "+48123456789", 5000m, EmployeePosition.Manager);

            Assert.Throws<ArgumentOutOfRangeException>(() => emp.Salary = -100m);
        }

        [Theory]
        [InlineData("+48123456789")]
        [InlineData("+12987654321")]
        public void PhoneNumber_ShouldAcceptValidFormats(string validPhone)
        {
            Customer cust = new Customer(1, "A", "B", "e@e.pl", "Add", "+48111222333", "City", "Reg", "00-000");

            cust.PhoneNumber = validPhone;

            Assert.Equal(validPhone, cust.PhoneNumber);
        }
    }
}