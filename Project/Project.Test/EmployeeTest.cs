using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Project.Test
{
    public class EmployeeTest
    {
        [Fact]
        public void KonstruktorDomyslny()
        {
            var employee = new Employee();
            Assert.Equal(string.Empty, employee.FirstName);
            Assert.Equal(string.Empty, employee.LastName);
            Assert.Equal(string.Empty, employee.Position);
        }
        [Fact]
        public void KonstruktorParametryczny()
        {
            string imie = "Jan";
            string nazwisko = "Pierzgalski";
            string stanowisko = "Magister Farmacji";
            var employee = new Employee(0, imie, nazwisko, stanowisko);
        }
        [Fact]
        public void ToStringTest()
        {
            string imie = "Jan";
            string nazwisko = "Pierzgalski";
            string stanowisko = "Magister Farmacji";
            var employee = new Employee(0, imie, nazwisko, stanowisko);
            employee.ToString();
        }
    }
}
