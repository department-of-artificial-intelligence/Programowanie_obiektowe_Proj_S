using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        void Remove(Employee employee);
        Employee GetByID(int id);
        Employee GetByName(string name);
        IReadOnlyList<Employee> GetAll();
    }
}
