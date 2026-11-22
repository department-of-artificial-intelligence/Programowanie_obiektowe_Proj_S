

using Project.Model;

namespace Project.Services.Services
{
    public class EmployeeService
    {
        public void ChnagePosition(Employee employee, string role)
        {
            employee.FirstName = role;
        }
    }
}
