using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Truck : Vehicle
    {
        public double Ladownosc { get; set; }
        public int LiczbaOsi { get; set; }
        public Truck()
        {

        }
        public Truck(string marka, string model, int przebieg, double silnik, int rocznik, string paliwo, string tablica,
        double ladownosc, int liczbaosi) : base(marka, model, przebieg, silnik, rocznik, paliwo, tablica)
        {
            Ladownosc = ladownosc;
            LiczbaOsi = liczbaosi;
        }
        public void Serwis(string tablica)
        {
            Console.WriteLine($"Serwis ciezarowki {tablica}");
            Console.WriteLine("Serwis objal przeglad tachografu, smarowanie siodla, kontrole stanu ciagnika");
        }
        public override string ToString()
        {
            return $"Ciezarowka {base.ToString()} ladownosc {Ladownosc} liczba osi {LiczbaOsi}";
        }
    }
}
