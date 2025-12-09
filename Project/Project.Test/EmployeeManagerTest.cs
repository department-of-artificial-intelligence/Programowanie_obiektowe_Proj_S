using Project.Model;
using Project.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
namespace Project.Test
{
    public class EmployeeManagerTest
    {
        [Fact]
        public void AddEmployeeTest()
        {
            Pharmacy phar = new Pharmacy();
            List<Employee> employees1 = new List<Employee>()
            {
                new Employee("Jan", "Kowalski", "Wlasciciel", phar) {Id = 1},
                new Employee("Olek", "Szczepanik", "Magister Farmacji", phar) {Id = 2},
                new Employee("Mateusz", "Wyrazik", "Technik Farmacji", phar) {Id = 3},
            };
            ISourceEmployee pracownicy1 = new EmployeeInMemory(employees1);
            EmployeeManager empManager1 = new EmployeeManager(pracownicy1);
            string imie = "Jan";
            string nazwisko = "Pierzgalski";
            string stanowisko = "Wlasciciel";
            bool wynnik = empManager1.AddEmployee(imie, nazwisko, stanowisko, phar);
            Assert.True(wynnik);
            Assert.Equal(4, pracownicy1.AllEmployees().Count); // Sprawdza liczbe elementow w liscie jezeli lista ma 3 pracownikow to nie przejdzie!!
            Assert.Equal(imie, pracownicy1.AllEmployees().Last().FirstName);
            Assert.Equal(nazwisko, pracownicy1.AllEmployees().Last().LastName);
            Assert.Equal(stanowisko, pracownicy1.AllEmployees().Last().Position);
        }
        [Fact]
        public void RemoveEmployeeTest()
        {
            Pharmacy phar = new Pharmacy();
            List<Employee> employees1 = new List<Employee>()
            {
                new Employee("Jan", "Kowalski", "Wlasciciel", phar) {Id = 1},
                new Employee("Olek", "Szczepanik", "Magister Farmacji", phar) {Id = 2},
                new Employee("Mateusz", "Wyrazik", "Technik Farmacji", phar) {Id = 3}
            };
            ISourceEmployee pracownicy1 = new EmployeeInMemory(employees1);
            EmployeeManager empManager1 = new EmployeeManager(pracownicy1);

            bool wynik = empManager1.RemoveEmployee(3, phar);
            Assert.True(wynik);
            Assert.Equal(2, pracownicy1.AllEmployees().Count);
        }
    }
}
