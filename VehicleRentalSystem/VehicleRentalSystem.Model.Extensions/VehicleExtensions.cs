using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalSystem.Model;

namespace VehicleRentalSystem.Model.Extensions
{
    public static class VehicleExtensions
    {
        public static bool NeedsServiceSoon(this Vehicle vehicle, int daysThreshold = 30)
        {
            return (vehicle.NextService - DateTime.Now).TotalDays <= daysThreshold;
        }
        public static bool NeedsServiceNow(this Vehicle vehicle, int daysThreshold = 1)
        {
            return (vehicle.NextService - DateTime.Now).TotalDays <= daysThreshold;
        }
        public static bool IsAvailableForRent(this Vehicle vehicle)
        {
            return !vehicle.IsRented && !vehicle.NeedsServiceSoon(7);
        }
        public static string GetFullName(this Vehicle vehicle)
        {
            return $"{vehicle.Brand} {vehicle.Model} {vehicle.ProdYear}";
        }
        public static int DaysToService(this Vehicle vehicle)
        {
            return (vehicle.NextService - DateTime.Now).Days;
        }
        public static int DaysSinceLastService(this Vehicle vehicle)
        {
            return (DateTime.Now - vehicle.LastService).Days;
        }
        public static bool CanBeRentedForPeriod(this Vehicle vehicle, DateTime startDate, DateTime endDate, int bufferDays = 14)
        {
            var reservationLength = (endDate - startDate).Days;
            var daysToService = (vehicle.NextService - DateTime.Now).Days;

            return daysToService - reservationLength >= bufferDays;
        }

        public static IEnumerable<Vehicle> GetAvailableVehicles(this IEnumerable<Vehicle> vehicles)
        {
            return vehicles.Where(v => v.IsAvailableForRent());
        }
        public static IEnumerable<Vehicle> GetRentedVehicles(this IEnumerable<Vehicle> vehicles)
        {
            return vehicles.Where(v => v.IsRented);
        }
    }
}
