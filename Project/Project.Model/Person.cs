using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Abstraction;

namespace Project.Model
{
    public class Person : IIdentifiable, IContactable
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        
        public Person() { }

        public Person(int id, string firstName, string lastName, string? email = null, string? phone = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }
        
    }
}
