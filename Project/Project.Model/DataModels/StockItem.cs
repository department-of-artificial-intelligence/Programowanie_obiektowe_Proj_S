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

    }
}
