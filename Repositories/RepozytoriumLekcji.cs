using Korepetycje.Models;

namespace Korepetycje.Repositories;

public class RepozytoriumLekcji : IRepozytoriumLekcji
{
    private readonly List<Lekcja> _lekcje = new();

    public void Dodaj(Lekcja lekcja) => _lekcje.Add(lekcja);
    public Lekcja? Pobierz(int id) => _lekcje.FirstOrDefault(l => l.Id == id);

    public IEnumerable<Lekcja> PobierzWszystkie() => _lekcje.ToList();
    public void Usun(Lekcja lekcja) => _lekcje.Remove(lekcja);
}