using Project.Services.Common;
using Project.Interfaces;

namespace Project.Tests.Services.Common
{
    public class TestRatableEntity(string name, double rating, uint totalRatings) : IRatable
    {
        public double Rating { get; set; } = rating;
        public uint TotalRatings { get; set; } = totalRatings;

        public string Name { get; set; } = name;

        public void AddRating(uint rating) { }
    }

    public class RatableServiceTests
    {
        [Fact]
        public void SortByRating_WhenEntitiesInRandomOrder_ShouldSortByRatingDescending()
        {
            // Given
            var entity1 = new TestRatableEntity("Low", 3.2, 10);
            var entity2 = new TestRatableEntity("Medium", 4.1, 15);
            var entity3 = new TestRatableEntity("High", 4.8, 20);
            var entities = new List<TestRatableEntity> { entity1, entity2, entity3 };

            // When
            var result = RatableService.SortByRating(entities);

            // Then
            Assert.Equal(entity3, result[0]); 
            Assert.Equal(entity2, result[1]);
            Assert.Equal(entity1, result[2]);
        }

        [Fact]
        public void SortByPopularity_WhenEntitiesInRandomOrder_ShouldSortByTotalRatingsDescending()
        {
            // Given
            var entity1 = new TestRatableEntity("Few", 4.5, 5);
            var entity2 = new TestRatableEntity("Some", 4.0, 15);
            var entity3 = new TestRatableEntity("Many", 3.8, 50);
            var entities = new List<TestRatableEntity> { entity1, entity2, entity3 };

            // When
            var result = RatableService.SortByPopularity(entities);

            // Then
            Assert.Equal(entity3, result[0]);
            Assert.Equal(entity2, result[1]);
            Assert.Equal(entity1, result[2]);
        }

        [Fact]
        public void SortByRating_WhenEqualRatings_ShouldMaintainOriginalOrder()
        {
            // Given
            var sameRating = 4.2;
            var entity1 = new TestRatableEntity("Entity1", sameRating, 10);
            var entity2 = new TestRatableEntity("Entity2", sameRating, 20);
            var entity3 = new TestRatableEntity("Entity3", sameRating, 30);
            var entities = new List<TestRatableEntity> { entity1, entity2, entity3 };

            // When
            var result = RatableService.SortByRating(entities);

            // Then
            Assert.Equal(entity1, result[0]);
            Assert.Equal(entity2, result[1]);
            Assert.Equal(entity3, result[2]);
        }

        [Fact]
        public void SortByPopularity_WhenEqualTotalRatings_ShouldMaintainOriginalOrder()
        {
            // Given
            var sameTotal = 25u;
            var entity1 = new TestRatableEntity("Entity1", 3.0, sameTotal);
            var entity2 = new TestRatableEntity("Entity2", 4.0, sameTotal);
            var entity3 = new TestRatableEntity("Entity3", 5.0, sameTotal);
            var entities = new List<TestRatableEntity> { entity1, entity2, entity3 };

            // When
            var result = RatableService.SortByPopularity(entities);

            // Then
            Assert.Equal(entity1, result[0]);
            Assert.Equal(entity2, result[1]);
            Assert.Equal(entity3, result[2]);
        }

        [Fact]
        public void SortByRating_WhenEmptyList_ShouldReturnEmptyList()
        {
            // Given
            var entities = new List<TestRatableEntity>();

            // When
            var result = RatableService.SortByRating(entities);

            // Then
            Assert.Empty(result);
        }

        [Fact]
        public void SortByPopularity_WhenEmptyList_ShouldReturnEmptyList()
        {
            // Given
            var entities = new List<TestRatableEntity>();

            // When
            var result = RatableService.SortByPopularity(entities);

            // Then
            Assert.Empty(result);
        }

        [Fact]
        public void SortByRating_WhenSingleEntity_ShouldReturnSameList()
        {
            // Given
            var entity = new TestRatableEntity("Single", 4.5, 10);
            var entities = new List<TestRatableEntity> { entity };

            // When
            var result = RatableService.SortByRating(entities);

            // Then
            Assert.Single(result);
            Assert.Equal(entity, result[0]);
        }

        [Fact]
        public void SortByPopularity_WhenSingleEntity_ShouldReturnSameList()
        {
            // Given
            var entity = new TestRatableEntity("Single", 3.7, 5);
            var entities = new List<TestRatableEntity> { entity };

            // When
            var result = RatableService.SortByPopularity(entities);

            // Then
            Assert.Single(result);
            Assert.Equal(entity, result[0]);
        }

        [Fact]
        public void SortByRating_WhenMixedPrecisionRatings_ShouldSortCorrectly()
        {
            // Given
            var entity1 = new TestRatableEntity("A", 4.19, 100);
            var entity2 = new TestRatableEntity("B", 4.2, 50);
            var entity3 = new TestRatableEntity("C", 4.199, 75);
            var entities = new List<TestRatableEntity> { entity1, entity2, entity3 };

            // When
            var result = RatableService.SortByRating(entities);

            // Then
            Assert.Equal(entity2, result[0]);
            Assert.Equal(entity3, result[1]);
            Assert.Equal(entity1, result[2]);
        }

        [Fact]
        public void SortByPopularity_WhenZeroRatings_ShouldHandleCorrectly()
        {
            // Given
            var entity1 = new TestRatableEntity("Zero1", 0.0, 0);
            var entity2 = new TestRatableEntity("Zero2", 0.0, 0);
            var entity3 = new TestRatableEntity("HasRatings", 4.5, 10);
            var entities = new List<TestRatableEntity> { entity1, entity2, entity3 };

            // When
            var result = RatableService.SortByPopularity(entities);

            // Then
            Assert.Equal(entity3, result[0]);
            Assert.Equal(entity1, result[1]);
            Assert.Equal(entity2, result[2]);
        }
    }
}