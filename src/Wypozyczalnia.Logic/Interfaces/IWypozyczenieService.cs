using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wypozyczalnia.Model;


namespace Wypozyczalnia.Logic.Interfaces;

public interface IWypozyczenieService {
    Wypozyczenie Wypozycz(Klient klient, Rower rower, DateTime start, DateTime koniec);
    void Przedluz(Wypozyczenie wypozyczenie, int liczbaGodzin);
    void Zakoncz(Wypozyczenie wypozyczenie);
}