using Project.Models;

namespace Project.Services.SortingFiltering
{
    public static class TicketSortingFiltering
    {
        public static List<Ticket> FilterTicketsByReservationId(List<Ticket> tickets, string reservationId)
        {
            return [.. tickets.Where(t => t.ReservationId == reservationId)];
        }

        public static List<Ticket> FilterTicketsByCinemaId(List<Ticket> tickets, string cinemaId)
        {
            return [.. tickets.Where(t => t.CinemaId == cinemaId)];
        }

        public static List<Ticket> FilterTicketsBySeanceId(List<Ticket> tickets, string seanceId)
        {
            return [.. tickets.Where(t => t.SeanceId == seanceId)];
        }

        public static List<Ticket> FilterTicketsByFilmId(List<Ticket> tickets, string filmId)
        {
            return [.. tickets.Where(t => t.FilmId == filmId)];
        }

        public static List<Ticket> FilterTicketsByAuditoriumId(List<Ticket> tickets, string auditoriumId)
        {
            return [.. tickets.Where(t => t.AuditoriumId == auditoriumId)];
        }

        public static List<Ticket> FilterTicketsByTicketType(List<Ticket> tickets, TicketType ticketType)
        {
            return [.. tickets.Where(t => t.Type == ticketType)];
        }

        public static List<Ticket> SortTicketsByFinalPrice(List<Ticket> tickets)
        {
            return [.. tickets.OrderByDescending(t => t.FinalPrice)];
        }
    }
}