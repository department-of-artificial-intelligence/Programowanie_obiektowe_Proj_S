namespace Korepetycje.Models;

public class Uczen : Osoba
{
    public string PoziomNauczania {get; private set;}
    public List<Ocena> Oceny { get; private set; } = new();

    public Uczen(int id, string imie, string nazwisko, string email, string poziomNauczania): base(id, imie, nazwisko, email)
    {
        PoziomNauczania = poziomNauczania;
    }

    public void DodajOcene(Ocena ocena)
    {
        _oceny.Add(ocena);
    }
}