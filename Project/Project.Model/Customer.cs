using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Customer : Person
    {
        
        private int _customerId;

        
        public required int CustomerId
        {
            get { return _customerId; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(CustomerId), "ID cannot be negative!");
                }
                _customerId = value;
            }
        }

        public decimal WalletBalance { get; set; }
        public required string City { get; set; }
        public required string Region { get; set; }
        public required string PostalCode { get; set; }



        [SetsRequiredMembers]

        public Customer(int id, string firstName, string lastName, string email, string address, string phone, string city, string region, string postalCode)
            : base(firstName, lastName, phone, email, address) 
        {
            CustomerId = id;
            City = city;
            Region = region;
            PostalCode = postalCode;
            WalletBalance = 0;
        }

       
        private Customer() { }

        
        public override string GetInfo()
        {
            
            return $"[CUSTOMER #{CustomerId}] {base.GetInfo()} | Address: {City}, {PostalCode} | Portfel: {WalletBalance:C}";
        }




    }
}
