using Project.Models;

namespace Project.Services
{
    public class ReservationService
    {
        public static List<Reservation> FilterReservationsBySeanceId(List<Reservation> reservations, string seanceId)
        {
            return [.. reservations.Where(r => r.SeanceId == seanceId)];
        }

        public static List<Reservation> FilterReservationsByPaymentMethod(List<Reservation> reservations, string paymentMethod)
        {
            return [.. reservations.Where(r => r.PaymentMethod.Contains(paymentMethod, StringComparison.OrdinalIgnoreCase))];
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
    }
}
