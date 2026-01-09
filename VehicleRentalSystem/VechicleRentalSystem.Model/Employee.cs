using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public JobTitle? Title { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public Employee() { }
        public Employee(string firstName, string lastName, JobTitle title)
        {
            FirstName = firstName;
            LastName = lastName;
            Title = title;
        }

        public override string ToString()
        {
            return $"[PRACOWNIK - ID: {Id}] {FirstName} {LastName} - {Title}";
        }
    }
}
