using Project.Models;

namespace Project.Services
{
    public class CinemaService
    {
        public static List<Cinema> SortByNumberOfAvailableFilms(List<Cinema> cinemas)
        {
            return [.. cinemas.OrderByDescending(c => c.Items.Count)];
        }

        public static List<Cinema> FilterCinemasByName(List<Cinema> cinemas, string name)
        {
            return [.. cinemas.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))];
        }

        public static List<Cinema> FilterCinemasWhereFilmAvailable(List<Cinema> cinemas, string? filmId)
        {
            return [.. cinemas.Where(c => c.Items.Contains(filmId))];
        }

        public static void DeleteCinema(List<Cinema> cinemas, List<Auditorium> auditoriums,
                                        List<Seance> seances, List<Reservation> reservations, List<Ticket> tickets, string cinemaId)
        {
            var auditoriumsToDelete = auditoriums.Where(a => a.CinemaId == cinemaId).ToList();

            foreach (var auditorium in auditoriumsToDelete)
            {
                AuditoriumService.DeleteAuditorium(auditoriums, seances, reservations, tickets, auditorium.Id);
            }

            var cinema = cinemas.FirstOrDefault(c => c.Id == cinemaId);
            if (cinema != null)
            {
                cinemas.Remove(cinema);
            }
        }
    }
}
