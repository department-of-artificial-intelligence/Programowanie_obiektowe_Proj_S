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

        public void AddStock(Ingridient ingridient, decimal quantity)
        {
            ArgumentNullException.ThrowIfNull(ingridient);
            ArgumentNullException.ThrowIfNull(quantity);
            if(quantity <= 0) 
                throw new ArgumentOutOfRangeException(nameof(quantity), "Nie może być <=0");
            StockItem item = new StockItem(ingridient, quantity);
            StockItems.Add(item);
        }
        public void UseStock(Ingridient ingridient, decimal quantity)

        {
            StockItem? foundedIngridient = StockItems.Find(i => i.Ingridient.Name == ingridient.Name);

        }
    }

    
}
