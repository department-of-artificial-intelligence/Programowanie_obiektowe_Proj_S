using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ComputerServiceNetwork.dataModel;
using ComputerServiceNetwork.Extensions;

namespace ComputerServiceNetwork.Services
{
    public class NetworkManager
    {
        public List<ServiceCenter> Centers { get; set; } = new List<ServiceCenter>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<User> Users { get; set; } = new List<User>();


        public List<Order> GetOrdersByStatus(OrderStatus status)
        {
            return Orders.Where(o => o.Status == status).OrderByDescending(o => o.CreatedAt).ToList();
        }

        public decimal GetAverageOrderCost()
        {
            return Orders.Any() ? Orders.Average(o => o.CalculateTotal()) : 0m;
        }

        public List<Technician> GetAvailableTechnicians()
        {
            return Users.OfType<Technician>().Where(t => !t.IsBusy).ToList();
        }


        public Dictionary<OrderStatus, int> GetOrdersCountByStatus()
        {
            return Orders
                .GroupBy(o => o.Status)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        public Dictionary<string, decimal> GetRevenueByTechnician()
        {
            return Orders
                .Where(o => o.Technician != null)
                .GroupBy(o => o.Technician!.FullName)
                .ToDictionary(
                    group => group.Key,
                    group => group.TotalRevenue() 
                );
        }

        public Dictionary<DeviceType, int> GetDeviceTypeStatistics()
        {
            return Orders
                .Where(o => o.Device != null)
                .GroupBy(o => o.Device!.Type)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        public List<string> GenerateGlobalReport()
        {
            var items = new List<IReportable>();
            items.AddRange(Centers);
            items.AddRange(Orders);
            return items.Select(i => i.GetReportLine()).ToList();
        }
    }
}
