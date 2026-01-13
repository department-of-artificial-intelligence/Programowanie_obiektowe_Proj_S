using Project.Logic.PaymentService;
using Project.Logic.StoreManagement;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;
using Project.Model.Interfaces;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Project.Test
{
    public class BuyingMethodUnitTest
    {
        private readonly ITestOutputHelper _output;

        public BuyingMethodUnitTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private IOrderRepository GetFakeRepository()
        {
            return new FakeOrderRepository();
        }

       
        private Store CreateStore()
        {
            return new Store
            {
                Name = "Test Store",
                PhoneNumber = "+48123456789", 
                Address = new Address { City = "TestCity", Street = "TestStreet", ZipCode = "00-000", Country = "PL" }
            };
        }

        [Fact]
        public void Scenario_Buying_With_BLIK_Should_Success()
        {
            _output.WriteLine("SCENARIUSZ 1: Zakup z płatnością BLIK (Udany).");

            var store = CreateStore();
            var customer = new Customer("Jan", "Kowalski", "+48123456789", "jan@wp.pl");
            customer.WalletBalance = 200m;

            var address = new Address { City = "Warszawa", Street = "Marszałkowska 1", ZipCode = "00-001", Country = "PL" };

           
            var service = new OrderService(GetFakeRepository());

            // Przekazujemy sklep do metody CreateOrder (musisz zaktualizować OrderService.cs!)
            var order = service.CreateOrder(customer, address, store);

            // Ważne: Stock musi być > 0
            var product = new Product("Słuchawki", 50m, 10, ProductCategory.Smartphone);

            service.AddItemToOrder(order, product, 1);

            var blik = new BlikPayment("123456");

            _output.WriteLine($"Portfel przed zakupem: {customer.WalletBalance} PLN");

            bool result = service.ProcessOrderPayment(order, blik);

            Assert.True(result);
            Assert.Equal(OrderStatus.Paid, order.Status);
            Assert.Equal(150m, customer.WalletBalance);

            _output.WriteLine($"SUKCES. Portfel po zakupie: {customer.WalletBalance} PLN");
        }

        [Fact]
        public void Scenario_Buying_With_CreditCard_NoFunds_Should_Fail()
        {
            _output.WriteLine("SCENARIUSZ 2: Zakup kartą bez środków (Odrzucenie).");

            var store = CreateStore();
            var customer = new Customer("Ewa", "Nowak", "+48111222333", "ewa@wp.pl");
            customer.WalletBalance = 10m;
            var address = new Address { City = "A", Street = "B", ZipCode = "00-000", Country = "C" };

            var service = new OrderService(GetFakeRepository());
            var order = service.CreateOrder(customer, address, store);

            var product = new Product("TV", 1000m, 40,ProductCategory.TV);
            service.AddItemToOrder(order, product, 1);

            var card = new CreditCardPayment("1234123412341234", "Ewa Nowak");

            bool result = service.ProcessOrderPayment(order, card);

            Assert.False(result);
            Assert.NotEqual(OrderStatus.Paid, order.Status);
            Assert.Equal(10m, customer.WalletBalance);

            _output.WriteLine("SUKCES. Transakcja prawidłowo odrzucona.");
        }

        [Fact]
        public void Scenario_Buying_With_Cash_Should_Success_Without_Wallet()
        {
            _output.WriteLine("SCENARIUSZ 3: Płatność gotówką (Nie rusza portfela w aplikacji).");

            var store = CreateStore();
            var customer = new Customer("Adam", "Gotówkowy", "+48999888777", "a@g.pl");
            customer.WalletBalance = 0m;
            var address = new Address { City = "A", Street = "B", ZipCode = "00-000", Country = "C" };

            var service = new OrderService(GetFakeRepository());
            var order = service.CreateOrder(customer, address, store);

            var product = new Product("Iphone", 4000m, 50, ProductCategory.Smartphone);
            service.AddItemToOrder(order, product, 1);

            var cash = new CashPayment();

            bool result = service.ProcessOrderPayment(order, cash);

            Assert.True(result);
            Assert.Equal(OrderStatus.Paid, order.Status);
            Assert.Equal(0m, customer.WalletBalance);

            _output.WriteLine("SUKCES. Płatność gotówką przyjęta.");
        }

        
        private class FakeOrderRepository : IOrderRepository
        {
            public void Add(Order order) { }
            public System.Collections.Generic.List<Order> GetAll() => new System.Collections.Generic.List<Order>();
            public Order GetById(int id) => null;
            public void Remove(Order order) { }
            public void Update(Order order) { }
        }
    }
}