using Project.Abstractions;

namespace Project.Logic
{
    public static class VehicleExensions
    {
        public static IEnumerable<Vehicle> FilterByType(this IEnumerable<Vehicle> vehicles, VehicleType type)
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
