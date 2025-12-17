using System;
using System.Linq;
using Project.DAL;
using Project.Model;

namespace Project.ConsoleApp
{
    public static class EmployeeManager
    {
        public static void Manage(ApplicationDbContext db, Hotel hotel)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== ZARZĄDZANIE PRACOWNIKAMI: {hotel.Nazwa.ToUpper()} ===");

                ListEmployees(db, hotel);

                Console.WriteLine("\n----------------------------------");
                Console.WriteLine("1. Dodaj pracownika");
                Console.WriteLine("2. Usuń pracownika");
                Console.WriteLine("0. Wróć do menu hotelu");
                Console.WriteLine("----------------------------------");
                Console.Write("\n>> Wybierz opcję: ");

                switch (Console.ReadLine())
                {
                    case "1": AddEmployee(db, hotel); break;
                    case "2": RemoveEmployee(db, hotel); break;
                    case "0": return;
                    default: Console.WriteLine("Niepoprawna opcja."); Console.ReadKey(); break;
                }
            }
        }

        private static void ListEmployees(ApplicationDbContext db, Hotel hotel)
        {
            var pracownicy = db.Employees.Where(e => e.HotelId == hotel.Id).ToList();
            if (!pracownicy.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  (Brak pracowników)");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("  Aktualni pracownicy:");
                for (int i = 0; i < pracownicy.Count; i++)
                    Console.WriteLine($"  {i + 1}. {pracownicy[i].PelneDane()}");
            }
        }

        private static void AddEmployee(ApplicationDbContext db, Hotel hotel)
        {
            Console.Clear();
            Console.WriteLine(">>> DODAWANIE PRACOWNIKA");
            Console.WriteLine("Wybierz stanowisko:");
            Console.WriteLine("1. Recepcjonista");
            Console.WriteLine("2. Pokojówka");
            Console.WriteLine("3. Manager");
            Console.WriteLine("4. Kucharz");
            Console.WriteLine("5. Ochroniarz");
            Console.WriteLine("6. Barman");
            Console.WriteLine("7. Konserwator");
            Console.WriteLine("8. Inne (wpisz własne)");

            int choice = Helpers.GetIntInRange("\n>> Twój wybór (1-8): ", 1, 8);
            string stanowisko = choice switch
            {
                1 => "Recepcjonista",
                2 => "Pokojówka",
                3 => "Manager",
                4 => "Kucharz",
                5 => "Ochroniarz",
                6 => "Barman",
                7 => "Konserwator",
                _ => Helpers.GetNonEmptyAlpha("Wpisz nazwę stanowiska: ")
            };

            string imie = Helpers.GetNonEmptyAlpha("Podaj imię: ");
            string nazwisko = Helpers.GetNonEmptyAlpha("Podaj nazwisko: ");

            decimal pensja = Helpers.GetDecimalInRange("Podaj miesięczną pensję (max 50000): ", 2000, 50000);
            string email = Helpers.GetValidatedEmail("Podaj e-mail: ");
            string telefon = Helpers.GetValidatedPhone9("Podaj telefon: ");

            var emp = new Employees
            {
                Imie = imie,
                Nazwisko = nazwisko,
                Email = email,
                Telefon = telefon,
                Stanowisko = stanowisko,
                Pensja = pensja,
                DataZatrudnienia = DateTime.Today,
                HotelId = hotel.Id
            };

            db.Employees.Add(emp);
            db.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Pracownik dodany.");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static void RemoveEmployee(ApplicationDbContext db, Hotel hotel)
        {
            var pracownicy = db.Employees.Where(e => e.HotelId == hotel.Id).ToList();
            if (!pracownicy.Any()) { Console.WriteLine("Brak pracowników."); Console.ReadKey(); return; }

            int idx = Helpers.GetIntInRange("\n>> Wybierz numer pracownika do usunięcia: ", 1, pracownicy.Count);
            db.Employees.Remove(pracownicy[idx - 1]);
            db.SaveChanges();
            Console.WriteLine("Usunięto.");
            Console.ReadKey();
        }
    }
}
