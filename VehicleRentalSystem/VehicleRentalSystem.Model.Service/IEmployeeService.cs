using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model.Service
{
    public interface IEmployeeService
    {
        Task HireEmployee(Employee employee);
        Task RankupEmployee(int employeeId);
        Task FireEmployee(int employeeId);
        Task<Employee?> GetEmployee(int employeeId);
        Task<List<Employee>> GetAllEmployees();
        Task<List<Employee>> GetEmployeesToRankup();
        Task<int> GetEmployeeCount();
        Task<int> GetManagementCount();
    }
}

