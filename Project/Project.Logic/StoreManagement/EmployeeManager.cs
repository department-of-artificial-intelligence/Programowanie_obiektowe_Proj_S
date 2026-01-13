using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model.Interfaces;
using Project.Model.People;
using Project.Model.Stores;

namespace Project.Logic.StoreManagement
{
    public class EmployeeManager
    {

        private readonly IEmployeeRepository _employeeRepository;


        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public List<Employee> AllEmployees()
        {

            return _employeeRepository.GetAll().ToList();
        }


        public Employee FindById(int id)
        {
            return _employeeRepository.GetById(id);
        }


        public Employee FindByEmail(string email)
        {
           
            return _employeeRepository.GetAll().FirstOrDefault(e => e.Email == email);
        }

        
        public void HireEmployee(Store store, Employee employee)
        {
            if (store == null || employee == null)
            {
                throw new ArgumentNullException("Sklep i pracownik nie mogą być null.");
            }

            
            var exists = _employeeRepository.GetById(employee.Id);

            if (exists == null)
            {
                
                _employeeRepository.Add(employee);
            }
            else
            {
                Console.WriteLine($"[HR] Pracownik {employee.FirstName} już istnieje w bazie danych.");
            }

            
            if (!store.Staff.Contains(employee))
            {
                store.Staff.Add(employee);
                Console.WriteLine($"[HR] Zatrudniono {employee.FirstName} {employee.LastName} w sklepie {store.Name}.");
            }
            else
            {
                Console.WriteLine($"[HR] Pracownik już pracuje w tym sklepie.");
            }
        }

       
        public void FireEmployeeByObject(Store store, Employee employee)
        {
            if (store == null || employee == null) return;

          
            bool removedFromStore = store.Staff.Remove(employee);

            
            _employeeRepository.Remove(employee);

            if (removedFromStore)
            {
                Console.WriteLine($"[HR] Zwolniono pracownika: {employee.FirstName} {employee.LastName}");
            }
            else
            {
                Console.WriteLine("[HR] Pracownik usunięty z bazy (nie był przypisany do tego sklepu).");
            }
        }

       
        public void ChangePosition(Employee employee, EmployeePosition newPosition, decimal newSalary)
        {
            if (employee == null) return;

            if (newSalary < 0)
                throw new ArgumentOutOfRangeException(nameof(newSalary), "Pensja nie może być ujemna.");

            var oldPosition = employee.Position;

           
            employee.Position = newPosition;
            employee.Salary = newSalary;

           
            _employeeRepository.Update(employee);

            Console.WriteLine($"[HR] Awans dla {employee.LastName}: {oldPosition} -> {newPosition}");
        }

        
        public decimal CalculatePayroll(Store store)
        {
            if (store == null || store.Staff == null) return 0;

            decimal total = store.Staff.Sum(e => e.Salary);

            Console.WriteLine($"[FINANSE] Łączna lista płac w '{store.Name}': {total:C}");
            return total;
        }
    }
}