using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.DataModels
{
    public class StockItem
    {
        public Ingridient Ingridient { get; set; }
        public decimal Quantity { get; set; }


        public StockItem(Ingridient ingridient, decimal quantity) 
        {
            Ingridient = ingridient;
            Quantity = quantity;
           
        }

        public void Increase(decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "must be > 0");
            Quantity += quantity;
        }
        public void Decrease(decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "must be > 0");
            if(Quantity-quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Not enough stock items for this operation");
            Quantity -= quantity;
        }
        public bool IsLowStock()
        {
            if (Quantity < 10) return true;
            return false;
        }

    }
}
