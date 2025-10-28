using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Rocznik { get; set; }
        private string numerRejestracyjny;
        public decimal CenaZaDobe { get; set; }


        public string GetNumerRejestracyjny()
        {hgjghjgh
            return numerRejestracyjny;
        }

        public void SetNumerRejestracyjny(string value)
        {
            numerRejestracyjny = value;
        }fgddg

    }
}
