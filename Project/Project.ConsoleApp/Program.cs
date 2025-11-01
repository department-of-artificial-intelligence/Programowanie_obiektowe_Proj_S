using RestaurantManagement.Models;
using RestaurantManagement.Models.Enums;

namespace RestaurantManagement.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Restaurant Employee Management ===\n");

            var addresses = new[]
            {
            new Address("Polska", "30-001", "Kraków", "Krakowska 15"),
            new Address("Polska", "20-001", "Warszawa", "Warszawska 10"),
            new Address("Polska", "22-134", "Częstochowa", "Armii Krajowej 69"),
            new Address("Polska", "42-021", "Wrocław", "Główna 5")
            };

            Employee chef1 = new Employee
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                EmployeeType = EmployeeType.Chef,
                Salary = 7000,
                HiredOn = DateTime.Now.AddYears(-2),
                PhoneNumber = "123456789",
                Email = "jan.kowalski@email.com",
                Address = addresses[0],
                DateOfBirth = new DateTime(1985, 5, 12)
            };

            Employee waiter1 = new Employee
            {
                FirstName = "Anna",
                LastName = "Nowak",
                EmployeeType = EmployeeType.Waiter,
                Salary = 3000,
                HiredOn = DateTime.Now.AddYears(-1),
                PhoneNumber = "987654321",
                Email = "anna.nowak@email.com",
                Address = addresses[1],
                DateOfBirth = new DateTime(1995, 3, 20)
            };



            Restaurant restaurant1 = new Restaurant
            {
                Name = "Restauracja Fantazja",
                Address = addresses[0],
                PhoneNumber = "12 345 67 89",
                Email = "kontakt@fantazja.pl",
                OpeningHours = new TimeOnly(9, 0),
                ClosingHours = new TimeOnly(22, 0),
                Employees = new List<Employee> { chef1, waiter1 },
                Menu = new List<MenuItem>(),
                Clients = new List<Person>()
            };

            restaurant1.AddMenus(new List<MenuItem>
        {
             new MenuItem { Name = "Sałatka grecka", Description = "świeże warzywa, feta i oliwki", Price = 30.00f },
             new MenuItem { Name = "Pizza Margherita", Description = "sos pomidorowy, mozzarella, bazylia", Price = 20.00f },
             new MenuItem { Name = "Spaghetti Bolognese", Description = "makaron z sosem mięsnym", Price = 35.00f }
        });

            restaurant1.RemoveMenu("pizza margherita");

            Restaurant restaurant2 = new Restaurant
            {
                Name = "Restauracja Warszawska",
                Address = addresses[1],
                PhoneNumber = "22 123 45 67",
                Email = "kontakt@warszawska.pl",
                OpeningHours = new TimeOnly(11, 0),
                ClosingHours = new TimeOnly(23, 0),
                Employees = new List<Employee>(),
                Menu = new List<MenuItem>(),
                Clients = new List<Person>()
            };


            restaurant2.AddMenus(new List<MenuItem>
        {
            new MenuItem { Name = "Tacos", Description = "tortilla, kurczak, salsa, sałata", Price = 22.00f },
            new MenuItem { Name = "Quesadilla", Description = "ser, tortilla, pomidory", Price = 18.00f },
            new MenuItem { Name = "Guacamole", Description = "awokado, limonka, przyprawy", Price = 15.00f },
            new MenuItem { Name = "Burrito", Description = "wołowina, ryż, fasola, salsa", Price = 28.00f },
        });


            Restaurant restaurant3 = new Restaurant
            {
                Name = "Restauracja Pod Złotymi Łukami",
                Address = addresses[2],
                PhoneNumber = "22 222 22 22",
                Email = "kontakt@złotełuki.pl",
                OpeningHours = new TimeOnly(13, 0),
                ClosingHours = new TimeOnly(23, 0),
                Employees = new List<Employee>(),
                Menu = new List<MenuItem>(),
                Clients = new List<Person>()
            };

            restaurant3.AddMenus(new List<MenuItem>
        {
          new MenuItem { Name = "Burger Classic", Description = "wołowina, ser, sałata, pomidor", Price = 25.00f },
          new MenuItem { Name = "Frytki", Description = "chrupiące, złociste frytki z młodych ziemniaków", Price = 10.00f },
          new MenuItem { Name = "Shake czekoladowy", Description = "mleko, lody, czekolada", Price = 15.00f }
        });

            restaurant2.RemoveMenu("frytki");


            Restaurant restaurant4 = new Restaurant
            {
                Name = "Restauracja JazzOn",
                Address = addresses[3],
                PhoneNumber = "11 213 52 08",
                Email = "kontakt@jazzon.pl",
                OpeningHours = new TimeOnly(16, 0),
                ClosingHours = new TimeOnly(23, 0),
                Employees = new List<Employee>(),
                Menu = new List<MenuItem>(),
                Clients = new List<Person>()
            };


            restaurant4.AddMenus(new List<MenuItem>
        {
           new MenuItem { Name = "Sushi zestaw", Description = "różne rodzaje sushi", Price = 60.00f },
           new MenuItem { Name = "Zupa miso", Description = "tradycyjna japońska zupa", Price = 25.00f },
           new MenuItem { Name = "Tempura warzywna", Description = "warzywa w chrupiącej panierce", Price = 28.00f }
        });

            List<Restaurant> restaurants = new List<Restaurant> { restaurant1, restaurant2, restaurant3, restaurant4 };



            // LINQ: Pobierz wszystkich kelnerów i posortuj po nazwisku
            var allWaiters = restaurants
                    .SelectMany(r => r.GetEmployeesByType(EmployeeType.Waiter))
                    .OrderBy(e => e.LastName)
                    .ThenBy(e => e.FirstName)
                    .ToList();

            Console.WriteLine("=== Kelnerzy w sieci restauracji ===");
            foreach (var w in allWaiters)
            {
                Console.WriteLine(w.ToString());
            }

            // LINQ: Pobierz wszystkich kucharzy
            var allChefs = restaurants
                          .SelectMany(r => r.GetEmployeesByType(EmployeeType.Chef))
                          .OrderBy(e => e.FirstName)
                          .ToList();

            Console.WriteLine("\n=== Kucharze w sieci restauracji ===");
            foreach (var c in allChefs)
            {
                Console.WriteLine(c.ToString());
            }

            // LINQ: Pobierz restauracje otwarte po 10:00
            var openAfterTen = restaurants
                               .Where(r => r.OpeningHours.Hour >= 10)
                               .OrderBy(r => r.Name)
                               .ToList();

            Console.WriteLine("\n=== Restauracje otwarte po 10:00 ===");
            foreach (var r in openAfterTen)
            {
                Console.WriteLine($"{r.Name} - {r.Address.City}");
            }
        }
    }
}
