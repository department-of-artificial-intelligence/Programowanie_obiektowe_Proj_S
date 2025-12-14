using System;

namespace Projekt.Model
{
    public class Driver
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NumerPrawaJazdy { get; set; }

        public Driver(string imie, string nazwisko, string numerPrawaJazdy)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            NumerPrawaJazdy = numerPrawaJazdy;
        }
        public Driver()
        { 
            
        }
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} ({NumerPrawaJazdy})";
        }
    }
}