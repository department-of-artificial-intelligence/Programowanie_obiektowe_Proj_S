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
            StockItem? foundedStockItem = StockItems.Find(i => i.Ingridient.Name == ingridient.Name);
            ArgumentNullException.ThrowIfNull(foundedStockItem);
            if (foundedStockItem.Quantity - quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Not enough stock items for this operation");
            foundedStockItem.Quantity -= quantity;
        }

        public List<StockItem> GetLowStockItems()
        {
            return StockItems.FindAll(i => i.IsLowStock());
        }

        public decimal GetStockValue()
        {
            return StockItems.Sum<StockItem>(i => i.Quantity * i.Ingridient.UnitPrice);
        }
    }

    
}
