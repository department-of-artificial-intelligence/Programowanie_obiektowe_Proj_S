using Xunit;
using Xunit.Abstractions;
using System;
using System.Linq;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;

namespace Project.Test
{
    public class OrderModelTests
    {
        private readonly ITestOutputHelper _output;

        public OrderModelTests(ITestOutputHelper output)
        {
            _output = output;
        }

        

        [Fact]
        public void Address_ToString_Should_Return_Formatted_String()
        {
            _output.WriteLine("TEST: Sprawdzanie formatowania adresu.");

            
            var address = new Address("Warszawa", "Złota 44", "00-120", "Polska");

            string result = address.ToString();

            _output.WriteLine($"Wynik: {result}");

            Assert.Equal("Złota 44, 00-120 Warszawa, Polska", result);
        }



        [Theory]
        [InlineData("12345")]       
        [InlineData("12-34")]       
        [InlineData("12-3456")]     
        [InlineData("AB-CDE")]      
        [InlineData("12 345")]      
        [InlineData("")]            
        public void Address_Should_Throw_Exception_When_ZipCode_Invalid(string invalidZip)
        {
            _output.WriteLine($"TEST: Walidacja błędnego kodu pocztowego: '{invalidZip}'");

            Assert.Throws<ArgumentException>(() =>
            {
                new Address("Warszawa", "Testowa", invalidZip, "Polska");
            });

            _output.WriteLine("SUKCES: Błędny kod został odrzucony.");
        }


        [Fact]
        public void Address_Should_Accept_Valid_ZipCode()
        {
            _output.WriteLine("TEST: Walidacja poprawnego kodu pocztowego.");

            string validZip = "00-950";
            var address = new Address("Warszawa", "Testowa", validZip, "Polska");

            Assert.Equal(validZip, address.ZipCode);
            _output.WriteLine($"SUKCES: Adres utworzony z kodem: {address.ZipCode}");
        }

        

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void OrderItem_Should_Throw_Exception_When_Quantity_Is_Invalid(int invalidQuantity)
        {
            _output.WriteLine($"TEST: Walidacja ujemnej ilości w OrderItem: {invalidQuantity}");

            var product = new Product("Słuchawki Bezprzewodowe", 199.00m, ProductCategory.Audio);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new OrderItem(product, invalidQuantity);
            });

            _output.WriteLine("SUKCES: Błędna ilość została zablokowana.");
        }


        [Fact]
        public void OrderItem_Should_Calculate_LineTotal_Correctly()
        {
            _output.WriteLine("TEST: Obliczanie wartości pozycji (Cena * Ilość).");

            var product = new Product("Tablet Graficzny", 1100.00m, ProductCategory.Computer);
            int quantity = 3;

            var item = new OrderItem(product, quantity);

            decimal expected = 3300m; 

            Assert.Equal(expected, item.GetLineTotal());
            _output.WriteLine($"SUKCES: {quantity}x {product.Price} = {item.GetLineTotal()}");
        }

        

        [Fact]
        public void Order_AddProduct_Should_Merge_Items_When_Product_Exists()
        {
            _output.WriteLine("TEST: Łączenie produktów (Ta sama nazwa powinna zwiększać ilość).");

            
            var customer = new Customer("Jan", "Test", "+48111222333", "j@test.pl");
            
            var address = new Address("City", "Str", "00-000", "PL");
            var order = new Order(customer, address);

            var p7 = new Product("Laptop Biurowy 15", 2400.00m, ProductCategory.Computer);
            var p8 = new Product("Tablet Graficzny", 1100.00m, ProductCategory.Computer);
            var p9 = new Product("Smartfon Galaxy S24", 3999.00m, ProductCategory.Smartphone);


            order.AddProduct(p7, 5);
            order.AddProduct(p8, 5); 
            order.AddProduct(p9, 10); 

            
            Assert.Equal(3, order.Items.Count); 

            var smartphoneItem = order.Items.First(i => i.Product.Name == "Smartfon Galaxy S24");
            Assert.Equal(10, smartphoneItem.Quantity);

            _output.WriteLine($"SUKCES: Pozycje scalone poprawnie. Ilość Smartfonu Galaxy: {smartphoneItem.Quantity}.");
        }


        [Fact]
        public void Order_AddProduct_Should_Throw_Exception_When_Adding_Zero_Quantity()
        {
            _output.WriteLine("TEST: Czy metoda AddProduct respektuje walidację ilości?");

            
            var order = new Order(new Customer("A", "B", "+48111222333", "a@b.com"), new Address("A", "B", "37-111", "D"));
            var product = new Product("Test", 100m, ProductCategory.Other);

           
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                order.AddProduct(product, 0);
            });

            _output.WriteLine($"SUKCES: Otrzymano oczekiwany błąd: {exception.Message}");
        }


        [Fact]
        public void Order_Status_Flow_And_Restrictions()
        {
            _output.WriteLine("TEST: Cykl życia zamówienia i blokady statusów.");

            var customer = new Customer("Jan", "Test", "+48111222333", "j@test.pl");
            var order = new Order(customer, new Address("A", "B", "00-000", "D"));

           
            Assert.Equal(OrderStatus.New, order.Status);

            
            order.MarkAsPaid();
            Assert.Equal(OrderStatus.Paid, order.Status);

            
            order.ShipOrder();
            Assert.Equal(OrderStatus.Shipped, order.Status);

            
            order.CancelOrder();
            Assert.Equal(OrderStatus.Cancelled, order.Status);
            _output.WriteLine("SUKCES: Udało się anulować zamówienie wysłane (zgodnie z nową logiką).");

            
        }

        [Fact]
        public void Order_Should_Block_Shipping_When_Cancelled()
        {
            _output.WriteLine("TEST: Sprawdzenie logiki anulowania zamówienia.");

            
            var order = new Order(new Customer("Jan", "K", "+48123456789", "e@m.pl"), new Address("U", "M", "99-999", "PL"));

            order.AddProduct(new Product("Szczoteczka soniczna", 120.00m, ProductCategory.SmallAppliance),2);


            order.CancelOrder();

            
            Assert.Equal(OrderStatus.Cancelled, order.Status);

            
            Assert.Throws<InvalidOperationException>(() =>
            {
                order.ShipOrder();
            });

            _output.WriteLine("SUKCES: Nie można wysłać anulowanego zamówienia.");
        }

        [Fact]
        public void Order_TotalAmount_Should_Sum_All_Items()
        {
            _output.WriteLine("TEST: Suma całkowita zamówienia.");

            
            var order = new Order(new Customer("A", "B", "+48111222333", "a@b.onet"), new Address("A", "B", "25-001", "D"));

            order.AddProduct(new Product("Klawiatura mechaniczna", 450.00m, ProductCategory.Accessory), 1); 
            order.AddProduct(new Product("Laptop Biurowy 15", 2400.00m, ProductCategory.Computer), 4);  

            Assert.Equal(10050m, order.GetTotalAmount());
            _output.WriteLine("SUKCES: Suma obliczona poprawnie.");
        }
    }
}