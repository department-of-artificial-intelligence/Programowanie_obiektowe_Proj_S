using Project.Models;

namespace Project.Tests.Models
{
    public class ActorTests
    {
        [Fact]
        public void Actor_Constructor_ValidData_CreatesActor()
        {
            // Arrange
            var birthDate = DateTime.Now.AddYears(-30);

            // Act
            var actor = new Actor("John", "Doe", "American", birthDate,
                "http://example.com/photo.jpg", "Test biography", 85.5);

            // Assert
            Assert.Equal("John", actor.FirstName);
            Assert.Equal("Doe", actor.LastName);
            Assert.Equal("John Doe", actor.FullName);
            Assert.Equal("American", actor.Nationality);
            Assert.Equal(birthDate, actor.BirthDate);
            Assert.Equal("Test biography", actor.Biography);
            Assert.Equal(85.5, actor.Popularity);
            Assert.True(actor.Age >= 29 && actor.Age <= 30);
        }

        [Fact]
        public void SetBiography_ValidBiography_UpdatesBiography()
        {
            // Arrange
            var actor = new Actor("John", "Doe", "American", DateTime.Now.AddYears(-30),
                "photo.jpg", "Old bio", 85.5);

            // Act
            actor.SetBiography("New biography");

            // Assert
            Assert.Equal("New biography", actor.Biography);
        }

        [Fact]
        public void SetPopularity_ValidValue_UpdatesPopularity()
        {
            // Arrange
            var actor = new Actor("John", "Doe", "American", DateTime.Now.AddYears(-30),
                "photo.jpg", "Bio", 50.0);

            // Act
            actor.SetPopularity(75.5);

            // Assert
            Assert.Equal(75.5, actor.Popularity);
        }

        [Theory]
        [InlineData(-1.0)]
        [InlineData(101.0)]
        public void SetPopularity_InvalidValue_ThrowsException(double invalidPopularity)
        {
            // Arrange
            var actor = new Actor("John", "Doe", "American", DateTime.Now.AddYears(-30),
                "photo.jpg", "Bio", 50.0);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => actor.SetPopularity(invalidPopularity));
        }

        [Fact]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            var actor = new Actor("John", "Doe", "American", new DateTime(1990, 1, 1),
                "photo.jpg", "Test biography", 85.5);

            // Act
            var result = actor.ToString();

            // Assert
            Assert.Contains("John Doe", result);
            Assert.Contains("American", result);
            Assert.Contains("Test biography", result);
            Assert.Contains("85,5", result);
        }
    }
}