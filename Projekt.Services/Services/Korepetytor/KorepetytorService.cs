namespace Projekt.Services.Korepetytor;
using Projekt.Models;

public class KorepetytorService : IKorepetytorService
{
    public bool AddDate(Korepetytor korepetytor, TerminDostepnosci date)
    {
        if(date != null)
        {
            korepetytor.AvailableDates.Add(date);
            return true;
        }
        return false;
    }

    public bool RemoveDate(Korepetytor korepetytor, TerminDostepnosci date)
    {
        if(date != null && korepetytor.AvailableDates.Contains(date))
        {
            korepetytor.AvailableDates.Remove(date);
            return true;
        }
        return false;
    }
}