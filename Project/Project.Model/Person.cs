using RestaurantManagement.Models.Interfaces;

namespace RestaurantManagement.Models
{
    public class Person
    {
        public int Id { get; set; } // PK dla EF

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required Address Address { get; set; }
        public string FullName => $"{FirstName} {LastName}";

      
    }
}
