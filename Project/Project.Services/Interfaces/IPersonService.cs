using System.Collections.Generic;
using Project.Model;

namespace Project.Services.Interfaces
{
    public interface IPersonService
    {
        void AddCustomer(string firstName, string lastName, string address);
        void AddEmployee(string firstName, string lastName, EmployeeRole role);
        List<Person> GetAllPeople(string sortBy = null); 
    }
}