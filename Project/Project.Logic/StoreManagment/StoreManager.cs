using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;
using Project.Model.Interfaces;
using Project.Model.People;
using Project.Model.Stores;

namespace Project.Logic.StoreManagment
{
    public class EmployeeManager
    {
        private readonly IEmployeeManager _employeeRepository;

        public EmployeeManager(IEmployeeManager employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> AllEmployees()
        {
            return _employeeRepository.GetAll();
        }

        public Employee FindById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public Employee FindByEmail(string email)
        {
            // Filtrowanie w pamięci (bezpieczniej niż zmieniać interfejs)
            return _employeeRepository.GetAll().FirstOrDefault(e => e.Email == email);
        }

        public void HireEmployee(Store store, Employee employee)
        {
            if (store == null || employee == null) return;

            var exists = _employeeRepository.GetById(employee.Id);
            if (exists == null)
            {
                _employeeRepository.Add(employee); // Dodaj do bazy, jeśli nie istnieje
            }

            if (!store.Staff.Contains(employee))
            {
                store.Staff.Add(employee);
                // Ponieważ zmieniliśmy listę Staff w sklepie, warto by zaktualizować sklep
                // Ale tutaj zakładamy, że wystarczy dodać pracownika.
            }
            Console.WriteLine($"[HR] Zatrudniono {employee.FirstName}.");
        }

        public void FireEmployeeByObject(Store store, Employee employee)
        {
            if (store == null || employee == null) return;

            store.Staff.Remove(employee);
            _employeeRepository.Remove(employee); // Usunięcie z bazy
            Console.WriteLine($"[HR] Zwolniono {employee.FirstName}.");
        }

        public void ChangePosition(Employee employee, EmployeePosition newPosition, decimal newSalary)
        {
            if (employee == null) return;
            employee.Position = newPosition;
            employee.Salary = newSalary;

            _employeeRepository.Update(employee);
        }

        public decimal CalculatePayroll(Store store)
        {
            return store?.Staff.Sum(e => e.Salary) ?? 0;
        }
    }
}