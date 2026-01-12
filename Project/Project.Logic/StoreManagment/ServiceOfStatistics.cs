using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;

namespace Project.Logic.StoreManagment
{
    public class ServiceOfStatistics
    {
        public Order GetMostExpensiveOrder(List<Order> orders)
        {
            if (orders == null || !orders.Any()) return null;
            return orders.OrderByDescending(o => o.GetTotalAmount()).FirstOrDefault();
        }

        public decimal GetAverageOrderValue(List<Order> orders)
        {
            if (orders == null || !orders.Any()) return 0;
            return orders.Average(o => o.GetTotalAmount());
        }

        public List<Order> GetOrdersBetween(List<Order> orders, DateTime start, DateTime end)
        {
            return orders.Where(o => o.OrderDate >= start && o.OrderDate <= end).ToList();
        }

        public Dictionary<Customer, decimal> GetTotalSpentByCustomer(List<Order> orders)
        {
            return orders
                .GroupBy(o => o.Purchaser)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(o => o.GetTotalAmount())
                );
        }

        public Dictionary<ProductCategory, decimal> GetRevenueByCategory(List<Order> orders)
        {
            return orders
                .SelectMany(o => o.Items)
                .GroupBy(i => i.Product.Category)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(i => i.GetLineTotal())
                );
        }

        public Dictionary<OrderStatus, int> GetStatusDistribution(List<Order> orders)
        {
            return orders
                .GroupBy(o => o.Status)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}