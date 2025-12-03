using CarsRental.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarsRental.Logic
{
    public class DepartmentService : IDepartmentService
    {
        private List<Department> _departments = new List<Department>();
        private int _departmentCounter = 0;

        public void AddDepartment(Department department)
        {
            Console.WriteLine("Dodawanie wypożyczalni...");

            if (string.IsNullOrEmpty(department.Name) && string.IsNullOrEmpty(department.Address) && (string.IsNullOrEmpty(department.PhoneNumber) || (string.IsNullOrEmpty(department.EmailAddress))))
            {
                Console.WriteLine("Nie można dodać pustych danych!\n");
                return;
            }

            foreach (Department d in _departments)
            {
                if (d.Name.Equals(department.Name))
                {
                    Console.WriteLine("Podana wypożyczalnia znajduje się już w bazie danych!\n");
                    return;
                }
            }

            _departmentCounter++;
            department.Id = _departmentCounter;
            _departments.Add(department);

            Console.WriteLine($"Dodano '{department.Name}' {department.Address}\n");
        }

        public void UpdateDepartment(Department department)
        {
            Console.WriteLine("Aktualizowanie wypożyczalni...");
            if (department == null)
            {
                Console.WriteLine("Pola nie mogą być puste!");
                return;
            }

            var existingDepartment = GetDepartment(department.Id);
            if (existingDepartment == null)
            {
                Console.WriteLine("Nie znaleziono takiej wypożyczalni");
                return;
            }

            existingDepartment.Name = department.Name;
            existingDepartment.Address = department.Address;
            existingDepartment.PhoneNumber = department.PhoneNumber;
            existingDepartment.EmailAddress = department.EmailAddress;

            Console.WriteLine($"Zaktualizowano wypożyczalnię [{department.Id}] {department.Name}");
        }

        public List<Department> GetAllDepartments()
        {
            return _departments;
        }

        public Department? GetDepartment(int id)
        {
            var dep = _departments.Find(d => d.Id == id);
            if (dep == null)
            {
                Console.WriteLine($"Nie znaleziono wypożyczalni o ID({id})\n");
                return null;
            }
            return dep;
        }

        public void RemoveDepartment(int id)
        {
            Console.WriteLine("Usuwanie wypożyczalni...");

            Department? depToRemove = GetDepartment(id);
            if (depToRemove != null)
            {
                _departments.Remove(depToRemove);
                Console.WriteLine($"Usunięto '{depToRemove.Name}' {depToRemove.Address}\n");
            }
        }
    }
}
