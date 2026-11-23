using Project.Model;
using Project.Model.DataModels;

namespace Project.Services.Interfaces;

public interface IStorageService
{
    void AddStock(
        Storage storage,
        Ingridient ingridient,
        decimal quantity);

    void UseStock(
        Storage storage,
        Ingridient ingridient,
        decimal quantity);

    List<StockItem> GetLowStockItems(Storage storage);

    decimal GetStockValue(Storage storage);
}