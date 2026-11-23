using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Model
{
    class Customer : Person
    {
        private string _driverLicense;
        public string DriverLicense
        {
            get => _driverLicense;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("NoDriverLicense");
                _driverLicense = value;
            }
        }
        public Customer() : base() 
        {
            DriverLicense = "NoDriverLicense";
        }
        public Customer(int id, string firstName, string lastName, string email, string phoneNumber, string driverLicense) : base(id, firstName, lastName, email, phoneNumber)
        {
            DriverLicense = driverLicense;
        }
    }
}
