using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Car : Vehicle
    {
        public int LiczbaDrzwi { get; set; }
        public string Nadwozie { get; set; }

        public Car(string marka, string model, int przebieg, double silnik, int rocznik, string paliwo, string tablica, int liczbadrzwi, string nadwozie)
            : base(marka, model, przebieg, silnik, rocznik, paliwo, tablica)
        {
            LiczbaDrzwi = liczbadrzwi;
            Nadwozie = nadwozie;
        }
        public void Serwis(string tablica)
        {
            Console.WriteLine($"Serwis samochodu {tablica}");
            Console.WriteLine("Serwis objal wymiane oleju, filtrow i kontrole stanu samochodu");
        }
        public override string ToString()
        {
            return $"Samochod osobowy {base.ToString()} dzrwi {LiczbaDrzwi} w nadwoziu {Nadwozie}";
        }
    }
}

