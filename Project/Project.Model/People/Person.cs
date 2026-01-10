using System;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{

    public abstract class Person
    {
        private string _phoneNumber;

        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }


        public required string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                string pattern = @"^\+\d{2}\d{9}$";
                if (!Regex.IsMatch(value, pattern))
                {
                    throw new ArgumentException("Numer telefonu musi być w formacie: +XXYYYYYYYYY");
                }
                _phoneNumber = value;
            }
        }

        [SetsRequiredMembers]
        protected Person() { }

        protected Person(string firstName, string lastName, string phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;

        }

        public virtual string GetInfo()
        {
            return $"{FirstName} {LastName}";
        }
    }
}