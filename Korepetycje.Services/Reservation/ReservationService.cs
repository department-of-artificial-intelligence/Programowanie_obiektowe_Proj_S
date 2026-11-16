namespace Korepetycje.Services.Reservation;

using Korepetycje.Models.Models;
using Korepetycje.Models.Repositories;
using Korepetycje.Models.Enums;

public class ReservationService : IReservationService
{
    readonly RepozytoriumLekcji Repo = new RepozytoriumLekcji();
    public bool CreateReservation(DateTime date, int czasTrwania, Korepetytor korepetytor, Przedmiot przedmiot, List<Uczen> uczniowie)
    {
        if (Repo.ZnajdzPoDacie(date) == null || czasTrwania == 0 || korepetytor == null || przedmiot == null || !uczniowie.Any())
        {
            return false;
        }

        Lekcja lekcja = new Lekcja(date, czasTrwania, StatusLekcji.Zaplanowana, korepetytor.StawkaGodzinowa, korepetytor, przedmiot, uczniowie);
        Repo.Dodaj(lekcja);
        return true;
    }

    public bool DeleteReservation(Lekcja lekcja)
    {
        if (Repo.CzyIstnieje(lekcja))
        {
            Repo.Usun(lekcja);
            return true;
        }
        return false;
    }

    public bool DeleteReservation(DateTime date)
    {
        if (Repo.ZnajdzPoDacie(date) != null)
        {
            Repo.Usun(date);
            return true;
        }
        return false;
    }
}