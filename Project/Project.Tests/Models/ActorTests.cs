using Project.Models;

namespace Project.Tests.Models
{
    public class ActorTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldCreateActorWithCorrectProperties()
        {
            // Given
            var firstName = "John";
            var lastName = "Doe";
            var nationality = "American";
            var birthDate = new DateTime(1980, 1, 1);
            var profileImageUrl = "http://example.com/photo.jpg";
            var biography = "A great actor";
            var popularity = 85.5;

            // When
            var actor = new Actor(firstName, lastName, nationality, birthDate, profileImageUrl, biography, popularity);

            // Then
            Assert.Equal(firstName, actor.FirstName);
            Assert.Equal(lastName, actor.LastName);
            Assert.Equal(nationality, actor.Nationality);
            Assert.Equal(birthDate, actor.BirthDate);
            Assert.Equal(profileImageUrl, actor.ProfileImageUrl);
            Assert.Equal(biography, actor.Biography);
            Assert.Equal(popularity, actor.Popularity);
            Assert.NotEmpty(actor.Id);
            Assert.True(actor.Age > 0);
        }

        [Fact]
        public void FullName_ShouldReturnFirstNameAndLastName()
        {
            // Given
            var actor = CreateTestActor();

            // When
            var fullName = actor.FullName;

            // Then
            Assert.Equal("John Doe", fullName);
        }

        [Fact]
        public void Age_ShouldCalculateCorrectAge()
        {
            // Given
            var birthDate = new DateTime(2000, 6, 15);
            var actor = new Actor("Test", "Actor", "Test", birthDate, "url", "bio", 50);

            // When
            var age = actor.Age;

            // Then
            var expectedAge = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear) expectedAge--;

            Assert.Equal(expectedAge, age);
        }

        [Fact]
        public void SetBiography_WithValidBiography_ShouldUpdateBiography()
        {
            // Given
            var actor = CreateTestActor();
            var newBiography = "New biography with updated information.";

            // When
            actor.SetBiography(newBiography);

            // Then
            Assert.Equal(newBiography, actor.Biography);
        }

        [Fact]
        public void SetBiography_WithNull_ShouldThrowArgumentNullException()
        {
            // Given
            var actor = CreateTestActor();

            // When & Then
            Assert.Throws<ArgumentNullException>(() => actor.SetBiography(null));
        }

        [Fact]
        public void SetPopularity_WithValidValue_ShouldUpdatePopularity()
        {
            // Given
            var actor = CreateTestActor();
            var newPopularity = 95.0;

            // When
            actor.SetPopularity(newPopularity);

            // Then
            Assert.Equal(newPopularity, actor.Popularity);
        }

        [Fact]
        public void SetPopularity_WithValueLessThanZero_ShouldThrowArgumentException()
        {
            // Given
            var actor = CreateTestActor();

            // When & Then
            Assert.Throws<ArgumentException>(() => actor.SetPopularity(-5.0));
        }

        [Fact]
        public void SetPopularity_WithValueGreaterThan100_ShouldThrowArgumentException()
        {
            // Given
            var actor = CreateTestActor();

            // When & Then
            Assert.Throws<ArgumentException>(() => actor.SetPopularity(150.0));
        }

        [Fact]
        public void UpdatePersonalInfo_WithValidData_ShouldUpdateAllProperties()
        {
            // Given
            var actor = CreateTestActor();
            var newFirstName = "Jane";
            var newLastName = "Smith";
            var newNationality = "Canadian";
            var newBirthDate = new DateTime(1990, 5, 20);
            var newProfileImageUrl = "http://example.com/new.jpg";

            // When
            actor.UpdatePersonalInfo(newFirstName, newLastName, newNationality, newBirthDate, newProfileImageUrl);

            // Then
            Assert.Equal(newFirstName, actor.FirstName);
            Assert.Equal(newLastName, actor.LastName);
            Assert.Equal(newNationality, actor.Nationality);
            Assert.Equal(newBirthDate, actor.BirthDate);
            Assert.Equal(newProfileImageUrl, actor.ProfileImageUrl);
        }

        [Fact]
        public void Constructor_WithEmptyFirstName_ShouldThrowArgumentException()
        {
            // Given
            // When & Then
            Assert.Throws<ArgumentException>(() => new Actor("", "Doe", "American", new DateTime(1980, 1, 1), "url", "bio", 50));
        }

        [Fact]
        public void Constructor_WithFutureBirthDate_ShouldThrowArgumentException()
        {
            // Given
            var futureDate = DateTime.Now.AddYears(1);

            // When & Then
            Assert.Throws<ArgumentException>(() => new Actor("John", "Doe", "American", futureDate, "url", "bio", 50));
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Given
            var actor = CreateTestActor();

            // When
            var result = actor.ToString();

            // Then
            Assert.Contains("Actor:", result);
            Assert.Contains(actor.FullName, result);
            Assert.Contains(actor.Nationality, result);
            Assert.Contains(actor.Id, result);
        }

        [Fact]
        public void MarkAsUpdated_ShouldUpdateUpdatedAt()
        {
            // Given
            var actor = CreateTestActor();
            var initialUpdatedAt = actor.UpdatedAt;

            System.Threading.Thread.Sleep(10);

            // When
            actor.MarkAsUpdated();

            // Then
            Assert.True(actor.UpdatedAt > initialUpdatedAt);
        }

        [Fact]
        public void Constructor_WithPopularityAtBoundaryValues_ShouldAccept()
        {
            // Given & When & Then
            var actor1 = new Actor("John", "Doe", "American", new DateTime(1980, 1, 1), "url", "bio", 0.0);
            var actor2 = new Actor("Jane", "Doe", "American", new DateTime(1980, 1, 1), "url", "bio", 100.0);

            Assert.Equal(0.0, actor1.Popularity);
            Assert.Equal(100.0, actor2.Popularity);
        }

        private static Actor CreateTestActor()
        {
            return new Actor("John", "Doe", "American", new DateTime(1980, 1, 1), "http://example.com/photo.jpg", "A great actor", 85.5);
        }
    }
}