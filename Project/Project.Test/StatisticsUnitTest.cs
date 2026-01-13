using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;
using System;
using System.Linq;
using Project.Model.Orders;
using Project.Model.People;
using Project.Logic.StoreManagement;
using Project.Model.Stores;
using Project.Model.Interfaces;


namespace Project.Test
{
    public class ServiceOfStatisticsTests
    {
        private readonly ITestOutputHelper _output;

        public ServiceOfStatisticsTests(ITestOutputHelper output)
        {
            _output = output;
        }

        
        private IOrderRepository GetFakeRepository()
        {
            return new FakeOrderRepository();
        }

        private List<Order> GetSampleOrders()
        {
            var store = new Store
            {
                Name = "Test Store",
                PhoneNumber = "123-456-789",
                Address = new Address { City = "TestCity", Street = "TestStreet", ZipCode = "00-000", Country = "PL" }
            };

            var c1 = new Customer("Jan", "Kowalski", "+48500100100", "jan@wp.pl");
            var c2 = new Customer("Ewa", "Nowak", "+48600200200", "ewa@wp.pl");

            var a1 = new Address { City = "Warszawa", Street = "Złota", ZipCode = "00-001", Country = "PL" };
            var a2 = new Address { City = "Kraków", Street = "Smocza", ZipCode = "30-001", Country = "PL" };

           
            var p1 = new Product("Suszarka", 100m, 100, ProductCategory.SmallAppliance);
            var p2 = new Product("Telewizor", 500m, 50, ProductCategory.TV);

            var o1 = new Order(c1, a1, store);
            o1.AddProduct(p1, 1);

            var o2 = new Order(c1, a1, store);
            o2.AddProduct(p1, 2); 

            var o3 = new Order(c2, a2, store);
            o3.AddProduct(p2, 1); 

            return new List<Order> { o1, o2, o3 };
        }

        

        [Fact]
        public void GetMostExpensiveOrder_ShouldReturnHighestValue()
        {
            
            var statsService = new ServiceOfStatistics(GetFakeRepository());
            var orders = GetSampleOrders();

           
            var result = statsService.GetMostExpensiveOrder();

            
            Assert.NotNull(result);
            Assert.Equal(500m, result.GetTotalAmount());
            Assert.Equal("Ewa", result.Purchaser.FirstName);
        }

        [Theory]
        [InlineData("Jan", 300)]
        [InlineData("Ewa", 500)]
        public void GetTotalSpentByCustomer_ShouldReturnCorrectAmount(string firstName, decimal expectedAmount)
        {
            
            var statsService = new ServiceOfStatistics(GetFakeRepository());
            var orders = GetSampleOrders();

           
            var result = statsService.GetTotalSpentByCustomer();
            var customerStat = result.FirstOrDefault(x => x.Key.FirstName == firstName);

            
            Assert.NotNull(customerStat.Key);
            Assert.Equal(expectedAmount, customerStat.Value);
        }

        [Theory]
        [InlineData(ProductCategory.SmallAppliance, 300)]
        [InlineData(ProductCategory.TV, 500)]
        public void GetRevenueByCategory_ShouldReturnCorrectSums(ProductCategory category, decimal expectedRevenue)
        {
            
            var statsService = new ServiceOfStatistics(GetFakeRepository());
            var orders = GetSampleOrders();

           
            var result = statsService.GetRevenueByCategory();

           
            Assert.True(result.ContainsKey(category));
            Assert.Equal(expectedRevenue, result[category]);
        }

        [Fact]
        public void GetStatusDistribution_ShouldCountStatuses()
        {
            
            var statsService = new ServiceOfStatistics(GetFakeRepository());
            var orders = GetSampleOrders();

           
            var result = statsService.GetStatusDistribution();

            
            Assert.Equal(3, result[OrderStatus.New]);
            Assert.False(result.ContainsKey(OrderStatus.Paid));
        }

        
        private class FakeOrderRepository : IOrderRepository
        {
            public void Add(Order order) { }
            public List<Order> GetAll() => new List<Order>();
            public Order GetById(int id) => null;
            public void Remove(Order order) { }
            public void Update(Order order) { }
        }
    }
}