using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsRental.Model;

namespace CarsRental.Logic
{
    public interface IDepartmentService
    {
        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        void RemoveDepartment(int DepartmentId);
        Department? GetDepartment(int DepartmentId);
        List<Department> GetAllDepartments();
    }
}
