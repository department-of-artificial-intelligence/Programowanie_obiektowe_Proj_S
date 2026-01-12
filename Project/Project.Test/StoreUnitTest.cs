using Xunit;
using Xunit.Abstractions;
using System;
using System.Linq;
using Project.Model;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;

namespace Project.Test
{
    public class StoreTestUnit
    {
        
        private readonly ITestOutputHelper _output;

        public StoreTestUnit(ITestOutputHelper output)
        {
            _output = output;
        }



        [Fact]
        public void Calculate_Its_TotalAmount()
        {
            _output.WriteLine("TEST: Obliczanie wartości pojedynczego zamówienia.");

            var customer = new Customer("Test", "User", "+48123456789", "test@test.pl");
            var address = new Address("A", "B", "50-361", "PL");
            var order = new Order(customer, address);

            var p5 = new Product("Telewizor OLED 65 cali", 5999.00m, ProductCategory.TV);
            var p6 = new Product("Soundbar kina domowego", 850.00m, ProductCategory.Audio);


            _output.WriteLine($"Cena p1 (Mleko): {p5.Price}");
            _output.WriteLine($"Cena p2 (Chleb): {p6.Price}");

            order.AddProduct(p5, 10);
            order.AddProduct(p6, 2);

            
            foreach (var item in order.Items)
            {
                _output.WriteLine($"Pozycja: {item.Product.Name}, Ilość: {item.Quantity}, Cena jedn.: {item.Product.Price}, Razem: {item.GetLineTotal()}");
            }

            decimal expected = 61690.00m;

            
            _output.WriteLine($"Suma zamówienia (Order.TotalAmount): {order.GetTotalAmount()}");

            Assert.Equal(expected, order.GetTotalAmount());
        }



        [Fact]
        public void Change_Status_To_Shipped()
        {
            _output.WriteLine("TEST: Zmiana statusu zamówienia na Wysłane.");

            
            var order = new Order(new Customer("A", "B", "+48123456789", "a@a.pl"), new Address("A", "B", "67-531", "P"));

           
            var product = new Product("Test", 100m, ProductCategory.Other);
            order.AddProduct(product, 13);

            
            order.ShipOrder();

            
            Assert.Equal(OrderStatus.Shipped, order.Status);
            _output.WriteLine("SUKCES: Status zmienił się poprawnie.");
        }



        [Fact]
        public void Store_Should_Add_Employee_Correctly()
        {
            _output.WriteLine("TEST: Dodawanie pracownika do sklepu.");

            var address = new Address("Wrocław", "Rynek", "50-001", "PL");
            var store = new Store("Sklep Testowy", address, "+48710000000");

            var employee = new Employee("Jan", "Kowalski", "+48123123123", "j@k.pl",
                store, EmployeePosition.Manager, 5000m, DateTime.Now);

            store.Staff.Add(employee);

            Assert.Single(store.Staff);
            Assert.Equal("Jan", store.Staff[0].FirstName);
            _output.WriteLine("SUKCES: Pracownik dodany.");
        }



        [Fact]
        public void Integration_Customer_TotalSpent_Should_Sum_Multiple_Orders()
        {
            _output.WriteLine("TEST INTEGRACYJNY: Pełny scenariusz.");


            var p1 = new Product("Kabel HDMI 2m", 25.00m, ProductCategory.Accessory);
            var p2 = new Product("Słuchawki Bezprzewodowe", 199.00m, ProductCategory.Audio);
            var p3 = new Product("Smartfon Pro X", 3500.00m, ProductCategory.Smartphone);
            var p4 = new Product("Ekspres do kawy", 1200.00m, ProductCategory.SmallAppliance);


            var customer = new Customer("Ewa", "Kowalska", "+48987654321", "ewa@klient.pl");
            var deliveryAddress = new Address("Warszawa", "ul. Polna 5", "00-123", "Polska");

            
            var order1 = new Order(customer, deliveryAddress);
            order1.AddProduct(p1, 10); 
            order1.AddProduct(p2, 2);  
            customer.Orders.Add(order1);

            
            var order2 = new Order(customer, deliveryAddress);
            order2.AddProduct(p3, 1); 
            order2.AddProduct(p4, 2);  
            customer.Orders.Add(order2);

           
            order1.ShipOrder();
            order2.ShipOrder();

            
            decimal totalSpent = customer.Orders
                .Where(o => o.Status == OrderStatus.Shipped)
                .Sum(o => o.GetTotalAmount());

            _output.WriteLine($"Łącznie wydano: {totalSpent} PLN");

           
            Assert.Equal(6548m, totalSpent);
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

            string validZip = "11-950";
            var address = new Address("Warszawa", "Testowa", validZip, "Polska");

            Assert.Equal(validZip, address.ZipCode);
            _output.WriteLine($"SUKCES: Adres utworzony z kodem: {address.ZipCode}");
        }

    }
}