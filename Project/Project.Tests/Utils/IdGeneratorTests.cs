using Project.Utils;


namespace Project.Tests.Utils
{
    public class IdGeneratorTests
    {
        [Fact]
        public void Generate_ReturnsNonEmptyString()
        {
            // Act
            var id = IdGenerator.Generate();

            // Assert
            Assert.False(string.IsNullOrEmpty(id));
        }

        [Fact]
        public void Generate_ReturnsUniqueIds()
        {
            // Act
            var id1 = IdGenerator.Generate();
            var id2 = IdGenerator.Generate();

            // Assert
            Assert.NotEqual(id1, id2);
        }

        [Fact]
        public void Generate_ReturnsStringWithCorrectFormat()
        {
            // Act
            var id = IdGenerator.Generate();

            // Assert
            Assert.Matches(@"^\d{21}$", id);
        }
    }
}