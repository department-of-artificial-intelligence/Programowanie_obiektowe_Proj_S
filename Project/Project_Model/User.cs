using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public abstract class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Person() { 
            Id=0; 
            FirstName=string.Empty; 
            LastName=string.Empty; 
            Email=string.Empty;
        }
        public Person(int id, string firstName, string lastName, string email){
            Id=id;
            FirstName=firstName;
            LastName=lastName;
            Email=email;
        }
        public override string ToString()
        {
            return $"{Id}: {FirstName} {LastName} ({Email})";
        }
    }
}