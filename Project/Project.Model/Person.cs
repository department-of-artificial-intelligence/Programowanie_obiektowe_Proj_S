using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ConsoleApp
{
    public abstract class Person : IContactable, IIdentifiable
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public string GetFullName() => $"{FirstName} {LastName}";

        public abstract void UpdateContactInfo(string phone, string email);

        public int GetId() => Id;







    }
}
