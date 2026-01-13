using System.Collections.Generic;
using Project.Model.People;

namespace Project.Model.Interfaces
{
   
    public interface IEmployeeRepository
    {
        bool Add(Employee employee);
        List<Employee> GetAll();

        List<Employee> GetAll(int storeId);

        Employee GetById(int id);
        bool Update(Employee employee);
        bool Remove(Employee employee);
    }
}