using Project.Models;

namespace Project.Services.SortingFiltering
{
    public static class ReservationSortingFiltering
    {
        public static List<Reservation> FilterReservationsBySeanceId(List<Reservation> reservations, string seanceId)
        {
            return [.. reservations.Where(r => r.SeanceId == seanceId)];
        }

        public static List<Reservation> FilterReservationsByPaymentMethod(List<Reservation> reservations, string paymentMethod)
        {
            return [.. reservations.Where(r => r.PaymentMethod.Contains(paymentMethod, StringComparison.OrdinalIgnoreCase))];
        }
    }
}