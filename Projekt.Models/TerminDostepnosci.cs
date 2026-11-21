
namespace Projekt.Models;

public class TerminDostepnosci
{
    public DayOfWeek Day { get; private set;}
    public TimeSpan From { get; private set;}
    public TimeSpan To { get; private set;}

    public TerminDostepnosci(DayOfWeek day, TimeSpan from, TimeSpan to)
    {
        if(from >= to)
        {
            throw new ArgumentException("Godzina poczatkowa musi byc wczesniej niz koncowa.");
        }
        Day = day;
        From = from;
        To = to;
    }
}