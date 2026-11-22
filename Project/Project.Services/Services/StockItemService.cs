

using Project.Model.DataModels;

namespace Project.Services.Services
{
    public class StockItemService
    {
        public void Increase(StockItem stockItem, decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "must be > 0");
            stockItem.Quantity += quantity;
        }
        public void Decrease(StockItem stockItem, decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "must be > 0");
            if (stockItem.Quantity - quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Not enough stock items for this operation");
            stockItem.Quantity -= quantity;
        }

    }
}
