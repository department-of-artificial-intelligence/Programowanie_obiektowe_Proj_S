using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biura.model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Person() { }
        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
        public Person(Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            Age = person.Age;
        }

        

        public override string ToString()
        {
            return $"{FirstName} {LastName} {Age}";
        }
    }
}
