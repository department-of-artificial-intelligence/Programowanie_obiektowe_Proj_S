using Project.Model.Interfaces;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using Project.DAL;
using Microsoft.EntityFrameworkCore;

namespace Project.Logic.StoreManagement
{
    public class ServiceOfStatistics
    {
       
        private readonly ApplicationDbContext _context;

       
        public ServiceOfStatistics(ApplicationDbContext context)
        {
            _context = context;
        }

        public Order GetMostExpensiveOrder()
        {
           
            return _context.Orders
                .Include(o => o.Purchaser)
                .Include(o => o.Items)
                .OrderByDescending(o => o.Items.Sum(i => i.UnitPrice * i.Quantity))
                .FirstOrDefault();
        }

        public decimal GetAverageOrderValue()
        {
           
            return _context.Orders
                .Average(o => (decimal?)o.Items.Sum(i => i.UnitPrice * i.Quantity)) ?? 0m;
        }

        public List<Order> GetOrdersBetween(DateTime start, DateTime end)
        {
            return _context.Orders
                .Include(o => o.Items)
                .Where(o => o.OrderDate >= start && o.OrderDate <= end)
                .ToList();
        }

        public Dictionary<Customer, decimal> GetTotalSpentByCustomer()
        {
            
            return _context.Customers
                .Select(c => new
                {
                    Customer = c,
                    
                    Total = c.Orders
                        .SelectMany(o => o.Items)
                        .Sum(item => (decimal?)item.UnitPrice * item.Quantity) ?? 0m
                })
                .Where(x => x.Total > 0)
                .OrderByDescending(x => x.Total)
                .ToDictionary(x => x.Customer, x => x.Total);
        }

        public Dictionary<ProductCategory, decimal> GetRevenueByCategory()
        {
           
            return _context.Orders
                .SelectMany(o => o.Items)
                .GroupBy(i => i.Product.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Revenue = g.Sum(i => i.UnitPrice * i.Quantity)
                })
                .ToDictionary(x => x.Category, x => x.Revenue);
        }

        public Dictionary<OrderStatus, int> GetStatusDistribution()
        {
           
            return _context.Orders
                .GroupBy(o => o.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionary(x => x.Status, x => x.Count);
        }
    }
}