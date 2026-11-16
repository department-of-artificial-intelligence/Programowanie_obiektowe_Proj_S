using Project.Model;
using Project.Model.DataModels;
using Project.Services.Services;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Ingridient tomato = new Ingridient(
                1, "Tomato", "kg", 10m);

            Ingridient cheese = new Ingridient(
                2, "Cheese", "kg", 30m);


            StockItem tomatoStock = new StockItem(
                tomato, 5m);

            StockItem cheeseStock = new StockItem(
                cheese, 2m);

            List<StockItem> stockItems = new List<StockItem>();

            stockItems.Add(tomatoStock);
            stockItems.Add(cheeseStock);

            Storage storage = new Storage(stockItems, 100m);

            List<DishIngridient> soupIngredients =
                new List<DishIngridient>();

            soupIngredients.Add(
                new DishIngridient(tomato, 0.2m));

            Dish soup = new Dish(
                1,
                "Tomato Soup",
                "Traditional tomato soup",
                15m,
                "300 ml",
                soupIngredients,
                true
            );

            List<DishIngridient> saladIngredients =
                new List<DishIngridient>();

            saladIngredients.Add(
                new DishIngridient(tomato, 0.1m));

            saladIngredients.Add(
                new DishIngridient(cheese, 0.15m));

            Dish salad = new Dish(
                2,
                "Greek Salad",
                "Salad with cheese",
                20m,
                "250 g",
                saladIngredients,
                true
            );

            Restaurant restaurant = new Restaurant(
                1,
                "My Restaurant",
                "Main Street 10",
                "123456789",
                null!,
                storage
            );

            restaurant.AddDish(soup);
            restaurant.AddDish(salad);

            Customer customer = new Customer(
                1,
                "Jan Kowalski",
                "123456789",
                100
            );

            
            OrderServices service = new OrderServices();


            Console.WriteLine("=== TEST 1: SUCCESSFUL ORDER ===");

            Order order1 = new Order(
                1,
                DateTime.Now,
                OrderStatus.InProgress,
                new List<OrderItem>(),
                customer,
                restaurant,
                0m
            );

            

            order1.AddItem(soup, 2);
            order1.AddItem(salad, 3);

            

            Console.WriteLine("Storage BEFORE:");
            Console.WriteLine($"Tomato: {tomatoStock.Quantity}");
            Console.WriteLine($"Cheese: {cheeseStock.Quantity}");

            try
            {
                decimal total = service.ProcessOrder(order1);

                Console.WriteLine($"Total: {total}");
                Console.WriteLine($"Status: {order1.Status}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            Console.WriteLine("Storage AFTER:");
            Console.WriteLine($"Tomato: {tomatoStock.Quantity}");
            Console.WriteLine($"Cheese: {cheeseStock.Quantity}");

            Console.WriteLine("\n=== TEST 2: NOT ENOUGH STOCK ===");

            Order order2 = new Order(
                2,
                DateTime.Now,
                OrderStatus.InProgress,
                new List<OrderItem>(),
                customer,
                restaurant,
                0m
            );

            order2.AddItem(salad, 20);

            decimal tomatoBefore = tomatoStock.Quantity;
            decimal cheeseBefore = cheeseStock.Quantity;

            try
            {
                service.ProcessOrder(order2);
                Console.WriteLine("ERROR: Order should have failed!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Expected error: {ex.Message}");
            }

            Console.WriteLine(
                $"Tomato unchanged: {tomatoStock.Quantity == tomatoBefore}");

            Console.WriteLine(
                $"Cheese unchanged: {cheeseStock.Quantity == cheeseBefore}");

            Console.WriteLine("\n=== TEST 3: DOUBLE PROCESSING ===");

            try
            {
                service.ProcessOrder(order1);
                Console.WriteLine("ERROR: Order processed twice!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Expected error: {ex.Message}");
            }

            Console.WriteLine("\n=== TEST 4: CANCEL ORDER ===");

            Order order3 = new Order(
                3,
                DateTime.Now,
                OrderStatus.InProgress,
                new List<OrderItem>(),
                customer,
                restaurant,
                0m
            );

            order3.AddItem(soup, 1);

            service.CancelOrder(order3);

            Console.WriteLine($"Status: {order3.Status}");

            try
            {
                service.ProcessOrder(order3);
                Console.WriteLine("ERROR: Canceled order processed!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Expected error: {ex.Message}");
            }

            Console.WriteLine("\n=== ALL TESTS FINISHED ===");
        }
    }
}
