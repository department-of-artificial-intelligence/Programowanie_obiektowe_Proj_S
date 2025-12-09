using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class EmployeeInMemory : ISourceEmployee
    {
        public List<Employee> Employees { get; private set; }

        public EmployeeInMemory(List<Employee> employees)
        {
            Employees = employees;
        }
        public List<Employee> AllEmployees()
        {
            return Employees;
        }
        public bool AddEmployee(Employee employee)
        {
            if (employee is null) return false;
            Employees.Add(employee);
            return true;
        }
        public bool RemoveEmployee(Employee employee)
        {
            if (employee is null) return false;
            Employees.Remove(employee);
            return true;
        }
    }
}
