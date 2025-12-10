using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public abstract class Person
    {

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required int PhoneNumber {  get; set; } 




        public Person() { }


        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }


    }
}
