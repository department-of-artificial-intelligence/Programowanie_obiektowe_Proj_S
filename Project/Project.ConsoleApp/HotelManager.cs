using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.ConsoleApp
{
    public static class HotelManager
    {
        public static void MenuHotele(ApplicationDbContext db)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU GŁÓWNE HOTELI ===");
                Console.WriteLine("1. Wybierz hotel do zarządzania (WEJDŹ)");
                Console.WriteLine("2. Dodaj nowy hotel");
                Console.WriteLine("3. Usuń hotel");
                Console.WriteLine("4. Lista wszystkich hoteli");
                Console.WriteLine("0. Powrót do menu głównego");

                Console.Write("\n>> Wybierz opcję: ");

                switch (Console.ReadLine())
                {
                    case "1": SelectAndManage(db); break;
                    case "2": AddHotel(db); break;
                    case "3": DeleteHotel(db); break;
                    case "4": ListHotels(db); break;
                    case "0": return;
                    default: Console.WriteLine("Niepoprawna opcja."); Console.ReadKey(); break;
                }
            }
        }

        private static void SelectAndManage(ApplicationDbContext db)
        {
            var hotel = SelectHotel(db);
            if (hotel == null) return;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== ZARZĄDZASZ HOTELEM: {hotel.Nazwa.ToUpper()} ===");
                Console.WriteLine($"Adres: {hotel.Miasto}, {hotel.Adres}");
                Console.WriteLine("---------------------------");
                Console.WriteLine("1. Zarządzaj Pracownikami");
                Console.WriteLine("2. Zarządzaj Pokojami (Mapa)");
                Console.WriteLine("3. Zarządzaj Rezerwacjami");
                Console.WriteLine("4. Zarządzaj Usługami i Rachunkami");
                Console.WriteLine("5. Raporty i Statystyki");
                Console.WriteLine("0. Wróć do listy hoteli");

                Console.Write("\n>> Co chcesz zrobić? Wybierz cyfrę: ");

                switch (Console.ReadLine())
                {
                    case "1": EmployeeManager.Manage(db, hotel); break;
                    case "2": RoomManager.Manage(db, hotel); break;
                    case "3": ReservationManager.Manage(db, hotel); break;
                    case "4": ServiceManager.Manage(db, hotel); break;
                    case "5": ReportManager.ShowReports(db, hotel); break;
                    case "0": return; break;
                    default: Console.WriteLine("Niepoprawna opcja."); Console.ReadKey(); break;
                }
            }
        }

        public static Hotel? SelectHotel(ApplicationDbContext db)
        {
            Console.Clear();
            var hotele = db.Hotels.ToList();
            if (!hotele.Any())
            {
                Console.WriteLine("Brak hoteli w bazie. Najpierw dodaj hotel.");
                Console.ReadKey();
                return null;
            }

            Console.WriteLine("DOSTĘPNE HOTELE:");
            for (int i = 0; i < hotele.Count; i++)
                Console.WriteLine($"{i + 1}. {hotele[i].Nazwa} ({hotele[i].Miasto})");

            int wyb = Helpers.GetIntInRange("\n>> Wybierz numer hotelu: ", 1, hotele.Count);
            return hotele[wyb - 1];
        }

        private static void AddHotel(ApplicationDbContext db)
        {
            Console.Clear();
            Console.WriteLine("DODAWANIE NOWEGO HOTELU");
            var hotel = new Hotel
            {
                Nazwa = Helpers.GetNonEmptyString("Nazwa: "),
                Miasto = Helpers.GetNonEmptyString("Miasto: "),
                Adres = Helpers.GetNonEmptyString("Adres: "),
                Gwiazdki = Helpers.GetIntInRange("Gwiazdki (1-5): ", 1, 5),
                RokOtwarcia = Helpers.GetIntInRange("Rok otwarcia: ", 1900, DateTime.Now.Year)
            };
            db.Hotels.Add(hotel);
            db.SaveChanges();
            Console.WriteLine("Hotel dodany.");
            Console.ReadKey();
        }

        private static void ListHotels(ApplicationDbContext db)
        {
            Console.Clear();
            var hotele = db.Hotels.Include(h => h.Pokoje).Include(h => h.Pracownicy).ToList();
            Console.WriteLine("LISTA HOTELI:");
            foreach (var h in hotele)
            {
                Console.WriteLine($"• {h.Nazwa} ({h.Miasto}) - {h.Gwiazdki}*");
                Console.WriteLine($"  Pokoje: {h.Pokoje.Count}, Pracownicy: {h.Pracownicy.Count}");
                Console.WriteLine("---");
            }
            Console.ReadKey();
        }

        private static void DeleteHotel(ApplicationDbContext db)
        {
            var h = SelectHotel(db);
            if (h != null)
            {
                db.Hotels.Remove(h);
                db.SaveChanges();
                Console.WriteLine("Usunięto.");
                Console.ReadKey();
            }
        }
    }
}
