using Project.Utils;

namespace Project.Tests.Utils
{
    public class CollectionHandlerTests
    {
        [Fact]
        public void AddUniqueItem_WhenCollectionIsEmptyAndItemNotExists_ShouldAddItem()
        {
            // Given
            var collection = new List<string>();
            var item = "test";

            // When
            var result = CollectionHelper.AddUniqueItem(collection, item);

            // Then
            Assert.True(result);
            Assert.Single(collection);
            Assert.Equal(item, collection[0]);
        }

        [Fact]
        public void AddUniqueItem_WhenItemAlreadyExists_ShouldNotAddItem()
        {
            // Given
            var collection = new List<string> { "existing" };
            var item = "existing";

            // When
            var result = CollectionHelper.AddUniqueItem(collection, item);

            // Then
            Assert.False(result);
            Assert.Single(collection);
        }

        [Fact]
        public void AddUniqueItem_WhenMaxItemsReached_ShouldNotAddItem()
        {
            // Given
            var collection = new List<int> { 1, 2, 3 };
            var maxItems = 3;
            var item = 4;

            // When
            var result = CollectionHelper.AddUniqueItem(collection, item, maxItems);

            // Then
            Assert.False(result);
            Assert.Equal(3, collection.Count);
        }

        [Fact]
        public void RemoveItem_WhenItemExists_ShouldRemoveItemAndReturnTrue()
        {
            // Given
            var collection = new List<string> { "a", "b", "c" };
            var itemToRemove = "b";

            // When
            var result = CollectionHelper.RemoveItem(collection, itemToRemove);

            // Then
            Assert.True(result);
            Assert.Equal(2, collection.Count);
            Assert.DoesNotContain(itemToRemove, collection);
        }

        [Fact]
        public void RemoveItem_WhenItemNotExists_ShouldReturnFalse()
        {
            // Given
            var collection = new List<string> { "a", "b", "c" };
            var itemToRemove = "d";

            // When
            var result = CollectionHelper.RemoveItem(collection, itemToRemove);

            // Then
            Assert.False(result);
            Assert.Equal(3, collection.Count);
        }

        [Fact]
        public void ToString_WhenCollectionHasItems_ShouldReturnJoinedString()
        {
            // Given
            var collection = new List<int> { 1, 2, 3 };
            var separator = ", ";

            // When
            var result = CollectionHelper.ToString(collection, separator);

            // Then
            Assert.Equal("1, 2, 3", result);
        }

        [Fact]
        public void ToString_WhenCollectionIsEmpty_ShouldReturnEmptyString()
        {
            // Given
            var collection = new List<string>();

            // When
            var result = CollectionHelper.ToString(collection);

            // Then
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ToString_WhenCustomSeparatorProvided_ShouldUseCustomSeparator()
        {
            // Given
            var collection = new List<string> { "a", "b", "c" };
            var separator = "|";

            // When
            var result = CollectionHelper.ToString(collection, separator);

            // Then
            Assert.Equal("a|b|c", result);
        }
    }
}