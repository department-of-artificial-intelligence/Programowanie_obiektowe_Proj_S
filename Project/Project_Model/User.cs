using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public abstract class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public User(int id, string firstName, string lastName, string email){
            Id=id;
            FirstName=firstName;
            LastName=lastName;
            Email=email;
        }
        public User()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }
        public override string ToString()
        {
            return $"{Id}: {FirstName} {LastName} ({Email})";
        }
    }
}