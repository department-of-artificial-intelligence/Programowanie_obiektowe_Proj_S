using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Model
{
    public class Department : IIdentify
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private string _address;
        public string Address { get { return _address; } set { _address = value; } }

        private string _phoneNumber;
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }

        private string _emailAddress;
        public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value; } }

        public Department() : this(0, string.Empty, string.Empty, string.Empty, string.Empty) { }
        public Department(int id, string name, string address, string phoneNumber, string emailAddress)
        {
            _id = id;
            _name = name;
            _address = address;
            _phoneNumber = phoneNumber;
            _emailAddress = emailAddress;
        }

        public override string ToString()
        {
            return $"'{Name}' {Address}\n{PhoneNumber}, {EmailAddress}\n";
        }
    }
}
