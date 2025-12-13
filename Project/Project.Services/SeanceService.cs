using Project.DAL;
using Project.Models;

namespace Project.Services
{
    public static class SeanceService
    {
        public static List<Seance> GetAll(ApplicationDBContext context)
        {
            return [.. context.Seances];
        }

        public static Seance? GetById(ApplicationDBContext context, string id)
        {
            return context.Seances.FirstOrDefault(s => s.Id == id);
        }

        public static Seance Add(ApplicationDBContext context, string filmId, string auditoriumId, DateTime startTime, decimal price, uint durationMinutes)
        {
            var seance = new Seance(filmId, auditoriumId, startTime, price, durationMinutes);

            context.Seances.Add(seance);
            context.SaveChanges();

            return seance;
        }

        public static void Update(ApplicationDBContext context, Seance seance)
        {
            context.Seances.Update(seance);
            context.SaveChanges();
        }

        public static void Delete(ApplicationDBContext context, string seanceId)
        {
            var seance = context.Seances.FirstOrDefault(s => s.Id == seanceId);

            if (seance != null)
            {
                var reservations = context.Reservations.Where(r => r.SeanceId == seanceId).ToList();

                foreach (var reservation in reservations)
                {
                    var tickets = context.Tickets.Where(t => t.ReservationId == reservation.Id).ToList();

                    context.Tickets.RemoveRange(tickets);
                    context.Reservations.Remove(reservation);
                }

                context.Seances.Remove(seance);
                context.SaveChanges();
            }
        }

        public static List<Seance> FilterByFilmId(ApplicationDBContext context, string filmId)
        {
            return [.. context.Seances.Where(s => s.FilmId == filmId)];
        }

        public static List<Seance> FilterByAuditoriumId(ApplicationDBContext context, string auditoriumId)
        {
            return [.. context.Seances.Where(s => s.AuditoriumId == auditoriumId)];
        }

        public static List<Seance> SortByStartTime(ApplicationDBContext context)
        {
            return [.. context.Seances.OrderBy(s => s.StartTime)];
        }

        public static List<Seance> SortByPrice(ApplicationDBContext context)
        {
            return [.. context.Seances.OrderBy(s => s.Price)];
        }

        public static List<Seance> SortByOccupiedSeats(ApplicationDBContext context)
        {
            return [.. context.Seances.AsEnumerable().OrderByDescending(s => s.OccupiedSeatIds.Count)];
        }
    }
}