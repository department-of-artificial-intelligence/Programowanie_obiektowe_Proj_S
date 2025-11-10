using System;

namespace Projekt.Model
{
    public class Kierowca
    {
        public Guid Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NumerPrawaJazdy { get; set; }

        public Kierowca(string imie, string nazwisko, string numerPrawaJazdy)
        {
            Id = Guid.NewGuid();
            Imie = imie;
            Nazwisko = nazwisko;
            NumerPrawaJazdy = numerPrawaJazdy;
        }

        public override string ToString()
        {
            return $"{Imie} {Nazwisko} ({NumerPrawaJazdy})";
        }
    }
}