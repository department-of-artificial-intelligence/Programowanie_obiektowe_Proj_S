using Projekt.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt.Model;
using Microsoft.EntityFrameworkCore;

namespace Projekt.DATABASE.Logic
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> ? _employees;
        private readonly ApplicationDbContext? _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public void Add(Employee employee) 
        {
            if (_context is null)
            {
                throw new Exception("Database context is not initialized.ERROR:E1");
            }
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }
        public void Remove(Employee employee) 
        {
            if (_context is null)
            {
                throw new Exception("Database context is not initialized.ERROR:E2");
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public Employee GetByID(int id)
        {
            if (_context is null)
            {
                throw new Exception("Database context is not initialized.ERROR:E3");
            }

            var employee = _context.Employees
                .Include(e=>e.Cinema)
                .FirstOrDefault(e => e.ID == id);
            
            if (employee == null)
            {
                throw new Exception($"Employee with id:{id} not found.ERROR:E3");
            }
            return employee;
        }

        public Employee GetByName(string name)
        {
            if (_context is null)
            {
                throw new Exception("Database context is not initialized.ERROR:E4");
            }
            var employee = _context.Employees.FirstOrDefault(e => e.Name == name);
            if (employee == null)
            {
                throw new Exception($"Employee with name:{name} not found.ERROR:E4");
            }
            return employee;
        }

        public IReadOnlyList<Employee> GetAll()
        {
            if (_context is null)
            {
                throw new Exception("Database context is not initialized.ERROR:E5");
            }
            return _context.Employees
                .Include(e =>e.Cinema)
                .ToList().AsReadOnly();
        }

        public void RemoveByID(int id) 
        {
            var employee = GetByID(id);
            if (employee is not null)
            {
                if (_context is null)
                {
                    throw new Exception("Database context is not initialized.ERROR:E6");
                }
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}
