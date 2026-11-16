using Korepetycje.Models.Models;
namespace Korepetycje.Services.Reservation;

public interface IReservationService
{
    public bool CreateReservation(DateTime date, int czasTrwania, Korepetytor korepetytor, Przedmiot przedmiot, List<Uczen> ucniowie);
    public bool DeleteReservation(Lekcja lekcja);

    public bool DeleteReservation(DateTime date);
}