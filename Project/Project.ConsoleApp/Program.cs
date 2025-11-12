#nullable disable
using RestaurantManagement.Models;
using RestaurantManagement.Models.Enums;
using RestaurantNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantManagement
{
    class Program
    {
        static List<Restaurant> restaurants = new(); // lista restauracji bo jest ich wiecej



        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Zarządzanie Siecią Restauracji ===");
                Console.WriteLine("1. Dodaj restaurację"); 
                Console.WriteLine("2. Lista restauracji");  
                Console.WriteLine("3. Wybierz restaurację"); 
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcje: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": AddRestaurant(); break;  // <-- dodane
                    case "2": ShowRestaurants(); break; // <-- dodane
                    case "3": ManageRestaurant(); break; // <-- dodane
                    case "0": return;
                    default: Console.WriteLine("Niepoprawna opcja!"); break;
                }

                Console.WriteLine("\nKliknij Enter, aby kontynuować...");
                Console.ReadLine();
            }
        }

        static void AddRestaurant() 
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
                Console.WriteLine("Nieprawidłowy format numeru telefonu");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();
            if (!email.Contains("@"))
            {
                Console.WriteLine("Nieprawidłowy format adresu e-mail");
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
                Clients = new List<Person>(),
                Reservations = new List<Reservation>() // <-- bardzo ważne
            };


            restaurants.Add(restaurant);
            Console.WriteLine("Dodano restaurację!");
        }

        static void ShowRestaurants()
        {
            if (!restaurants.Any())
            {
                Console.WriteLine("Brak restauracji w sieci.");
                return;
            }

            Console.WriteLine("\nLista restauracji:");
            for (int i = 0; i < restaurants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {restaurants[i].Name} ({restaurants[i].Address.City})");
            }
        }


        static void ManageRestaurant()
        {
            if (!restaurants.Any())
            {
                Console.WriteLine("Brak restauracji do zarządzania.");
                return;
            }

            ShowRestaurants();

            Console.Write("\nWybierz restaurację: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > restaurants.Count)
            {
                Console.WriteLine("Niepoprawny wybór.");
                return;
            }

            var selected = restaurants[choice - 1];
            Console.WriteLine($"\nWybrano restaurację: {selected.Name}");

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Zarządzanie restauracją: {selected.Name} ===");
                Console.WriteLine("1. Zarządzaj pracownikami");
                Console.WriteLine("2. Zarządzaj rezerwacjami");
                Console.WriteLine("0. Powrót");
                Console.Write("Wybierz opcję: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ManageEmployees(selected); // Podmenu dla pracowników
                        break;
                    case "2":
                        ManageReservations(selected); // Podmenu dla rezerwacji
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Niepoprawna opcja!");
                        break;
                }

                Console.WriteLine("\nKliknij Enter, aby kontynuować...");
                Console.ReadLine();
            }
        }


        static void ManageReservations(Restaurant restaurant) // <-- dodane

        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Zarządzanie restauracją: {restaurant.Name} ===");
                Console.WriteLine("1. Dodaj rezerwację");
                Console.WriteLine("2. Usuń rezerwację");
                Console.WriteLine("3. Lista rezerwacji");
                Console.WriteLine("0. Powrót");
                Console.Write("Wybierz opcję: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": AddReservation(restaurant); break;
                    case "2": RemoveReservation(restaurant); break;
                    case "3": ShowReservation(restaurant); break;
                    case "0": return;
                    default: Console.WriteLine("Niepoprawna opcja!"); break;
                }

                Console.WriteLine("\nKliknij Enter, aby kontynuować...");
                Console.ReadLine();
            }
        }

        static void AddReservation(Restaurant restaurant)
        {
            Console.Write("Nazwisko: ");
            string customerName = Console.ReadLine();

            Console.Write("Liczba osób: ");
            if (!int.TryParse(Console.ReadLine(), out int numberOfPeople) || numberOfPeople <= 0)
            {
                Console.WriteLine("Nieprawidłowa liczba osób!");
                return;
            }

            Console.Write("Numer telefonu: ");
            string phoneNumber = Console.ReadLine();
            if (phoneNumber.Length != 9)
            {
                Console.WriteLine("Nieprawidłowy format numeru telefonu");
                return;
            }

            Console.Write("Data (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Nieprawidłowa data!");
                return;
            }

            Console.Write("Godzina rezerwacji (HH:mm): ");
            if (!TimeOnly.TryParse(Console.ReadLine(), out TimeOnly time))
            {
                Console.WriteLine("Nieprawidłowa godzina!");
                return;
            }

            var reservation = new Reservation
            {
                CustomerName = customerName,
                NumberOfPeople = numberOfPeople,
                PhoneNumber = phoneNumber,
                Date = date,
                Time = time
            };

            restaurant.Reservations.Add(reservation);
            Console.WriteLine("Rezerwacja dodana!");
        }

        static void RemoveReservation(Restaurant restaurant)
        {
            Console.Write("Podaj nazwisko do usunięcia: ");
            string nameToRemove = Console.ReadLine();

            var res = restaurant.Reservations.FirstOrDefault(r =>
                       r.CustomerName.Equals(nameToRemove, StringComparison.OrdinalIgnoreCase));

            if (res != null)
            {
                restaurant.Reservations.Remove(res);
                Console.WriteLine("Rezerwacja usunięta!");
            }
            else
            {
                Console.WriteLine("Nie znaleziono rezerwacji!");
            }
        }

        static void ShowReservation(Restaurant restaurant)
        {
            if (!restaurant.Reservations.Any())
            {
                Console.WriteLine("Brak rezerwacji w tej restauracji.");
                return;
            }

            Console.WriteLine($"\nRezerwacje w restauracji {restaurant.Name}:");
            foreach (var r in restaurant.Reservations)
            {
                Console.WriteLine($"- {r.CustomerName}, {r.NumberOfPeople} osób, {r.Date:yyyy-MM-dd} {r.Time:HH:mm}, Tel: {r.PhoneNumber}");
            }
        }


        //
        static void ManageEmployees(Restaurant restaurant) // <-- dodane
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Zarządzanie restauracją: {restaurant.Name} ===");
                Console.WriteLine("1. Dodaj pracownika");
                Console.WriteLine("2. Usuń pracownika");
                Console.WriteLine("3. Lista pracowników");
                Console.WriteLine("0. Powrót");
                Console.Write("Wybierz opcję: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": AddEmployee(restaurant); break;
                    case "2": RemoveEmployee(restaurant); break;
                    case "3": ShowEmployees(restaurant); break;
                    case "0": return;
                    default: Console.WriteLine("Niepoprawna opcja!"); break;
                }

                Console.WriteLine("\nKliknij Enter, aby kontynuować...");
                Console.ReadLine();
            }
            static void ManageRestaurant()
{
    if (!restaurants.Any())
    {
        Console.WriteLine("Brak restauracji do zarządzania.");
        return;
    }

    ShowRestaurants();

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
        Console.WriteLine($"=== Zarządzanie restauracją: {selected.Name} ===");
        Console.WriteLine("1. Zarządzaj pracownikami");
        Console.WriteLine("2. Zarządzaj rezerwacjami");
        Console.WriteLine("0. Powrót");
        Console.Write("Wybierz opcję: ");

        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                ManageEmployees(selected);
                break;
            case "2":
                ManageReservations(selected);
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Niepoprawna opcja!");
                break;
        }

        Console.WriteLine("\nKliknij Enter, aby kontynuować...");
        Console.ReadLine();
    }
}

        }

        static void AddEmployee(Restaurant restaurant) 
        {
            Console.Write("Imię: ");
            string firstName = Console.ReadLine();

            Console.Write("Nazwisko: ");
            string lastName = Console.ReadLine();

            Console.Write("Stanowisko (Kelner, Szef, Kucharz, Menadżer, Barman, Host, Sprzątaczka, Dostawca): ");
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
            if (phoneNumber.Length != 9)
            {
                Console.WriteLine("Nieprawidłowy format numeru telefonu");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();
            if (!email.Contains("@"))
            {
                Console.WriteLine("Nieprawidłowy format adresu e-mail");
                return;
            }

            Console.Write("Data urodzenia (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
            {
                Console.WriteLine("Nieprawidłowy format daty.");
                return;
            }

            if (dateOfBirth.Month < 1 || dateOfBirth.Month > 12) // <-- dodane
            {
                Console.WriteLine("Miesiąc poza zakresem"); // <-- dodane
                return; // <-- dodane
            }

            if (dateOfBirth.Day < 1 || dateOfBirth.Day > 31) // <-- dodane
            {
                Console.WriteLine("Dzień poza zakresem"); 
                return; 
            }

            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now < dateOfBirth.AddYears(age))
                age--;

            if (age < 18)
            {
                Console.WriteLine("Osoba musi mieć ukończone 18 lat, aby mogła być zatrudniona");
                return; 
            }

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
                EmployeeType = type,
                Salary = salary,
                HiredOn = DateTime.Now,
                PhoneNumber = phoneNumber,
                Email = email,
                DateOfBirth = dateOfBirth,
                Address = address
            };

            restaurant.Employees.Add(employee); // <-- zmienione
            Console.WriteLine("Pracownik dodany!");
        }

        static void RemoveEmployee(Restaurant restaurant) // <-- zmienione
        {
            Console.Write("Podaj nazwisko pracownika do usunięcia: ");
            string lastName = Console.ReadLine();

            var emp = restaurant.Employees.FirstOrDefault(e => e.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
            if (emp != null)
            {
                emp.FiredOn = DateTime.Now;
                restaurant.Employees.Remove(emp);
                Console.WriteLine("Pracownik usunięty!");
            }
            else
                Console.WriteLine("Nie znaleziono pracownika!");
        }

        static void ShowEmployees(Restaurant restaurant) // <-- zmienione
        {
            if (!restaurant.Employees.Any())
            {
                Console.WriteLine("Brak pracowników w tej restauracji.");
                return;
            }

            Console.WriteLine($"\nPracownicy restauracji {restaurant.Name}:");
            foreach (var e in restaurant.Employees)
            {
                Console.WriteLine($"- {e.FirstName} {e.LastName}, {e.EmployeeType}, Pensja: {e.Salary}");
            }
        }
    }


//sortowanie jakiejs listy
}
