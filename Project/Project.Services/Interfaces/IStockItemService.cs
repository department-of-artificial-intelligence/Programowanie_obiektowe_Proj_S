using Project.Model;
using Project.Model.DataModels;

namespace Project.Services.Interfaces;

public interface IStockItemService
{
    void Increase(StockItem stockItem, decimal quantity);

    void Decrease(StockItem stockItem, decimal quantity);
}