using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; } 
        public Supplier Suplliers {  get; set; }

        public Product(int id, string name, float price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        public Product()
        {
            Id = 0;
            Name = "";
            Price = 0;
            Quantity = 0;
        }

       
        public void DecreaseQuantity(int quantitySold)
        {
            if (quantitySold <= Quantity)
            {
                Quantity -= quantitySold;
            }
            else
            {
                Console.WriteLine("Niema tyle produktu");
            }
        }
        public override string ToString()
        {
            return $"{Id} {Name} {Price} {Quantity}";
        }

    }

}
