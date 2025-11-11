using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projekt.Model
{
    public class Vehicle
    {

        public string Marka { get; set; }
        public string Model { get; set; }
        public int Przebieg { get; set; }
        public double Silnik { get; set; }
        public int Rocznik { get; set; }
        public string Paliwo { get; set; }
        public string Tablica { get; set; }
        public Driver PrzypisanyKierwoca { get; set; }
        public List<Service> HistoriaSerwisowa { get; set; }
        public Vehicle(string marka, string model, int przebieg, double silnik, int rocznik, string paliwo, string tablica)
        {
            Marka = marka;
            Model = model;
            Przebieg = przebieg;
            Silnik = silnik;
            Rocznik = rocznik;
            Paliwo = paliwo;
            Tablica = tablica;
            HistoriaSerwisowa = new List<Service>();
        }
        public void PrzypiszKierowce(Driver kierowca) 
        {
            PrzypisanyKierwoca = kierowca;
            Console.WriteLine($"Przypisano kierowce {kierowca.Imie} do pojazdu {Tablica}");
        }
        public void UsunKierowce()
        {
            PrzypisanyKierwoca = null;
        }
        public void DodajWpisSerwisowy(string opis, double koszt)
        {
            Service nowyWpis = new Service(opis, koszt);
            HistoriaSerwisowa.Add(nowyWpis);
            Console.WriteLine($"Dodano wpis serwisowy do {Tablica}- {opis}");
        
        }
        public void PokazSerwisy()
        {
            Console.WriteLine($"Historia serwisowa dla {Marka} {Model} {Tablica}");
            foreach (var wpis in HistoriaSerwisowa)
            {
                Console.WriteLine(wpis);
            }
        }
        public override string ToString()
        {
            return $"{Marka} {Model} przebieg {Przebieg} {Silnik} {Paliwo} {Tablica}";
        }


    }

}
