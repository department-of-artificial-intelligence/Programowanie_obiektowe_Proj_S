using Project.Abstractions;

namespace Project.Logic
{
    public static class DriverExtensions
    {
        public static IEnumerable<IDriver> FilterAvailable(this IEnumerable<IDriver> drivers)
        {
            return drivers.Where(d => d.IsAvailable);
        }

        public static IEnumerable<IDriver> SortByLastName(this IEnumerable<IDriver> drivers)
        {
            return drivers.OrderBy(d => d.LastName);
        }

        public static void PrintDriverStatistics(this IEnumerable<IDriver> drivers)
        {
            int totalDrivers = drivers.Count();
            int availableDrivers = drivers.Count(d => d.IsAvailable);
            int assignedDrivers = drivers.Count(d => d.Status == DriverStatus.Assigned);

            Console.WriteLine($"--- Driver Statistics ({DateTime.Now}) ---");
            Console.WriteLine($"Total drivers: {totalDrivers}");
            Console.WriteLine($"Available drivers: {availableDrivers}");
            Console.WriteLine($"Assigned drivers: {assignedDrivers}");
            Console.WriteLine("------------------------------------------");
        }
    }
}
