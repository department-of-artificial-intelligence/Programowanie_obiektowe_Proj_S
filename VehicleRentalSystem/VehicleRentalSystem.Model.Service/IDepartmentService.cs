using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalSystem.Model;

namespace VehicleRentalSystem.Model.Service
{
    public interface IDepartmentService
    {
        Task AddDepartment(Department department);
        Task<bool> UpdateDepartment(Department department);
        Task<bool> DeleteDepartment(int departmentId);
        Task<Department?> GetDepartment(int departmentId);
        Task<List<Department>> GetAllDepartments();
        Task<int> GetDepartmentCount();
    }
}

