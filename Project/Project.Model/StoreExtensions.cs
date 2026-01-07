using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;

namespace Project.Extensions
{
    public static class StoreExtensions
    {
        

        public static int GetTotalQuantity(this List<InventoryItem> inventory)
        {
            if (inventory == null || !inventory.Any()) return 0;
            return inventory.Sum(x => x.Quantity);
        }

        public static InventoryItem? GetMostExpensiveItem(this List<InventoryItem> inventory)
        {
            if (inventory == null || !inventory.Any()) return null;
            return inventory.OrderByDescending(x => x.Product.Price).FirstOrDefault();
        }

        public static List<string> GetUniqueManufacturers(this List<InventoryItem> inventory)
        {
            if (inventory == null) return new List<string>();
            return inventory.Select(x => x.Product.Manufacturer).Distinct().OrderBy(x => x).ToList();
        }

      
        public static decimal CalculateTotalRevenue(this List<Order> orders)
        {
            if (orders == null || !orders.Any()) return 0m;

            
            return orders
                .Where(o => o.Status == OrderStatus.Paid || o.Status == OrderStatus.Completed)
                .Sum(o => o.TotalValue);
        }

        public static List<Order> GetPendingOrders(this List<Order> orders)
        {
            if (orders == null) return new List<Order>();
            return orders
                .Where(o => o.Status == OrderStatus.New || o.Status == OrderStatus.Confirmed)
                .OrderBy(o => o.DatePlaced)
                .ToList();
        }

        public static bool IsVipCustomer(this Customer customer, List<Order> allOrders, decimal threshold = 5000m)
        {
            if (customer == null || allOrders == null) return false;

            decimal totalSpent = allOrders
                .Where(o => o.CustomerId == customer.CustomerId && o.Status == OrderStatus.Paid)
                .Sum(o => o.TotalValue);

            return totalSpent >= threshold;
        }

        

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }

        public static string ToStatusLabel(this OrderStatus status)
        {
            return $"[{status.ToString().ToUpper()}]";
        }
    }
}