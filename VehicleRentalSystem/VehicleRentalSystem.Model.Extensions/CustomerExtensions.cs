using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.Model.Extensions
{
    public static class CustomerExtensions
    {
        public static string GetFullName(this Customer customer)
        {
            return $"{customer.FirstName} {customer.LastName}";
        }
        public static bool HasValidDriverLicense(this Customer customer)
        {
            return customer.DriverLicenseExpiration > DateTime.Now;
        }
        public static bool LicenseExpiresSoon(this Customer customer, int daysThreshold = 30)
        {
            return customer.HasValidDriverLicense() &&
                   (customer.DriverLicenseExpiration - DateTime.Now).TotalDays <= daysThreshold;
        }
        public static int GetActiveReservationsCount(this Customer customer)
        {
            return customer.Reservations?.Count(r => r.Status == ActualStatus.Potwierdzona || r.Status == ActualStatus.Rozpoczęta) ?? 0;
        }
        public static bool HasActiveReservations(this Customer customer)
        {
            return customer.GetActiveReservationsCount() > 0;
        }
        public static IEnumerable<Reservation> GetCompletedReservations(this Customer customer)
        {
            return customer.Reservations?.Where(r => r.Status == ActualStatus.Zakończona) ?? Enumerable.Empty<Reservation>();
        }
        public static double GetTotalSpent(this Customer customer)
        {
            return customer.Reservations?.Where(r => r.Status == ActualStatus.Zakończona).Sum(r => r.Price) ?? 0;
        }
        public static string FormatPhone(this Customer customer)
        {
            var phone = customer.PhoneNumber;

            if (string.IsNullOrWhiteSpace(phone) || phone.Length != 9)
                return customer.PhoneNumber ?? "";

            return $"{phone.Substring(0, 3)}-{phone.Substring(3, 3)}-{phone.Substring(6, 3)}";
        }
    }
}
