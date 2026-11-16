using Project.Model;
using Project.Model.DataModels;


namespace Project.Services.Services
{
    public class OrderServices
    {
        public decimal ProcessOrder(Order order)
        {
            if (order.Status == OrderStatus.Done)
                throw new InvalidOperationException("Order is already Done");
            if (order.Status == OrderStatus.Canceled)
                throw new InvalidOperationException("Order is Canceled");

            Storage storage = order.Place.Storage;
            List<(Ingridient ingridient, decimal quintity)> ingridients = [];

            foreach (var item in order.Items)
            {
                foreach (var dishIngridient in item.Dish.Ingridients)
                {
                    ingridients.Add((dishIngridient.Ingridient, dishIngridient.Quantity * item.Quantity));
                }
            }


            foreach (var ingridientItem in ingridients)
            {
                StockItem? foundedItem = storage.StockItems
                    .Find(i => i.Ingridient.Id == ingridientItem.ingridient.Id && i.Ingridient.Name == ingridientItem.ingridient.Name);
                if (foundedItem == null)
                    throw new InvalidOperationException("ingridient doesn`t exist in storage");

            }

            foreach (var ingridientItem in ingridients)
            {
                StockItem? foundedItem = storage.StockItems
                    .Find(i => i.Quantity < ingridientItem.quintity);
                if (foundedItem != null)
                {
                    throw new InvalidOperationException("not enough ingridients in storage");
                }
            }


            try
            {
                StorageServices storageServices = new StorageServices();
                foreach (var ingridientItem in ingridients)
                {
                    storageServices.UseStock(storage, ingridientItem.ingridient, ingridientItem.quintity);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error with storage: " + e.Message);
            }


            order.Status = OrderStatus.Done;
            return order.Price;

        }

        public void CancelOrder(Order order)
        {
            if (order.Status == OrderStatus.Done)
                throw new InvalidOperationException("Order is already Done");
            if (order.Status == OrderStatus.Canceled)
                throw new InvalidOperationException("Order is already Canceled");

            order.Status = OrderStatus.Canceled;
    }
    }
}
