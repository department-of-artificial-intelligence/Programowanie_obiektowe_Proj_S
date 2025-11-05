using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Position { get; set; }

        public Employee(int id, string firstName, string lastName, string position)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
        }
        public Employee() : this(0, string.Empty, string.Empty, string.Empty) { }

        public override string ToString()
        {
            return $"Id: {Id}, Imie: {FirstName}, Nazwisko:{LastName}, Stanowisko: {Position}";
        }
    }
}
