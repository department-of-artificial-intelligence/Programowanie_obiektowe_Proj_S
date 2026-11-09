namespace Korepetycje.Models;

public class TerminDostepnosci
{
    public DayOfWeek Dzien {get; private set;}
    public TimeSpan Od { get; private set;}
    public TimeSpan Do { get; private set;}

    public TerminDostepnosci(DayOfWeek dzien, TimeSpan od, TimeSpan @do)
    {
        if(od >= @do)
        {
            throw new ArgumentException("Godzina początkowa musi być wcześniej niż końcowa.");
        }
        Dzien = dzien;
        Od = od;
        Do = @do;
    }
}