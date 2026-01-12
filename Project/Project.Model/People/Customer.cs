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


        public required decimal WalletBalance { get; set; }


        [SetsRequiredMembers]
        public Customer() { }

        [SetsRequiredMembers]
        public Customer(string firstName, string lastName, string phone, string email)
            : base(firstName, lastName, phone, email)
        {
            Orders = new List<Order>();
            WalletBalance = 0;
        }

        public override string GetInfo()
        {
            string idInfo = CustomerId == 0 ? "NEW" : CustomerId.ToString();

            string result = $"[CUSTOMER #{idInfo}] {base.GetInfo()} | Zamówień: {Orders.Count}";

            if (Orders.Count > 0)
            {
                result += "\nLista zamówień:";
                foreach (var order in Orders)
                {
                    result += $"\n\t -> {order}";
                }
            }

            return result;
        }
    }
}
