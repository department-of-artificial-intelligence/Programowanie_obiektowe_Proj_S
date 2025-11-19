using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class Customer: Person
    {

        public required int CustomerId { get; set; }

        public required string CustomerEmail { get; set; }

        public required int CustomerPhone { get; set; }

        public required string CustomerCity { get; set; }

        public required string CustomerRegion { get; set; }

        public required string CustomerPostalCode { get; set;
        
        }




        public Customer() : base() { }

        
        public Customer(int customerId, string customerFirstName, string customerLastName, string customerEmail, int customerPhone, string customerCity, string customerRegion, string customerPostalCode): base(customerFirstName, customerLastName)
        {
            if(customerId < 0) throw new ArgumentException("Id nie może być wartością ujemną", nameof(customerId));


            
            CustomerEmail = customerEmail ?? "";
            CustomerPhone = customerPhone;
            CustomerCity = customerCity ?? "";
            CustomerRegion = customerRegion ?? "";
            CustomerPostalCode = customerPostalCode ?? "";

        }




    }
}
