using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Model;

namespace Project.ConsoleApp
{
    public static class ServiceManager
    {
        public static void Manage(ApplicationDbContext db, Hotel hotel)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== USŁUGI I RACHUNKI: {hotel.Nazwa.ToUpper()} ===");
                Console.WriteLine("1. Lista usług (Cennik)");
                Console.WriteLine("2. Dodaj usługę do cennika");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("3. ZAMÓW USŁUGĘ DO POKOJU");
                Console.WriteLine("4. RACHUNEK KOŃCOWY");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("0. Wróć");
                Console.Write("\n>> Wybierz opcję: ");

                switch (Console.ReadLine())
                {
                    case "1": ListServices(db, hotel); Console.ReadKey(); break;
                    case "2": AddServiceDefinition(db, hotel); break;
                    case "3": OrderService(db, hotel); break;
                    case "4": Checkout(db, hotel); break;
                    case "0": return;
                }
            }
        }

        private static void ListServices(ApplicationDbContext db, Hotel hotel)
        {
            var uslugi = db.Services.Where(s => s.HotelId == hotel.Id).ToList();
            if (!uslugi.Any()) Console.WriteLine("  (Brak usług w cenniku)");
            else foreach (var s in uslugi) Console.WriteLine($"  - {s.Nazwa}: {s.Cena:C}");
        }

        private static void AddServiceDefinition(ApplicationDbContext db, Hotel hotel)
        {
            Console.WriteLine("\n>>> DODAWANIE USŁUGI DO CENNIKA");
            Console.WriteLine("Wybierz typ usługi:");
            Console.WriteLine("1. Śniadanie");
            Console.WriteLine("2. SPA - Masaż");
            Console.WriteLine("3. Siłownia - Wejście");
            Console.WriteLine("4. Basen");
            Console.WriteLine("5. Parking (doba)");
            Console.WriteLine("6. Inna (własna)");

            int choice = Helpers.GetIntInRange(">> Wybierz (1-6): ", 1, 6);

            string nazwa = choice switch
            {
                1 => "Śniadanie",
                2 => "SPA - Masaż",
                3 => "Siłownia - Wejście",
                4 => "Basen",
                5 => "Parking (doba)",
                _ => Helpers.GetNonEmptyString("Podaj nazwę usługi: ")
            };

            decimal cena = Helpers.GetDecimalPositive("Cena usługi: ");
            db.Services.Add(new Service { Nazwa = nazwa, Cena = cena, HotelId = hotel.Id });
            db.SaveChanges();

            Console.WriteLine("✔ Dodano do cennika.");
            Console.ReadKey();
        }

        private static void OrderService(ApplicationDbContext db, Hotel hotel)
        {
            Console.Clear();
            Console.WriteLine(">>> ZAMAWIANIE USŁUGI");

            var rezerwacje = db.Reservations
                .Include(r => r.Gosc).Include(r => r.Pokoj)
                .Where(r => r.HotelId == hotel.Id)
                .OrderBy(r => r.DataOd).ToList();

            if (!rezerwacje.Any()) { Console.WriteLine("Brak zameldowanych gości."); Console.ReadKey(); return; }

            Console.WriteLine("Wybierz rezerwację:");
            for (int i = 0; i < rezerwacje.Count; i++)
                Console.WriteLine($"{i + 1}. Pokój {rezerwacje[i].Pokoj.Numer} | {rezerwacje[i].Gosc.PelneDane()}");

            int rIdx = Helpers.GetIntInRange(">> Numer: ", 1, rezerwacje.Count);
            var rez = rezerwacje[rIdx - 1];

            var uslugi = db.Services.Where(s => s.HotelId == hotel.Id).ToList();
            if (!uslugi.Any()) { Console.WriteLine("Cennik pusty."); Console.ReadKey(); return; }

            Console.WriteLine("\nDostępne usługi:");
            for (int i = 0; i < uslugi.Count; i++)
                Console.WriteLine($"{i + 1}. {uslugi[i].Nazwa} ({uslugi[i].Cena:C})");

            int sIdx = Helpers.GetIntInRange(">> Wybierz: ", 1, uslugi.Count);
            var usl = uslugi[sIdx - 1];

            var data = Helpers.GetDate("Data usługi (DD-MM-YYYY): ");
            if (data < rez.DataOd || data >= rez.DataDo)
            {
                Console.WriteLine("Błąd: Data usługi poza terminem pobytu!"); Console.ReadKey(); return;
            }

            db.ServiceReservations.Add(new ServiceReservation
            {
                ReservationId = rez.Id,
                ServiceId = usl.Id,
                Data = data,
                CenaWChwiliZakupu = usl.Cena
            });
            db.SaveChanges();

            Console.WriteLine("Zamówiono."); Console.ReadKey();
        }

        private static void Checkout(ApplicationDbContext db, Hotel hotel)
        {
            Console.Clear();
            var rezerwacje = db.Reservations
                 .Include(r => r.Gosc).Include(r => r.Pokoj)
                 .Where(r => r.HotelId == hotel.Id).ToList();

            if (!rezerwacje.Any()) { Console.WriteLine("Brak rezerwacji."); Console.ReadKey(); return; }

            Console.WriteLine("Wybierz rezerwację do rozliczenia:");
            for (int i = 0; i < rezerwacje.Count; i++)
                Console.WriteLine($"{i + 1}. {rezerwacje[i].Gosc.Nazwisko} (Pokój {rezerwacje[i].Pokoj.Numer})");

            int idx = Helpers.GetIntInRange(">> Wybierz: ", 1, rezerwacje.Count);
            var rez = rezerwacje[idx - 1];

            var zamowione = db.ServiceReservations.Include(sr => sr.Service).Where(sr => sr.ReservationId == rez.Id).ToList();

            Console.Clear();
            Console.WriteLine($"RACHUNEK: {hotel.Nazwa}\nGość: {rez.Gosc.PelneDane()}\n----------------");
            decimal suma = rez.Koszt;
            Console.WriteLine($"Noclegi: {suma:C}");

            foreach (var s in zamowione)
            {
                Console.WriteLine($"+ {s.Service.Nazwa}: {s.CenaWChwiliZakupu:C}");
                suma += s.CenaWChwiliZakupu;
            }
            Console.WriteLine("----------------\nSUMA: " + suma.ToString("C"));
            Console.ReadKey();
        }
    }
}
