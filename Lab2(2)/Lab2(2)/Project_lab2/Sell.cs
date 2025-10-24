using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_2_.Project_lab2
{
    class Sprzedaz : Invertar
    {
        public void Sprzedaj(string nazwa, int ilosc)
        {
            var towar = _towars.FirstOrDefault(t => t.Nazwa == nazwa);
            if (towar == null)
            {
                Console.WriteLine("Tego towaru nie ma w magazynie!");
                return;
            }

            if (towar.Ilosc < ilosc)
            {
                Console.WriteLine("Za mało na stanie!");
                return;
            }

            towar.Ilosc -= ilosc;
            Console.WriteLine($"Sprzedano {ilosc} szt. {towar.Nazwa}. Pozostało: {towar.Ilosc}");
        }
    }


}
