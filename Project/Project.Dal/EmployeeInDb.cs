using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Dal;
namespace Project.Dal
{
    public class EmpInDataBase : ISourceEmployee
    {
        private readonly ApplicationDbContext _db;

        public EmpInDataBase(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Employee> AllEmployees()
        {
            return _db.Employees.OrderBy(x => x.Id).ToList();
        }
        public bool AddEmployee(Employee employee)
        {
            if (employee is null) return false;
            _db.Employees.Add(employee);
            _db.SaveChanges();
            return true;
        }
        public bool RemoveEmployee(Employee employee)
        {
            if (employee is null) return false;
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return true;
        }
    }
}
