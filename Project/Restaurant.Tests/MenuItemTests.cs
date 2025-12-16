using RestaurantManagement.Models;

namespace RestaurantManagement.Tests
{
    public class MenuItemTest
    {
        [Fact]
        public void Create_Works()
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
        public void Price_Works()
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
        public void Name_Works()
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
        public void Description_Works()
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
        public void Change_Works()
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

        [Fact]
        public void Values_Pierogi_Works()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Pierogi",
                Description = "Polskie pierogi",
                Price = 18.00f
            };

            // Assert
            Assert.Equal("Pierogi", menuItem.Name);
            Assert.Equal("Polskie pierogi", menuItem.Description);
            Assert.Equal(18.00f, menuItem.Price);
        }

        [Fact]
        public void Values_Schabowy_Works()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Schabowy",
                Description = "Kotlet schabowy z ziemniakami",
                Price = 35.00f
            };

            // Assert
            Assert.Equal("Schabowy", menuItem.Name);
            Assert.Equal("Kotlet schabowy z ziemniakami", menuItem.Description);
            Assert.Equal(35.00f, menuItem.Price);
        }

        [Fact]
        public void Values_Barszcz_Works()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Barszcz",
                Description = "Czerwony barszcz",
                Price = 10.00f
            };

            // Assert
            Assert.Equal("Barszcz", menuItem.Name);
            Assert.Equal("Czerwony barszcz", menuItem.Description);
            Assert.Equal(10.00f, menuItem.Price);
        }
    }
}
