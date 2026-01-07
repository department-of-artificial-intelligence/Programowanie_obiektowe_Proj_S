using System;
using System.Text.RegularExpressions; 
using System.Diagnostics.CodeAnalysis; 

namespace Project.Model
{
    public abstract class Person
    {
       
        private string _phoneNumber;

        
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
                    
                    throw new ArgumentException("Numer telefonu musi być w formacie: +XXYYYYYYYYY (np. +48123456789)");
                }

                
                _phoneNumber = value;
            }
        }

        protected Person() { }

        [SetsRequiredMembers]
        protected Person(string firstName, string lastName, string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phone; 
            Email = email;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public virtual string GetInfo()
        {
            return $"{GetFullName()} | Tel: {PhoneNumber} | Email: {Email}";
        }
    }
}