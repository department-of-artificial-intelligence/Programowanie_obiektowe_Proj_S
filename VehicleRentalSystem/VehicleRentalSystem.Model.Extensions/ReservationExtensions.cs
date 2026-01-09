using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model.Extensions
{
    public static class ReservationExtensions
    {
        public static int GetDurationInDays(this Reservation reservation)
        {
            return (reservation.EndDate - reservation.StartDate).Days;
        }
        public static bool IsActive(this Reservation reservation)
        {
            return reservation.Status == ActualStatus.Potwierdzona ||
                   reservation.Status == ActualStatus.Rozpoczęta;
        }
        public static double GetPricePerDay(this Reservation reservation)
        {
            var days = reservation.GetDurationInDays();
            return days > 0 ? reservation.Price / days : 0;
        }
        public static bool EndsToday(this Reservation reservation)
        {
            return reservation.EndDate.Date == DateTime.Now.Date;
        }
        public static int DaysUntilStart(this Reservation reservation)
        {
            return (reservation.StartDate - DateTime.Now).Days;
        }
        public static int DaysUntilEnd(this Reservation reservation)
        {
            return (reservation.EndDate - DateTime.Now).Days;
        }
        public static bool CanBeCancelled(this Reservation reservation)
        {
            return reservation.Status != ActualStatus.Zakończona &&
                   reservation.Status != ActualStatus.Anulowana;
        }
        public static IEnumerable<Reservation> GetActiveReservations(this IEnumerable<Reservation> reservations)
        {
            return reservations.Where(r => r.IsActive());
        }
    }
}
