#nullable disable
using Microsoft.EntityFrameworkCore;
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

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Zarządzanie Siecią Restauracji ===");
                Console.WriteLine("1. Dodaj restaurację");
                Console.WriteLine("2. Lista restauracji");
                Console.WriteLine("3. Wybierz restaurację");
                Console.WriteLine("4. Usuń restaurację");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": AddRestaurant(db); break;
                    case "2": ShowRestaurants(db); break;
                    case "3": ManageRestaurant(db); break;
                    case "4": RemoveRestaurant(db); break;
                    case "0": return;
                    default: Console.WriteLine("Niepoprawna opcja!"); break;
                }

                Console.WriteLine("\nKliknij Enter, aby kontynuować...");
                Console.ReadLine();
            }

            // ==================== RESTAURACJE ====================
            static void AddRestaurant(ApplicationDbContext db)
            {
                Console.Write("Nazwa restauracji: ");
                string name = Console.ReadLine();

                Console.Write("Państwo: ");
                string country = Console.ReadLine();

                Console.Write("Kod pocztowy (xx-xxx): ");
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

                Console.Write("Numer telefonu (9 cyfr): ");
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
                    Reservations = new List<Reservation>()
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
                var restaurants = db.Restaurant
                    .Include(r => r.Address)
                    .Include(r => r.Employees)
                    .Include(r => r.Reservations)
                    .Include(r => r.Menu)
                    .ToList();

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
                    Console.WriteLine("3. Menu");
                    Console.WriteLine("0. Powrót");
                    Console.Write("Wybierz opcję: ");

                    string opt = Console.ReadLine();

                    switch (opt)
                    {
                        case "1": ManageEmployees(selected, db); break;
                        case "2": ManageReservations(selected, db); break;
                        case "3": ManageMenu(selected, db); break;
                        case "0": return;
                        default: Console.WriteLine("Niepoprawna opcja!"); break;
                    }
                }
            }

            // ==================== USUWANIE RESTAURACJI ====================
            static void RemoveRestaurant(ApplicationDbContext db)
            {
                var restaurants = db.Restaurant
                    .Include(r => r.Address)
                    .Include(r => r.Employees)
                    .Include(r => r.Menu)
                    .Include(r => r.Reservations)
                    .ToList();

                if (!restaurants.Any())
                {
                    Console.WriteLine("Brak restauracji do usunięcia.");
                    return;
                }

                ShowRestaurants(db);
                Console.Write("\nWybierz restaurację do usunięcia: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > restaurants.Count)
                {
                    Console.WriteLine("Niepoprawny wybór.");
                    return;
                }

                var selected = restaurants[choice - 1];

                Console.Write($"Czy na pewno chcesz usunąć restaurację '{selected.Name}'? (t/n): ");
                string confirm = Console.ReadLine().ToLower();
                if (confirm != "t")
                {
                    Console.WriteLine("Usuwanie anulowane.");
                    return;
                }

                db.Employee.RemoveRange(selected.Employees);
                db.MenuItem.RemoveRange(selected.Menu);
                db.Reservation.RemoveRange(selected.Reservations);
                db.Restaurant.Remove(selected);
                db.SaveChanges();

                Console.WriteLine("Restauracja została usunięta.");
            }

            // ==================== PRACOWNICY ====================
            static void ManageEmployees(Restaurant restaurant, ApplicationDbContext db)
            {
                db.Entry(restaurant).Collection(r => r.Employees).Load();

                while (true)
                {
                   // Console.Clear();
                    Console.WriteLine($"=== PRACOWNICY: {restaurant.Name} ===");
                    Console.WriteLine("1. Dodaj pracownika");
                    Console.WriteLine("2. Usuń pracownika");
                    Console.WriteLine("3. Lista pracowników");
                    Console.WriteLine("0. Wróć");
                    Console.Write("Wybierz opcję: ");

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

                Console.Write("Data urodzenia (yyyy-MM-dd): ");
                DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth);

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
                    RestaurantId = restaurant.Id
                };

                db.Employee.Add(employee);
                db.SaveChanges();

                Console.WriteLine("Pracownik dodany i zapisany do bazy!");
            }

            static void RemoveEmployee(Restaurant restaurant, ApplicationDbContext db)
            {
                db.Entry(restaurant).Collection(r => r.Employees).Load();

                Console.Write("Nazwisko: ");
                string name = Console.ReadLine();

                var emp = restaurant.Employees.FirstOrDefault(x =>
                    x.LastName.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (emp != null)
                {
                    db.Employee.Remove(emp);
                    db.SaveChanges();
                    Console.WriteLine("Usunięto pracownika z bazy!");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono pracownika o podanym nazwisku.");
                }
            }

            static void ShowEmployees(Restaurant restaurant, ApplicationDbContext db)
            {
                db.Entry(restaurant).Collection(r => r.Employees).Load();

                foreach (var e in restaurant.Employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName}");
                }
            }

            // ==================== REZERWACJE ====================
            static void ManageReservations(Restaurant restaurant, ApplicationDbContext db)
            {
                db.Entry(restaurant).Collection(r => r.Reservations).Load();

                while (true)
                {
                    //Console.Clear();
                    Console.WriteLine($"=== REZERWACJE: {restaurant.Name} ===");
                    Console.WriteLine("1. Dodaj rezerwację");
                    Console.WriteLine("2. Usuń rezerwację");
                    Console.WriteLine("3. Lista rezerwacji");
                    Console.WriteLine("0. Powrót");
                    Console.Write("Wybierz opcję: ");

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
                    RestaurantId = restaurant.Id
                };

                db.Reservation.Add(reservation);
                db.SaveChanges();

                Console.WriteLine("Rezerwacja dodana i zapisana w bazie!");
            }

            static void RemoveReservation(Restaurant restaurant, ApplicationDbContext db)
            {
                db.Entry(restaurant).Collection(r => r.Reservations).Load();

                Console.Write("Nazwisko klienta: ");
                string name = Console.ReadLine();

                var res = restaurant.Reservations.FirstOrDefault(x =>
                    x.CustomerName.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (res != null)
                {
                    db.Reservation.Remove(res);
                    db.SaveChanges();
                    Console.WriteLine("Rezerwacja usunięta z bazy!");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono rezerwacji o podanym nazwisku.");
                }
            }

            static void ShowReservation(Restaurant restaurant, ApplicationDbContext db)
            {
                db.Entry(restaurant).Collection(r => r.Reservations).Load();

                foreach (var r in restaurant.Reservations)
                {
                    Console.WriteLine($"{r.CustomerName} {r.Date:yyyy-MM-dd} {r.Time}");
                }
            }

            // ==================== MENU ====================
            static void ManageMenu(Restaurant restaurant, ApplicationDbContext db)
            {
                db.Entry(restaurant).Collection(r => r.Menu).Load();

                while (true)
                {
                   // Console.Clear();
                    Console.WriteLine($"=== MENU: {restaurant.Name} ===");
                    Console.WriteLine("1. Dodaj pozycję do menu");
                    Console.WriteLine("2. Usuń pozycję z menu");
                    Console.WriteLine("3. Pokaż menu");
                    Console.WriteLine("0. Wróć");
                    Console.Write("Wybierz opcję: ");

                    string opt = Console.ReadLine();

                    switch (opt)
                    {
                        case "1": AddMenuItem(restaurant, db); break;
                        case "2": RemoveMenuItem(restaurant, db); break;
                        case "3": ShowMenu(restaurant, db); break;
                        case "0": return;
                    }
                }
            }

            static void AddMenuItem(Restaurant restaurant, ApplicationDbContext db)
            {
                Console.Write("Nazwa dania: ");
                string name = Console.ReadLine();

                Console.Write("Opis dania: ");
                string description = Console.ReadLine();

                Console.Write("Cena (zł): ");
                float.TryParse(Console.ReadLine(), out float price);

                var item = new MenuItem
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    RestaurantId = restaurant.Id
                };

                db.MenuItem.Add(item);
                db.SaveChanges();

                Console.WriteLine("Dodano pozycję do menu!");
            }

            static void RemoveMenuItem(Restaurant restaurant, ApplicationDbContext db)
            {
                db.Entry(restaurant).Collection(r => r.Menu).Load();

                Console.Write("Nazwa dania do usunięcia: ");
                string name = Console.ReadLine();

                var item = restaurant.Menu
                    .FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (item != null)
                {
                    db.MenuItem.Remove(item);
                    db.SaveChanges();
                    Console.WriteLine("Usunięto pozycję z menu!");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono dania o podanej nazwie.");
                }
            }

            static void ShowMenu(Restaurant restaurant, ApplicationDbContext db)
            {
                db.Entry(restaurant).Collection(r => r.Menu).Load();

                if (!restaurant.Menu.Any())
                {
                    Console.WriteLine("Brak pozycji w menu.");
                    return;
                }

                foreach (var m in restaurant.Menu)
                {
                    Console.WriteLine($"{m.Name} - {m.Price} zł");
                }
            }
        }
    }
}
