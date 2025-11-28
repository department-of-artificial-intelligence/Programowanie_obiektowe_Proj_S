using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pharmacy.Models
{
    public class Product : ID
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public DateTime BestBeforeDate { get; set; } 
        public bool NeedPrescription { get; set; }
        public bool IsLowStock=> Quantity<10? false: true;

        public bool IsExpired => BestBeforeDate < DateTime.Now;


        public Product() {
            Id = 0;
            Name = "Nirznany";
            Price = 0;
            Quantity = 0;
            Manufacturer = "Nirznany";
            BestBeforeDate = DateTime.MinValue;
            NeedPrescription = false;
        }

        public Product(string name, decimal price, int quantity, string manufacturer, DateTime bestBeforeDate, bool needPrescription)
        {
            Id = 0;
            Name = name ;
            Price = price;
            Quantity = quantity;
            Manufacturer = manufacturer ;
            BestBeforeDate = bestBeforeDate;
            NeedPrescription = needPrescription;
        }
    }
}
