using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNetworkManagement
{
    public class Cinema
    {
        private List<Employee> _employees;

        public Cinema()
        {
            _employees = new List<Employee>();
        }

        public bool HireEmployee(Employee employee)
        {
            if (_employees.Contains(employee))
            {
                return false;
            }
            employee.IsEmployed = true;
            _employees.Add(employee);

            return true;
        } 

        public bool FireEmployee(Employee employee)
        {
            if (!_employees.Contains(employee))
            {
                return false;
            }
            employee.IsEmployed = false;
            _employees.Remove(employee);

            return true;
        }
    }
}
