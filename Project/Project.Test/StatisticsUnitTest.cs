using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;
using System;
using System.Linq;
using Project.Model.Orders;
using Project.Model.People;
using Project.Logic.StoreManagment;
using Project.Model.Stores;

namespace Project.Test
{
    public class ServiceOfStatisticsTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ServiceOfStatistics _statsService;

        public ServiceOfStatisticsTests(ITestOutputHelper output)
        {
            _output = output;
            _statsService = new ServiceOfStatistics();
        }

        private List<Order> GetSampleOrders()
        {
            var c1 = new Customer("Jan", "Kowalski", "+48500100100", "jan@wp.pl");
            var c2 = new Customer("Ewa", "Nowak", "+48600200200", "ewa@wp.pl");

            var a1 = new Address("Warszawa", "Złota", "00-001", "PL");
            var a2 = new Address("Kraków", "Smocza", "30-001", "PL");

            var o1 = new Order(c1, a1);
            o1.AddProduct(new Product("Suszarka", 100m, ProductCategory.SmallAppliance), 1);

            var o2 = new Order(c1, a1);
            o2.AddProduct(new Product("Suszarka", 100m, ProductCategory.SmallAppliance), 2);

            var o3 = new Order(c2, a2);
            o3.AddProduct(new Product("Telewizor", 500m, ProductCategory.TV), 1);

            return new List<Order> { o1, o2, o3 };
        }

        [Fact]
        public void GetMostExpensiveOrder()
        {
            var orders = GetSampleOrders();
            var result = _statsService.GetMostExpensiveOrder(orders);

            Assert.NotNull(result);
            Assert.Equal(500m, result.GetTotalAmount());
            Assert.Equal("Ewa", result.Purchaser.FirstName);
        }

        [Fact]
        public void GetTotalSpentByCustomer()
        {
            var orders = GetSampleOrders();
            var result = _statsService.GetTotalSpentByCustomer(orders);

            var janStats = result.First(x => x.Key.FirstName == "Jan");
            var ewaStats = result.First(x => x.Key.FirstName == "Ewa");

            Assert.Equal(300m, janStats.Value);
            Assert.Equal(500m, ewaStats.Value);
        }

        [Fact]
        public void GetRevenueByCategory()
        {
            var orders = GetSampleOrders();
            var result = _statsService.GetRevenueByCategory(orders);

            Assert.Equal(300m, result[ProductCategory.SmallAppliance]);
            Assert.Equal(500m, result[ProductCategory.TV]);
        }

        [Fact]
        public void GetStatusDistribution()
        {
            var orders = GetSampleOrders();
            var result = _statsService.GetStatusDistribution(orders);

            Assert.Equal(3, result[OrderStatus.New]);
            Assert.False(result.ContainsKey(OrderStatus.Paid));
        }
    }
}