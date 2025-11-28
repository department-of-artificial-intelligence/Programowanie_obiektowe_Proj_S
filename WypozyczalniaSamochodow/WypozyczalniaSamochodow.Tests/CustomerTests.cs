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

            var customer = new Customer(expectedId,  expectedFirstName, expectedLastName, expectedLicenseNumber);

            Assert.Equal(expectedId, customer.Id);
            Assert.Equal(expectedFirstName, customer.FirstName);
            Assert.Equal(expectedLastName, customer.LastName);
            Assert.Equal(expectedLicenseNumber, customer.LicenseNumber);
        }

        [Fact]
        public void Test2()
        {
            var customer  = new Customer(2, "Wojtek", "Suchodolski", "123ABC");

            string result = customer.ToString();
            string expected = "[2] Wojtek Suchodolski | Numer prawa jazdy: 123ABC";

            Assert.Equal(expected, result);
        }
    }
}
