using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
namespace Project.Model
{
    public class EmpInMemory : ISourceEmployee
    {
        public List<Employee> Employees { get; private set; }

        public EmpInMemory(List<Employee> employees)
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
        public bool DeleteEmployee(int id)
        {
            if (id >= 0)
            {
                Employees.RemoveAt(id);
                return true;
            }
            return false;
        }
        public bool sortEmployees()
        {
            Employees = Employees.OrderBy(emp => emp.Id).ToList();
            return true;
        }
    }
}
