using Project.Model.People;
using Project.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Interfaces
{
    public interface IEmployeeManager
    {
        List<Employee> AllEmployees();


        void HireEmployee(Store store, Employee employee);
        void FireEmployee(Store store, int employeeId);
        void ChangePosition(Employee employee, EmployeePosition newPosition, decimal newSalary);
        decimal CalculatePayroll(Store store);

        Employee FindById(int id);
    }
}
