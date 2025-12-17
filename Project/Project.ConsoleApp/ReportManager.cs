using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.ConsoleApp
{
    public static class ReportManager
    {
        public static void ShowReports(ApplicationDbContext db, Hotel hotel)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== CENTRUM ANALITYCZNE: {hotel.Nazwa.ToUpper()} ===");
                Console.WriteLine("1. Raport Finansowy (Przychody)");
                Console.WriteLine("2. Statystyki Pokoi (Popularność)");
                Console.WriteLine("3. Statystyki Pracowników (Zarobki)");
                Console.WriteLine("0. Wróć");

                Console.Write("\n>> Wybierz opcję: ");
                switch (Console.ReadLine())
                {
                    case "1": FinancialReport(db, hotel); break;
                    case "2": RoomStats(db, hotel); break;
                    case "3": EmployeeStats(db, hotel); break;
                    case "0": return;
                    default: Console.WriteLine("Niepoprawny wybór"); Console.ReadKey(); break;
                }
            }
        }

        private static void FinancialReport(ApplicationDbContext db, Hotel hotel)
        {
            Console.Clear();
            Console.WriteLine("--- RAPORT FINANSOWY ---");

            var rezerwacje = db.Reservations
                .Include(r => r.Pokoj)
                .Where(r => r.HotelId == hotel.Id)
                .ToList();

            decimal przychodPokoje = rezerwacje.Sum(r => r.Koszt);

            var przychodUslugi = db.ServiceReservations
                .Include(sr => sr.Reservation)
                .Where(sr => sr.Reservation.HotelId == hotel.Id)
                .Sum(sr => sr.CenaWChwiliZakupu);

            Console.WriteLine($"Liczba rezerwacji (łącznie): {rezerwacje.Count}");
            Console.WriteLine($"Przychód z noclegów:         {przychodPokoje:C}");
            Console.WriteLine($"Przychód z usług dodatkowych: {przychodUslugi:C}");
            Console.WriteLine("-------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"SUMA CAŁKOWITA:              {przychodPokoje + przychodUslugi:C}");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static void RoomStats(ApplicationDbContext db, Hotel hotel)
        {
            Console.Clear();
            Console.WriteLine("--- STATYSTYKI POKOI ---");

            var totalRooms = db.Rooms.Count(r => r.HotelId == hotel.Id);
            var rezerwacje = db.Reservations.Where(r => r.HotelId == hotel.Id).ToList();

            Console.WriteLine($"Łączna liczba pokoi: {totalRooms}");
            Console.WriteLine($"Dokonanych rezerwacji: {rezerwacje.Count}");

            if (rezerwacje.Any())
            {
                var popularneTypy = rezerwacje
                    .GroupBy(r => r.Pokoj.Typ)
                    .Select(g => new { Typ = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count);

                Console.WriteLine("\nPopularność typów:");
                foreach (var stat in popularneTypy)
                {
                    Console.WriteLine($"- {stat.Typ}: {stat.Count} rezerwacji");
                }
            }
            Console.ReadKey();
        }

        private static void EmployeeStats(ApplicationDbContext db, Hotel hotel)
        {
            Console.Clear();
            var pracownicy = db.Employees.Where(e => e.HotelId == hotel.Id).ToList();

            if (!pracownicy.Any()) { Console.WriteLine("Brak pracowników."); Console.ReadKey(); return; }

            decimal sumaPensji = pracownicy.Sum(e => e.Pensja);
            decimal srednia = pracownicy.Average(e => e.Pensja);

            Console.WriteLine($"Liczba pracowników: {pracownicy.Count}");
            Console.WriteLine($"Miesięczny koszt wypłat: {sumaPensji:C}");
            Console.WriteLine($"Średnia pensja:          {srednia:C}");
            Console.ReadKey();
        }
    }
}
