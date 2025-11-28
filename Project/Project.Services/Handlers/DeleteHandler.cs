using Project.Models;

namespace Project.Services.Handlers
{
    public static class DeleteHandler
    {
        public static void DeleteFilm(List<Film> films, List<Cinema> cinemas, List<Seance> seances,
            List<Reservation> reservations, List<Ticket> tickets, string filmId)
        {
            foreach (var cinema in cinemas)
            {
                cinema.RemoveItem(filmId);
            }

            var seancesToDelete = seances.Where(s => s.FilmId == filmId).ToList();

            foreach (var seance in seancesToDelete)
            {
                DeleteSeance(seances, reservations, tickets, seance.Id);
            }

            var film = films.FirstOrDefault(f => f.Id == filmId);
            if (film != null)
            {
                films.Remove(film);
            }
        }

        public static void DeleteCinema(List<Cinema> cinemas, List<Auditorium> auditoriums,
            List<Seance> seances, List<Reservation> reservations, List<Ticket> tickets, string cinemaId)
        {
            var auditoriumsToDelete = auditoriums.Where(a => a.CinemaId == cinemaId).ToList();

            foreach (var auditorium in auditoriumsToDelete)
            {
                DeleteAuditorium(auditoriums, seances, reservations, tickets, auditorium.Id);
            }

            var cinema = cinemas.FirstOrDefault(c => c.Id == cinemaId);
            if (cinema != null)
            {
                cinemas.Remove(cinema);
            }
        }

        public static void DeleteCinemaNetwork(List<CinemaNetwork> cinemaNetworks, string networkId)
        {
            var network = cinemaNetworks.FirstOrDefault(cn => cn.Id == networkId);

            if (network != null)
            {
                cinemaNetworks.Remove(network);
            }
        }

        public static void DeleteReservation(List<Reservation> reservations, List<Ticket> tickets, string reservationId)
        {
            var ticketsToDelete = tickets.Where(t => t.ReservationId == reservationId).ToList();

            foreach (var ticket in ticketsToDelete)
            {
                tickets.Remove(ticket);
            }

            var reservation = reservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservation != null)
            {
                reservations.Remove(reservation);
            }
        }

        public static void DeleteTicket(List<Ticket> tickets, string ticketId)
        {
            var ticket = tickets.FirstOrDefault(t => t.Id == ticketId);
            if (ticket != null)
            {
                tickets.Remove(ticket);
            }
        }
    }
}