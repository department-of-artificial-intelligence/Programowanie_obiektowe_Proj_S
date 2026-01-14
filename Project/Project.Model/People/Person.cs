using System;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{

    public abstract class Person
    {
        

        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }


        private string _phoneNumber;

        public required string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                
                if (string.IsNullOrWhiteSpace(value)) return;

                
                string cleanedValue = value.Trim();
                string pattern = @"^\+\d{2}\d{9}$";

                if (!Regex.IsMatch(cleanedValue, pattern))
                {
                    throw new ArgumentException($"Numer telefonu musi być w formacie: +XXYYYYYYYYY. Otrzymano: '{value}'");
                }

               
                _phoneNumber = cleanedValue;
            }
        }


        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                
                if (string.IsNullOrWhiteSpace(value)) return;

                string cleanedValue = value.Trim();
                string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$";

                if (!Regex.IsMatch(cleanedValue, emailPattern))
                {
                    throw new ArgumentException($"Niepoprawny format adresu email: '{value}'");
                }

                
                _email = cleanedValue;
            }
        }


        [SetsRequiredMembers]
        public Person() { }

        public Person(string firstName, string lastName, string phoneNumber, string email)
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