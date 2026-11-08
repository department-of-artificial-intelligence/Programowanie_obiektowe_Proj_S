using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wypozyczalnia.Model;

public class Wypozyczenie {
    public int Id { get; set; }
    public Klient Klient { get; set; }
    public Rower Rower { get; set; }
    public DateTime DataStart {  get; set; }
    public DateTime DataKonca {  get; set; }

    public Wypozyczenie(Klient klient, Rower rower, DateTime dataStart, DateTime dataKonca) {
        Klient = klient;
        Rower = rower;
        DataStart = dataStart;
        DataKonca = dataKonca;
        Rower.ZmienStatus(StatusRoweru.Wypozyczony);
    }
    public void Przedluz(int liczbaGodzin) {
        DataKonca = DataKonca.AddHours(liczbaGodzin);
    }
    public void Zakoncz() {
        Rower.ZmienStatus(StatusRoweru.Dostepny);
        DataKonca = DateTime.Now;
    }
}
