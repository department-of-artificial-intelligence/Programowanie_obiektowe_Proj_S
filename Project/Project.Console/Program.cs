using Project.Model;
using Project.Model.DataModels;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Ingridient cheese = new Ingridient(1,"Cheese","kg",25.00m);

            Ingridient tomato = new Ingridient(2, "Tomato", "kg", 8.00m);
            Ingridient onion = new Ingridient(3, "Onion", "g", 80.00m);
           

            DishIngridient cheses = new DishIngridient(cheese, 12);
            DishIngridient tomatoes = new DishIngridient(tomato, 24);
            DishIngridient onions = new DishIngridient(onion, 240);
            List<DishIngridient> dishIngridients = new List<DishIngridient> { cheses, tomatoes };

            Dish pizza = new Dish(1, "Margherita", "Pizza with cheese and tomato", 30.00m, "Medium", dishIngridients, true);
            
            pizza.Ingridients.Add(onions);
           

            // Display dish information
            Console.WriteLine("Dish: " + pizza.Name);
            Console.WriteLine("Description: " + pizza.Description);
            Console.WriteLine("Price: " + pizza.Price);
            Console.WriteLine("Available: " + pizza.IsAvailable);

            
            Console.WriteLine("\nIngredients:");

            foreach (DishIngridient dishIngredient in pizza.Ingridients)
            {
                Console.WriteLine(
                    dishIngredient.Ingridient.Name + " - " +
                    dishIngredient.Quantity + " " +
                    dishIngredient.Ingridient.Unit
                );
            }
        }
    }
}
