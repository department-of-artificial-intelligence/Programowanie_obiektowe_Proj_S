using Project.Model.Orders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.People
{
    public class Customer : Person
    {
        
        public int CustomerId { get; private set; }

       
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

       
        public List<Order> Orders { get; set; } = new List<Order>();


        [SetsRequiredMembers]
        public Customer() { }

        [SetsRequiredMembers]
        public Customer(string firstName, string lastName, string phone, string email)
            : base(firstName, lastName, phone, email)
        {
            
        }

        public override string GetInfo()
        {
            string idInfo = CustomerId == 0 ? "NEW" : CustomerId.ToString();
            
            return $"[CUSTOMER #{idInfo}] {base.GetInfo()} | Zamówień: {Orders.Count}";
        }
    }
}
