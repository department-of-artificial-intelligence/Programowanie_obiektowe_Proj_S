using Project.DAL;
using Project.Model.Interfaces;
using Project.Model.People;
using Project.Model.Stores;
using System.Collections.Generic;
using System.Linq;

namespace Project.Dal.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;



        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public List<Employee> GetAll()
        {
            return _db.Employees.ToList();
        }


        public List<Employee> GetAll(int storeId)
        {
            return _db.Employees
                      .Where(e => e.StoreId == storeId)
                      .ToList();
        }


        public Employee GetById(int id)
        {
            return _db.Employees.FirstOrDefault(e => e.Id == id);
        }


        public bool Add(Employee employee)
        {
            if (employee == null) return false;

            _db.Employees.Add(employee);
            _db.SaveChanges();
            return true;
        }



        public bool Update(Employee employee)
        {
            
            if (employee == null) return false;

            try
            {
                
                _db.Employees.Update(employee);

                
                _db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }



        public bool Remove(Employee employee)
        {
            if (employee == null) return false;

            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return true;
        }
    }
}