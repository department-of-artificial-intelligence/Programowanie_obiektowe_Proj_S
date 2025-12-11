using Project.Models;

namespace Project.Tests.Models
{
    public class CinemaTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldCreateCinemaWithCorrectProperties()
        {
            // Given
            var name = "Multiplex Cinema";
            var address = "123 Main Street";
            var contactPhone = "+380441234567";
            var contactEmail = "info@cinema.com";
            var managerName = "John Doe";

            // When
            var cinema = new Cinema(name, address, contactPhone, contactEmail, managerName);

            // Then
            Assert.Equal(name, cinema.Name);
            Assert.Equal(address, cinema.Address);
            Assert.Equal(contactPhone, cinema.ContactPhone);
            Assert.Equal(contactEmail, cinema.ContactEmail);
            Assert.Equal(managerName, cinema.ManagerName);
            Assert.Empty(cinema.AvailableFilmIds);
            Assert.Equal(0.0, cinema.Rating);
            Assert.Equal(0u, cinema.TotalRatings);
            Assert.NotEmpty(cinema.Id);
        }

        [Fact]
        public void AddItem_WithNewFilmId_ShouldAddFilmId()
        {
            // Given
            var cinema = CreateTestCinema();
            var filmId = "film-123";

            // When
            var result = cinema.AddItem(filmId);

            // Then
            Assert.True(result);
            Assert.Single(cinema.AvailableFilmIds);
            Assert.Contains(filmId, cinema.AvailableFilmIds);
        }

        [Fact]
        public void AddItem_WithDuplicateFilmId_ShouldNotAddFilmId()
        {
            // Given
            var cinema = CreateTestCinema();
            var filmId = "film-123";
            cinema.AddItem(filmId);

            // When
            var result = cinema.AddItem(filmId);

            // Then
            Assert.False(result);
            Assert.Single(cinema.AvailableFilmIds);
        }

        [Fact]
        public void AddItem_WhenMaxFilmsReached_ShouldNotAddFilmId()
        {
            // Given
            var cinema = CreateTestCinema();
            for (int i = 1; i <= 10; i++)
            {
                cinema.AddItem($"film-{i}");
            }

            // When
            var result = cinema.AddItem("film-11");

            // Then
            Assert.False(result);
            Assert.Equal(10, cinema.AvailableFilmIds.Count);
        }

        [Fact]
        public void RemoveItem_WithExistingFilmId_ShouldRemoveFilmId()
        {
            // Given
            var cinema = CreateTestCinema();
            var filmId = "film-123";
            cinema.AddItem(filmId);

            // When
            var result = cinema.RemoveItem(filmId);

            // Then
            Assert.True(result);
            Assert.Empty(cinema.AvailableFilmIds);
        }

        [Fact]
        public void RemoveItem_WithNonExistingFilmId_ShouldReturnFalse()
        {
            // Given
            var cinema = CreateTestCinema();

            // When
            var result = cinema.RemoveItem("non-existing-film");

            // Then
            Assert.False(result);
        }

        [Fact]
        public void AddRating_WithValidRating_ShouldUpdateRatingAndTotalRatings()
        {
            // Given
            var cinema = CreateTestCinema();

            // When
            cinema.AddRating(5);
            cinema.AddRating(4);
            cinema.AddRating(3);

            // Then
            Assert.Equal(3u, cinema.TotalRatings);
            Assert.Equal(4.0, cinema.Rating);
        }

        [Fact]
        public void AddRating_WithInvalidRating_ShouldThrowArgumentException()
        {
            // Given
            var cinema = CreateTestCinema();

            // When & Then
            Assert.Throws<ArgumentException>(() => cinema.AddRating(0));
            Assert.Throws<ArgumentException>(() => cinema.AddRating(6));
        }

        [Fact]
        public void UpdateInfo_WithValidData_ShouldUpdateProperties()
        {
            // Given
            var cinema = CreateTestCinema();
            var newName = "Updated Cinema";
            var newAddress = "456 New Street";
            var newPhone = "+380987654321";
            var newEmail = "updated@cinema.com";
            var newManager = "Jane Smith";

            // When
            cinema.UpdateInfo(newName, newAddress, newPhone, newEmail, newManager);

            // Then
            Assert.Equal(newName, cinema.Name);
            Assert.Equal(newAddress, cinema.Address);
            Assert.Equal(newPhone, cinema.ContactPhone);
            Assert.Equal(newEmail, cinema.ContactEmail);
            Assert.Equal(newManager, cinema.ManagerName);
        }

        [Fact]
        public void Constructor_WithEmptyName_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new Cinema("", "Address", "Phone", "Email", "Manager"));
        }

        [Fact]
        public void Constructor_WithEmptyAddress_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new Cinema("Name", "", "Phone", "Email", "Manager"));
        }

        [Fact]
        public void GetItemsAsString_WithMultipleFilmIds_ShouldReturnCommaSeparatedString()
        {
            // Given
            var cinema = CreateTestCinema();
            cinema.AddItem("film-1");
            cinema.AddItem("film-2");
            cinema.AddItem("film-3");

            // When
            var result = cinema.GetItemsAsString();

            // Then
            Assert.Equal("film-1, film-2, film-3", result);
        }

        [Fact]
        public void Items_ShouldReturnAvailableFilmIdsAsReadOnlyList()
        {
            // Given
            var cinema = CreateTestCinema();
            cinema.AddItem("film-1");
            cinema.AddItem("film-2");

            // When
            var items = cinema.Items;

            // Then
            Assert.Equal(2, items.Count);
            Assert.Contains("film-1", items);
            Assert.Contains("film-2", items);
        }

        [Fact]
        public void MarkAsUpdated_ShouldUpdateUpdatedAtTimestamp()
        {
            // Given
            var cinema = CreateTestCinema();
            var initialUpdatedAt = cinema.UpdatedAt;

            System.Threading.Thread.Sleep(10);

            // When
            cinema.MarkAsUpdated();

            // Then
            Assert.True(cinema.UpdatedAt > initialUpdatedAt);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Given
            var cinema = CreateTestCinema();
            cinema.AddItem("film-123");
            cinema.AddRating(5);

            // When
            var result = cinema.ToString();

            // Then
            Assert.Contains("Cinema:", result);
            Assert.Contains(cinema.Name, result);
            Assert.Contains(cinema.Address, result);
            Assert.Contains(cinema.Rating.ToString(), result);
            Assert.Contains(cinema.Id, result);
        }

        private static Cinema CreateTestCinema()
        {
            return new Cinema("Test Cinema", "Test Address", "+380441234567", "test@cinema.com", "Test Manager");
        }
    }
}