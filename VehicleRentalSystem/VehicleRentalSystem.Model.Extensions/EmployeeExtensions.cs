using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model.Extensions
{
    public static class EmployeeExtensions
    {
        public static string GetFullName(this Employee employee)
        {
            return $"{employee.FirstName} {employee.LastName}";
        }
        public static string GetFullNameWithTitle(this Employee employee)
        {
            return $"{employee.FirstName} {employee.LastName} - {employee.Title}";
        }
        public static bool IsInManagement(this Employee employee)
        {
            return employee.Title == JobTitle.Manager ||
                   employee.Title == JobTitle.Kierownik ||
                   employee.Title == JobTitle.Dyrektor;
        }
        public static bool CanBePromoted(this Employee employee)
        {
            return employee.Title != JobTitle.Dyrektor;
        }
        public static JobTitle? GetNextTitle(this Employee employee)
        {
            return employee.Title switch
            {
                JobTitle.Stażysta => JobTitle.Asystent,
                JobTitle.Asystent => JobTitle.Pracownik,
                JobTitle.Pracownik => JobTitle.Manager,
                JobTitle.Manager => JobTitle.Kierownik,
                JobTitle.Kierownik => JobTitle.Dyrektor,
                JobTitle.Dyrektor => null,
                _ => null
            };
        }
        public static int GetTitleLevel(this Employee employee)
        {
            return employee.Title switch
            {
                JobTitle.Stażysta => 0,
                JobTitle.Asystent => 1,
                JobTitle.Pracownik => 2,
                JobTitle.Manager => 3,
                JobTitle.Kierownik => 4,
                JobTitle.Dyrektor => 5,
                _ => 0
            };
        }
        public static IEnumerable<Employee> GetManagementStaff(this IEnumerable<Employee> employees)
        {
            return employees
                .Where(e => e.IsInManagement())
                .OrderBy(e => e.Title);
        }
        public static IEnumerable<Employee> OrderByTitle(this IEnumerable<Employee> employees)
        {
            return employees.OrderByDescending(e => e.GetTitleLevel());
        }
    }
}
