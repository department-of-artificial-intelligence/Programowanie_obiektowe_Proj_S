using System;
using System.Linq;
using Project.DAL;
using Project.Model;

namespace Project.ConsoleApp
{
    public static class RoomManager
    {
        public static void Manage(ApplicationDbContext db, Hotel hotel)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== ZARZĄDZANIE POKOJAMI: {hotel.Nazwa.ToUpper()} ===");

                ShowVisualMap(db, hotel);

                Console.WriteLine("\n----------------------------------");
                Console.WriteLine("1. Dodaj pokój");
                Console.WriteLine("2. Usuń pokój");
                Console.WriteLine("0. Wróć");
                Console.WriteLine("----------------------------------");

                Console.Write("\n>> Co chcesz zrobić? Wybierz cyfrę: ");

                switch (Console.ReadLine())
                {
                    case "1": AddRoom(db, hotel); break;
                    case "2": RemoveRoom(db, hotel); break;
                    case "0": return;
                    default: Console.WriteLine("Niepoprawny wybór"); Console.ReadKey(); break;
                }
            }
        }

        private static void ShowVisualMap(ApplicationDbContext db, Hotel hotel)
        {
            var pokoje = db.Rooms.Where(r => r.HotelId == hotel.Id).OrderBy(r => r.Numer).ToList();
            if (!pokoje.Any()) return;

            var dzis = DateTime.Today;
            var zajeteId = db.Reservations
                .Where(r => r.HotelId == hotel.Id && r.DataOd <= dzis && r.DataDo > dzis)
                .Select(r => r.PokojId)
                .ToHashSet();

            Console.WriteLine("MAPA POKOI (Stan na dziś):");
            Console.WriteLine("[ ] - Wolny, [X] - Zajęty\n");

            int licznik = 0;
            foreach (var p in pokoje)
            {
                bool zajety = zajeteId.Contains(p.Id);

                Console.ForegroundColor = zajety ? ConsoleColor.Red : ConsoleColor.Green;
                Console.Write(zajety ? $" [X {p.Numer}] " : $" [  {p.Numer}] ");

                Console.ResetColor();

                licznik++;
                if (licznik % 4 == 0) Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
        }

        private static void AddRoom(ApplicationDbContext db, Hotel hotel)
        {
            Console.WriteLine("\n>>> DODAWANIE POKOJU");

            int numer;
            while (true)
            {
                numer = Helpers.GetIntPositive("Numer pokoju: ");
                if (!db.Rooms.Any(r => r.Numer == numer && r.HotelId == hotel.Id)) break;
                Console.WriteLine("Błąd: Ten numer jest już zajęty w tym hotelu.");
            }

            var room = new Room
            {
                Numer = numer,
                HotelId = hotel.Id,
                LiczbaMiejsc = Helpers.GetIntPositive("Miejsc: "),
                CenaZaDobe = Helpers.GetDecimalPositive("Cena: "),
                Typ = (RoomType)Helpers.GetIntInRange("1.Standard 2.Deluxe 3.Suit: ", 1, 3)
            };
            db.Rooms.Add(room); db.SaveChanges();
            Console.WriteLine("Pokój dodany.");
            Console.ReadKey();
        }

        private static void RemoveRoom(ApplicationDbContext db, Hotel hotel)
        {
            int numer = Helpers.GetIntPositive("Numer do usunięcia: ");
            var r = db.Rooms.FirstOrDefault(x => x.Numer == numer && x.HotelId == hotel.Id);

            if (r == null) { Console.WriteLine("Nie znaleziono pokoju."); }
            else
            {
                var maRezerwacje = db.Reservations.Any(res => res.PokojId == r.Id && res.DataDo > DateTime.Today);
                if (maRezerwacje)
                {
                    Console.WriteLine("Nie można usunąć pokoju - ma przyszłe rezerwacje!");
                }
                else
                {
                    db.Rooms.Remove(r); db.SaveChanges(); Console.WriteLine("Usunięto");
                }
            }
            Console.ReadKey();
        }
    }
}
