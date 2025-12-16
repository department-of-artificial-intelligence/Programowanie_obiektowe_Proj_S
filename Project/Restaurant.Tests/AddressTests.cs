using RestaurantManagement.Models;
using RestaurantManagement.Models.Interfaces;

namespace RestaurantManagement.Tests
{
    public class AddressTest
    {
        [Fact]
        public void Create_Works()
        {
            // Arrange & Act
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");

            // Assert
            Assert.Equal("Polska", address.Country);
            Assert.Equal("00-001", address.ZipCode);
            Assert.Equal("Warszawa", address.City);
            Assert.Equal("Warszawska 10", address.Street);
        }

        [Fact]
        public void ToString_Works()
        {
            // Arrange
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");

            // Act
            var result = address.ToString();

            // Assert
            Assert.Contains("Polska", result);
            Assert.Contains("00-001", result);
            Assert.Contains("Warszawa", result);
            Assert.Contains("Warszawska 10", result);
        }

        [Fact]
        public void Change_Works()
        {
            // Arrange
            var address = new Address("Polska", "00-001", "Warszawa", "Warszawska 10");

            // Act
            address.Country = "Polska";
            address.ZipCode = "00-001";
            address.City = "Warszawa";
            address.Street = "Warszawska 10";

            // Assert
            Assert.Equal("Polska", address.Country);
            Assert.Equal("00-001", address.ZipCode);
            Assert.Equal("Warszawa", address.City);
            Assert.Equal("Warszawska 10", address.Street);
        }
    }
}


