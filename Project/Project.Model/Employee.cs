using RestaurantManagement.Models.Enums;
using System;

namespace RestaurantManagement.Models
{
    public class Employee : Person
    {
        public int Id { get; set; } // PK dla EF

     public int RestaurantId { get; set; }       // FK
        public Restaurant? Restaurant { get; set; } // Nawigacja
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
