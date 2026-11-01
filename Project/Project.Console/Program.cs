using Project.Model;
using Project.Model.DataModels;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("===== WEEK 3: STORAGE TESTS =====\n");

            // ==========================================
            // 1. CREATE INGREDIENTS
            // ==========================================

            Ingridient cheese = new Ingridient(1, "Cheese", "kg", 25.00m);
            Ingridient tomato = new Ingridient(2, "Tomato", "kg", 8.00m);
            Ingridient flour = new Ingridient(3, "Flour", "kg", 4.00m);

            Console.WriteLine("===== INGREDIENT TESTS =====");

            Console.WriteLine(
                $"{cheese.Name}: {cheese.UnitPrice} PLN/{cheese.Unit}"
            );

            // Test UpdatePrice()
            cheese.UpdatePrice(28.00m);

            Console.WriteLine(
                $"Updated cheese price: {cheese.UnitPrice} PLN/{cheese.Unit}"
            );




            // ==========================================
            // 2. CREATE STORAGE
            // ==========================================

            Storage storage = new Storage(100m);

            Console.WriteLine("\n===== STORAGE TESTS =====");

            // Test AddStock()
            storage.AddStock(cheese, 10m);
            storage.AddStock(tomato, 20m);
            storage.AddStock(flour, 5m);

            Console.WriteLine("Stock added successfully.");


            // ==========================================
            // 3. DISPLAY CURRENT STOCK
            // ==========================================

            Console.WriteLine("\n===== CURRENT STOCK =====");

            foreach (StockItem item in storage.StockItems)
            {
                Console.WriteLine(
                    $"{item.Ingridient.Name}: " +
                    $"{item.Quantity} {item.Ingridient.Unit}"
                );
            }


            // ==========================================
            // 4. TEST STOCKITEM METHODS
            // ==========================================

            Console.WriteLine("\n===== STOCK ITEM TESTS =====");

            StockItem? cheeseStock = storage.StockItems.Find(
                item => item.Ingridient.Id == cheese.Id
            );

            if (cheeseStock != null)
            {
                Console.WriteLine(
                    $"Initial cheese quantity: {cheeseStock.Quantity}"
                );

                // Test Increase()
                cheeseStock.Increase(5m);

                Console.WriteLine(
                    $"After Increase(5): {cheeseStock.Quantity}"
                );

                // Test Decrease()
                cheeseStock.Decrease(3m);

                Console.WriteLine(
                    $"After Decrease(3): {cheeseStock.Quantity}"
                );

                // Test IsLowStock()
                Console.WriteLine(
                    $"Is cheese stock low? {cheeseStock.IsLowStock()}"
                );
            }


            // ==========================================
            // 5. TEST USE STOCK
            // ==========================================

            Console.WriteLine("\n===== USE STOCK TEST =====");

            storage.UseStock(cheese, 4m);

            Console.WriteLine("Used 4 kg of cheese.");

            foreach (StockItem item in storage.StockItems)
            {
                Console.WriteLine(
                    $"{item.Ingridient.Name}: " +
                    $"{item.Quantity} {item.Ingridient.Unit}"
                );
            }


            // ==========================================
            // 6. TEST LOW STOCK ITEMS
            // ==========================================

            Console.WriteLine("\n===== LOW STOCK ITEMS =====");

            var lowStockItems = storage.GetLowStockItems();

            foreach (StockItem item in lowStockItems)
            {
                Console.WriteLine(
                    $"{item.Ingridient.Name}: " +
                    $"{item.Quantity} {item.Ingridient.Unit}"
                );
            }


            // ==========================================
            // 7. TEST STOCK VALUE
            // ==========================================

            Console.WriteLine("\n===== STOCK VALUE =====");

            Console.WriteLine(
                $"Total stock value: {storage.GetStockValue()} PLN"
            );


            // ==========================================
            // 8. FINISH
            // ==========================================

            Console.WriteLine("\n===== ALL TESTS FINISHED =====");
        }
    }
}
