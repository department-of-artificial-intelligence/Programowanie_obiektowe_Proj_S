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
            
            Assert.Throws<ArgumentException>(() =>
            {
                new Customer("Jan", "Testowy", invalidPhone, "jan@test.pl");
            });
        }

        [Fact]
        public void Create_Successfully_When_PhoneNumber_Valid()
        {
            
            string validPhone = "+48123456789";

            
            var customer = new Customer("Jan", "Testowy", validPhone, "jan@test.pl");

           
            Assert.Equal(validPhone, customer.PhoneNumber);
        }

        
        [Fact]
        public void Throw_Exception_When_Salary_Is_Negative()
        {
            
            var dummyAddress = new Address("A", "B", "00-000", "PL");
            var dummyStore = new Store("Test Store", dummyAddress, "+48111222333");

            
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                
                new Employee("Jan", "Pracownik", "+48111222333", "a@a.pl",
                    dummyStore, EmployeePosition.Cashier, -100m, DateTime.Now);
            });
        }

        [Fact]
        public void Calculate_HireDate_Correctly()
        {
           
            var dummyAddress = new Address("A", "B", "00-000", "PL");
            var dummyStore = new Store("Test Store", dummyAddress, "+48111222333");
            var hireDate = new DateTime(2020, 1, 1);

            
            var employee = new Employee("Jan", "Pracownik", "+48111222333", "a@a.pl",
                    dummyStore, EmployeePosition.Cashier, 5000m, hireDate);

            
            Assert.Equal(hireDate, employee.HireDate);
            Assert.Equal(5000m, employee.Salary);
        }

       
        [Fact]
        public void Empty_Orders_List_On_Creation()
        {
            
            var customer = new Customer("Ewa", "Nowa", "+48999888777", "ewa@test.pl");

           
            Assert.NotNull(customer.Orders); 
            Assert.Empty(customer.Orders);   
            Assert.Equal(0, customer.WalletBalance); 
        }



        [Theory]
        [InlineData("test")]                
        [InlineData("test@")]               
        [InlineData("@gmail.com")]          
        [InlineData("jan kowalski@wp.pl")]  
        [InlineData("jan@wp")]              
        public void Throw_Exception_When_Email_Invalid(string invalidEmail)
        {
            
            Assert.Throws<ArgumentException>(() =>
            {
                
                var klient1 = new Customer("Jan", "Testowy", "+48123456789", invalidEmail);

                klient1.GetInfo();
            });
        }

        [Fact]
        public void Accept_Valid_Email()
        {
            
            string validEmail = "jan.kowalski@firma.com.pl";

           
            var customer = new Customer("Jan", "Kowalski", "+48123456789", validEmail);

            
            Assert.Equal(validEmail, customer.Email);
        }
    }
}