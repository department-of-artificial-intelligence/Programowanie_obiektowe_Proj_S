using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Projekt.DAL;
using Projekt.Model;

namespace Projekt.ConsoleApp
{
    public class Management : IManagement
    {
        private readonly ApplicationDbContext _context;
        public Management(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool DodajPojazd(Vehicle pojazd)
        {
            bool istnieje = _context.Vehicles.Any(p => p.Tablica == pojazd.Tablica);

            if (istnieje)
            {
                Console.WriteLine($"Blad: pojazd o tablicy {pojazd.Tablica} juz istnieje w bazie.");
                return false;
            }
            _context.Vehicles.Add(pojazd);
            _context.SaveChanges();
            Console.WriteLine($"Dodano do bazy: {pojazd.Marka} {pojazd.Model} {pojazd.Tablica}");
            return true;
        }
        public Vehicle ZnajdzPojazdPoRejestracji(string tablica)
        {
            var pojazd = _context.Vehicles
                .Include(v => v.PrzypisanyKierowca)
                .Include(v => v.HistoriaSerwisowa)
                .FirstOrDefault(p => p.Tablica == tablica);
            if (pojazd == null)
            {
                Console.WriteLine($"Blad: nie znaleziono w bazie pojazdu {tablica}");
            }
            return pojazd;
        }
        public bool UsunPojazd(string tablica)
        {
            var pojazdDoUsuniecia = ZnajdzPojazdPoRejestracji(tablica);
            if (pojazdDoUsuniecia != null)
            {
                _context.Vehicles.Remove(pojazdDoUsuniecia);
                _context.SaveChanges();
                Console.WriteLine($"Usunieto z bazy pojazd: {pojazdDoUsuniecia.Marka} {tablica}");
                return true;
            }
            return false;
        }
        public void PokazWszystkie()
        {
            var wszystkiePojazdy = _context.Vehicles
                .Include(v => v.PrzypisanyKierowca)
                .AsNoTracking()
                .ToList();
            if (!wszystkiePojazdy.Any())
            {
                Console.WriteLine("Baza danych jest pusta.");
                return;
            }

            Console.WriteLine("Lista Pojazdow w bazie:");
            foreach (var pojazd in wszystkiePojazdy)
            {
                Console.WriteLine(pojazd.ToString());
            }
        }
        public bool PrzypiszKierowceDoPojazdu(string tablica, Driver kierowca)
        {
            var pojazd = ZnajdzPojazdPoRejestracji(tablica);
            if (pojazd != null)
            {
                if (kierowca.Id == 0)
                {
                    _context.Drivers.Add(kierowca);
                }
                pojazd.PrzypiszKierowce(kierowca);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DodajWpisSerwisowy(string tablica, string opis, double koszt)
        {
            var pojazd = ZnajdzPojazdPoRejestracji(tablica);
            if (pojazd != null)
            {
                pojazd.DodajWpisSerwisowy(opis, koszt);
                _context.SaveChanges();
                Console.WriteLine("Zapisano serwis w bazie.");
                return true;
            }
            return false;
        }
        public void PokazSerwisPojazdu(string tablica)
        {
            var pojazd = ZnajdzPojazdPoRejestracji(tablica);

            if (pojazd != null)
            {
                pojazd.PokazSerwisy();
            }
        }
    }
}