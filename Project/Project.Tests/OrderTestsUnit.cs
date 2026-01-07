using Xunit;
using System;
using System.Linq;
using Project.Model;

namespace Project.Tests
{
    public class OrderTests
    {
        private Order CreateTestOrder(decimal customerBalance = 1000m)
        {
            var customer = new Customer(1, "Jan", "Testowy", "jan@test.pl", "+48111222333", "Warszawa", "Mazowieckie", "00-100")
            {
                WalletBalance = customerBalance
            };

            var store = new Store
            {
                Id = 1,
                Name = "TestStore",
                Address = "Ulica 1",
                City = "Miasto",
                Region = "Region",
                PostalCode = "00-000",
                Country = "PL",
                PhoneNumber = "123"
            };

            return new Order
            {
                Id = 1,
                DatePlaced = DateTime.Now,
                Status = OrderStatus.New,
                CustomerId = customer.CustomerId,
                Customer = customer,
                StoreId = store.Id,
                FulfillingStore = store
            };
        }

        private Product CreateTestProduct(decimal price)
        {
            return new ElectronicDevice
            {
                Id = 101,
                Name = "Test Phone",
                Manufacturer = "TestCo",
                Price = price,
                Category = ProductCategory.Smartphone,
                Processor = "TestCPU",
                RamSizeGB = 8,
                ScreenSize = "6 inch"
            };
        }

        [Fact]
        public void AddProduct_ShouldAddNewItem_WhenProductNotExistsInOrder()
        {
            var order = CreateTestOrder();
            var product = CreateTestProduct(100m);

            order.AddProduct(product, 1);

            Assert.Single(order.OrderItems);
            Assert.Equal(100m, order.TotalValue);
        }

        [Fact]
        public void AddProduct_ShouldIncreaseQuantity_WhenProductAlreadyExists()
        {
            var order = CreateTestOrder();
            var product = CreateTestProduct(50m);

            order.AddProduct(product, 1);
            order.AddProduct(product, 2);

            Assert.Single(order.OrderItems);
            Assert.Equal(3, order.OrderItems.First().Quantity);
            Assert.Equal(150m, order.TotalValue);
        }

        [Fact]
        public void CalculateTotalValue_ShouldIncludeDeliveryCost()
        {
            var order = CreateTestOrder();
            var product = CreateTestProduct(200m);
            order.AddProduct(product, 1);

            order.Delivery = new CourierDelivery("Test Address");

            decimal total = order.CalculateTotalValue();

            Assert.Equal(219.99m, total);
        }

        [Fact]
        public void FinalizeOrder_ShouldMarkAsPaid_WhenPaymentSuccessful()
        {
            var order = CreateTestOrder(customerBalance: 500m);
            var product = CreateTestProduct(100m);
            order.AddProduct(product, 1);

            order.SetPaymentMethod(new CashPayment());

            order.FinalizeOrder();

            Assert.Equal(OrderStatus.Paid, order.Status);
        }

        [Fact]
        public void FinalizeOrder_ShouldNotChangeStatus_WhenPaymentFails()
        {
            var order = CreateTestOrder(customerBalance: 10m);
            var product = CreateTestProduct(100m);
            order.AddProduct(product, 1);

            order.SetPaymentMethod(new CreditCardPayment("1111222233334444", "Jan Testowy"));

            order.FinalizeOrder();

            Assert.NotEqual(OrderStatus.Paid, order.Status);
            Assert.Equal(OrderStatus.New, order.Status);
        }

        [Fact]
        public void FinalizeOrder_ShouldFail_WhenNoPaymentMethodSelected()
        {
            var order = CreateTestOrder();
            var product = CreateTestProduct(100m);
            order.AddProduct(product, 1);

            order.PaymentMethod = null;

            order.FinalizeOrder();

            Assert.Equal(OrderStatus.New, order.Status);
        }

        [Fact]
        public void AddProduct_ShouldThrowException_WhenQuantityIsNegative()
        {
            var order = CreateTestOrder();
            var product = CreateTestProduct(100m);

            Assert.Throws<ArgumentException>(() =>
            {
                order.AddProduct(product, -5);
            });
        }
    }
}