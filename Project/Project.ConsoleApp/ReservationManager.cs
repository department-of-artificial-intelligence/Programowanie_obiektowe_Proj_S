using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.ConsoleApp
{
    public static class ReservationManager
    {
        public static void Manage(ApplicationDbContext db, Hotel hotel)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== REZERWACJE W HOTELU: {hotel.Nazwa.ToUpper()} ===");

                ListReservations(db, hotel);

                Console.WriteLine("\n----------------------------------");
                Console.WriteLine("1. Utwórz nową rezerwację");
                Console.WriteLine("2. Anuluj rezerwację");
                Console.WriteLine("0. Wróć do menu hotelu");
                Console.WriteLine("----------------------------------");

                Console.Write("\n>> Co chcesz zrobić? Wybierz cyfrę: ");

                switch (Console.ReadLine())
                {
                    case "1": AddReservation(db, hotel); break;
                    case "2": RemoveReservation(db, hotel); break;
                    case "0": return;
                    default: Console.WriteLine("Niepoprawna opcja."); Console.ReadKey(); break;
                }
            }
        }

        private static void ListReservations(ApplicationDbContext db, Hotel hotel)
        {
            var rezerwacje = db.Reservations
                .Include(r => r.Pokoj)
                .Include(r => r.Gosc)
                .Where(r => r.HotelId == hotel.Id)
                .OrderBy(r => r.DataOd)
                .ToList();

            if (!rezerwacje.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  (Brak aktywnych rezerwacji)");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("  Aktualne rezerwacje:");
                for (int i = 0; i < rezerwacje.Count; i++)
                {
                    var r = rezerwacje[i];
                    Console.WriteLine($"  {i + 1}. Pokój {r.Pokoj.Numer} | {r.DataOd:dd.MM} - {r.DataDo:dd.MM} | {r.Gosc.PelneDane()} | Osób: {r.LiczbaOsob}");
                }
            }
        }

        private static void AddReservation(ApplicationDbContext db, Hotel hotel)
        {
            Console.Clear();
            Console.WriteLine(">>> NOWA REZERWACJA");

            var od = Helpers.GetFutureOrTodayDate("Data przyjazdu (DD-MM-YYYY): ");
            int liczbaNocy = Helpers.GetIntPositive("Ile nocy chcesz zostać?: ");
            var doo = od.AddDays(liczbaNocy);

            int liczbaOsob = Helpers.GetIntPositive("Dla ilu osób jest rezerwacja?: ");

            Console.WriteLine($"\n-> Szukam pokoi dla {liczbaOsob} osób w terminie: {od:DD-MM-YYYY} do {doo:DD-MM-YYYY}...");

            var zajetePokojeIds = db.Reservations
                .Where(r => r.HotelId == hotel.Id)
                .Where(r => r.DataOd < doo && r.DataDo > od)
                .Select(r => r.PokojId)
                .ToList();

            var wolnePokoje = db.Rooms
                .Where(r => r.HotelId == hotel.Id)
                .Where(r => !zajetePokojeIds.Contains(r.Id))
                .Where(r => r.LiczbaMiejsc >= liczbaOsob)
                .OrderBy(r => r.CenaZaDobe)
                .ToList();

            if (!wolnePokoje.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Brak wolnych pokoi w tym terminie spełniających wymóg {liczbaOsob} miejsc.");
                Console.ResetColor();
                Console.WriteLine("Spróbuj zmienić datę lub liczbę osób.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n--- Dostępne i pasujące pokoje ---");
            foreach (var p in wolnePokoje)
            {
                string dopasowanie = p.LiczbaMiejsc == liczbaOsob ? "(IDEALNY)" : $"(Miejsc: {p.LiczbaMiejsc})";
                Console.WriteLine($"  [Nr {p.Numer}] {p.Typ} | {p.CenaZaDobe:C}/noc | {dopasowanie}");
            }

            Room? wybranyPokoj = null;
            while (wybranyPokoj == null)
            {
                int numer = Helpers.GetIntPositive("\n>> Podaj numer pokoju do rezerwacji: ");
                wybranyPokoj = wolnePokoje.FirstOrDefault(r => r.Numer == numer);
                if (wybranyPokoj == null) Console.WriteLine("Wybierz poprawny numer z listy powyżej.");
            }

            Console.WriteLine("\n-- Dane Gościa --");
            var guest = new Guest
            {
                Imie = Helpers.GetNonEmptyAlpha("Imię: "),
                Nazwisko = Helpers.GetNonEmptyAlpha("Nazwisko: "),
                Email = Helpers.GetValidatedEmail("Email: "),
                Telefon = Helpers.GetValidatedPhone9("Telefon: ")
            };

            try
            {
                wybranyPokoj.Zarezerwuj(od, doo);

                var rezerwacja = new Reservation
                {
                    PokojId = wybranyPokoj.Id,
                    HotelId = hotel.Id,
                    Gosc = guest,
                    DataOd = od,
                    DataDo = doo,
                    LiczbaOsob = liczbaOsob
                };

                db.Reservations.Add(rezerwacja);
                db.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSukces! Rezerwacja utworzona dla {liczbaOsob} osób.");
                Console.WriteLine($"Całkowity koszt: {rezerwacja.Koszt:C}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✖ Błąd: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        private static void RemoveReservation(ApplicationDbContext db, Hotel hotel)
        {
            var rezerwacje = db.Reservations
                .Include(r => r.Pokoj)
                .Include(r => r.Gosc)
                .Where(r => r.HotelId == hotel.Id)
                .OrderBy(r => r.DataOd)
                .ToList();

            if (!rezerwacje.Any())
            {
                Console.WriteLine("Brak rezerwacji do usunięcia. Naciśnij klawisz...");
                Console.ReadKey();
                return;
            }

            Console.Write("\n>> Wybierz numer rezerwacji z listy powyżej (1-" + rezerwacje.Count + "): ");
            int idx = Helpers.GetIntInRange("", 1, rezerwacje.Count);

            var doUsuniecia = rezerwacje[idx - 1];
            doUsuniecia.Pokoj.Zwolnij();

            db.Reservations.Remove(doUsuniecia);
            db.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Rezerwacja anulowana.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
