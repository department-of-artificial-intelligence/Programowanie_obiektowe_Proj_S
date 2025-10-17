using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
osoba -> klijent
osoba -> pracownik

Banki 
    pracownik
        poziom dostępu
            [0-tylko czyta , 1 - twrzenie nowych kont klijentów , 3 - admin]
    klijent
        acount
        Historia
    aktywa banku



coś o zdolności kredytowej
spr czy osoba może wziąść kredyt
    czy nie ma ktedytu w innym banku
    czy posiada zdolność kredytową
transfery środków między kontami/bankami
wpłaty/wypłaty
dodawanie/usuwanie konta
odsetki "dla banku"
historia konta / ligi akcji
    czyszczenie historii [...]


logowanie się ?
  */
namespace jezwik_01.Model
{
    public class acoun
    {
        protected double _kwota;


        public acoun() { 
            Kwota = 0;
        }
        public double Kwota
        {
            get { return _kwota; } set { _kwota = value; }
        }
    }
    public class bank_acount : acoun
    {
        
        public bank_acount():base() { }
    }
    public class acces
    {
        protected int _pu; // poziom uprawnień

        public int Pu
        {
            get { return _pu; }
            set { _pu = value; }
        }
        acces() { Pu = 0; }
        acces(int i) {
            if ((i <= 0) && (i < 4)) //testowo zakres 0-3
            {
                Pu = i;
            }
        }
    }
    public class history
    {
        protected List<string> _lz { get; set; } // lista zmian

            
    }
    public class osoba
    {
        public required int _id ;
        protected string _name;
        protected string _second_name;

        public osoba() { _id = 0; _name = " "; _second_name = " "; }
    }
    public class klijent:osoba
    {


        protected acoun _kwota;
        public klijent():base() { }

        public double Kwota
        {
            get { return _kwota.Kwota; }
            set { _kwota.Kwota = value; }
        }
    }
    public class pracownik:osoba
    {
        public required acces _pu;

        public pracownik():base() { }
    }
    public class Bank
    {
        protected List<pracownik> workers;
        protected List<klijent> consumers;
        protected bank_acount aktywa;
    }


}
