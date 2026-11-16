using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Model.DataModels;

namespace Project.Services.Services;

    public class StorageServices
    {
    public void AddStock(Storage storage, Ingridient ingridient, decimal quantity)
    {
        ArgumentNullException.ThrowIfNull(ingridient);
        ArgumentNullException.ThrowIfNull(quantity);
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Nie może być <=0");
        StockItem item = new StockItem(ingridient, quantity);
        storage.StockItems.Add(item);
    }
    public void UseStock(Storage storage, Ingridient ingridient, decimal quantity)

    {
        StockItem? foundedStockItem = storage.StockItems.Find(i => i.Ingridient.Name == ingridient.Name);
        ArgumentNullException.ThrowIfNull(foundedStockItem);
        if (foundedStockItem.Quantity - quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Not enough stock items for this operation");
        foundedStockItem.Quantity -= quantity;
    }

    public List<StockItem> GetLowStockItems(Storage storage)
    {
        return storage.StockItems.FindAll(i => i.IsLowStock());
    }

    public decimal GetStockValue(Storage storage)
    {
        return storage.StockItems.Sum<StockItem>(i => i.Quantity * i.Ingridient.UnitPrice);
    }
}

