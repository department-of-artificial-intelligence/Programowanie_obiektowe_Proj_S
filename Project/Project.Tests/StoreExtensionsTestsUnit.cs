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
            // Poprawione: Dodano wszystkie pola required dla Store
            var store = new Store
            {
                Id = 1,
                Name = "TechStore Częstochowa",
                Address = "Al. NMP 1",
                City = "Częstochowa",
                Region = "Śląskie",
                PostalCode = "42-200",
                Country = "PL",
                PhoneNumber = "+48341234567"
            };

            var p1 = new Product { Id = 1, Name = "Tani", Manufacturer = "Sony", Price = 100m, Category = ProductCategory.Computer };
            var p2 = new Product { Id = 2, Name = "Drogi", Manufacturer = "Samsung", Price = 1000m, Category = ProductCategory.Computer };

            return new List<InventoryItem>
            {
                new InventoryItem { Product = p1, ProductId = 1, Quantity = 5, Store = store, StoreId = 1 },
                new InventoryItem { Product = p2, ProductId = 2, Quantity = 2, Store = store, StoreId = 1 }
            };
        }

        private List<Order> CreateTestOrders()
        {
            var customer = new Customer(1, "Jan", "Test", "j@t.pl", "Adres 1", "+48111222333", "Wwa", "Maz", "00-001");

            // Poprawione: Store musi mieć wszystkie pola required i poprawny telefon
            var store = new Store
            {
                Id = 1,
                Name = "Magazyn Centralny",
                Address = "Logistyczna 5",
                City = "Warszawa",
                Region = "Mazowieckie",
                PostalCode = "00-001",
                Country = "PL",
                PhoneNumber = "+48221112233"
            };

            var order1 = new Order
            {
                Id = 1,
                Status = OrderStatus.Paid,
                Customer = customer,
                CustomerId = customer.CustomerId,
                DatePlaced = DateTime.Now,
                StoreId = store.Id,
                FulfillingStore = store
            };
            order1.AddProduct(new Product { Id = 101, Name = "Słuchawki", Manufacturer = "Sony", Price = 200m, Category = ProductCategory.Accessory }, 1);

            var order2 = new Order
            {
                Id = 2,
                Status = OrderStatus.New,
                Customer = customer,
                CustomerId = customer.CustomerId,
                DatePlaced = DateTime.Now,
                StoreId = store.Id,
                FulfillingStore = store
            };
            order2.AddProduct(new Product { Id = 102, Name = "Mysz", Manufacturer = "Logitech", Price = 500m, Category = ProductCategory.Accessory }, 1);

            return new List<Order> { order1, order2 };
        }

        [Fact]
        public void GetTotalQuantity_ShouldSumAllItems()
        {
            var inventory = CreateTestInventory();
            Assert.Equal(7, inventory.GetTotalQuantity());
        }

        [Fact]
        public void CalculateTotalRevenue_ShouldSumOnlyPaidAndCompleted()
        {
            var orders = CreateTestOrders();
            decimal revenue = orders.CalculateTotalRevenue();
            Assert.Equal(200m, revenue);
        }

        [Fact]
        public void IsVipCustomer_ShouldReturnTrue_WhenSpendingExceedsThreshold()
        {
            var c = new Customer(1, "VIP", "Man", "v@ip.pl", "Adres VIP", "+48000111222", "C", "R", "00-000");

            
            var s = new Store
            {
                Id = 1,
                Name = "Sklep VIP",
                Address = "Złota 44",
                City = "Warszawa",
                Region = "Mazowieckie",
                PostalCode = "00-001",
                Country = "PL",
                PhoneNumber = "+48229998877"
            };

            var order = new Order
            {
                Id = 10,
                DatePlaced = DateTime.Now,
                Status = OrderStatus.Paid,
                CustomerId = c.CustomerId,
                Customer = c,
                StoreId = s.Id,
                FulfillingStore = s
            };


            var product = new Product(1, "Laptop", "T", 6000m, ProductCategory.Computer);
            order.AddProduct(product, 1);


            order.AddProduct(product, 1);

            var allOrders = new List<Order> { order };
            Assert.True(c.IsVipCustomer(allOrders));
        }

        [Fact]
        public void Truncate_ShouldShortenString()
        {
            string text = "Bardzo długi tekst";
            Assert.Equal("Bardzo...", text.Truncate(6));
        }
    }
}