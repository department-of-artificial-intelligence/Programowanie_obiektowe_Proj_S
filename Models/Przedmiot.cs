namespace Korepetycje.Models;

public class Przedmiot
{
    public int Id {get; private set;}
    public string Nazwa { get; private set;}
    public string Poziom { get; private set;}

    public Przedmiot(int id, string nazwa, string poziom)
    {
        Id = id;
        Nazwa = nazwa;
        Poziom = poziom;
    }

    public override string ToString()
    {
        return $"[{Nazwa}] - ({Poziom})";
    }
}