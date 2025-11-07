using System;
using System.Linq;

namespace Zadanie
{
    class Ocena
    {
        public double Wartosc { get; set; }

        public Ocena(double wartosc) { this.Wartosc = wartosc; }

        public override string ToString()
        {
            return $"Wartość oceny: {Wartosc}";
        }
    }

    class Program
    {
        public static void Main()
        {
            Ocena o1 = new(4.0);
            Ocena o2 = new(3.5);
            Ocena o3 = new(2.0);

            List<Ocena> listaOcen = new List<Ocena>{o1, o2, o3};

            var ocenyPozytywne = listaOcen.Where(o => o.Wartosc > 2.0);

            foreach(var o in ocenyPozytywne) Console.WriteLine($"Pozytywne oceny: {o}");
        }

    }
}