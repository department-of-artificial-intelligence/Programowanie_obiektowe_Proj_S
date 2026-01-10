using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model.Orders;
using Project.Model.People;

namespace Project.Extensions
{
    public static class StoreExtensions
    {
        public static string MaskEmail(this string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@")) return email;
            var parts = email.Split('@');
            var name = parts[0];
            if (name.Length > 2) name = $"{name.Substring(0, 2)}***";
            return $"{name}@{parts[1]}";
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }

        public static ConsoleColor ToConsoleColor(this OrderStatus status)
        {
            return status switch
            {
                OrderStatus.New => ConsoleColor.White,
                OrderStatus.Confirmed => ConsoleColor.Yellow,
                OrderStatus.Paid => ConsoleColor.Green,
                OrderStatus.Completed => ConsoleColor.Cyan,
                OrderStatus.Cancelled => ConsoleColor.Red,
                _ => ConsoleColor.Gray
            };
        }

        public static string ToStatusLabel(this OrderStatus status)
        {
            return $"[{status.ToString().ToUpper()}]";
        }

        public static string ToRelativeTime(this DateTime date)
        {
            var span = DateTime.Now - date;
            if (span.TotalDays < 1) return "Dzisiaj";
            if (span.TotalDays < 2) return "Wczoraj";
            return $"{(int)span.TotalDays} dni temu";
        }

        public static T GetRandomElement<T>(this List<T> list)
        {
            if (list == null || list.Count == 0) return default;
            Random rnd = new Random();
            return list[rnd.Next(list.Count)];
        }

        public static List<InventoryItem> GetLowStockItems(this List<InventoryItem> inventory, int threshold)
        {
            return inventory.Where(item => item.Quantity <= threshold).ToList();
        }

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
    }
}