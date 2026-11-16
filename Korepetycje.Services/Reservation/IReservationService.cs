using Korepetycje.Models.Models;
namespace Korepetycje.Services.Reservation;

public interface IReservationService
{
    public bool CreateReservation(DateTime date, int czas, Korepetytor korepetytor);
}