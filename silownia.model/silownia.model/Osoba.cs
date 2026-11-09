using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace silownia.model
{

    public abstract class Osoba
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NumerTelefonu { get; set; }

        protected Osoba(int id, string imie, string nazwisko, string numerTelefonu)
        {
            Id = id;
            Imie = imie;
            Nazwisko = nazwisko;
            NumerTelefonu = numerTelefonu;
        }

        public virtual string PobierzDane()
        {
            return $"{Imie} {Nazwisko} Tel: {NumerTelefonu}";
        }
    }
}