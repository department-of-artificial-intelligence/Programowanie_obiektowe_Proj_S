using System;
using Microsoft.EntityFrameworkCore;
using Project.DAL;

namespace Project.ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "System Zarządzania Hotelami";

            // Próba inicjalizacji bazy danych
            if (!InitDatabase())
            {
                Console.WriteLine("Naciśnij dowolny klawisz, aby zamknąć...");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                using var db = CreateContext(); // Tworzymy kontekst EF Core

                Console.Clear();
                Console.WriteLine("=== SYSTEM HOTELOWY ===");
                Console.WriteLine("1. Zarządzanie Hotelami (Wejdź tutaj aby zarządzać wszystkim)");
                Console.WriteLine("0. Wyjście");

                Console.Write("\n>> Wybierz opcję: ");
                var key = Console.ReadLine();

                try
                {
                    switch (key)
                    {
                        case "1":
                            HotelManager.MenuHotele(db);
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Niepoprawny wybór");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nWystąpił nieoczekiwany błąd: {ex.Message}");
                    Console.ResetColor();
                    Console.WriteLine("Naciśnij dowolny klawisz...");
                    Console.ReadKey();
                }
            }
        }

        // Inicjalizacja bazy danych i migracje
        static bool InitDatabase()
        {
            Console.WriteLine("Ładowanie bazy danych i sprawdzanie migracji...");
            try
            {
                using var db = CreateContext();
                db.Database.Migrate(); // Tworzy bazę, jeśli nie istnieje

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Połączono z bazą danych pomyślnie.");
                Console.ResetColor();
                System.Threading.Thread.Sleep(1000); // Pauza dla komunikatu
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BŁĄD KRYTYCZNY BAZY DANYCH:");
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Szczegóły: {ex.InnerException.Message}");
                }
                Console.ResetColor();
                return false;
            }
        }

        // Tworzenie kontekstu EF Core
        static ApplicationDbContext CreateContext()
            => new ApplicationDbContextFactory().CreateDbContext(null);
    }
}
