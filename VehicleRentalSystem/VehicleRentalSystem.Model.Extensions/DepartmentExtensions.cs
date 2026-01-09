using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model.Extensions
{
    public static class DepartmentExtensions
    {
        public static string GetFullAddress(this Department department)
        {
            return $"{department.StreetAddress}, {department.City}";
        }
        public static int GetAvailableVehiclesCount(this Department department)
        {
            return department.Vehicles?.Count(v => !v.IsRented) ?? 0;
        }
        public static int GetRentedVehiclesCount(this Department department)
        {
            return department.Vehicles?.Count(v => v.IsRented) ?? 0;
        }
        public static IEnumerable<Vehicle> GetAvailableVehicles(this Department department)
        {
            return department.Vehicles?.Where(v => !v.IsRented) ?? Enumerable.Empty<Vehicle>();
        }
        public static int GetEmployeeCount(this Department department)
        {
            return department.Employees?.Count ?? 0;
        }
    }
}
