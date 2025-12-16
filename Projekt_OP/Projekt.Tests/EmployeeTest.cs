using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt.Model;
using Xunit;
using Moq;

namespace Projekt.Tests
{
    public class EmployeeTesting
    {
        [Fact]
        public void Kontruktor_DomyslnyEmployee()
        {
            var employee = new Employee();
            Assert.NotNull(employee);
            Assert.Null(employee.Name);
            Assert.Null(employee.LastName);
            Assert.Null(employee.Cinema);
            Assert.Equal(0, employee.CinemaID);
        }
        
        [Fact]
        public void KontruktorParametrycznyEmployee()
        {
            string expName = "Jan";
            string expLastName = "Kowalski";
            var mockCinema = new Mock<Cinema>();
            var employee = new Employee(expName, expLastName, mockCinema.Object);
            Assert.Equal(expName, employee.Name);
            Assert.Equal(expLastName, employee.LastName);
            Assert.Equal(mockCinema.Object, employee.Cinema);
        }

    }
}
