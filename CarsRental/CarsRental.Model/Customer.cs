using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Model
{
    public class Customer : IIdentify
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private string _firstName;
        public string FirstName { get { return _firstName; } set { _firstName = value; } }

        private string _lastName;
        public string LastName { get { return _lastName; } set { _lastName = value; } }

        private string _emailAddress;
        public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value; } }

        public string _phoneNumber;
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }

        public Customer() : this(0, string.Empty, string.Empty, string.Empty, string.Empty) { }
        public Customer(int id, string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _emailAddress = emailAddress;
            _phoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"Klient: {FirstName} {LastName}\nKontakt: {EmailAddress}, {PhoneNumber}\n";
        }
    }
}
