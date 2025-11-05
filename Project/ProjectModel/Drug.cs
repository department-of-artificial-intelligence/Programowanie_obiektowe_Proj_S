using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Drug
    {
        public string Name { get; set; }
        public string TypeOfMedicine { get; set; }

        public double Price { get; set; }
        public string Description { get; set; }

        public Drug(string name, string typeOfMedicine, double price, string description)
        {
            Name = name;
            TypeOfMedicine = typeOfMedicine;
            Price = price;
            Description = description;
        }
        public Drug() : this(string.Empty, string.Empty, 0, string.Empty) { }

        public override string ToString()
        {
            return $"Nazwa: {Name}, Typ: {TypeOfMedicine}, Cena: {Price}, Opis: {Description}";
        }
    }
}
