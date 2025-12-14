using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Service
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Opis { get; set; }
        public double Koszt { get; set; }

        public Service(string opis, double koszt)
        {
            Data = DateTime.Now;
            Opis = opis;
            Koszt = koszt;
        }

        public override string ToString()
        {
            return $"[{Data}]: Wykonano {Opis} - Koszt {Koszt:C}";
        }
    }
}
