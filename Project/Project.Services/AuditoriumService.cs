using Project.Models;
using Project.DAL;

namespace Project.Services
{
    public static class AuditoriumService
    {
        public static List<Auditorium> GetAll(ApplicationDBContext context)
        {
            return [.. context.Auditoriums];
        }

        public static Auditorium? GetById(ApplicationDBContext context, string id)
        {
            return context.Auditoriums.FirstOrDefault(a => a.Id == id);
        }

        public static Auditorium Add(ApplicationDBContext context, string cinemaId, string name, uint roomNumber, uint rows, uint seatsPerRow)
        {
            Auditorium auditorium = new(cinemaId, name, roomNumber, rows, seatsPerRow);

            context.Auditoriums.Add(auditorium);
            context.SaveChanges();

            return auditorium;
        }

        public static void Update(ApplicationDBContext context, Auditorium auditorium)
        {
            context.Auditoriums.Update(auditorium);
            context.SaveChanges();
        }

        public static void Delete(ApplicationDBContext context, string auditoriumId)
        {
            var auditorium = context.Auditoriums.FirstOrDefault(a => a.Id == auditoriumId);

            if (auditorium != null)
            {
                var seances = context.Seances.Where(s => s.AuditoriumId == auditoriumId).ToList();

                foreach (var seance in seances)
                {
                    var reservations = context.Reservations.Where(r => r.SeanceId == seance.Id).ToList();

                    foreach (var reservation in reservations)
                    {
                        var tickets = context.Tickets.Where(t => t.ReservationId == reservation.Id).ToList();

                        context.Tickets.RemoveRange(tickets);
                        context.Reservations.Remove(reservation);
                    }

                    context.Seances.Remove(seance);
                }

                context.Auditoriums.Remove(auditorium);
                context.SaveChanges();
            }
        }

        public static List<Auditorium> FilterByName(ApplicationDBContext context, string name)
        {
            return [.. context.Auditoriums.Where(a => a.Name.Contains(name))];
        }

        public static List<Auditorium> FilterByFeature(ApplicationDBContext context, string feature)
        {
            return [.. context.Auditoriums.Where(a => a.Features.Any(f => f.Contains(feature)))];
        }

        public static List<Auditorium> SortByFeatures(ApplicationDBContext context)
        {
            return [.. context.Auditoriums.OrderByDescending(a => a.Features.Count)];
        }

        public static List<Auditorium> SortByMaxCapacity(ApplicationDBContext context)
        {
            return [.. context.Auditoriums.OrderByDescending(a => a.Capacity)];
        }
    }
}