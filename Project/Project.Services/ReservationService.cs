using Project.Models;
using Project.DAL;

namespace Project.Services
{
    public static class ReservationService
    {
        public static List<Reservation> GetAll(ApplicationDBContext context)
        {
            return [.. context.Reservations];
        }

        public static Reservation? GetById(ApplicationDBContext context, string id)
        {
            return context.Reservations.FirstOrDefault(r => r.Id == id);
        }

        public static Reservation Add(ApplicationDBContext context, string seanceId, string customerFirstName, string customerLastName, string customerEmail, 
                                      string customerPhone, string paymentMethod)
        {
            var reservation = new Reservation(seanceId, customerFirstName, customerLastName, customerEmail, customerPhone, paymentMethod);

            context.Reservations.Add(reservation);
            context.SaveChanges();

            return reservation;
        }

        public static void Update(ApplicationDBContext context, Reservation reservation)
        {
            context.Reservations.Update(reservation);
            context.SaveChanges();
        }

        public static void Delete(ApplicationDBContext context, string reservationId)
        {
            var reservation = context.Reservations.FirstOrDefault(r => r.Id == reservationId);

            if (reservation != null)
            {
                var tickets = context.Tickets.Where(t => t.ReservationId == reservationId).ToList();

                context.Tickets.RemoveRange(tickets);
                context.Reservations.Remove(reservation);
                context.SaveChanges();
            }
        }

        public static List<Reservation> FilterBySeanceId(ApplicationDBContext context, string seanceId)
        {
            return [.. context.Reservations.Where(r => r.SeanceId == seanceId)];
        }

        public static List<Reservation> FilterByPaymentMethod(ApplicationDBContext context, string paymentMethod)
        {
            return [.. context.Reservations.Where(r => r.PaymentMethod.Contains(paymentMethod))];
        }
    }
}