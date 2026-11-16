using Korepetycje.Models.Extensions;

namespace Korepetycje.Models.Models;

public class Uczen : Osoba
{
    public string PoziomNauczania {get; private set;}
    public List<Ocena> Oceny { get; private set; } = new();
    public static int NastepneId { get => nastepneId; set => nastepneId = value; }

    private static int nastepneId = 1;

    public Uczen(string imie, string nazwisko, string email, string poziomNauczania): base(NastepneId++, imie, nazwisko, email)
    {
        PoziomNauczania = poziomNauczania;
    }

    public void DodajOcene(Ocena ocena)
    {
        Oceny.Add(ocena);
    }
    public void DodajOcene(int num, string txt)
    {
        Ocena ocena = new(num, txt);
        Oceny.Add(ocena);
    }

    public double ObliczSrednia()
    {
        return Oceny.ObliczSrednia();
    }
}