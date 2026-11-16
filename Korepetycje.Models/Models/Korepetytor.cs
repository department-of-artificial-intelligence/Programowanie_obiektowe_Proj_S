namespace Korepetycje.Models.Models;

public class Korepetytor : Osoba
{
    private readonly List<TerminDostepnosci> _terminy = new();
    public decimal StawkaGodzinowa { get; private set;}
    public static int _nastepneId = 1;

    public Korepetytor(string imie, string nazwisko, string email, decimal stawkaGodzinowa): base(_nastepneId++, imie, nazwisko, email)
    {
        if(stawkaGodzinowa < 0)
        {
            Console.WriteLine("Stawka godzinowa musi być większa od 0!");
        }
        StawkaGodzinowa = stawkaGodzinowa;
    }

    public bool DodajTermin(TerminDostepnosci termin)
    {
        if(termin != null)
        {
            _terminy.Add(termin);
            return true;
        }
        return false;
    }

    public bool UsunTermin(TerminDostepnosci termin)
    {
        if(termin != null && _terminy.Contains(termin))
        {
            _terminy.Remove(termin);
            return true;
        }
        return false;
    }
}