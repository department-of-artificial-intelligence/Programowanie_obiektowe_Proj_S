using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace silownia.model
{
    public class Trener: Osoba
    {
        public string Specjalizacja { get; set; }
        public string Dostepnosc {  get; set; }

        public Trener(int id, string imie, string nazwisko, string numerTelefonu, string specjalizacja, string dostepnosc): base(id, imie, nazwisko,numerTelefonu)
        {
            Specjalizacja = specjalizacja;
            Dostepnosc = dostepnosc;
        }
    }
}