using Xunit;
using Project.Model;
using System;

namespace Project.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void PelneDane_Zawiera_Pensje_I_Format_Daty()
        {
            var emp = new Employees
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Stanowisko = "Kucharz",
                Pensja = 4500.50m,
                DataZatrudnienia = new DateTime(2023, 5, 10)
            };

            string info = emp.PelneDane();

            Assert.Contains("Jan Kowalski", info);
            Assert.Contains("Kucharz", info);
            Assert.Contains("4 500,50", info);
            Assert.Contains("10-05-2023", info);
        }
    }
}