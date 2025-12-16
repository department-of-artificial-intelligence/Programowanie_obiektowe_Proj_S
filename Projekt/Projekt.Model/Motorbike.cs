using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Motorbike : Vehicle
    {
        public int PojemnoscSilnikaCm3 { get; set; }
        public string TypRamy { get; set; }
        public Motorbike()
        {

        }

        public Motorbike(string marka, string model, int przebieg, double silnik, int rocznik, string paliwo, string tablica, int pojemnoscSilnikaCm3, string typRamy)
        : base(marka, model, przebieg, silnik, rocznik, paliwo, tablica)
        {
            PojemnoscSilnikaCm3 = pojemnoscSilnikaCm3;
            TypRamy = typRamy;
        }
        public override string ToString()
        {
            return $"Motocykl {base.ToString()} Pojemność: {PojemnoscSilnikaCm3}cm3 Typ: {TypRamy}";
        }

    }
}

