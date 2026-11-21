namespace Projekt.Models;

public class Korepetytor : Osoba
{
    public List<TerminDostepnosci> AvailableDates { get; private set;} = new();
    private static int NextId{get; set;} = 1;
    public decimal HourlyRate {get; set;}

    public Korepetytor(string name, string surname, string email, decimal hourlyRate): base(NextId++, name, surname, email)
    {
        HourlyRate = hourlyRate;
    }
}