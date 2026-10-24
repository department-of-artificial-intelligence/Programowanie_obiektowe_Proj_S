using Project.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Model
{
    public class Storage
    {
        public List<StockItem> StockItems { get; set; }
        public decimal MaxCapacity { get; set; }

        public Storage(List<StockItem> stockItems, decimal maxCapacity)
        {
            StockItems = stockItems;
            MaxCapacity = maxCapacity;
        }
        public Storage( decimal maxCapacity)
        {
            StockItems = [];
            MaxCapacity = maxCapacity;
        }
    }

    
}
