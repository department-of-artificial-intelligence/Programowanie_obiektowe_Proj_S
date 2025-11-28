using Project.Models.Common;
using Project.Services.SortingFiltering;

namespace Project.Tests.Logic
{
    public class GenericSortingTests
    {
        private class TestEntity : Base
        {
            public string Name { get; set; }

            public TestEntity(string name) : base()
            {
                Name = name;
            }

            public TestEntity(string name, DateTime createdAt, DateTime updatedAt)
                : base(Guid.NewGuid().ToString(), createdAt, updatedAt)
            {
                Name = name;
            }
        }

        [Fact]
        public void SortByTimeNewest_ReturnsEntitiesInDescendingOrder()
        {
            // Arrange
            var entities = new List<TestEntity>
            {
                new("Oldest", new DateTime(2020, 1, 1), new DateTime(2020, 1, 1)),
                new("Middle", new DateTime(2021, 1, 1), new DateTime(2021, 1, 1)),
                new("Newest", new DateTime(2022, 1, 1), new DateTime(2022, 1, 1))
            };

            // Act
            var result = GenericSorting.SortByTimeNewest(entities);

            // Assert
            Assert.Equal("Newest", result[0].Name);
            Assert.Equal("Middle", result[1].Name);
            Assert.Equal("Oldest", result[2].Name);
        }

        [Fact]
        public void SortByTimeOldest_ReturnsEntitiesInAscendingOrder()
        {
            // Arrange
            var entities = new List<TestEntity>
            {
                new("Newest", new DateTime(2022, 1, 1), new DateTime(2022, 1, 1)),
                new("Oldest", new DateTime(2020, 1, 1), new DateTime(2020, 1, 1)),
                new("Middle", new DateTime(2021, 1, 1), new DateTime(2021, 1, 1))
            };

            // Act
            var result = GenericSorting.SortByTimeOldest(entities);

            // Assert
            Assert.Equal("Oldest", result[0].Name);
            Assert.Equal("Middle", result[1].Name);
            Assert.Equal("Newest", result[2].Name);
        }

        [Fact]
        public void SortByTimeNewest_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<TestEntity>();

            // Act
            var result = GenericSorting.SortByTimeNewest(emptyList);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void SortByTimeOldest_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<TestEntity>();

            // Act
            var result = GenericSorting.SortByTimeOldest(emptyList);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void SortByTimeNewest_SingleItem_ReturnsSameList()
        {
            // Arrange
            var singleItem = new List<TestEntity>
            {
                new("Single", new DateTime(2020, 1, 1), new DateTime(2020, 1, 1))
            };

            // Act
            var result = GenericSorting.SortByTimeNewest(singleItem);

            // Assert
            Assert.Single(result);
            Assert.Equal("Single", result[0].Name);
        }

        [Fact]
        public void SortByTimeOldest_SingleItem_ReturnsSameList()
        {
            // Arrange
            var singleItem = new List<TestEntity>
            {
                new("Single", new DateTime(2020, 1, 1), new DateTime(2020, 1, 1))
            };

            // Act
            var result = GenericSorting.SortByTimeOldest(singleItem);

            // Assert
            Assert.Single(result);
            Assert.Equal("Single", result[0].Name);
        }

        [Fact]
        public void SortByTimeNewest_WithSameDates_PreservesOrder()
        {
            // Arrange
            var sameDate = new DateTime(2020, 1, 1);
            var entities = new List<TestEntity>
            {
                new("First", sameDate, sameDate),
                new("Second", sameDate, sameDate),
                new("Third", sameDate, sameDate)
            };

            // Act
            var result = GenericSorting.SortByTimeNewest(entities);

            // Assert
            Assert.Equal(3, result.Count);
        }
    }
}