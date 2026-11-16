using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wypozyczalnia.Logic.Interfaces;
using Wypozyczalnia.Model;

namespace Wypozyczalnia.Logic;

public class WypozyczenieService : IWypozyczenieService {
    private readonly IRowerService _rowerService;
    public WypozyczenieService(IRowerService rowerService) {
        _rowerService = rowerService;
    }
    public Wypozyczenie Wypozycz(Klient klient, Rower rower, DateTime start, DateTime koniec) {
        if (!_rowerService.CzyDostepny(rower))
            throw new InvalidOperationException($"Rower {rower.Nazwa} nie jest dostępny.");
        if (koniec <= start)
            throw new ArgumentException("Data konca musi byc pozniejsza niz data startu.");
        _rowerService.ZmienStatus(rower, StatusRoweru.Wypozyczony);
        return new Wypozyczenie {
            Klient = klient,
            Rower = rower,
            DataStart = start,
            DataKonca = koniec
        };
    }

    public void Przedluz(Wypozyczenie wypozyczenie, int liczbaGodzin) {
        if (liczbaGodzin <= 0)
            throw new ArgumentException("Liczba godzin musi byc wieksza niz 0");
        wypozyczenie.DataKonca = wypozyczenie.DataKonca.AddHours(liczbaGodzin);
    }
        
    public void Zakoncz(Wypozyczenie wypozyczenie) {
        wypozyczenie.DataKonca = DateTime.Now;
        _rowerService.ZmienStatus(wypozyczenie.Rower, StatusRoweru.Dostepny);
    }
}
