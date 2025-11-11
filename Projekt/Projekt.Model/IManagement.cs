using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public interface IManagement
    {
        bool DodajPojazd(Vehicle pojazd);
        bool UsunPojazd(string tablica);
        Vehicle ZnajdzPojazdPoRejestracji(string tablica);
        void PokazWszystkie();

        bool PrzypiszKierowceDoPojazdu(string tablica, Driver kierowca);
        bool DodajWpisSerwisowy(string tablica, string opis, double koszt);
        void PokazSerwisPojazdu(string tablica);
    }
}
