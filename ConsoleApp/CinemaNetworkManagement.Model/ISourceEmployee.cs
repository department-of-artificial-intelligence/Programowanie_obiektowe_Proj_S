using System.Collections.Generic;

namespace CinemaNetworkManagement.Model
{
    public interface ISourceEmployee
    {
        List<Employee> AllEmployees();
        bool AddEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
    }
}
