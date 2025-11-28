using Project.Interfaces;
using Project.Logic.SortingFiltering;
using Project.Models;

namespace Project.Tests.Logic
{
    public class RatableSortingTests
    {
        private class TestRatable(string name, double rating, uint totalRatings) : IRatable
        {
            public double Rating { get; set; } = rating;
            public uint TotalRatings { get; set; } = totalRatings;

            public string Name { get; set; } = name;

            public void AddRating(uint rating)
            {
                Rating = (Rating * TotalRatings + rating) / (TotalRatings + 1);
                TotalRatings++;
            }
        }

        [Fact]
        public void SortByRating_ReturnsEntitiesInDescendingOrder()
        {
            // Arrange
            var entities = new List<TestRatable>
            {
                new("High", 4.8, 10),
                new("Low", 3.2, 5),
                new("Medium", 4.1, 8)
            };

            // Act
            var result = RatableSorting.SortByRating(entities);

            // Assert
            Assert.Equal(4.8, result[0].Rating);
            Assert.Equal(4.1, result[1].Rating);
            Assert.Equal(3.2, result[2].Rating);
        }

        [Fact]
        public void SortByPopularity_ReturnsEntitiesInDescendingOrderByTotalRatings()
        {
            // Arrange
            var entities = new List<TestRatable>
            {
                new("Popular", 4.0, 100),
                new("Unpopular", 5.0, 2),
                new("Medium", 4.5, 50)
            };

            // Act
            var result = RatableSorting.SortByPopularity(entities);

            // Assert
            Assert.Equal(100u, result[0].TotalRatings);
            Assert.Equal(50u, result[1].TotalRatings);
            Assert.Equal(2u, result[2].TotalRatings);
        }

        [Fact]
        public void SortByRating_WithCinemas_ReturnsCorrectOrder()
        {
            // Arrange
            var cinemas = new List<Cinema>
            {
                new("Cinema A", "Address 1", "+380441111111", "a@test.com", "Manager A"),
                new("Cinema B", "Address 2", "+380442222222", "b@test.com", "Manager B"),
                new("Cinema C", "Address 3", "+380443333333", "c@test.com", "Manager C")
            };

            cinemas[0].AddRating(5); 
            cinemas[0].AddRating(5);

            cinemas[1].AddRating(3);

            cinemas[2].AddRating(4); 
            cinemas[2].AddRating(5);

            // Act
            var result = RatableSorting.SortByRating(cinemas);

            // Assert
            Assert.Equal(5.0, result[0].Rating); 
            Assert.Equal(4.5, result[1].Rating); 
            Assert.Equal(3.0, result[2].Rating);
        }

        [Fact]
        public void SortByPopularity_WithFilms_ReturnsCorrectOrder()
        {
            // Arrange
            var films = new List<Film>
            {
                new("Film A", "Description A", 120, "Director A", "Action", false,
                    "posterA.jpg", "trailerA.mov"),
                new("Film B", "Description B", 90, "Director B", "Comedy", false,
                    "posterB.jpg", "trailerB.mov"),
                new("Film C", "Description C", 150, "Director C", "Drama", false,
                    "posterC.jpg", "trailerC.mov")
            };

            films[0].AddRating(4); 
            films[0].AddRating(5);

            films[1].AddRating(3);

            films[2].AddRating(5);
            films[2].AddRating(5); 
            films[2].AddRating(4);

            // Act
            var result = RatableSorting.SortByPopularity(films);

            // Assert
            Assert.Equal(3u, result[0].TotalRatings); 
            Assert.Equal(2u, result[1].TotalRatings); 
            Assert.Equal(1u, result[2].TotalRatings);
        }

        [Fact]
        public void SortByRating_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<TestRatable>();

            // Act
            var result = RatableSorting.SortByRating(emptyList);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void SortByPopularity_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<TestRatable>();

            // Act
            var result = RatableSorting.SortByPopularity(emptyList);

            // Assert
            Assert.Empty(result);
        }
    }
}