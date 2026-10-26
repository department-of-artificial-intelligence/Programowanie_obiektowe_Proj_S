using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp;

public class Wypozyczenie {
    private int id;
    private Klient klient;
    private Rower rower;
    private DateTime dataStart;
    private DateTime dataKonca;

    public Wypozyczenie(int id, Klient klient, Rower rower, DateTime dataStart, DateTime dataKonca) {
        this.id = id;
        this.klient = klient;
        this.rower = rower;
        this.dataStart = dataStart;
        this.dataKonca = dataKonca;
    }
    public void Przedluz(int liczbaGodzin) {
        dataKonca.AddHours(liczbaGodzin);
    }
    public void Zakoncz() {
        rower.ZmienStatus("Dostępny");
        dataKonca = DateTime.Now;
    }
}
