using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class SaleItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public float Subtotal { get; set; } 

        public SaleItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Subtotal = product.Price * quantity;
        }
    }

}
