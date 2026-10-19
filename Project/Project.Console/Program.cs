using Project.Model;

namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestaurantChain chain = new RestaurantChain();
            City city = new City("Krakow");
            Restaurant rest1 = new Restaurant(1, "fajno tu", "ul Damba, 1", "+12312321321");
            Restaurant rest2 = new Restaurant(1, "fajno tu", "ul Damba, 1", "+12312321321");
            Restaurant rest3 = new Restaurant(1, "fajno tu", "ul Damba, 1", "+12312321321");
            city.AddRestaurant(rest1);
            city.AddRestaurant(rest2);
            city.AddRestaurant(rest3);

            City city2 = new City("wroclaw");

            chain.AddCity(city);
            chain.AddCity(city2);
            //if (chain.FindRestaurants("Krakow") != null) {
            //    Console.WriteLine($"{chain.Сities[0].Name}");
            //}
            chain.GetAllRestaurants();

            Employee emp1 = new Employee(1, "ffr", "frfr", "casier", 1234);
            rest1.AddEmployee(emp1);
            Console.WriteLine(rest1.Employees[0].FirstName);

        }
    }
}
