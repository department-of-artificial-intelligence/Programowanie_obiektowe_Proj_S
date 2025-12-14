using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;

namespace Project.Model
{
    class Program
    {
        static List<Hotel> Hotele = new();

        static void Main(string[] args)
        {
            Console.WriteLine("Inicjalizacja systemu i bazy danych...");

            IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                var cns = context.Configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(cns))
                {
                    cns = "Server=(localdb)\\mssqllocaldb;Database=HotelManagementDB;Trusted_Connection=True;";
                }

                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
            }).Build();

            using (var scope = _host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                if (context != null)
                {
                    context.Database.Migrate();
                    context.Database.EnsureCreated();

                    if (!context.Persons.Any(p => p.LastName == "Kowalski"))
                    {
                        Person person = new Person() { FirstName = "Jan", LastName = "Kowalski" };
                        context.Persons.Add(person);
                        context.SaveChanges();
                    }
                }
            }

            Console.WriteLine("\nSystem gotowy. Wciśnij dowolny klawisz, aby przejść do menu...");
            Console.ReadKey();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SYSTEM ZARZĄDZANIA SIECIĄ HOTELI ===");
                Console.WriteLine("1. Dodaj hotel");
                Console.WriteLine("2. Dodaj pracownika do hotelu");
                Console.WriteLine("3. Dodaj pokój do hotelu");
                Console.WriteLine("4. Dodaj rezerwację pokoju");
                Console.WriteLine("5. Dodaj usługę");
                Console.WriteLine("6. Dodaj rezerwację usługi");
                Console.WriteLine("7. Wyświetl hotele");
                Console.WriteLine("8. Usuń hotel");
                Console.WriteLine("9. Usuń pracownika");
                Console.WriteLine("10. Usuń rezerwację pokoju");
                Console.WriteLine("11. Usuń rezerwację usługi");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                string wybor = Console.ReadLine() ?? "";

                switch (wybor)
                {
                    case "1": DodajHotel(); break;
                    case "2": DodajPracownika(); break;
                    case "3": DodajPokoj(); break;
                    case "4": DodajRezerwacjePokoju(); break;
                    case "5": DodajUsluge(); break;
                    case "6": DodajRezerwacjeUslugi(); break;
                    case "7": WyswietlHotele(); break;
                    case "8": UsunHotel(); break;
                    case "9": UsunPracownika(); break;
                    case "10": UsunRezerwacjePokoju(); break;
                    case "11": UsunRezerwacjeUslugi(); break;
                    case "0": return;
                    default: Console.WriteLine("Nieprawidłowy wybór."); Console.ReadKey(); break;
                }
            }
        }

        static void DodajHotel()
        {
            Console.Clear();
            string nazwa = Helpers.GetNonEmptyString("Nazwa hotelu: ");
            string miasto = Helpers.GetNonEmptyString("Miasto: ");
            string adres = Helpers.GetNonEmptyString("Adres: ");
            int gwiazdki = Helpers.GetIntInRange("Liczba gwiazdek (1-5): ", 1, 5);
            int rok = Helpers.GetIntInRange("Rok otwarcia: ", 1900, DateTime.Now.Year);

            Hotele.Add(new Hotel
            {
                Nazwa = nazwa,
                Miasto = miasto,
                Adres = adres,
                Gwiazdki = gwiazdki,
                RokOtwarcia = rok
            });
            Console.WriteLine("✅ Hotel dodany!");
            Console.ReadKey();
        }

        static void DodajPracownika()
        {
            Console.Clear();
            var hotel = WybierzHotel();
            if (hotel == null) return;

            string imie = Helpers.GetNonEmptyAlpha("Imię: ");
            string nazwisko = Helpers.GetNonEmptyAlpha("Nazwisko: ");
            string telefon = Helpers.GetValidatedPhone9("Telefon (9 cyfr): ");
            string email = Helpers.GetValidatedEmail("Email: ");
            Console.WriteLine("Stanowiska: 1 - Recepcjonista, 2 - Kelner, 3 - Szef kuchni, 4 - Konserwator, 5 - Menedżer");
            int st = Helpers.GetIntInRange("Wybierz stanowisko: ", 1, 5);
            string stanowisko = st switch
            {
                1 => "Recepcjonista",
                2 => "Kelner",
                3 => "Szef kuchni",
                4 => "Konserwator",
                5 => "Menedżer",
                _ => "Pracownik"
            };
            DateTime dataZatrudnienia = Helpers.GetDate("Data zatrudnienia (dd-MM-yyyy): ");

            int id = hotel.Pracownicy.Count + 1;
            hotel.Pracownicy.Add(new Employees
            {
                Id = id,
                Imie = imie,
                Nazwisko = nazwisko,
                Telefon = telefon,
                Email = email,
                Stanowisko = stanowisko,
                DataZatrudnienia = dataZatrudnienia
            });
            Console.WriteLine("✅ Pracownik dodany!");
            Console.ReadKey();
        }

        static void DodajPokoj()
        {
            Console.Clear();
            var hotel = WybierzHotel();
            if (hotel == null) return;

            if (hotel.Pokoje.Any())
            {
                Console.WriteLine("Już istniejące pokoje:");
                foreach (var p in hotel.Pokoje) Console.WriteLine(p);
            }

            int numer = Helpers.GetIntPositive("Numer pokoju: ");
            if (hotel.Pokoje.Any(p => p.Numer == numer))
            {
                Console.WriteLine("❌ Pokój o tym numerze już istnieje!");
                Console.ReadKey();
                return;
            }

            int miejsca = Helpers.GetIntPositive("Liczba miejsc: ");
            Console.WriteLine("Typ pokoju: 1-Standard, 2-Deluxe, 3-Suite");
            int typ = Helpers.GetIntInRange("Wybierz typ: ", 1, 3);
            RoomType typPok = typ switch { 1 => RoomType.Standard, 2 => RoomType.Deluxe, 3 => RoomType.Suite, _ => RoomType.Standard };
            decimal cena = Helpers.GetDecimalPositive("Cena za dobę: ");

            hotel.Pokoje.Add(new Room
            {
                Numer = numer,
                LiczbaMiejsc = miejsca,
                Typ = typPok,
                CenaZaDobe = cena
            });
            Console.WriteLine("✅ Pokój dodany!");
            Console.ReadKey();
        }

        static void DodajRezerwacjePokoju()
        {
            Console.Clear();
            var hotel = WybierzHotel();
            if (hotel == null) return;

            if (!hotel.PokojeWolne.Any())
            {
                Console.WriteLine("❌ Brak wolnych pokoi w hotelu!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Dostępne pokoje:");
            foreach (var p in hotel.PokojeWolne) Console.WriteLine(p);

            int numer = Helpers.GetIntInRange("Numer pokoju do rezerwacji: ", 1, 10000);
            var pokoj = hotel.Pokoje.FirstOrDefault(p => p.Numer == numer && p.Dostepny);
            if (pokoj == null)
            {
                Console.WriteLine("❌ Pokój niedostępny!");
                Console.ReadKey();
                return;
            }

            string imie = Helpers.GetNonEmptyAlpha("Imię klienta: ");
            string nazwisko = Helpers.GetNonEmptyAlpha("Nazwisko klienta: ");
            string email = Helpers.GetValidatedEmail("Email klienta: ");
            string telefon = Helpers.GetValidatedPhone9("Telefon klienta (9 cyfr): ");
            int dni = Helpers.GetIntPositive("Ilość dni pobytu: ");
            DateTime od = Helpers.GetFutureOrTodayDate("Data przyjazdu (dd-MM-yyyy): ");
            DateTime doo = od.AddDays(dni);

            int id = hotel.Rezerwacje.Count + 1;
            hotel.Rezerwacje.Add(new Reservation
            {
                Id = id,
                Gosc = new Guest { Imie = imie, Nazwisko = nazwisko, Email = email, Telefon = telefon },
                Pokoj = pokoj,
                DataOd = od,
                DataDo = doo
            });
            pokoj.Zarezerwuj(od, doo);
            Console.WriteLine("✅ Rezerwacja dodana!");
            Console.ReadKey();
        }

        static void DodajUsluge()
        {
            Console.Clear();
            var hotel = WybierzHotel();
            if (hotel == null) return;

            Console.WriteLine("Dostępne usługi do dodania: 1-SPA, 2-Siłownia, 3-Masaż, 4-Basen");
            int typ = Helpers.GetIntInRange("Wybierz usługę: ", 1, 4);
            string nazwa = typ switch
            {
                1 => "SPA",
                2 => "Siłownia",
                3 => "Masaż",
                4 => "Basen",
                _ => "Usługa"
            };
            decimal cena = Helpers.GetDecimalPositive("Cena: ");
            int id = hotel.Uslugi.Count + 1;
            hotel.Uslugi.Add(new Service { Id = id, Nazwa = nazwa, Cena = cena });

            Console.WriteLine("✅ Usługa dodana!");
            Console.ReadKey();
        }

        static void DodajRezerwacjeUslugi()
        {
            Console.Clear();
            var hotel = WybierzHotel();
            if (hotel == null) return;
            if (!hotel.Uslugi.Any()) { Console.WriteLine("❌ Brak usług w hotelu!"); Console.ReadKey(); return; }

            string imie = Helpers.GetNonEmptyAlpha("Imię klienta: ");
            string nazwisko = Helpers.GetNonEmptyAlpha("Nazwisko klienta: ");
            string email = Helpers.GetValidatedEmail("Email klienta: ");
            string telefon = Helpers.GetValidatedPhone9("Telefon klienta (9 cyfr): ");
            DateTime data = Helpers.GetFutureOrTodayDate("Data rezerwacji usługi (dd-MM-yyyy): ");

            Console.WriteLine("Dostępne usługi:");
            foreach (var u in hotel.Uslugi) Console.WriteLine(u);

            int nr = Helpers.GetIntInRange("Numer usługi: ", 1, hotel.Uslugi.Count);
            var usluga = hotel.Uslugi.FirstOrDefault(u => u.Id == nr);
            int id = hotel.RezerwacjeUslug.Count + 1;
            hotel.RezerwacjeUslug.Add(new ServiceReservation
            {
                Id = id,
                Gosc = new Guest { Imie = imie, Nazwisko = nazwisko, Email = email, Telefon = telefon },
                Usluga = usluga,
                Data = data
            });

            Console.WriteLine("✅ Rezerwacja usługi dodana!");
            Console.ReadKey();
        }

        static void UsunPracownika()
        {
            Console.Clear();
            var hotel = WybierzHotel();
            if (hotel == null) return;
            if (!hotel.Pracownicy.Any()) { Console.WriteLine("❌ Brak pracowników!"); Console.ReadKey(); return; }

            foreach (var p in hotel.Pracownicy) Console.WriteLine(p);
            int id = Helpers.GetIntInRange("Podaj ID pracownika do usunięcia: ", 1, hotel.Pracownicy.Count);
            var pracownik = hotel.Pracownicy.FirstOrDefault(p => p.Id == id);
            if (pracownik != null) hotel.Pracownicy.Remove(pracownik);
            Console.WriteLine("✅ Pracownik usunięty!");
            Console.ReadKey();
        }

        static void UsunRezerwacjePokoju()
        {
            Console.Clear();
            var hotel = WybierzHotel();
            if (hotel == null) return;
            if (!hotel.Rezerwacje.Any()) { Console.WriteLine("❌ Brak rezerwacji!"); Console.ReadKey(); return; }

            foreach (var r in hotel.Rezerwacje) Console.WriteLine(r);
            int id = Helpers.GetIntInRange("Podaj ID rezerwacji do usunięcia: ", 1, hotel.Rezerwacje.Count);
            var res = hotel.Rezerwacje.FirstOrDefault(r => r.Id == id);
            if (res != null)
            {
                res.Pokoj.Zwolnij();
                hotel.Rezerwacje.Remove(res);
                Console.WriteLine("✅ Rezerwacja pokoju usunięta!");
            }
            Console.ReadKey();
        }

        static void UsunRezerwacjeUslugi()
        {
            Console.Clear();
            var hotel = WybierzHotel();
            if (hotel == null) return;
            if (!hotel.RezerwacjeUslug.Any()) { Console.WriteLine("❌ Brak rezerwacji usług!"); Console.ReadKey(); return; }

            foreach (var r in hotel.RezerwacjeUslug) Console.WriteLine(r);
            int id = Helpers.GetIntInRange("Podaj ID rezerwacji usługi do usunięcia: ", 1, hotel.RezerwacjeUslug.Count);
            var res = hotel.RezerwacjeUslug.FirstOrDefault(r => r.Id == id);
            if (res != null) { hotel.RezerwacjeUslug.Remove(res); Console.WriteLine("✅ Rezerwacja usługi usunięta!"); }
            Console.ReadKey();
        }

        static void WyswietlHotele()
        {
            Console.Clear();
            if (!Hotele.Any()) { Console.WriteLine("Brak hoteli w systemie."); Console.ReadKey(); return; }

            foreach (var hotel in Hotele)
            {
                Console.WriteLine(hotel);
                Console.WriteLine("Pracownicy:");
                if (hotel.Pracownicy.Any()) hotel.Pracownicy.ForEach(p => Console.WriteLine(" - " + p));
                else Console.WriteLine(" Brak pracowników.");

                Console.WriteLine("Pokoje:");
                if (hotel.Pokoje.Any()) hotel.Pokoje.ForEach(p => Console.WriteLine(" - " + p));
                else Console.WriteLine(" Brak pokoi.");

                Console.WriteLine("Rezerwacje pokoi:");
                if (hotel.Rezerwacje.Any()) hotel.Rezerwacje.ForEach(r => Console.WriteLine(" - " + r));
                else Console.WriteLine(" Brak rezerwacji.");

                Console.WriteLine("Usługi:");
                if (hotel.Uslugi.Any()) hotel.Uslugi.ForEach(u => Console.WriteLine(" - " + u));
                else Console.WriteLine(" Brak usług.");

                Console.WriteLine("Rezerwacje usług:");
                if (hotel.RezerwacjeUslug.Any()) hotel.RezerwacjeUslug.ForEach(r => Console.WriteLine(" - " + r));
                else Console.WriteLine(" Brak rezerwacji usług.");
                Console.WriteLine("----------------------------------------------------");
            }
            Console.ReadKey();
        }

        static void UsunHotel()
        {
            Console.Clear();
            var hotel = WybierzHotel();
            if (hotel == null) return;

            Hotele.Remove(hotel);
            Console.WriteLine("✅ Hotel usunięty!");
            Console.ReadKey();
        }

        static Hotel WybierzHotel()
        {
            if (!Hotele.Any())
            {
                Console.WriteLine("❌ Brak hoteli w systemie!");
                Console.ReadKey();
                return null;
            }

            Console.WriteLine("Dostępne hotele:");
            for (int i = 0; i < Hotele.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Hotele[i].Nazwa} ({Hotele[i].Miasto})");
            }

            int wybor = Helpers.GetIntInRange("Wybierz numer hotelu: ", 1, Hotele.Count);
            return Hotele[wybor - 1];
        }
    }
}