namespace Korepetycje.Models;

public class Korepetytor : Osoba
{
    private readonly List<TerminDostepnosci> _terminy = new();
    public decimal StawkaGodzinowa { get; private set;}

    public Korepetytor(int id, string imie, string nazwisko, string email, decimal stawkaGodzinowa): base(id, imie, nazwisko, email)
    {
        if(stawkaGodzinowa < 0)
        {
            Console.WriteLine("Stawka godzinowa musi być większa od 0!");
        }
        StawkaGodzinowa = stawkaGodzinowa;
    }

}