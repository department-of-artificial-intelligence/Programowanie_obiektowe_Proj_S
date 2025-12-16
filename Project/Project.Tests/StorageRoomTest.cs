using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    [Collection("Sequential")]
    public class StorageRoomTest
    {
        [Fact]
        public void Constructor_InitializesEmptyList()
        {
            StorageRoom storage = new StorageRoom();

            Assert.NotNull(storage.Ingredients);
            Assert.Empty(storage.Ingredients);
        }

        [Fact]
        public void AddIngredient_AddsToList()
        {
            StorageRoom storage = new StorageRoom();

            storage.AddIngredient("Flour");
            storage.AddIngredient("Tomato Sauce");

            Assert.Equal(2, storage.Ingredients.Count);
            Assert.Contains("Flour", storage.Ingredients);
        }

        [Fact]
        public void CheckStock_ReturnsTrueIfFound()
        {
            StorageRoom storage = new StorageRoom();
            storage.AddIngredient("Mozzarella");

            bool hasCheese = storage.CheckStock("Mozzarella");
            bool hasPepperoni = storage.CheckStock("Pepperoni");

            Assert.True(hasCheese);
            Assert.False(hasPepperoni);
        }

        [Fact]
        public void GetInfo_ReturnsCorrectCount()
        {
            StorageRoom storage = new StorageRoom();
            storage.AddIngredient("A");
            storage.AddIngredient("B");
            storage.AddIngredient("C");

            string info = storage.GetInfo();

            Assert.Equal("Storage contains 3 ingredient types.", info);
        }

        [Fact]
        public void DisplayStock_PrintsInfoToConsole()
        {
            StorageRoom storage = new StorageRoom();
            storage.AddIngredient("Yeast");

            var originalOut = Console.Out;
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    Console.SetOut(stringWriter);

                    storage.DisplayStock();

                    var output = stringWriter.ToString();
                    Assert.Contains("Storage contains 1 ingredient types", output);
                }
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}
