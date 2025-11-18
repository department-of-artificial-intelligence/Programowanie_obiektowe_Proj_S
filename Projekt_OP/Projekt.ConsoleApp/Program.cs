using System;
using System.Linq;
using Projekt.Model;


namespace Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            var cinemaMenager = new CinemaMenager();

            bool isRunning = true;

            Console.WriteLine(" Witamy w Systemie Zarzadzania Kinami! ");
            Console.WriteLine("---------------------------------------");

            while (isRunning)
            {
                DisplayMenu();

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddNewCinema(cinemaMenager);
                            break;
                        case 2:
                            cinemaMenager.DisplayCinemas();
                            break;
                        case 3:
                            AddNewFilm(cinemaMenager);
                            break;
                        case 4:
                            cinemaMenager.DisplayFilms();
                            break;
                        case 5:
                            isRunning = false;
                            Console.WriteLine("\nDziękujemy za korzystanie z systemu. Do widzenia!");
                            break;
                        default:
                            Console.WriteLine("Nieznana opcja. Wprowadź numer od 1 do 5.");
                            break;

                    }
                }


            }
            
        }
        static void DisplayMenu()
        {
            Console.WriteLine("--- MENU GŁÓWNE ---");
            Console.WriteLine("1. Dodaj Kino");
            Console.WriteLine("2. Wyświetl Kina");
            Console.WriteLine("3. Dodaj Film");
            Console.WriteLine("4. Wyświetl Filmy");
            Console.WriteLine("5. Wyjście");
            Console.Write("Wybierz opcję: ");
        }

        static void AddNewCinema(CinemaMenager manager)
        {
            Console.WriteLine("\n--- DODAWANIE KINA ---");
            Console.Write("Podaj Nazwę Kina: ");
            string name = Console.ReadLine();

            int newID = 1;
            if (manager._cinemas.Count > 0)
            {
                // 1. Generowanie ID (LINQ)
                newID = manager._cinemas.Max(c => c.CinemaID) + 1;
            }

            // 2. Tworzenie obiektu Kino
            var newCinema = new Cinema(newID, name);

            //do menago
            manager.AddCinema(newCinema);

            Console.WriteLine($"\n✅ Kino '{name}' (ID: {newID}) dodane pomyślnie.");
        }

        static void AddNewFilm(CinemaMenager manager)
        {
            Console.WriteLine("\n--- DODAWANIE FILMU ---");
            Console.Write("Podaj Tytuł Filmu: ");
            string title = Console.ReadLine();

            // 1. Generowanie ID (LINQ)
            int newID = 1;
            if (manager._films.Count > 0)
            {
                newID = manager._films.Max(f => f.ID) + 1;
            }

            // 2. dodatkowe dane dla filmu
            int timeMin = 0;
            Console.Write("Podaj Czas trwania w minutach: ");

            // sprawdzenie czy wprowadzono liczbe calkowita wieksza od 0
            while (!int.TryParse(Console.ReadLine(), out timeMin) || timeMin <= 0)
            {
                Console.Write("Nieprawidłowa wartość. Podaj czas w minutach (> 0): ");
            }

            Console.Write("Podaj Gatunek: ");
            string genre = Console.ReadLine();

            // 3. Tworzenie  Filmu
            var newFilm = new Film(newID, title, timeMin, genre);

            //do menago
            manager.AddFilm(newFilm);

            Console.WriteLine($"\n✅ Film '{title}' (ID: {newID}, {timeMin} min) dodany pomyślnie.");
        }
    }
}
    




