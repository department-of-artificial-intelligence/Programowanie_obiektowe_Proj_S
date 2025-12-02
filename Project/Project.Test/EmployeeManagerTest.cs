using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Project.Test
{
    public class EmployeeManagerTest
    {
        [Fact]
        public void AddEmployeeTest()
        {
            List<Employee> employees1 = new List<Employee>()
            {
                new Employee(1, "Jan", "Kowalski", "Wlasciciel"),
                new Employee(2, "Olek", "Szczepanik", "Magister Farmacji"),
                new Employee(3, "Mateusz", "Wyrazik", "Technik Farmacji"),
            };
            ISourceEmployee pracownicy1 = new EmpInMemory(employees1);
            IEmployeeManager empManager1 = new EmployeeManager(pracownicy1);
            string imie = "Jan";
            string nazwisko = "Pierzgalski";
            string stanowisko = "Wlasciciel";
            bool wynnik = empManager1.AddEmployee(imie, nazwisko, stanowisko);
            Assert.True(wynnik);
            Assert.Equal(4, pracownicy1.AllEmployees().Count); // Sprawdza liczbe elementow w liscie jezeli lista ma 3 pracownikow to nie przejdzie!!
            Assert.Equal(imie, pracownicy1.AllEmployees().Last().FirstName);
            Assert.Equal(nazwisko, pracownicy1.AllEmployees().Last().LastName);
            Assert.Equal(stanowisko, pracownicy1.AllEmployees().Last().Position);
        }
        [Fact]
        public void RemoveEmployeeTest()
        {
            List<Employee> employees1 = new List<Employee>()
            {
                new Employee(1, "Jan", "Kowalski", "Wlasciciel"),
                new Employee(2, "Olek", "Szczepanik", "Magister Farmacji"),
                new Employee(3, "Mateusz", "Wyrazik", "Technik Farmacji"),
            };
            ISourceEmployee pracownicy1 = new EmpInMemory(employees1);
            IEmployeeManager empManager1 = new EmployeeManager(pracownicy1);

            bool wynik = empManager1.RemoveEmployee(3);
            Assert.True(wynik);
            Assert.Equal(2, pracownicy1.AllEmployees().Count);
        }
    }
}
