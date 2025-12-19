using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Owner : Person
    {
        public List<Animal> Animals { get; set; } = new List<Animal>();

        public Owner() : base() { }

        public Owner(string firstName, string lastName, string? email = null, string? phone = null)
            : base(firstName, lastName, email, phone)
        {


        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}