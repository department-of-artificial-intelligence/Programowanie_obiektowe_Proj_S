using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Drug
    {
        public int DrugId { get; set; } // Klucz glowny
        public string Name { get; set; }
        public string TypeOfMedicine { get; set; }

        public string Price { get; set; }
        public string Description { get; set; }

        public string IsPrescription { get; set; } = "Nie";
        public int PharmacyId { get; set; } // Klucz obcy
        public Pharmacy Pharmacy { get; set; }

        public Drug() : this(string.Empty, string.Empty, string.Empty, string.Empty, new Pharmacy()) { }
        public Drug(string name, string typeOfMedicine, string price, string description, Pharmacy pharmacy)
        {
            Name = name;
            TypeOfMedicine = typeOfMedicine;
            Price = price;
            Description = description;
            Pharmacy = pharmacy;
        }

        public override string ToString()
        {
            return $"Id: {DrugId}, Nazwa: {Name}, Typ: {TypeOfMedicine}, Cena: {Price}, Opis: {Description}";
        }
    }
}