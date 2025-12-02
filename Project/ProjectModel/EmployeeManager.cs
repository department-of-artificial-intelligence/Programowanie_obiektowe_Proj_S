using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly ISourceEmployee _source;

        public EmployeeManager(ISourceEmployee source) 
        {
            _source = source;
        }
        public void DisplayEmployees()
        {
            var pracownicy = _source.AllEmployees();
            foreach(Employee emp in pracownicy)
            {
                Console.WriteLine(emp);
            }
        }
        public bool AddEmployee(string firstName, string lastName, string position)
        {
            List<Employee> lista = _source.AllEmployees();
            int new_id = 1;
            while (lista.Any(x => x.Id == new_id))
            {
                new_id++;
            }
            var newEmployee = new Employee(new_id, firstName, lastName, position);
            _source.AddEmployee(newEmployee);
            _source.SortEmployees();
            return true;
        }
        public bool RemoveEmployee(int id)
        {
            var lista = _source.AllEmployees();
            Employee? doUsuniecia = lista.FirstOrDefault(x => x.Id == id);
            if (doUsuniecia is null) return false;
            _source.RemoveEmployee(doUsuniecia);
            return true;
        }
    }
}
