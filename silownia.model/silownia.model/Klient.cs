using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace silownia.model
{
    public class Klient : Osoba
    {
        public string RodzajKarnetu { get; set; }
        public DateTime DataDolaczenia { get; set; }

        public Klient(int id, string imie, string nazwisko, string numerTelefonu, string rodzajKarnetu) : base(id, imie, nazwisko, numerTelefonu)
        {
            RodzajKarnetu = rodzajKarnetu;
            DataDolaczenia = DateTime.Now;
        }
    }
}