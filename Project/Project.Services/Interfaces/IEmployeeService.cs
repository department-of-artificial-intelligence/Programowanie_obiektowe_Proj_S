using Project.Model;

namespace Project.Services.Interfaces;

public interface IEmployeeService
{
    void ChnagePosition(Employee employee, string role);
}