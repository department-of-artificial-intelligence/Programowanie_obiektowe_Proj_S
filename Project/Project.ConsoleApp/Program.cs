#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantManagement.DAL;
using RestaurantManagement.Models;


namespace RestaurantManagement
{
    class Program
{
        static void Main(string[] args)
        {
            // ==== KONFIGURACJA HOSTA I DB CONTEXT ====
            IHost _host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var configuration = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=RestaurantDB;Integrated Security=True;Trusted_Connection=yes;TrustServerCertificate=True;";
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(configuration));
                })
                .Build();

            using var scope = _host.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

            //if (db != null)
            //{
            //    db.Database.Migrate();
            //    db.Database.EnsureCreated();
            //}
            //else
            //{
            //    Console.WriteLine("Błąd: brak kontekstu bazy danych!");
            //    return;
            //}

            // =========== MENU GŁÓWNE ===========
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Zarządzanie Siecią Restauracji ===");
                Console.WriteLine("1. Dodaj restaurację");
                Console.WriteLine("2. Lista restauracji");
                Console.WriteLine("3. Wybierz restaurację");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": AddRestaurant(db); break;
                    case "2": ShowRestaurants(db); break;
                    case "3": ManageRestaurant(db); break;
                    case "0": return;
                    default: Console.WriteLine("Niepoprawna opcja!"); break;
                }

                Console.WriteLine("\nKliknij Enter, aby kontynuować...");
                Console.ReadLine();
            }
        }

        // ==================== RESTAURACJE ====================
        static void AddRestaurant(ApplicationDbContext db)
        {
            Console.Write("Nazwa restauracji: ");
            string name = Console.ReadLine();

            Console.Write("Państwo: ");
            string country = Console.ReadLine();

            Console.Write("Kod pocztowy: ");
            string zipCode = Console.ReadLine();
            if (zipCode.Length != 6 || !zipCode.Contains("-"))
            {
                Console.WriteLine("Nieprawidłowy format kodu");
                return;
            }

            Console.Write("Miasto: ");
            string city = Console.ReadLine();

            Console.Write("Ulica: ");
            string street = Console.ReadLine();

            Console.Write("Numer telefonu: ");
            string phoneNumber = Console.ReadLine();
            if (phoneNumber.Length != 9)
            {
                Console.WriteLine("Nieprawidłowy numer telefonu");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();
            if (!email.Contains("@"))
            {
                Console.WriteLine("Nieprawidłowy email");
                return;
            }

            Console.Write("Godzina otwarcia (HH:mm): ");
            TimeOnly opening = TimeOnly.Parse(Console.ReadLine());

            Console.Write("Godzina zamknięcia (HH:mm): ");
            TimeOnly closing = TimeOnly.Parse(Console.ReadLine());

            var address = new Address(country, zipCode, city, street);

            var restaurant = new Restaurant
            {
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                OpeningHours = opening,
                ClosingHours = closing,
                Menu = new List<MenuItem>(),
                Employees = new List<Employee>(),
                //Clients = new List<Person>()
            };


            db.Restaurant.Add(restaurant);
            db.SaveChanges();

            Console.WriteLine("Dodano restaurację i zapisano w bazie!");
        }

        static void ShowRestaurants(ApplicationDbContext db)
        {
            var restaurants = db.Restaurant.Include(r => r.Address).ToList();

            if (!restaurants.Any())
            {
                Console.WriteLine("Brak restauracji.");
                return;
            }

            var sorted = restaurants.OrderBy(r => r.Name).ToList();

            Console.WriteLine("\nLista restauracji:");
            for (int i = 0; i < sorted.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sorted[i].Name} ({sorted[i].Address.City})");
            }
        }

        static void ManageRestaurant(ApplicationDbContext db)
        {
            var restaurants = db.Restaurant.Include(r => r.Address).ToList();
            if (!restaurants.Any())
            {
                Console.WriteLine("Brak restauracji.");
                return;
            }

            ShowRestaurants(db);
            Console.Write("\nWybierz restaurację: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > restaurants.Count)
            {
                Console.WriteLine("Niepoprawny wybór.");
                return;
            }

            var selected = restaurants[choice - 1];

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== {selected.Name} ===");
                Console.WriteLine("1. Pracownicy");
                Console.WriteLine("2. Rezerwacje");
                Console.WriteLine("0. Powrót");

                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1": ManageEmployees(selected, db); break;
                    case "2": ManageReservations(selected, db); break;
                    case "0": return;
                }
            }
        }

        // ==================== PRACOWNICY ====================
        static void ManageEmployees(Restaurant restaurant, ApplicationDbContext db)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Dodaj");
                Console.WriteLine("2. Usuń");
                Console.WriteLine("3. Lista");
                Console.WriteLine("0. Wróć");

                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1": AddEmployee(restaurant, db); break;
                    case "2": RemoveEmployee(restaurant, db); break;
                    case "3": ShowEmployees(restaurant, db); break;
                    case "0": return;
                }
            }
        }

        static void AddEmployee(Restaurant restaurant, ApplicationDbContext db)
        {
            Console.Write("Imię: ");
            string firstName = Console.ReadLine();

            Console.Write("Nazwisko: ");
            string lastName = Console.ReadLine();

            Console.Write("Numer telefonu: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Data urodzenia (yyyy-MM-dd): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth);

            Console.Write("Państwo: ");
            string country = Console.ReadLine();

            Console.Write("Kod pocztowy: ");
            string zipCode = Console.ReadLine();

            Console.Write("Miasto: ");
            string city = Console.ReadLine();

            Console.Write("Ulica: ");
            string street = Console.ReadLine();

            var address = new Address(country, zipCode, city, street);

            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                DateOfBirth = dateOfBirth,
                Address = address,
                HiredOn = DateTime.Now,
                Restaurant = restaurant
            };

            db.Employee.Add(employee);
            db.SaveChanges();

            Console.WriteLine("Pracownik dodany i zapisany do bazy!");
        }

        static void RemoveEmployee(Restaurant restaurant, ApplicationDbContext db)
        {
            Console.Write("Nazwisko: ");
            string name = Console.ReadLine();

            var emp = db.Employee.FirstOrDefault(x =>
                x.LastName.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                x.RestaurantId == restaurant.Id);

            if (emp != null)
            {
                db.Employee.Remove(emp);
                db.SaveChanges();
                Console.WriteLine("Usunięto pracownika z bazy!");
            }
        }

        static void ShowEmployees(Restaurant restaurant, ApplicationDbContext db)
        {
            var employees = db.Employee
                .Where(e => e.RestaurantId == restaurant.Id)
                .ToList();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName}");
            }
        }

        // ==================== REZERWACJE ====================
        static void ManageReservations(Restaurant restaurant, ApplicationDbContext db)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Dodaj");
                Console.WriteLine("2. Usuń");
                Console.WriteLine("3. Lista");
                Console.WriteLine("0. Powrót");

                string opt = Console.ReadLine();
                switch (opt)
                {
                    case "1": AddReservation(restaurant, db); break;
                    case "2": RemoveReservation(restaurant, db); break;
                    case "3": ShowReservation(restaurant, db); break;
                    case "0": return;
                }
            }
        }

        static void AddReservation(Restaurant restaurant, ApplicationDbContext db)
        {
            Console.Write("Nazwisko klienta: ");
            string name = Console.ReadLine();

            Console.Write("Liczba osób: ");
            int.TryParse(Console.ReadLine(), out int people);

            Console.Write("Telefon: ");
            string phone = Console.ReadLine();

            Console.Write("Data (yyyy-MM-dd): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime date);

            Console.Write("Godzina (HH:mm): ");
            TimeOnly.TryParse(Console.ReadLine(), out TimeOnly time);

            var reservation = new Reservation
            {
                CustomerName = name,
                NumberOfPeople = people,
                PhoneNumber = phone,
                Date = date,
                Time = time,
                Restaurant = restaurant
            };

            db.Reservation.Add(reservation);
            db.SaveChanges();

            Console.WriteLine("Rezerwacja dodana i zapisana w bazie!");
        }

        static void RemoveReservation(Restaurant restaurant, ApplicationDbContext db)
        {
            Console.Write("Nazwisko klienta: ");
            string name = Console.ReadLine();

            var res = db.Reservation.FirstOrDefault(x =>
                x.CustomerName.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                x.RestaurantId == restaurant.Id);

            if (res != null)
            {
                db.Reservation.Remove(res);
                db.SaveChanges();
                Console.WriteLine("Rezerwacja usunięta z bazy!");
            }
        }

        static void ShowReservation(Restaurant restaurant, ApplicationDbContext db)
        {
            var reservations = db.Reservation
                .Where(r => r.RestaurantId == restaurant.Id)
                .ToList();

            foreach (var r in reservations)
            {
                Console.WriteLine($"{r.CustomerName} {r.Date:yyyy-MM-dd} {r.Time}");
            }
        }
    }
}
