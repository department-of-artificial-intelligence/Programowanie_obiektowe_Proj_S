using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    public class MenuItemTest
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            int id = 10;
            string name = "Pepsi";
            decimal price = 5.50m;
            string description = "Cold drink";

            MenuItem item = new MenuItem(id, name, price, description);

            Assert.Equal(id, item.Id);
            Assert.Equal(name, item.Name);
            Assert.Equal(price, item.Price);
            Assert.Equal(description, item.Description);
        }

        [Fact]
        public void DefaultConstructor_SetsDefaultValues()
        {
            MenuItem item = new MenuItem();

            Assert.Equal(0, item.Id);
            Assert.Equal(string.Empty, item.Name);
            Assert.Equal(0m, item.Price);
            Assert.Equal(string.Empty, item.Description);
        }

        [Fact]
        public void SetPrice_UpdatesThePriceProperty()
        {
            MenuItem item = new MenuItem(1, "Pizza", 20m, "Tasty");

            item.SetPrice(25.99m);

            Assert.Equal(25.99m, item.Price);
        }

        [Fact]
        public void GetInfo_ReturnsFormattedString()
        {
            MenuItem item = new MenuItem(5, "Pasta", 18m, "Italian style");

            string info = item.GetInfo();

            Assert.Contains("---- Menu Item ----", info);
            Assert.Contains("Name: Pasta", info);
            Assert.Contains("Price: 18 zł", info);
            Assert.Contains("Description: Italian style", info);
        }
    }
}
