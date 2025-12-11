using Project.Abstractions;
using Project.Model;

namespace Project.Logic
{
    public static class OrderExtensions
    {
        public static IEnumerable<Order> FilterByStatus(this IEnumerable<Order> orders, OrderStatus status)
        {
            return orders.Where(o => o.Status == status);
        }

        public static IEnumerable<Order> SortByLoadingAddress(this IEnumerable<Order> orders)
        {
            return orders.OrderBy(o => o.LoadingAddress);
        }

        public static void PrintOrderStatusSummary(this IEnumerable<Order> orders)
        {
            Console.WriteLine($"--- Order Status Summary ({DateTime.Now}) ---");

            var summary = orders
                .GroupBy(o => o.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .OrderBy(x => (int)x.Status);

            foreach (var item in summary)
            {
                Console.WriteLine($"{item.Status,-15}: {item.Count}");
            }

            Console.WriteLine("------------------------------------------");
        }
    }
}
