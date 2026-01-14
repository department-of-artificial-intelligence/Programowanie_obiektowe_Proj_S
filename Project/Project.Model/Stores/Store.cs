using Project.Model.Orders;
using Project.Model.People;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace Project.Model.Stores
{
    public class Store
    {
        public int Id { get; private set; }
        public required string Name { get; set; }

        public string _phoneNumber { get; set; }
        public required string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                string pattern = @"^\+\d{2}\d{9}$";

                if (!Regex.IsMatch(value, pattern))
                {
                    throw new ArgumentException("Numer telefonu sklepu musi być w formacie: +XXYYYYYYYYY (np. +48123456789)");
                }
                _phoneNumber = value;
            }
        }


        public required Address Address { get; set; }
        public List<Employee> Staff { get; set; }
        public List<Product> Inventory { get; set; } = new List<Product>();
        public List<Order> Orders { get; set; }

        

        [SetsRequiredMembers]
        public Store(string name, Address address, string phoneNumber)
        {
            
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Staff = new List<Employee>();
            Inventory = new List<Product>();
            Orders = new List<Order>();

        }


        public Store() { }



        public override string ToString()
        {
            return $"Sklep #{Id}: {Name} ({Address}) | Tel: {PhoneNumber}";
        }
    }
}