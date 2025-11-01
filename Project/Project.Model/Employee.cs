using RestaurantManagement.Models.Enums;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Emit;

namespace RestaurantManagement.Models
{
    public class Employee : Person
    {
        public EmployeeType EmployeeType { get; set; }
        public int Salary { get; set; }
        public required DateTime HiredOn { get; set; }
        public DateTime? FiredOn { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({EmployeeType})";
        }
    }
}
