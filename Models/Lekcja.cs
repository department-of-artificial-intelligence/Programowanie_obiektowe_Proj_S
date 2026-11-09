using Korepetycje.Enums;

namespace Korepetycje.Models;

public class Lekcja
{
    private readonly List<Uczen> _uczniowie = new();

    public int Id {get; private set;}
    public DateTime DataStart {get; private set;}
    public int CzasTrwaniaMin { get; private set;}
    public StatusLekcji Status { get; private set;}
    public decimal Cena { get; private set;}

    public Korepetytor Korepetytor { get; private set;}
    public Przedmiot Przedmiot { get; private set;}
    public IReadOnlyList<Uczen> Uczniowie => _uczniowie;

    public Lekcja(int id, DateTime dstart, int czas, StatusLekcji status, decimal cena)
    {
        Id = id;
        DataStart = dstart;
        CzasTrwaniaMin = czas;
        Status = status;
        Cena = cena;
    }

    public void Odwolaj()
    {
        if(Status != StatusLekcji.Zaplanowana)
        {
            System.Console.WriteLine("Można odwołać tylko zaplanowaną lekcję!");
        }
        Status = StatusLekcji.Odwolana;
    }

    public void Zakoncz()
    {
        if(Status != StatusLekcji.Zakonczona)
        {
            Status = StatusLekcji.Zakonczona;
        }
    }
}