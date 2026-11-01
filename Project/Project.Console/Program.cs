using Project.Model;
using Project.Model.DataModels;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Ingridient cheese = new Ingridient(1, "Cheese", "kg", 25m);
            Ingridient tomato = new Ingridient(2, "Tomato", "kg", 8m);
            Ingridient flour = new Ingridient(3, "Flour", "kg", 4m);

            cheese.UpdatePrice(27m);

            DishIngridient dishIngridient1 = new DishIngridient(cheese, 10);
            DishIngridient dishIngridient2 = new DishIngridient(cheese, 10);
            List<DishIngridient> dishIngridients = new List<DishIngridient>() { dishIngridient1, dishIngridient2 };
            Dish dish = new Dish(1, "pizza", "blabla", 34, "big", dishIngridients, true);
            Console.WriteLine(dish.Price);
            Console.WriteLine(dish.CalculateCost());
            //Storage storage = new Storage(100m);

            //storage.AddStock(cheese, 10m);
            // storage.AddStock(tomato, 20m);
            //storage.AddStock(flour, 5m);

            //StockItem cheeseStock = storage.items.Find(item => item.ingredient.id == cheese.id);

            //cheeseStock.Increase(5m);
            //cheeseStock.Decrease(3m);

            //Console.WriteLine($"Is cheese stock low: {cheeseStock.IsLowStock()}");

            //storage.UseStock(cheese, 4m);

            //Console.WriteLine("\nLow stock items:");

            //foreach (StockItem item in storage.GetLowStockItems())
            //{
            //    Console.WriteLine($"{item.ingredient.name}: {item.quantity} {item.ingredient.unit}");
            //}

            //Console.WriteLine($"\nTotal stock value: {storage.GetStockValue()}");
        }
    }
}
