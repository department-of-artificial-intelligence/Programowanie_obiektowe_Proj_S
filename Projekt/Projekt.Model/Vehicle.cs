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

        public Vehicle(string marka, string model, int przebieg, double silnik, int rocznik, string paliwo, string tablica)
        {
            Marka = marka;
            Model = model;
            Przebieg = przebieg;
            Silnik = silnik;
            Rocznik = rocznik;
            Paliwo = paliwo;
            Tablica = tablica;
        }
        public override string ToString()
        {
            return $"{Marka} {Model} przebieg {Przebieg} {Silnik} {Paliwo} {Tablica}";
        }


    }

}
