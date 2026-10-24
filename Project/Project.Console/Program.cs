using Project.Model;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Cities
            City warsaw = new City
            {
                Name = "Warszawa"
            };

            City krakow = new City
            {
                Name = "Kraków"
            };

            // Restaurant chain
            RestaurantChain chain = new RestaurantChain
            {
                Name = "Good Food"
            };

            chain.AddCity(warsaw);
            chain.AddCity(krakow);

            // Employees
            Employee employee1 = new Employee(1, "jan", "Kowalski", "Manager", 6500m);
            //{
            //    Id = 1,
            //    FirstName = "Jan",
            //    LastName = "Kowalski",
            //    Position = "Manager",
            //    Salary = 6500m
            //};

            Employee employee2 = new Employee(2, "ja1n", "Kowalsk1i", "Manage1r", 6500m);

            // Storage
            Storage storage = new Storage(5000);

            // Restaurant
            Restaurant restaurant = new Restaurant(1, "Italiano", "Marszałkowska 10", "+1312312321321", employee1, storage);
            Restaurant restaurant1 = new Restaurant(2, "Italiano1", "Marszałkowska1 10", "+1312312321321312321", employee2, storage);
            //{
            //    Id
            //    Name = "Italiano",
            //    Address = "Marszałkowska 10",
            //    Manager = employee1,
            //    Storage = storage
            //};

            // Adding employee using the method from the diagram
            restaurant.AddEmployee(employee1);
            restaurant.AddEmployee(employee2);

            // Displaying information
            Console.WriteLine("=== RESTAURANT CHAIN ===");
            Console.WriteLine(chain.Name);

            Console.WriteLine("\n=== CITIES ===");
            Console.WriteLine(warsaw.Name);
            Console.WriteLine(krakow.Name);

            Console.WriteLine("\n=== RESTAURANT ===");
            Console.WriteLine(restaurant.Name);
            Console.WriteLine(restaurant.Adress);
            //Console.WriteLine(restaurant.Manager.Name);
            Console.WriteLine(restaurant.Storage.MaxCapacity);

            Console.WriteLine("\n=== EMPLOYEES ===");
            Console.WriteLine(employee1.FirstName + " " + employee1.LastName);
            Console.WriteLine(employee2.FirstName + " " + employee2.LastName);


            warsaw.AddRestaurant(restaurant);
            warsaw.AddRestaurant(restaurant1);
            List<Restaurant>? founded = chain.FindRestaurants("Warszawa");
            Console.WriteLine(founded?[0].Name);
            if(founded != null)
            foreach(Restaurant res in founded)
            {
                    Console.WriteLine(res.Name);
             }

            try
            {
                warsaw.RemoveRestaurant(21);
            }
            catch (Exception ex) { 
                Console.WriteLine("Restaurant is not removed: " + ex.Message);
            }
            
            List<Restaurant>? founded2 = chain.FindRestaurants("Warszawa");

            
            if (founded2 != null)
                foreach (Restaurant res in founded2)
                {
                    Console.WriteLine(res.Name);
                }
        }
    }
}
