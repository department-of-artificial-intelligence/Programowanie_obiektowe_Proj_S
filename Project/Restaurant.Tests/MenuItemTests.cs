using RestaurantManagement.Models;

namespace RestaurantManagement.Tests
{
    public class MenuItemTests
    {
        [Fact]
        public void MenuItemCreation_ShouldSetAllProperties()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Pizza Margherita",
                Description = "Klasyczna pizza z sosem pomidorowym i mozzarellą",
                Price = 25.99f
            };

            // Assert
            Assert.Equal("Pizza Margherita", menuItem.Name);
            Assert.Equal("Klasyczna pizza z sosem pomidorowym i mozzarellą", menuItem.Description);
            Assert.Equal(25.99f, menuItem.Price);
        }

        [Fact]
        public void MenuItem_PriceShouldBePositive()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Spaghetti Carbonara",
                Description = "Makaron z sosem carbonara",
                Price = 32.50f
            };

            // Assert
            Assert.True(menuItem.Price > 0);
        }

        [Fact]
        public void MenuItem_NameShouldNotBeEmpty()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Burger",
                Description = "Soczyste mięso z warzywami",
                Price = 28.00f
            };

            // Assert
            Assert.NotNull(menuItem.Name);
            Assert.NotEmpty(menuItem.Name);
        }

        [Fact]
        public void MenuItem_DescriptionShouldNotBeEmpty()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Sałatka grecka",
                Description = "Świeże warzywa z serem feta",
                Price = 18.50f
            };

            // Assert
            Assert.NotNull(menuItem.Description);
            Assert.NotEmpty(menuItem.Description);
        }

        [Fact]
        public void MenuItem_PropertiesCanBeModified()
        {
            // Arrange
            var menuItem = new MenuItem
            {
                Name = "Zupa pomidorowa",
                Description = "Tradycyjna zupa",
                Price = 12.00f
            };

            // Act
            menuItem.Name = "Zupa krem pomidorowa";
            menuItem.Description = "Aksamitna zupa pomidorowa";
            menuItem.Price = 15.00f;

            // Assert
            Assert.Equal("Zupa krem pomidorowa", menuItem.Name);
            Assert.Equal("Aksamitna zupa pomidorowa", menuItem.Description);
            Assert.Equal(15.00f, menuItem.Price);
        }

        [Theory]
        [InlineData("Pierogi", "Polskie pierogi", 18.00f)]
        [InlineData("Schabowy", "Kotlet schabowy z ziemniakami", 35.00f)]
        [InlineData("Barszcz", "Czerwony barszcz", 10.00f)]
        public void MenuItem_ShouldAcceptVariousValues(string name, string description, float price)
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = name,
                Description = description,
                Price = price
            };

            // Assert
            Assert.Equal(name, menuItem.Name);
            Assert.Equal(description, menuItem.Description);
            Assert.Equal(price, menuItem.Price);
        }
    }
}

