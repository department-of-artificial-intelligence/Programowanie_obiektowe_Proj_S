using Project.Models;

namespace Project.Tests.Models
{
    public class AuditoriumTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldCreateAuditoriumWithCorrectProperties()
        {
            // Given
            var cinemaId = "cinema-123";
            var name = "IMAX Hall";
            var roomNumber = 5u;
            var rows = 10u;
            var seatsPerRow = 15u;

            // When
            var auditorium = new Auditorium(cinemaId, name, roomNumber, rows, seatsPerRow);

            // Then
            Assert.Equal(cinemaId, auditorium.CinemaId);
            Assert.Equal(name, auditorium.Name);
            Assert.Equal(roomNumber, auditorium.RoomNumber);
            Assert.Equal(rows, auditorium.Rows);
            Assert.Equal(seatsPerRow, auditorium.SeatsPerRow);
            Assert.Equal(150u, auditorium.Capacity);
            Assert.Empty(auditorium.Features);
            Assert.Equal(0.0, auditorium.Rating);
            Assert.Equal(0u, auditorium.TotalRatings);
            Assert.NotEmpty(auditorium.Id);
        }

        [Fact]
        public void Capacity_ShouldCalculateRowsTimesSeatsPerRow()
        {
            // Given
            var auditorium = CreateTestAuditorium();

            // When
            var capacity = auditorium.Capacity;

            // Then
            Assert.Equal(200u, capacity);
        }

        [Fact]
        public void AddItem_WithNewFeature_ShouldAddFeature()
        {
            // Given
            var auditorium = CreateTestAuditorium();
            var feature = "Dolby Atmos";

            // When
            var result = auditorium.AddItem(feature);

            // Then
            Assert.True(result);
            Assert.Single(auditorium.Features);
            Assert.Contains(feature, auditorium.Features);
        }

        [Fact]
        public void AddItem_WithDuplicateFeature_ShouldNotAddFeature()
        {
            // Given
            var auditorium = CreateTestAuditorium();
            var feature = "3D Projection";
            auditorium.AddItem(feature);

            // When
            var result = auditorium.AddItem(feature);

            // Then
            Assert.False(result);
            Assert.Single(auditorium.Features);
        }

        [Fact]
        public void AddItem_WhenMaxFeaturesReached_ShouldNotAddFeature()
        {
            // Given
            var auditorium = CreateTestAuditorium();
            for (int i = 1; i <= 5; i++)
            {
                auditorium.AddItem($"Feature{i}");
            }

            // When
            var result = auditorium.AddItem("Feature6");

            // Then
            Assert.False(result);
            Assert.Equal(5, auditorium.Features.Count);
        }

        [Fact]
        public void RemoveItem_WithExistingFeature_ShouldRemoveFeature()
        {
            // Given
            var auditorium = CreateTestAuditorium();
            var feature = "Recliner Seats";
            auditorium.AddItem(feature);

            // When
            var result = auditorium.RemoveItem(feature);

            // Then
            Assert.True(result);
            Assert.Empty(auditorium.Features);
        }

        [Fact]
        public void RemoveItem_WithNonExistingFeature_ShouldReturnFalse()
        {
            // Given
            var auditorium = CreateTestAuditorium();

            // When
            var result = auditorium.RemoveItem("NonExistingFeature");

            // Then
            Assert.False(result);
        }

        [Fact]
        public void AddRating_WithValidRating_ShouldUpdateRatingAndTotalRatings()
        {
            // Given
            var auditorium = CreateTestAuditorium();

            // When
            auditorium.AddRating(5);
            auditorium.AddRating(4);
            auditorium.AddRating(3);

            // Then
            Assert.Equal(3u, auditorium.TotalRatings);
            Assert.Equal(4.0, auditorium.Rating);
        }

