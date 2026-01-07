using Xunit;
using System;
using Project.Model;

namespace Project.Tests
{
    public class PeopleTestsUnit
    {



            [Fact]
            public void PhoneNumber_ShouldSetCorrectly_WhenFormatIsValid()
            {
                var customer = new Customer(1, "Jan", "Kowalski", "j@k.pl", "+48123456789", "Miasto", "Reg", "00-000");

                Assert.Equal("+48123456789", customer.PhoneNumber);
            }

            [Fact]
            public void PhoneNumber_ShouldThrowException_WhenFormatIsInvalid()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    new Customer(1, "Jan", "Kowalski", "j@k.pl", "123456789", "Miasto", "Reg", "00-000");
                });
            }

            [Fact]
            public void PhoneNumber_ShouldThrowException_WhenContainsLetters()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    new Customer(1, "Jan", "Kowalski", "j@k.pl", "+48aabbccdd", "Miasto", "Reg", "00-000");
                });
            }

            [Fact]
            public void Employee_Salary_ShouldThrowException_WhenNegative()
            {
                var emp = new Employee(1, "Test", "Emp", "e@e.pl", "+48111222333", 5000m, EmployeePosition.Manager);

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    emp.Salary = -100m;
                });
            }

            [Fact]
            public void GetFullName_ShouldCombineFirstAndLastName()
            {
                var customer = new Customer(1, "Anna", "Nowak", "a@n.pl", "+48999888777", "X", "Y", "00");

                Assert.Equal("Anna Nowak", customer.GetFullName());
            }
        
    }
}

