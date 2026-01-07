using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;
using Project.Extensions;

namespace Project.Tests
{
    public class StoreExtensionsTests
    {
        private List<InventoryItem> CreateTestInventory()
        {
            var store = new Store { Id = 1, Name = "S", Address = "A", City = "C", Region = "R", PostalCode = "P", Country = "C", PhoneNumber = "+48111222333" };

            var p1 = new ElectronicDevice { Id = 1, Name = "Tani", Manufacturer = "Sony", Price = 100m, Category = ProductCategory.Computer, Processor = "X", RamSizeGB = 1, ScreenSize = "S" };
            var p2 = new ElectronicDevice { Id = 2, Name = "Drogi", Manufacturer = "Samsung", Price = 1000m, Category = ProductCategory.Computer, Processor = "X", RamSizeGB = 1, ScreenSize = "S" };

            return new List<InventoryItem>
            {
                new InventoryItem { Product = p1, ProductId = 1, Quantity = 5, Store = store, StoreId = 1 },
                new InventoryItem { Product = p2, ProductId = 2, Quantity = 2, Store = store, StoreId = 1 }
            };
        }

        private List<Order> CreateTestOrders()
        {
            var c = new Customer(1, "A", "B", "a@b.c", "+48000111222", "C", "R", "00");
            var s = new Store { Id = 1, Name = "S", Address = "A", City = "C", Region = "R", PostalCode = "P", Country = "C", PhoneNumber = "+48123456789" };

            var o1 = new Order
            {
                Id = 1,
                DatePlaced = DateTime.Now.AddDays(-2),
                Status = OrderStatus.Paid,
                CustomerId = 1,
                Customer = c,
                StoreId = 1,
                FulfillingStore = s
            };

            var item1 = new OrderItem
            {
                Order = o1,
                OrderId = o1.Id,
                Product = new ElectronicDevice { Id = 1, Name = "X", Manufacturer = "M", Price = 200m, Category = ProductCategory.Computer, Processor = "", RamSizeGB = 1, ScreenSize = "" },
                ProductId = 1,
                Quantity = 1,
                UnitPrice = 200m
            };
            o1.OrderItems.Add(item1);
            o1.CalculateTotalValue();

            var o2 = new Order
            {
                Id = 2,
                DatePlaced = DateTime.Now,
                Status = OrderStatus.New,
                CustomerId = 1,
                Customer = c,
                StoreId = 1,
                FulfillingStore = s
            };

            var item2 = new OrderItem
            {
                Order = o2,
                OrderId = o2.Id,
                Product = new ElectronicDevice { Id = 2, Name = "Y", Manufacturer = "M", Price = 500m, Category = ProductCategory.Computer, Processor = "", RamSizeGB = 1, ScreenSize = "" },
                ProductId = 2,
                Quantity = 1,
                UnitPrice = 500m
            };
            o2.OrderItems.Add(item2);
            o2.CalculateTotalValue();

            return new List<Order> { o1, o2 };
        }

        [Fact]
        public void GetTotalQuantity_ShouldSumAllItems()
        {
            var inventory = CreateTestInventory();
            Assert.Equal(7, inventory.GetTotalQuantity());
        }

        [Fact]
        public void GetMostExpensiveItem_ShouldReturnItemWithHighestPrice()
        {
            var inventory = CreateTestInventory();
            var result = inventory.GetMostExpensiveItem();

            Assert.NotNull(result);
            Assert.Equal("Drogi", result.Product.Name);
        }

        [Fact]
        public void GetUniqueManufacturers_ShouldReturnDistinctList()
        {
            var inventory = CreateTestInventory();
            var manufacturers = inventory.GetUniqueManufacturers();

            Assert.Equal(2, manufacturers.Count);
            Assert.Contains("Sony", manufacturers);
            Assert.Contains("Samsung", manufacturers);
        }

        [Fact]
        public void CalculateTotalRevenue_ShouldSumOnlyPaidAndCompleted()
        {
            var orders = CreateTestOrders();
            decimal revenue = orders.CalculateTotalRevenue();

            Assert.Equal(200m, revenue);
        }

        [Fact]
        public void GetPendingOrders_ShouldReturnOnlyNewAndConfirmed()
        {
            var orders = CreateTestOrders();
            var pending = orders.GetPendingOrders();

            Assert.Single(pending);
            Assert.Equal(OrderStatus.New, pending.First().Status);
        }

        [Fact]
        public void IsVipCustomer_ShouldReturnTrue_WhenSpendingExceedsThreshold()
        {
            var c = new Customer(1, "VIP", "Man", "v@ip.pl", "+48000111222", "C", "R", "00");
            var s = new Store { Id = 1, Name = "S", Address = "A", City = "C", Region = "R", PostalCode = "P", Country = "C", PhoneNumber = "+48123456789" };

            var order = new Order
            {
                Id = 10,
                DatePlaced = DateTime.Now,
                Status = OrderStatus.Paid,
                CustomerId = 1,
                Customer = c,
                StoreId = 1,
                FulfillingStore = s
            };

            var item = new OrderItem
            {
                Order = order,
                OrderId = order.Id,
                Product = new ElectronicDevice { Id = 1, Name = "Car", Manufacturer = "T", Price = 6000m, Category = ProductCategory.Computer, Processor = "", RamSizeGB = 1, ScreenSize = "" },
                ProductId = 1,
                Quantity = 1,
                UnitPrice = 6000m
            };
            order.OrderItems.Add(item);
            order.CalculateTotalValue();

            var allOrders = new List<Order> { order };

            Assert.True(c.IsVipCustomer(allOrders));
        }

        [Fact]
        public void Truncate_ShouldShortenString()
        {
            string text = "Bardzo długi tekst";
            Assert.Equal("Bardzo...", text.Truncate(6));
        }

        [Fact]
        public void ToStatusLabel_ShouldFormatCorrectly()
        {
            var status = OrderStatus.New;
            Assert.Equal("[NEW]", status.ToStatusLabel());
        }
    }
}