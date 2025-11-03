using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Person {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person() : this(string.Empty, string.Empty) { }
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
