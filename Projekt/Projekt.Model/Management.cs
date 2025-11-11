using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Management: IManagement
    {
        private List<Vehicle> pojazdy;
        public Management()
        { 
            pojazdy = new List<Vehicle>();
        }
        public bool DodajPojazd(Vehicle pojazd)
        {
            bool istnieje = pojazdy.Any(p => p.Tablica.Equals(pojazd.Tablica, StringComparison.OrdinalIgnoreCase));
            if (istnieje)
            {
                Console.WriteLine($"Blad pojazd o tablicy {pojazd.Tablica} juz istnieje");
                return false;
            }
            pojazdy.Add(pojazd);
            Console.WriteLine($"Dodano {pojazd.Marka} {pojazd.Model} {pojazd.Tablica}");
            return true;
        }
        public Vehicle ZnajdzPojazdPoRejestracji(string tablica)
        {
            var pojazd = pojazdy.FirstOrDefault(p => p.Tablica.Equals(tablica, StringComparison.OrdinalIgnoreCase));
            if (pojazd==null)
            {
                Console.WriteLine($"Bład nie znaleziono pojazdu {tablica}");
            }
            return pojazd;
        }
        public bool UsunPojazd(string tablica)
        {
            Vehicle pojazdDoUsuniecia = ZnajdzPojazdPoRejestracji(tablica);
            if (pojazdDoUsuniecia != null)
            {
                pojazdy.Remove(pojazdDoUsuniecia);
                Console.WriteLine($"Usunieto pojazd {pojazdDoUsuniecia.Marka} {tablica}");
                return true;
            }
            return false;
        }
        public void PokazWszystkie()
        {
            if (!pojazdy.Any())
            { 
                Console.WriteLine("Flota pusta");
                return;
            }
            Console.WriteLine("Lista Pojazdow we flocie");
            foreach (var pojazd in pojazdy)
            {
                Console.WriteLine(pojazd);
            }
        }
        public bool PrzypiszKierowceDoPojazdu(string tablica, Driver kierowca)
        {
            var pojazd = ZnajdzPojazdPoRejestracji(tablica);
            if (pojazd!=null)
            {
                pojazd.PrzypiszKierowce(kierowca);
                return true;
            }
            return false;
        }
        public bool DodajWpisSerwisowy(string tablica, string opis, double koszt)
        {
            var pojazd = ZnajdzPojazdPoRejestracji(tablica);
            if (pojazd!=null)
            {
                pojazd.DodajWpisSerwisowy(opis, koszt);
                return true;
            }
            return false;
        }
        public void PokazSerwisPojazdu(string tablica)
        {
            var pojazd = ZnajdzPojazdPoRejestracji(tablica);
            pojazd?.PokazSerwisy();
        }
    }
}
