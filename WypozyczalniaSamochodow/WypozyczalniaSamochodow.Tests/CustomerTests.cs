using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Test1()
        {
            int expectedId = 1;
            string expectedFirstName = "Jan";
            string expectedLastName = "Kowalski";
            string expectedLicenseNumber = "G36JK1";
            string expectedEmail = "jan@poczta.pl";
            string expectedContactNumber = "111222333";

            var customer = new Customer(expectedId,  expectedFirstName, expectedLastName, expectedLicenseNumber, expectedEmail, expectedContactNumber);

            Assert.Equal(expectedId, customer.Id);
            Assert.Equal(expectedFirstName, customer.FirstName);
            Assert.Equal(expectedLastName, customer.LastName);
            Assert.Equal(expectedLicenseNumber, customer.LicenseNumber);
        }

        [Fact]
        public void Test2()
        {
            var customer  = new Customer(2, "Wojtek", "Suchodolski", "123ABC", "wojtek@poczta.pl", "555666777");

            string result = customer.ToString();
            string expected = "[2] Wojtek Suchodolski | Prawo jazdy: 123ABC | Email: wojtek@poczta.pl | Tel: 555666777";

            Assert.Equal(expected, result);
        }
    }
}