        [Fact]
        public void AddRating_WithRatingLessThan1_ShouldThrowArgumentException()
        {
            // Given
            var auditorium = CreateTestAuditorium();

            // When & Then
            Assert.Throws<ArgumentException>(() => auditorium.AddRating(0));
        }

        [Fact]
        public void AddRating_WithRatingGreaterThan5_ShouldThrowArgumentException()
        {
            // Given
            var auditorium = CreateTestAuditorium();

            // When & Then
            Assert.Throws<ArgumentException>(() => auditorium.AddRating(6));
        }

        [Fact]
        public void UpdateLayout_WithValidData_ShouldUpdateProperties()
        {
            // Given
            var auditorium = CreateTestAuditorium();
            var newName = "VIP Hall";
            var newRoomNumber = 10u;
            var newRows = 8u;
            var newSeatsPerRow = 12u;

            // When
            auditorium.UpdateLayout(newName, newRoomNumber, newRows, newSeatsPerRow);

            // Then
            Assert.Equal(newName, auditorium.Name);
            Assert.Equal(newRoomNumber, auditorium.RoomNumber);
            Assert.Equal(newRows, auditorium.Rows);
            Assert.Equal(newSeatsPerRow, auditorium.SeatsPerRow);
            Assert.Equal(96u, auditorium.Capacity);
        }

        [Fact]
        public void UpdateLayout_WithZeroRows_ShouldThrowArgumentException()
        {
            // Given
            var auditorium = CreateTestAuditorium();

            // When & Then
            Assert.Throws<ArgumentException>(() => auditorium.UpdateLayout("Test", 1, 0, 10));
        }

        [Fact]
        public void UpdateLayout_WithZeroSeatsPerRow_ShouldThrowArgumentException()
        {
            // Given
            var auditorium = CreateTestAuditorium();

            // When & Then
            Assert.Throws<ArgumentException>(() => auditorium.UpdateLayout("Test", 1, 10, 0));
        }

        [Fact]
        public void GetItemsAsString_WithMultipleFeatures_ShouldReturnCommaSeparatedString()
        {
            // Given
            var auditorium = CreateTestAuditorium();
            auditorium.AddItem("Feature1");
            auditorium.AddItem("Feature2");
            auditorium.AddItem("Feature3");

            // When
            var result = auditorium.GetItemsAsString();

            // Then
            Assert.Equal("Feature1, Feature2, Feature3", result);
        }

        [Fact]
        public void GetItemsAsString_WithNoFeatures_ShouldReturnEmptyString()
        {
            // Given
            var auditorium = CreateTestAuditorium();

            // When
            var result = auditorium.GetItemsAsString();

            // Then
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Constructor_WithEmptyCinemaId_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new Auditorium("", "Test", 1, 10, 15));
        }

        [Fact]
        public void Constructor_WithZeroRoomNumber_ShouldThrowArgumentException()
        {
            // Given & When & Then
            Assert.Throws<ArgumentException>(() => new Auditorium("cinema-1", "Test", 0, 10, 15));
        }

        [Fact]
        public void Items_ShouldReturnFeaturesAsReadOnlyList()
        {
            // Given
            var auditorium = CreateTestAuditorium();
            auditorium.AddItem("Feature1");
            auditorium.AddItem("Feature2");

            // When
            var items = auditorium.Items;

            // Then
            Assert.Equal(2, items.Count);
            Assert.Contains("Feature1", items);
            Assert.Contains("Feature2", items);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Given
            var auditorium = CreateTestAuditorium();
            auditorium.AddItem("3D");
            auditorium.AddRating(5);

            // When
            var result = auditorium.ToString();

            // Then
            Assert.Contains("Auditorium:", result);
            Assert.Contains(auditorium.Name, result);
            Assert.Contains(auditorium.Capacity.ToString(), result);
            Assert.Contains(auditorium.Id, result);
        }

        private static Auditorium CreateTestAuditorium()
        {
            return new Auditorium("cinema-123", "Main Hall", 1, 10, 20);
        }
    }
}