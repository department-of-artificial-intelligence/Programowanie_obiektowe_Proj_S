using Project.DAL;
using Project.Models;

namespace Project.Services
{
    public static class TicketService
    {
        public static List<Ticket> GetAll(ApplicationDBContext context)
        {
            return [.. context.Tickets];
        }

        public static Ticket? GetById(ApplicationDBContext context, string id)
        {
            return context.Tickets.FirstOrDefault(t => t.Id == id);
        }

        public static Ticket Add(ApplicationDBContext context, string reservationId, string cinemaId, string auditoriumId, string seanceId, 
                                 string filmId, string seatId, decimal price, TicketType ticketType)
        {
            var ticket = new Ticket(reservationId, cinemaId, auditoriumId, seanceId, filmId, seatId, price, ticketType);

            context.Tickets.Add(ticket);
            context.SaveChanges();

            return ticket;
        }

        public static void Update(ApplicationDBContext context, Ticket ticket)
        {
            context.Tickets.Update(ticket);
            context.SaveChanges();
        }

        public static void Delete(ApplicationDBContext context, string ticketId)
        {
            var ticket = context.Tickets.FirstOrDefault(t => t.Id == ticketId);

            if (ticket != null)
            {
                context.Tickets.Remove(ticket);
                context.SaveChanges();
            }
        }

        public static List<Ticket> FilterByReservationId(ApplicationDBContext context, string reservationId)
        {
            return [.. context.Tickets.Where(t => t.ReservationId == reservationId)];
        }

        public static List<Ticket> FilterByCinemaId(ApplicationDBContext context, string cinemaId)
        {
            return [.. context.Tickets.Where(t => t.CinemaId == cinemaId)];
        }

        public static List<Ticket> FilterBySeanceId(ApplicationDBContext context, string seanceId)
        {
            return [.. context.Tickets.Where(t => t.SeanceId == seanceId)];
        }

        public static List<Ticket> FilterByFilmId(ApplicationDBContext context, string filmId)
        {
            return [.. context.Tickets.Where(t => t.FilmId == filmId)];
        }

        public static List<Ticket> FilterByAuditoriumId(ApplicationDBContext context, string auditoriumId)
        {
            return [.. context.Tickets.Where(t => t.AuditoriumId == auditoriumId)];
        }

        public static List<Ticket> FilterByTicketType(ApplicationDBContext context, TicketType ticketType)
        {
            return [.. context.Tickets.Where(t => t.Type == ticketType)];
        }

        public static List<Ticket> SortByFinalPrice(ApplicationDBContext context)
        {
            return [.. context.Tickets.OrderByDescending(t => t.FinalPrice)];
        }
    }
}