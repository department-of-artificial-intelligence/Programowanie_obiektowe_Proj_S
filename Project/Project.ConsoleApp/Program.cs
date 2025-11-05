#nullable disable
using System;
using System.Collections.Generic;

namespace SiecHoteli
{
    class Program
    {
        static List<Hotel> Hotele = new();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== SYSTEM ZARZĄDZANIA SIECIĄ HOTELI ===");
                Console.WriteLine("1. Dodaj hotel");
                Console.WriteLine("2. Dodaj pracownika do hotelu");
                Console.WriteLine("3. Dodaj pokój do hotelu");
                Console.WriteLine("4. Dodaj rezerwację");
                Console.WriteLine("5. Wyświetl hotele");
                Console.WriteLine("6. Usuń hotel");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                string wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1": DodajHotel(); break;
                    case "2": DodajPracownika(); break;
                    case "3": DodajPokoj(); break;
                    case "4": DodajRezerwacje(); break;
                    case "5": WyswietlHotele(); break;
                    case "6": UsunHotel(); break;
                    case "0": return;
                    default: Console.WriteLine("Nieprawidłowy wybór."); break;
                }
            }
        }
        
        static void DodajHotel()
        {
            Console.Write("Podaj nazwę hotelu: ");
            string nazwa = Console.ReadLine();
            Console.Write("Podaj miasto: ");
            string miasto = Console.ReadLine();

            Hotele.Add(new Hotel { Nazwa = nazwa ?? string.Empty, Miasto = miasto ?? string.Empty });
            Console.WriteLine("✅ Hotel dodany!");
        }

        static void DodajPracownika()
        {
            Hotel hotel = WybierzHotel();
            if (hotel == null) return;

            Console.Write("Imię: ");
            string imie = Console.ReadLine();
            Console.Write("Nazwisko: ");
            string nazwisko = Console.ReadLine();
            Console.Write("Stanowisko: ");
            string stanowisko = Console.ReadLine();

            int id = hotel.Pracownicy.Count + 1;
            hotel.Pracownicy.Add(new Employees
            {
                Id = id,
                Imie = imie ?? string.Empty,
                Nazwisko = nazwisko ?? string.Empty,
                Stanowisko = stanowisko ?? string.Empty
            });
            Console.WriteLine("✅ Pracownik dodany!");
        }

        static void DodajPokoj()
        {
            Hotel hotel = WybierzHotel();
            if (hotel == null) return;

            Console.Write("Numer pokoju: ");
            if (!int.TryParse(Console.ReadLine(), out int numer))
            {
                Console.WriteLine("Niepoprawny numer.");
                return;
            }

            Console.Write("Liczba miejsc: ");
            if (!int.TryParse(Console.ReadLine(), out int miejsca))
            {
                Console.WriteLine("Niepoprawna liczba.");
                return;
            }

            hotel.Pokoje.Add(new Room { Numer = numer, LiczbaMiejsc = miejsca });
            Console.WriteLine("✅ Pokój dodany!");
        }

        static void DodajRezerwacje()
        {
            Hotel hotel = WybierzHotel();
            if (hotel == null) return;

            Console.Write("Numer pokoju: ");
            if (!int.TryParse(Console.ReadLine(), out int numer))
            {
                Console.WriteLine("Niepoprawny numer.");
                return;
            }

            Room pokoj = hotel.Pokoje.Find(p => p.Numer == numer);
            if (pokoj == null || !pokoj.Dostepny)
            {
                Console.WriteLine("❌ Pokój niedostępny!");
                return;
            }

            Console.Write("Imię klienta: ");
            string imie = Console.ReadLine();
            Console.Write("Nazwisko klienta: ");
            string nazwisko = Console.ReadLine();

            Console.Write("Data od (rrrr-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime od))
            {
                Console.WriteLine("Niepoprawna data.");
                return;
            }

            Console.Write("Data do (rrrr-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime doo))
            {
                Console.WriteLine("Niepoprawna data.");
                return;
            }

            int id = hotel.Rezerwacje.Count + 1;
            hotel.Rezerwacje.Add(new Reservation
            {
                Id = id,
                ImieKlienta = imie ?? string.Empty,
                NazwiskoKlienta = nazwisko ?? string.Empty,
                Pokoj = pokoj,
                DataOd = od,
                DataDo = doo
            });

            pokoj.Dostepny = false;
            Console.WriteLine("✅ Rezerwacja dodana!");
        }

        static void WyswietlHotele()
        {
            if (Hotele.Count == 0)
            {
                Console.WriteLine("Brak hoteli.");
                return;
            }

            foreach (var hotel in Hotele)
            {
                Console.WriteLine($"\n🏨 {hotel.Nazwa} ({hotel.Miasto})");
                Console.WriteLine("Pracownicy:");
                hotel.Pracownicy.ForEach(p => Console.WriteLine(" - " + p));
                Console.WriteLine("Pokoje:");
                hotel.Pokoje.ForEach(p => Console.WriteLine(" - " + p));
                Console.WriteLine("Rezerwacje:");
                hotel.Rezerwacje.ForEach(r => Console.WriteLine(" - " + r));
            }
        }

        static void UsunHotel()
        {
            Hotel hotel = WybierzHotel();
            if (hotel == null) return;

            Hotele.Remove(hotel);
            Console.WriteLine("✅ Hotel usunięty!");
        }

        static Hotel WybierzHotel()
        {
            if (Hotele.Count == 0)
            {
                Console.WriteLine("Brak hoteli w systemie!");
                return null;
            }

            for (int i = 0; i < Hotele.Count; i++)
                Console.WriteLine($"{i + 1}. {Hotele[i]}");

            Console.Write("Wybierz numer hotelu: ");
            if (int.TryParse(Console.ReadLine(), out int wybor) && wybor > 0 && wybor <= Hotele.Count)
                return Hotele[wybor - 1];

            Console.WriteLine("Nieprawidłowy wybór!");
            return null;
        }
    }
}
