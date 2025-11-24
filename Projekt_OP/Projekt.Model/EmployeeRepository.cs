using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;
        private int _nextId = 1;

        public EmployeeRepository(List<Employee> employees)
        {
            _employees = employees;
            if (_employees.Any())
            {
                _nextId = _employees.Max(x => x.ID) + 1;
            }

        }

        public void Add(Employee employee) 
        {
            employee.ID = ++_nextId;
            _employees.Add(employee);
        }

        public Employee GetByID(int id)
        {
            if (id <= _employees.Count() && id > 0)
            {
                return _employees.FirstOrDefault(x => x.ID == id)!;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Nie ma pracownika o takim ID");
            }
        }

        public Employee GetByName(string name)
        {
            return _employees.FirstOrDefault(x => x.Name == name)!;
        }

        public IReadOnlyList<Employee> GetAll()
        {
            return _employees.AsReadOnly();
        }



    }
}
