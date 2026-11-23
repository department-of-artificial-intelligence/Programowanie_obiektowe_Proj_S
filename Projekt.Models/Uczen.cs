using Projekt.Models.Enums;

namespace Projekt.Models;

public class Uczen : Osoba
{
    
    private static int NastepneId {get; set;} = 1;
    public PoziomNauczania PoziomNauczania {get; set;}
    public List<Ocena> Oceny {get; private set;} = new();

    public Uczen(string name, string surname, string email, PoziomNauczania poziom, List<Ocena> oceny): base(NastepneId++, name, surname, email)
    {
        PoziomNauczania = poziom;
        Oceny = oceny;
    }
}
