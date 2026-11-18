using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ComputerServiceNetwork.dataModel;
using ComputerServiceNetwork.Extensions;
using ComputerServiceNetwork.Services;

namespace ComputerServiceNetwork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new NetworkManager();

            var client1 = new Client
            {
                Id = 1,
                FullName = "Jan Kowalski",
                Phone = "500-100-200",
                Email = "jan@mail.pl"
            };

            var tech1 = new Technician
            {
                Id = 2,
                FullName = "Piotr Nowak",
                Specialization = "Laptops & GPU",
                IsBusy = false
            };

            var tech2 = new Technician
            {
                Id = 3,
                FullName = "Anna Zielińska",
                Specialization = "Motherboards",
                IsBusy = true
            };

            manager.Users.AddRange(new User[] { client1, tech1, tech2 });

            var center = new ServiceCenter
            {
                Id = 1,
                City = "Częstochowa",
                Address = "al. NMP 15"
            };
            center.Staff.AddRange(new[] { tech1, tech2 });
            manager.Centers.Add(center);

            var ram = new SparePart { Id = 1, Name = "DDR5 16GB", Price = 250m, QuantityInStock = 2 };
            var ssd = new SparePart { Id = 2, Name = "NVMe 1TB", Price = 320m, QuantityInStock = 10 };

            var order1 = new Order
            {
                Id = 101,
                Client = client1,
                Technician = tech1,
                Device = new Device
                {
                    Id = 1,
                    Model = "Lenovo Legion 5",
                    SerialNumber = "SN998877",
                    Type = DeviceType.Laptop,
                    IssueDescription = "Overheating"
                },
                LaborCost = 150m,
                Status = OrderStatus.Closed,
                CompletedAt = DateTime.Now,
                UsedParts = new List<SparePart> { ram }
            };

            var order2 = new Order
            {
                Id = 102,
                Client = client1,
                Technician = tech2,
                Device = new Device
                {
                    Id = 2,
                    Model = "RTX 4060",
                    SerialNumber = "SN112233",
                    Type = DeviceType.GPU,
                    IssueDescription = "No display"
                },
                LaborCost = 200m,
                Status = OrderStatus.InProgress,
                UsedParts = new List<SparePart> { ssd }
            };

            client1.OrdersHistory.AddRange(new[] { order1, order2 });
            manager.Orders.AddRange(new[] { order1, order2 });

            Console.WriteLine("=== 1. POLYMORPHISM (USERS) ===");
            foreach (var user in manager.Users)
            {
                Console.WriteLine($"{user.GetRoleDescription()}: {user.GetContactInfo()}");
            }

            Console.WriteLine("\n=== 2. LINQ STATS (PROJ 2) ===");
            Console.WriteLine($"Average order cost: {manager.GetAverageOrderCost():F2} PLN");
            Console.WriteLine($"Completed revenue (Extension): {manager.Orders.OnlyCompleted().TotalRevenue():F2} PLN");

            Console.WriteLine("\n=== 3. GROUP BY STATS (PROJ 3) ===");
            foreach (var entry in manager.GetOrdersCountByStatus())
            {
                Console.WriteLine($"Status {entry.Key}: {entry.Value} order(s)");
            }

            foreach (var techRev in manager.GetRevenueByTechnician())
            {
                Console.WriteLine($"Technician {techRev.Key}: {techRev.Value} PLN");
            }

            Console.WriteLine("\n=== 4. INTERFACE REPORT (IReportable) ===");
            foreach (var line in manager.GenerateGlobalReport())
            {
                Console.WriteLine(line);
            }
        }
    }
}