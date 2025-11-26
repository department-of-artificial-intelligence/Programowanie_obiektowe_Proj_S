using Project.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Project.Logic
{
    public static class PrintableExtension
    {
        // DRIVER METHODS
        public static IEnumerable<Driver> FilterAvailable(this IEnumerable<Driver> drivers)
        {
            return drivers.Where(d => d.IsAvailable);
        }

        public static IEnumerable<Driver> SortByLastName(this IEnumerable<Driver> drivers)
        {
            return drivers.OrderBy(d => d.LastName);
        }

        public static void PrintDriverStatistics(this IEnumerable<Driver> drivers)
        {
            int totalDrivers = drivers.Count();
            int availableDrivers = drivers.Count(d => d.IsAvailable);
            int assignedDrivers = drivers.Count(d => d.Status == Driver.DriverStatus.Assigned);

            Console.WriteLine($"--- Driver Statistics ({DateTime.Now}) ---");
            Console.WriteLine($"Total drivers: {totalDrivers}");
            Console.WriteLine($"Available drivers: {availableDrivers}");
            Console.WriteLine($"Assigned drivers: {assignedDrivers}");
            Console.WriteLine("------------------------------------------");
        }

        // ORDER METHODS

        public static IEnumerable<Order> FilterByStatus(this IEnumerable<Order> orders, Order.OrderStatus status)
        {
            return orders.Where(o => o.OStatus == status);
        }

        public static IEnumerable<Order> SortByLoadingAddress(this IEnumerable<Order> orders)
        {
            return orders.OrderBy(o => o.LoadingAddress);
        }

        public static void PrintOrderStatusSummary(this IEnumerable<Order> orders)
        {
            Console.WriteLine($"--- Order Status Summary ({DateTime.Now}) ---");

            var summary = orders
                .GroupBy(o => o.OStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .OrderBy(x => (int)x.Status);

            foreach (var item in summary)
            {
                Console.WriteLine($"{item.Status,-15}: {item.Count}");
            }

            Console.WriteLine("------------------------------------------");
        }

        // VEHICLE METHODS

        public static IEnumerable<Vehicle> FilterByType(this IEnumerable<Vehicle> vehicles, Vehicle.VehicleType type)
        {
            return vehicles.Where(v => v.VType == type);
        }

        public static IEnumerable<Vehicle> SortByMileageDescending(this IEnumerable<Vehicle> vehicles)
        {
            return vehicles.OrderByDescending(v => v.Mileage);
        }

        public static void PrintVehicleStatistics(this IEnumerable<Vehicle> vehicles)
        {
            Console.WriteLine($"--- Vehicle Statistics ({DateTime.Now}) ---");

            var availability = vehicles
                .GroupBy(v => v.VStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .OrderBy(x => (int)x.Status);

            Console.WriteLine("Availability:");

            foreach (var item in availability)
            {
                Console.WriteLine($"- {item.Status,-15}: {item.Count}");
            }

            double averageMileage = vehicles.Any() ? vehicles.Average(v => v.Mileage) : 0;
            Console.WriteLine($"Average Mileage: {averageMileage:N0} km");

            Console.WriteLine("------------------------------------------");
        }
    }
}
