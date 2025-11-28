using Project.Utils;

namespace Project.Tests.Utils
{
    public class CollectionHelperTests
    {
        [Fact]
        public void AddUniqueItem_ItemNotInCollection_AddsItem()
        {
            // Arrange
            var collection = new List<string> { "a", "b" };

            // Act
            var result = CollectionHelper.AddUniqueItem(collection, "c");

            // Assert
            Assert.True(result);
            Assert.Contains("c", collection);
        }

        [Fact]
        public void AddUniqueItem_ItemInCollection_DoesNotAdd()
        {
            // Arrange
            var collection = new List<string> { "a", "b" };

            // Act
            var result = CollectionHelper.AddUniqueItem(collection, "a");

            // Assert
            Assert.False(result);
            Assert.Equal(2, collection.Count);
        }

        [Fact]
        public void AddUniqueItem_CollectionAtMaxItems_DoesNotAdd()
        {
            // Arrange
            var collection = new List<string> { "a", "b", "c" };
            int maxItems = 3;

            // Act
            var result = CollectionHelper.AddUniqueItem(collection, "d", maxItems);

            // Assert
            Assert.False(result);
            Assert.DoesNotContain("d", collection);
        }

        [Fact]
        public void AddUniqueItem_CollectionBelowMaxItems_AddsItem()
        {
            // Arrange
            var collection = new List<string> { "a", "b" };
            int maxItems = 3;

            // Act
            var result = CollectionHelper.AddUniqueItem(collection, "c", maxItems);

            // Assert
            Assert.True(result);
            Assert.Contains("c", collection);
        }

        [Fact]
        public void RemoveItem_ItemExists_RemovesItem()
        {
            // Arrange
            var collection = new List<string> { "a", "b", "c" };

            // Act
            var result = CollectionHelper.RemoveItem(collection, "b");

            // Assert
            Assert.True(result);
            Assert.DoesNotContain("b", collection);
            Assert.Equal(2, collection.Count);
        }

        [Fact]
        public void RemoveItem_ItemNotExists_ReturnsFalse()
        {
            // Arrange
            var collection = new List<string> { "a", "b" };

            // Act
            var result = CollectionHelper.RemoveItem(collection, "c");

            // Assert
            Assert.False(result);
            Assert.Equal(2, collection.Count);
        }

        [Fact]
        public void ToString_ReturnsJoinedString()
        {
            // Arrange
            var collection = new List<int> { 1, 2, 3 };

            // Act
            var result = CollectionHelper.ToString(collection, "-");

            // Assert
            Assert.Equal("1-2-3", result);
        }

        [Fact]
        public void ToString_EmptyCollection_ReturnsEmptyString()
        {
            // Arrange
            var collection = new List<string>();

            // Act
            var result = CollectionHelper.ToString(collection);

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void ToString_DefaultSeparator_UsesComma()
        {
            // Arrange
            var collection = new List<string> { "a", "b" };

            // Act
            var result = CollectionHelper.ToString(collection);

            // Assert
            Assert.Equal("a, b", result);
        }
    }
}