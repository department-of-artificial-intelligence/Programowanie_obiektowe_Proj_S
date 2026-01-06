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

        public required string Email { get; set; }


        protected Person() { }


        protected Person(string firstName, string lastName, int phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public virtual string GetInfo()
        {
            return $"Name: {GetFullName()} | Contact: {Email}, PhoneNumber: {PhoneNumber}";
        }
    }
}
