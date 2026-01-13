using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;
using Project.Model.Interfaces; 

namespace Project.Logic.StoreManagement
{
    public class ServiceOfStatistics
    {
        
        private readonly IOrderRepository _orderRepository;

        
        public ServiceOfStatistics(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

       

        public Order GetMostExpensiveOrder()
        {
            
            var orders = _orderRepository.GetAll();

            if (!orders.Any()) return null;
            return orders.OrderByDescending(o => o.GetTotalAmount()).FirstOrDefault();
        }

        public decimal GetAverageOrderValue()
        {
            var orders = _orderRepository.GetAll();

            if (!orders.Any()) return 0;
            return orders.Average(o => o.GetTotalAmount());
        }

        public List<Order> GetOrdersBetween(DateTime start, DateTime end)
        {
            return _orderRepository.GetAll()
                .Where(o => o.OrderDate >= start && o.OrderDate <= end)
                .ToList();
        }

        public Dictionary<Customer, decimal> GetTotalSpentByCustomer()
        {
            return _orderRepository.GetAll()
                .GroupBy(o => o.Purchaser)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(o => o.GetTotalAmount())
                );
        }

        public Dictionary<ProductCategory, decimal> GetRevenueByCategory()
        {
            
            return _orderRepository.GetAll()
                .SelectMany(o => o.Items)
                .GroupBy(i => i.Product.Category)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(i => i.GetLineTotal())
                );
        }

        public Dictionary<OrderStatus, int> GetStatusDistribution()
        {
            return _orderRepository.GetAll()
                .GroupBy(o => o.Status)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}