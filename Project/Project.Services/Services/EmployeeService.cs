

using Project.Model;
using Project.Services.Interfaces;

namespace Project.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        public void ChnagePosition(Employee employee, string role)
        {
            employee.FirstName = role;
        }
    }
}
