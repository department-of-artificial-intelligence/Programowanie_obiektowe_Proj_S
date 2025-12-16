using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    [Collection("Sequential")]
    public class MenuTest
    {
        [Fact]
        public void Constructor_InitializesEmptyList()
        {
            Menu menu = new Menu();

            Assert.NotNull(menu.AvailableItems);
            Assert.Empty(menu.AvailableItems);
        }

        [Fact]
        public void AddItem_ValidItem_AddsToAvailableItems()
        {
            Menu menu = new Menu();
            MenuItem item = new MenuItem(1, "Pizza", 25m, "Delicious");

            menu.AddItem(item);

            Assert.Single(menu.AvailableItems);
            Assert.Equal("Pizza", menu.AvailableItems[0].Name);
        }

        [Fact]
        public void AddItem_NullItem_DoesNotAdd()
        {
            Menu menu = new Menu();

            menu.AddItem(null);

            Assert.Empty(menu.AvailableItems);
        }

        [Fact]
        public void AddItem_DuplicateName_DoesNotAddAndPrintsWarning()
        {
            Menu menu = new Menu();
            menu.AddItem(new MenuItem(1, "Pizza", 20m, "Original"));

            MenuItem duplicate = new MenuItem(2, "Pizza", 30m, "Duplicate");

            var originalOut = Console.Out;
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    Console.SetOut(stringWriter);

                    menu.AddItem(duplicate);

                    var output = stringWriter.ToString();
                    
                    Assert.Single(menu.AvailableItems);

                    Assert.Equal(20m, menu.AvailableItems[0].Price);

                    Assert.Contains("already exists", output);
                }
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void RemoveItem_ExistingItem_ReturnsTrueAndRemoves()
        {
            Menu menu = new Menu();
            menu.AddItem(new MenuItem(1, "Soup", 10m, "Hot"));

            bool result = menu.RemoveItem("Soup");

            Assert.True(result);
            Assert.Empty(menu.AvailableItems);
        }

        [Fact]
        public void RemoveItem_NonExistingItem_ReturnsFalse()
        {
            Menu menu = new Menu();
            menu.AddItem(new MenuItem(1, "Soup", 10m, "Hot"));

            bool result = menu.RemoveItem("Burger");

            Assert.False(result);
            Assert.Single(menu.AvailableItems);
        }

        [Fact]
        public void FindItem_ReturnsCorrectItem()
        {
            Menu menu = new Menu();
            menu.AddItem(new MenuItem(1, "Burger", 15m, "Beef"));

            MenuItem? found = menu.FindItem("Burger");

            Assert.NotNull(found);
            Assert.Equal(15m, found.Price);
        }

        [Fact]
        public void FindItem_ReturnsNullIfNotFound()
        {
            Menu menu = new Menu();

            MenuItem? found = menu.FindItem("Ghost Pizza");

            Assert.Null(found);
        }

        [Fact]
        public void GetInfo_ReturnsFormattedString()
        {
            Menu menu = new Menu();
            menu.AddItem(new MenuItem(1, "Water", 5m, "Plain"));

            string info = menu.GetInfo();

            Assert.Contains("======= MENU =======", info);
            Assert.Contains("Water", info);
            Assert.Contains("5 zł", info);
        }
    }
}
