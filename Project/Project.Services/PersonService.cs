using System.Collections.Generic;
using System.Linq;
using Project.DAL;
using Project.Model;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCustomer(string firstName, string lastName, string address)
        {
            _context.Customers.Add(new Customer(firstName, lastName, address));
            _context.SaveChanges();
        }

        public void AddEmployee(string firstName, string lastName, EmployeeRole role)
        {
            _context.Employees.Add(new Employee(firstName, lastName, role));
            _context.SaveChanges();
        }

        public List<Person> GetAllPeople(string sortBy = null)
        {
            var query = _context.People.AsQueryable();

            
            switch (sortBy?.ToLower())
            {
                case "name":
                    query = query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
                    break;
                case "type":
                    
                    query = query.OrderBy(p => p.GetType().Name);
                    break;
            }

            return query.ToList();
        }
    }
}