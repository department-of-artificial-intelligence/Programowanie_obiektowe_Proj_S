using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class EmployeeManager
    {
        private readonly ISourceEmployee _source;

        public EmployeeManager(ISourceEmployee source) 
        {
            _source = source;
        }
        public bool AddEmployee(string firstName, string lastName, string position, Pharmacy phar)
        {
            if (phar is null) return false;
            var newEmployee = new Employee(firstName, lastName, position, phar);
            if (newEmployee is null) return false;
            if (!_source.AddEmployee(newEmployee)) return false;
            return true;
        }
        public bool RemoveEmployee(int id, Pharmacy phar)
        {
            if (phar is null) return false;
            var lista = _source.AllEmployees().Where(e => e.PharmacyId == phar.Id).ToList();
            Employee? doUsuniecia = lista.FirstOrDefault(x => x.Id == id);
            if (doUsuniecia is null) return false;
            if (!_source.RemoveEmployee(doUsuniecia)) return false;
            return true;
        }
    }
}
