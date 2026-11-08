using Project.Model;
using Project.Model.DataModels;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Customer customer = new Customer(
                1,
                "Jan Kowalski",
                "123456789",
                100
            );

            
            Dish soup = new Dish(
                1,
                "Tomato Soup",
                "Traditional tomato soup",
                15.00m,
                "300 ml",
                new List<DishIngridient>(),
                true
            );

            Dish salad = new Dish(
                2,
                "Greek Salad",
                "Salad with feta cheese",
                20.00m,
                "250 g",
                new List<DishIngridient>(),
                true
            );

            
            Restaurant restaurant = new Restaurant(
                1,
                "My Restaurant",
                "Main Street 10",
                "123456789",
                null!,
                null!
            );

            
            List<OrderItem> items = new List<OrderItem>();

            Order order = new Order(
                1,
                DateTime.Now,
                OrderStatus.InProgress,
                items,
                customer,
                restaurant,
                0m
            );

            
            order.AddItem(soup, 2);
            order.AddItem(salad, 3);

            Console.WriteLine("ORDER CREATED");
            Console.WriteLine("------------------------------");

            Console.WriteLine($"Customer: {order.Customer.FullName}");
            Console.WriteLine($"Order status: {order.Status}");

            Console.WriteLine("\nORDER ITEMS:");

            foreach (OrderItem item in order.Items)
            {
                Console.WriteLine(
                    $"{item.Dish.Name} x {item.Quantity} = {item.GetLineTotal()}"
                );
            }

            
            decimal total = order.CalculateTotal();

            Console.WriteLine("\nTOTAL PRICE:");
            Console.WriteLine(total.ToString());

            
            Console.WriteLine("\nREMOVING SALAD...");

            order.RemoveItem(2);

            Console.WriteLine($"New total: {order.CalculateTotal()}");

            
            Console.WriteLine("\nTESTING INVALID QUANTITIES:");

            try
            {
                order.AddItem(soup, 0);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Quantity 0: {ex.Message}");
            }

            try
            {
                order.AddItem(soup, -2);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Quantity -2: {ex.Message}");
            }

            
            Console.WriteLine("\nCANCELING ORDER...");

            order.Cancel();

            Console.WriteLine($"Order status: {order.Status}");

            Console.WriteLine("\nDISPLAY MENU");
            restaurant.AddDish(soup);
            restaurant.AddDish(salad);
            restaurant.GetAvailableDishes();
        }
    }
}
