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

        public int PharmacyId { get; set; }

        public Pharmacy Pharmacy { get; set; }
        public Employee(string firstName, string lastName, string position, Pharmacy pharmacy)
        {
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Pharmacy = pharmacy;
        }
        public Employee() : this(string.Empty, string.Empty, string.Empty, new Pharmacy()) { }

        public override string ToString()
        {
            return $"Id: {Id}, Imie: {FirstName}, Nazwisko: {LastName}, Stanowisko: {Position}";
        }
    }
}
