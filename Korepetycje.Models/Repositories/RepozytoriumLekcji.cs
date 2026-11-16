using Korepetycje.Models.Models;

namespace Korepetycje.Models.Repositories;

public class RepozytoriumLekcji : IRepozytoriumLekcji
{
    private readonly List<Lekcja> _lekcje = new();

    public void Dodaj(Lekcja lekcja) => _lekcje.Add(lekcja);
    public Lekcja? Pobierz(int id) => _lekcje.FirstOrDefault(l => l.Id == id);

    public IEnumerable<Lekcja> PobierzWszystkie() => _lekcje.ToList();
    public void Usun(Lekcja lekcja) => _lekcje.Remove(lekcja);
    public void Usun(DateTime data)
    {
        Lekcja? lekcja = _lekcje.FirstOrDefault(l => l.DataStart == data);
        if(lekcja != null)
        {
            _lekcje.Remove(lekcja);
        }
    }

    public Lekcja? ZnajdzPoDacie(DateTime data)
    {
        return _lekcje.FirstOrDefault(l => l.DataStart == data);
    }

    public bool CzyIstnieje(Lekcja lekcja)
    {
        if (_lekcje.Contains(lekcja))
        {
            return true;
        }
        return false;
    }
}