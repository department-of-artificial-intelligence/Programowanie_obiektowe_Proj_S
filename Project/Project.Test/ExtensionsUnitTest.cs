using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;
using System;
using Project.Logic.Extensions;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;

namespace Project.Test
{
    public class ProjectExtensionsTests
    {
        private readonly ITestOutputHelper _output;

        public ProjectExtensionsTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData("1234567812345678", "****-****-****-5678")]
        [InlineData("12345", "****-****-****-2345")]
        [InlineData("123", "****")]
        [InlineData("", "")]
        [InlineData(null, "")]
        public void MaskSensitiveData_Should_Mask_Correctly(string input, string expected)
        {
            string result = input.MaskSensitiveData();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("123-456-789", "123456789")]
        [InlineData("12 34 56", "123456")]
        [InlineData("  123  ", "123")]
        public void RemoveSpecialCharacters_Should_Clean_String(string input, string expected)
        {
            string result = input.RemoveSpecialCharacters();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void HasItems_Should_Detect_Empty_Or_Null_Lists()
        {
            List<string> nullList = null;
            List<string> emptyList = new List<string>();
            List<string> fullList = new List<string> { "A" };

            Assert.False(nullList.HasItems());
            Assert.False(emptyList.HasItems());
            Assert.True(fullList.HasItems());
        }

        [Fact]
        public void ToConsoleString_Should_Format_List_With_NewLines()
        {
            var list = new List<string> { "Mleko", "Chleb" };

            string result = list.ToConsoleString();

            _output.WriteLine(result);

            Assert.Contains(" - Mleko", result);
            Assert.Contains(" - Chleb", result);
            Assert.Contains(Environment.NewLine, result);
        }

        [Fact]
        public void CalculateTotalRevenue_Should_Sum_Only_Active_Orders()
        {
            var customer = new Customer("Jan", "K", "+53673492000", "e@m.pl");
            var addr = new Address("A", "B", "00-000", "C");

            var order1 = new Order(customer, addr);
            order1.AddProduct(new Product("A", 100m, ProductCategory.SmallAppliance), 1);
            order1.MarkAsPaid();

            var order2 = new Order(customer, addr);
            order2.AddProduct(new Product("B", 50m, ProductCategory.TV), 1);

            var order3 = new Order(customer, addr);
            order3.AddProduct(new Product("C", 2000m, ProductCategory.Smartphone), 1);
            order3.CancelOrder();

            var orders = new List<Order> { order1, order2, order3 };

            decimal total = orders.CalculateTotalRevenue();

            Assert.Equal(150m, total);
        }

        [Theory]
        [InlineData(OrderStatus.New, "Nowe (Oczekuje na płatność)")]
        [InlineData(OrderStatus.Confirmed, "Potwierdzone")]
        [InlineData(OrderStatus.Paid, "Opłacone")]
        [InlineData(OrderStatus.Shipped, "Wysłane do klienta")]
        [InlineData(OrderStatus.Completed, "Zakończone")]
        [InlineData(OrderStatus.Cancelled, "Anulowane")]
        public void ToPolishDescription_Should_Return_Correct_Translation(OrderStatus status, string expectedText)
        {
            string result = status.ToPolishDescription();
            Assert.Equal(expectedText, result);
        }

        [Theory]
        [InlineData(OrderStatus.New, ConsoleColor.Yellow)]
        [InlineData(OrderStatus.Paid, ConsoleColor.Green)]
        [InlineData(OrderStatus.Shipped, ConsoleColor.Cyan)]
        [InlineData(OrderStatus.Completed, ConsoleColor.DarkGreen)]
        [InlineData(OrderStatus.Cancelled, ConsoleColor.Red)]
        public void ToConsoleColor_Should_Assign_Logical_Colors(OrderStatus status, ConsoleColor expectedColor)
        {
            ConsoleColor result = status.ToConsoleColor();
            Assert.Equal(expectedColor, result);
        }
    }
}