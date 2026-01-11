using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;
using Project.Model.Interfaces;
using Project.Model.People;
using Project.Model.Stores;


namespace Project.Logic.StoreManagment
{
    public class EmployeeManager : IEmployeeManager
    {
        
        private readonly List<Employee> _employeesDatabase;

        public EmployeeManager()
        {
            _employeesDatabase = new List<Employee>();
        }

       

        public List<Employee> AllEmployees()
        {
            return _employeesDatabase;
        }

        public Employee FindById(int id)
        {

            return _employeesDatabase.FirstOrDefault(e => e.EmployeeId == id);

        }

       
        public Employee FindByEmail(string email)
        {
            return _employeesDatabase.FirstOrDefault(e => e.Email == email);
        }

        public void HireEmployee(Store store, Employee employee)
        {
            if (store == null || employee == null)
            {
                throw new ArgumentNullException("Sklep i pracownik nie mogą być null.");
            }

            
            if (_employeesDatabase.Contains(employee))
            {
                Console.WriteLine($"[HR] Pracownik {employee.GetInfo()} jest już zatrudniony!");
                return;
            }

           
            _employeesDatabase.Add(employee);

            
            store.Staff.Add(employee);

            Console.WriteLine($"[HR] Zatrudniono {employee.FirstName} {employee.LastName} na stanowisko {employee.Position} w sklepie {store.Name}.");
        }

        public void FireEmployee(Store store, int employeeId)
        {
            
            if (store == null) return;


            Console.WriteLine("[HR] Metoda FireEmployee wymaga ID w klasie Person. Użyj FireEmployeeByObject tymczasowo.");
        }

        
        public void FireEmployeeByObject(Store store, Employee employee)
        {
            if (store == null || employee == null) return;

            
            bool removedFromStore = store.Staff.Remove(employee);

           
            bool removedFromDb = _employeesDatabase.Remove(employee);

            if (removedFromStore || removedFromDb)
            {
                Console.WriteLine($"[HR] Zwolniono pracownika: {employee.GetInfo()}");
            }
            else
            {
                Console.WriteLine("[HR] Nie znaleziono takiego pracownika.");
            }
        }

        public void ChangePosition(Employee employee, EmployeePosition newPosition, decimal newSalary)
        {
            if (employee == null) return;

            if (newSalary < 0)
                throw new ArgumentOutOfRangeException(nameof(newSalary), "Pensja nie może być ujemna.");

            var oldPosition = employee.Position;
            var oldSalary = employee.Salary;

            
            employee.Position = newPosition;
            employee.Salary = newSalary;

            Console.WriteLine($"[HR] Awans/Zmiana dla {employee.LastName}: {oldPosition} ({oldSalary:C}) -> {newPosition} ({newSalary:C})");
        }

        public decimal CalculatePayroll(Store store)
        {
            if (store == null || store.Staff.Count == 0) return 0;

            
            decimal total = store.Staff.Sum(emp => emp.Salary);

            Console.WriteLine($"[FINANSE] Lista płac dla '{store.Name}': {total:C}");
            return total;
        }
    }
}
