using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Model
{
    abstract class Person
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber; 
        public int Id {
            get => _id;
            set
            {
                if (value < 0) throw new ArgumentException("Id<0");
                _id = value;
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("EmptyOrNull");
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("EmptyOrNull");
                _lastName = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("EmptyOrNull");
                _email = value;
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("EmptyOrNull");
                _phoneNumber = value;
            }
        }
        public Person() : this(1, "Empty", "Empty", "Empty", "Empty") { }
        public Person(int id, string firstName, string lastName, string email, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
