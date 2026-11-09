namespace Korepetycje.Models;

public abstract class Osoba
{
    public int Id { get; protected set; }
    public string Imie { get; protected set; }
    public string Nazwisko { get; protected set; }
    public string Email { get; protected set; }

    protected Osoba(int id, string imie, string nazwisko, string email)
    {
        Id = id;
        Imie = imie;
        Nazwisko = nazwisko;
        Email = email;
    }

    public string PobierzPelneImie()
    {
        return $"{Imie} {Nazwisko}";
    }
}

