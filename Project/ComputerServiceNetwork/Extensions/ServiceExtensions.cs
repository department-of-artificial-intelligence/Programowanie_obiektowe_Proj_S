using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ComputerServiceNetwork.dataModel;

namespace ComputerServiceNetwork.Extensions
{
    public static class ServiceExtensions
    {
        public static IEnumerable<Order> OnlyCompleted(this IEnumerable<Order> orders)
        {
            return orders.Where(o => o.Status == OrderStatus.Closed);
        }

        public static decimal TotalRevenue(this IEnumerable<Order> orders)
        {
            return orders.Sum(o => o.CalculateTotal());
        }

        public static IEnumerable<SparePart> LowStock(this IEnumerable<SparePart> parts, int threshold = 3)
        {
            return parts.Where(p => p.QuantityInStock <= threshold);
        }
    }
}