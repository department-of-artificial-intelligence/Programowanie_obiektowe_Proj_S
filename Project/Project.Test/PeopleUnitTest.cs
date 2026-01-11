using Project.Model;
using Project.Model.People;
using Project.Model.Stores;
using Project.Model.Orders;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Project.Test
{
    public class PeopleUnitTest
    {
        private readonly ITestOutputHelper _output;

        
        public PeopleUnitTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData("123")]
        [InlineData("abcdefg")]
        [InlineData("123-456-789")]
        [InlineData("")]
        public void Throw_Exception_When_PhoneNumber_Invalid(string invalidPhone)
        {
            _output.WriteLine($"TEST: Sprawdzam błędny numer telefonu: '{invalidPhone}'");

            Assert.Throws<ArgumentException>(() =>
            {
                new Customer("Jan", "Testowy", invalidPhone, "jan@test.pl");
            });

            _output.WriteLine("SUKCES: Oczekiwany błąd (ArgumentException) został rzucony.");
        }

        [Fact]
        public void Create_Successfully_When_PhoneNumber_Valid()
        {
            string validPhone = "+48123456789";
            _output.WriteLine($"TEST: Tworzę klienta z poprawnym numerem: {validPhone}");

            var customer = new Customer("Jan", "Testowy", validPhone, "jan@test.pl");

            Assert.Equal(validPhone, customer.PhoneNumber);
            _output.WriteLine($"SUKCES: Klient utworzony poprawnie. Telefon w obiekcie: {customer.PhoneNumber}");
        }

        [Fact]
        public void Throw_Exception_When_Salary_Is_Negative()
        {
            _output.WriteLine("TEST: Próba utworzenia pracownika z ujemną pensją (-100)");

            var dummyAddress = new Address("A", "B", "00-000", "PL");
            var dummyStore = new Store("Test Store", dummyAddress, "+48111222333");

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new Employee("Jan", "Pracownik", "+48111222333", "a@a.pl",
                    dummyStore, EmployeePosition.Cashier, -100m, DateTime.Now);
            });

            _output.WriteLine("SUKCES: System zablokował ujemną pensję.");
        }

        [Fact]
        public void Calculate_HireDate_Correctly()
        {
            var hireDate = new DateTime(2020, 1, 1);
            _output.WriteLine($"TEST: Sprawdzam datę zatrudnienia: {hireDate.ToShortDateString()}");

            var dummyAddress = new Address("A", "B", "00-000", "PL");
            var dummyStore = new Store("Test Store", dummyAddress, "+48111222333");

            var employee = new Employee("Jan", "Pracownik", "+48111222333", "a@a.pl",
                    dummyStore, EmployeePosition.Cashier, 5000m, hireDate);

            Assert.Equal(hireDate, employee.HireDate);
            Assert.Equal(5000m, employee.Salary);

            _output.WriteLine($"SUKCES: Data zatrudnienia i pensja (5000) są poprawne.");
        }

        [Fact]
        public void Empty_Orders_List_On_Creation()
        {
            _output.WriteLine("TEST: Sprawdzam czy nowy klient ma pustą listę zamówień.");

            var customer = new Customer("Ewa", "Nowa", "+48999888777", "ewa@test.pl");

            Assert.NotNull(customer.Orders);
            Assert.Empty(customer.Orders);
            Assert.Equal(0, customer.WalletBalance);

            _output.WriteLine("SUKCES: Lista zamówień jest zainicjalizowana, ale pusta.");
        }

        [Theory]
        [InlineData("test")]
        [InlineData("test@")]
        [InlineData("@gmail.com")]
        [InlineData("jan kowalski@wp.pl")]
        [InlineData("jan@wp")]
        public void Throw_Exception_When_Email_Invalid(string invalidEmail)
        {
            _output.WriteLine($"TEST: Walidacja błędnego adresu email: '{invalidEmail}'");

            Assert.Throws<ArgumentException>(() =>
            {
                
                new Customer("Jan", "Testowy", "+48123456789", invalidEmail);
            });

            _output.WriteLine("SUKCES: Błędny email został odrzucony.");
        }

        [Fact]
        public void Accept_Valid_Email()
        {
            string validEmail = "jan.kowalski@firma.com.pl";
            _output.WriteLine($"TEST: Sprawdzam poprawny email: {validEmail}");

            var customer = new Customer("Jan", "Kowalski", "+48123456789", validEmail);

            Assert.Equal(validEmail, customer.Email);
            _output.WriteLine($"SUKCES: Email zaakceptowany: {customer.Email}");
        }
    }
}