using Project.Logic.PaymentService; 
using Project.Logic.StoreManagment; 
using Project.Model; 
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores; 
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

        [Fact]
        public void Scenario_Buying_With_BLIK_Should_Success()
        {
            _output.WriteLine("SCENARIUSZ 1: Zakup z płatnością BLIK (Udany).");

            var customer = new Customer("Jan", "Kowalski", "+48123456789", "jan@wp.pl");
            customer.WalletBalance = 200m;

            var address = new Address("Warszawa", "Marszałkowska 1", "00-001", "PL");
            var service = new OrderService(); 

           
            var order = service.CreateOrder(customer, address);

            
            var product = new Product("Słuchawki", 50m, "Elektronika");
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

           
            var customer = new Customer("Ewa", "Nowak", "+48111222333", "ewa@wp.pl");
            customer.WalletBalance = 10m;

            var service = new OrderService();
            var order = service.CreateOrder(customer, new Address("A", "B", "00-000", "C"));

            
            service.AddItemToOrder(order, new Product("TV", 1000m, "RTV"), 1);

            
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

            
            var customer = new Customer("Adam", "Gotówkowy", "+48999888777", "a@g.pl");
            customer.WalletBalance = 0m;

            var service = new OrderService();
            var order = service.CreateOrder(customer, new Address("A", "B", "00-000", "C"));
            service.AddItemToOrder(order, new Product("Pizza", 40m, "Jedzenie"), 1);

           
            var cash = new CashPayment();

            
            bool result = service.ProcessOrderPayment(order, cash);

           
            Assert.True(result);
            Assert.Equal(OrderStatus.Paid, order.Status);
            Assert.Equal(0m, customer.WalletBalance); 

            _output.WriteLine("SUKCES. Płatność gotówką przyjęta.");
        }
    }
}