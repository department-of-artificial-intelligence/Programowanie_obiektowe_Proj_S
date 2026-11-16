using Korepetycje.Models.Models;

namespace Korepetycje.Models.Repositories;

public interface IRepozytoriumLekcji
{
    void Dodaj(Lekcja lekcja);
    Lekcja? Pobierz(int id);
    IEnumerable<Lekcja> PobierzWszystkie();
    void Usun(Lekcja lekcja);

    void Usun(DateTime data);

    Lekcja? ZnajdzPoDacie(DateTime data);
    bool CzyIstnieje(Lekcja lekcja);

}