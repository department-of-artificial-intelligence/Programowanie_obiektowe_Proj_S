using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class Customer: Person
    {

        public required int CustomerId 
        { 
            get = return _customerId;

            set{
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Id nie może być wartością poniżej 0!");
                }
                 _customerId = value;
            };
           

        }

        public required int CustomerPhone { get; set; }

        public required string CustomerCity { get; set; }

        public required string CustomerRegion { get; set; }

        public required string CustomerPostalCode { get; set;
        
        }




        public Customer() : base() { }

        
        public Customer(int customerId, string customerFirstName, string customerLastName, string customerEmail, int customerPhone, string customerCity, string customerRegion, string customerPostalCode): base(customerFirstName, customerLastName)
        {
            
            CustomerEmail = customerEmail ?? "";
            CustomerPhone = customerPhone;
            CustomerCity = customerCity ?? "";
            CustomerRegion = customerRegion ?? "";
            CustomerPostalCode = customerPostalCode ?? "";

        }




    }
}
