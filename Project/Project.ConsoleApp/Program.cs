/*using RestaurantManagement.Models;
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
*/
#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManagement.Models;
using RestaurantManagement.Models.Enums;

namespace RestaurantManagement
{
    class Program
    {
        static List<Employee> employees = new();
        static List<Reservation> reservations = new();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Sieć Restauracji ===");
                Console.WriteLine("1. Dodaj pracownika");
                Console.WriteLine("2. Usun pracownika");
                Console.WriteLine("3. Lista pracowników");
                Console.WriteLine("4. Dodaj rezerwacje");
                Console.WriteLine("5. Usuń rezerwacje");
                Console.WriteLine("6. Lista rezerwacji");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcje: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": AddEmployee(); break;
                    case "2": RemoveEmployee(); break;
                    case "3": ShowEmployees(); break;
                    case "4": AddReservation(); break;
                    case "5": RemoveReservation(); break;
                    case "6": ShowReservations(); break;
                    case "0": return;
                    default: Console.WriteLine("Niepoprawna opcja!"); break;
                }

                Console.WriteLine("\nKliknij Enter, aby kontynuować...");
                Console.ReadLine();
            }
        }

        static void AddEmployee()
        {
            Console.Write("Imię: ");
            string firstName = Console.ReadLine();

            Console.Write("Nazwisko: ");
            string lastName = Console.ReadLine();

            Console.Write("Stanowisko pracownika (Kelner, Szef, Kucharz, Menadżer, Barman, Host, Sprzątaczka, Dostawca): ");
            string typeInput = Console.ReadLine();
            if (!Enum.TryParse(typeInput, true, out EmployeeType type))
            {
                Console.WriteLine("Nieprawidłowy format.");
                return;
            }

            Console.Write("Wypłata: ");
            if (!int.TryParse(Console.ReadLine(), out int salary))
            {
                Console.WriteLine("Nieprawidłowy format");
                return;
            }

            Console.Write("Numer telefonu: ");
            string phoneNumber = Console.ReadLine();
            if(phoneNumber.Length !=9)
            {
                Console.WriteLine("Nieprawidłowy format daty");
                return;
            }
              

            Console.Write("Email: ");
            string email = Console.ReadLine();
            if (!email.Contains("@"))
            {
                Console.WriteLine("Nieprawidłowy format daty");
                return;
            }

            Console.Write("Data urodzenia (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
            {
                Console.WriteLine("Nieprawidłowy format daty");
                return;
            } 

            Console.Write("Państwo: ");
            string country = Console.ReadLine();

            Console.Write("Kod pocztowy: ");
            string zipCode = Console.ReadLine();
            if (zipCode.Length != 6)
               
            {
                Console.WriteLine("Nieprawidłowy format kodu");
                return;
            }
            if (!zipCode.Contains("-"))
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
                EmployeeType = type,
                Salary = salary,
                HiredOn = DateTime.Now,
                PhoneNumber = phoneNumber,
                Email = email,
                DateOfBirth = dateOfBirth,
                Address = address
            };

            employees.Add(employee);
            Console.WriteLine("Pracownik dodany!");
        }

        static void RemoveEmployee()
        {
            Console.Write("Dodaj nazwisko, aby usunąć ");
            string lastName = Console.ReadLine();

            var emp = employees.FirstOrDefault(e => e.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
            if (emp != null)
            {
                emp.FiredOn = DateTime.Now;
                employees.Remove(emp);
                Console.WriteLine("Pracownik usunięty!");
            }
            else
                Console.WriteLine("Nieznaleziono pracownika!");
        }

        static void ShowEmployees()
        {
            if (!employees.Any())
            {
                Console.WriteLine("Nieznaleziono pracownika");
                return;
            }

            Console.WriteLine("\nAktualni pracownicy:");
            foreach (var e in employees)
            {
                Console.WriteLine($"- {e}, Pensja: {e.Salary}, Zatrudniony/a: {e.HiredOn:d}");
                Console.WriteLine($"  Adres: {e.Address}");
                Console.WriteLine($"  Email: {e.Email}, Numer telefonu: {e.PhoneNumber}");
                Console.WriteLine();
            }
        }

        static void AddReservation()
        {
            Console.Write("Imię klienta: ");
            string name = Console.ReadLine();

            Console.Write("Liczba osób: ");
            if (!int.TryParse(Console.ReadLine(), out int count))
            {
                Console.WriteLine("Niepoprawny numer");
                return;
            }

            Console.Write("Data (yyyy-mm-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Niepoprawny format daty");
                return;
            }

            reservations.Add(new Reservation
            {
                CustomerName = name,
                NumberOfPeople = count,
                Date = date
            });

            Console.WriteLine("Rezerwacja dodana");
        }

        static void RemoveReservation()
        {
            Console.Write("Wpisz imię klienta, aby usunąć: ");
            string name = Console.ReadLine();

            var res = reservations.FirstOrDefault(r => r.CustomerName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (res != null)
            {
                reservations.Remove(res);
                Console.WriteLine("Rezerwacja usunięta!");
            }
            else
                Console.WriteLine("Brak rezerwacji");
        }

        static void ShowReservations()
        {
            if (!reservations.Any())
            {
                Console.WriteLine("Brak rezerwacji");
                return;
            }

            Console.WriteLine("\nRezerwacje:");
            foreach (var r in reservations)
                Console.WriteLine($"- {r}");
        }
    }
}
