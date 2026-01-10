using Project.Model.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Project.Model
{
    public class Store
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<Employee> Staff { get; set; }
        public List<Product> Inventory { get; set; }

        private string _phoneNumber {  get; set; }
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


        public Store(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
            Staff = new List<Employee>();
            Inventory = new List<Product>();
        }

        


        public override string ToString()
        {
            return $"Sklep #{Id}: {Name} ({City}) | Tel: {PhoneNumber}";
        }
    }
}