using Project.Models;

namespace Project.Services
{
    public class SeanceService
    {
        public static List<Seance> FilterSeancesByFilmId(List<Seance> seances, string filmId)
        {
            if (seances == null || string.IsNullOrEmpty(filmId))
                return [];

            return [.. seances.Where(s => s.FilmId == filmId)];
        }

        public static List<Seance> FilterSeancesByAuditoriumId(List<Seance> seances, string auditoriumId)
        {
            if (seances == null || string.IsNullOrEmpty(auditoriumId))
                return [];

            return [.. seances.Where(s => s.AuditoriumId == auditoriumId)];
        }

        public static List<Seance> SortSeancesByStartTime(List<Seance> seances)
        {
            if (seances == null) return [];
            return [.. seances.OrderBy(s => s.StartTime)];
        }

        public static List<Seance> SortSeancesByPrice(List<Seance> seances)
        {
            if (seances == null) return [];
            return [.. seances.OrderBy(s => s.Price)];
        }

        public static List<Seance> SortSeancesByOccupiedSeats(List<Seance> seances, List<Auditorium> auditoriums)
        {
            if (seances == null || auditoriums == null) return [];

            return [.. seances.OrderByDescending(s =>
            {
                var auditorium = auditoriums.FirstOrDefault(a => a.Id == s.AuditoriumId);
                return auditorium != null && auditorium.Capacity > 0
                    ? s.OccupiedSeatIds.Count / (double)auditorium.Capacity
                    : 0;
            })];
        }

        public static void DeleteSeance(List<Seance> seances, List<Reservation> reservations,
                                        List<Ticket> tickets, string seanceId)
        {
            var reservationsToDelete = reservations.Where(r => r.SeanceId == seanceId).ToList();

            foreach (var reservation in reservationsToDelete)
            {
                ReservationService.DeleteReservation(reservations, tickets, reservation.Id);
            }

            var seance = seances.FirstOrDefault(s => s.Id == seanceId);
            if (seance != null)
            {
                seances.Remove(seance);
            }
        }
    }
}
