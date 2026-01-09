using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalSystem.DAL;
using VehicleRentalSystem.Model;

namespace VehicleRentalSystem.Model.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDepartment(Department department)
        {
            Console.WriteLine($"\n[INFO] Dodawanie placówki...");

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            Console.WriteLine($"\n[INFO] Pomyślnie dodano placówkę {department.Name}!");
        }


        public async Task<bool> UpdateDepartment(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.Id);
            if (existingDepartment == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono takiej placówki!");
                return false;
            }

            existingDepartment.Name = department.Name;
            existingDepartment.City = department.City;
            existingDepartment.StreetAddress = department.StreetAddress;
            existingDepartment.EmailAddress = department.EmailAddress;
            existingDepartment.PhoneNumber = department.PhoneNumber;

            Console.WriteLine($"\n[INFO] Aktualizowanie placówki...");

            await _context.SaveChangesAsync();
            Console.WriteLine($"\n[INFO] Pomyślnie zaktualizowano placówkę [ID: {existingDepartment.Id}]!");
            return true;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _context.Departments
                .Include(d => d.Employees)
                .Include(d => d.Vehicles)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Department?> GetDepartment(int id)
        {
            var dep = await _context.Departments.FindAsync(id);
            if (dep == null)
            {
                Console.WriteLine("\n[BŁĄD] Nie znaleziono placówki o takim ID!");
                return null;
            }
            return dep;
        }

        public async Task<bool> DeleteDepartment(int departmentId)
        {
            var department = await _context.Departments
                .Include(d => d.Employees)
                .Include(d => d.Vehicles)
                .AsSplitQuery()
                .FirstOrDefaultAsync(d => d.Id == departmentId);

            if (department == null)
            {
                Console.WriteLine("\n[BŁĄD] Placówka o podanym ID nie została znaleziona!");
                return false;
            }

            if (department.Employees.Any() || department.Vehicles.Any())
            {
                Console.WriteLine("\n[BŁĄD] Nie można usunąć placówki, która ma przypisanych pracowników lub pojazdy!");
                return false;
            }

            Console.WriteLine($"\n[INFO] Usuwanie placówki...");

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            Console.WriteLine($"\n[INFO] Pomyślnie usunięto placówkę [{department.Id}] {department.Name}!");
            return true;
        }

        public async Task<int> GetDepartmentCount()
        {
            return await _context.Departments.CountAsync();
        }
    }
}