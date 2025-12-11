using Project.Services.Common;

namespace Project.Tests.Services.Common
{
    public class TestEntity : Project.Models.Common.Base
    {
        public string Name { get; set; }

        public TestEntity(string name, DateTime? createdAt = null)
        {
            Name = name;

            if (createdAt.HasValue)
            {
                var field = typeof(Project.Models.Common.Base).GetField("_createdAt",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                field?.SetValue(this, createdAt.Value);
            }
        }
    }

    public class BaseServiceTests
    {
        [Fact]
        public void SortByTimeNewest_WhenEntitiesInRandomOrder_ShouldSortByCreatedAtDescending()
        {
            // Given
            var entity1 = new TestEntity("Entity1", DateTime.Now.AddDays(-2));
            var entity2 = new TestEntity("Entity2", DateTime.Now.AddDays(-1));
            var entity3 = new TestEntity("Entity3", DateTime.Now);
            var entities = new List<TestEntity> { entity1, entity2, entity3 };

            // When
            var result = BaseService.SortByTimeNewest(entities);

            // Then
            Assert.Equal(entity3, result[0]);
            Assert.Equal(entity2, result[1]);
            Assert.Equal(entity1, result[2]);
        }

        [Fact]
        public void SortByTimeOldest_WhenEntitiesInRandomOrder_ShouldSortByCreatedAtAscending()
        {
            // Given
            var entity1 = new TestEntity("Entity1", DateTime.Now.AddDays(-2));
            var entity2 = new TestEntity("Entity2", DateTime.Now.AddDays(-1));
            var entity3 = new TestEntity("Entity3", DateTime.Now);
            var entities = new List<TestEntity> { entity3, entity1, entity2 };

            // When
            var result = BaseService.SortByTimeOldest(entities);

            // Then
            Assert.Equal(entity1, result[0]);
            Assert.Equal(entity2, result[1]);
            Assert.Equal(entity3, result[2]);
        }

        [Fact]
        public void SortByTimeNewest_WhenEmptyList_ShouldReturnEmptyList()
        {
            // Given
            var entities = new List<TestEntity>();

            // When
            var result = BaseService.SortByTimeNewest(entities);

            // Then
            Assert.Empty(result);
        }

        [Fact]
        public void SortByTimeOldest_WhenEmptyList_ShouldReturnEmptyList()
        {
            // Given
            var entities = new List<TestEntity>();

            // When
            var result = BaseService.SortByTimeOldest(entities);

            // Then
            Assert.Empty(result);
        }

        [Fact]
        public void SortByTimeNewest_WhenSingleEntity_ShouldReturnSameList()
        {
            // Given
            var entity = new TestEntity("Single", DateTime.Now.AddDays(-5));
            var entities = new List<TestEntity> { entity };

            // When
            var result = BaseService.SortByTimeNewest(entities);

            // Then
            Assert.Single(result);
            Assert.Equal(entity, result[0]);
        }
    }
}