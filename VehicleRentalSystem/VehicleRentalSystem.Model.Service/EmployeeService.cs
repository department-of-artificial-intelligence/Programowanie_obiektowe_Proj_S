using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model;
using VehicleRentalSystem.Model.Extensions;

namespace VehicleRentalSystem.Model.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task HireEmployee(Employee employee)
        {
            if (employee == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie można dodać pustych danych pracownika!");
                return;
            }

            if (string.IsNullOrWhiteSpace(employee.FirstName) ||
                string.IsNullOrWhiteSpace(employee.LastName))
            {
                Console.WriteLine("\n[BŁĄD] Pracownik musi mieć imię i nazwisko!");
                return;
            }

            var department = await _context.Departments.FindAsync(employee.DepartmentId);
            if (department == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono placówki o takim ID!");
                return;
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            Console.WriteLine($"\n[INFO] Pracownik {employee.FirstName} {employee.LastName} został zatrudniony w placówce {department.Name} jako {employee.Title}.");
        }


        public async Task RankupEmployee(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono pracownika!");
                return;
            }

            if (!employee.CanBePromoted())
            {
                Console.WriteLine("\n[BŁĄD] Ten pracownik ma już najwyższe stanowisko!");
                return;
            }

            var oldTitle = employee.Title;
            var newTitle = employee.GetNextTitle();

            if(newTitle == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie można pobrać nowego stanowiska!");
                return;
            }

            employee.Title = newTitle.Value;
            await _context.SaveChangesAsync();

            Console.WriteLine($"\n[INFO] Awansowano {employee.GetFullName()} z {oldTitle} na {newTitle}!");
        }


        public async Task FireEmployee(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono pracownika!");
                return;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            Console.WriteLine($"\n[INFO] Zwolniono pracownika: {employee.GetFullName()} ({employee.Title}) z placówki {employee.Department?.Name ?? "Nie przypisano"}.");
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesToRankup()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.Title != JobTitle.Dyrektor)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployee(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }


        public async Task<int> GetEmployeeCount()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<int> GetManagementCount()
        {
            return await _context.Employees
                .CountAsync(e => e.Title == JobTitle.Dyrektor || e.Title == JobTitle.Kierownik || e.Title == JobTitle.Manager);
        }
    }
}