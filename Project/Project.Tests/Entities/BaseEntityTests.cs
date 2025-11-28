using Project.Entities;

namespace Project.Tests.Entities
{
    public class BaseEntityTests
    {
        private class TestEntity : BaseEntity
        {
            public TestEntity() : base() { }

            public TestEntity(string id, DateTime createdAt, DateTime updatedAt)
                : base(id, createdAt, updatedAt) { }
        }

        [Fact]
        public void BaseEntity_Constructor_SetsProperties()
        {
            // Act
            var entity = new TestEntity();

            // Assert
            Assert.NotNull(entity.Id);
            Assert.NotEmpty(entity.Id);
            Assert.True(entity.CreatedAt <= DateTime.Now);
            Assert.True(entity.UpdatedAt <= DateTime.Now);
            Assert.True(entity.CreatedAt >= DateTime.Now.AddSeconds(-1));
            Assert.True(entity.UpdatedAt >= DateTime.Now.AddSeconds(-1));
        }

        [Fact]
        public void BaseEntity_ConstructorWithParameters_SetsProperties()
        {
            // Arrange
            var id = "test-id";
            var createdAt = new DateTime(2020, 1, 1);
            var updatedAt = new DateTime(2020, 1, 2);

            // Act
            var entity = new TestEntity(id, createdAt, updatedAt);

            // Assert
            Assert.Equal(id, entity.Id);
            Assert.Equal(createdAt, entity.CreatedAt);
            Assert.Equal(updatedAt, entity.UpdatedAt);
        }

        [Fact]
        public void MarkAsUpdated_UpdatesUpdatedAt()
        {
            // Arrange
            var entity = new TestEntity();
            var originalUpdatedAt = entity.UpdatedAt;

            // Act
            System.Threading.Thread.Sleep(10);
            entity.MarkAsUpdated();

            // Assert
            Assert.True(entity.UpdatedAt > originalUpdatedAt);
        }

        [Fact]
        public void DifferentEntities_HaveDifferentIds()
        {
            // Act
            var entity1 = new TestEntity();
            var entity2 = new TestEntity();

            // Assert
            Assert.NotEqual(entity1.Id, entity2.Id);
        }

        [Fact]
        public void BaseEntity_MarkAsUpdated_MultipleTimes_UpdatesCorrectly()
        {
            // Arrange
            var entity = new TestEntity();
            var timestamps = new List<DateTime>();

            // Act
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1);
                entity.MarkAsUpdated();
                timestamps.Add(entity.UpdatedAt);
            }

            // Assert
            for (int i = 1; i < timestamps.Count; i++)
            {
                Assert.True(timestamps[i] > timestamps[i - 1]);
            }
        }
    }
}