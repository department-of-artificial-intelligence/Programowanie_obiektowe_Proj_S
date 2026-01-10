using Xunit;
using System;
using Project.Model.People;

namespace Project.Tests
{
    public class PaymentTests
    {
        private Customer CreateTestCustomer(decimal balance)
        {
            return new Customer(1, "Test", "User", "test@test.pl", "ul. Polna 1", "+48111222333", "City", "Region", "00-000")
            {
                WalletBalance = balance
            };
        }

        [Fact]
        public void Blik_ShouldReduceBalance_WhenFundsAreSufficient()
        {
            var customer = CreateTestCustomer(100m);
            var payment = new BlikPayment("123456");
            decimal amountToPay = 40m;

            bool result = payment.Pay(amountToPay, customer);

            Assert.True(result);
            Assert.Equal(60m, customer.WalletBalance);
        }

        [Fact]
        public void Blik_Constructor_ShouldThrowException_WhenCodeInvalid()
        {
            Assert.Throws<ArgumentException>(() => new BlikPayment("123"));
        }

        [Fact]
        public void CreditCard_ShouldReduceBalance_WhenFundsAreSufficient()
        {
            var customer = CreateTestCustomer(200m);
            var payment = new CreditCardPayment("1111222233334444", "Jan Nowak");

            bool result = payment.Pay(100m, customer);

            Assert.True(result);
            Assert.Equal(100m, customer.WalletBalance);
        }

        [Fact]
        public void CashPayment_ShouldNotReduceBalance()
        {
            var customer = CreateTestCustomer(100m);
            var payment = new CashPayment();

            bool result = payment.Pay(50m, customer);

            Assert.True(result);
            Assert.Equal(100m, customer.WalletBalance);
        }
    }
}