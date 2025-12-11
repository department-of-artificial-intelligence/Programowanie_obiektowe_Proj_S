using Project.Utils;

namespace Project.Tests.Utils
{
    public class RatingHandlerTests
    {
        [Fact]
        public void CalculateNewRating_WhenFirstRating_ShouldReturnNewRating()
        {
            // Given
            uint currentTotalRatings = 0;
            double currentRating = 0;
            uint newRating = 5;

            // When
            var result = RatingCalculator.CalculateNewRating(currentTotalRatings, currentRating, newRating);

            // Then
            Assert.Equal(5.0, result);
        }

        [Fact]
        public void CalculateNewRating_WhenMultipleRatingsExist_ShouldCalculateCorrectAverage()
        {
            // Given
            uint currentTotalRatings = 3;
            double currentRating = 4.0;
            uint newRating = 5;

            // When
            var result = RatingCalculator.CalculateNewRating(currentTotalRatings, currentRating, newRating);

            // Then
            Assert.Equal(4.25, result);
        }

        [Fact]
        public void CalculateNewRating_WhenAddingLowRating_ShouldDecreaseAverage()
        {
            // Given
            uint currentTotalRatings = 2;
            double currentRating = 4.5;
            uint newRating = 1;

            // When
            var result = RatingCalculator.CalculateNewRating(currentTotalRatings, currentRating, newRating);

            // Then
            Assert.Equal(3.333, result, 3);
        }

        [Fact]
        public void CalculateNewRating_WhenAddingHighRating_ShouldIncreaseAverage()
        {
            // Given
            uint currentTotalRatings = 4;
            double currentRating = 3.0;
            uint newRating = 5;

            // When
            var result = RatingCalculator.CalculateNewRating(currentTotalRatings, currentRating, newRating);

            // Then
            Assert.Equal(3.4, result);
        }

        [Fact]
        public void CalculateNewRating_WhenCurrentRatingIsZero_ShouldHandleCorrectly()
        {
            // Given
            uint currentTotalRatings = 2;
            double currentRating = 0;
            uint newRating = 3;

            // When
            var result = RatingCalculator.CalculateNewRating(currentTotalRatings, currentRating, newRating);

            // Then
            Assert.Equal(1.0, result);
        }

        [Fact]
        public void CalculateNewRating_WhenLargeNumberOfRatings_ShouldCalculatePrecisely()
        {
            // Given
            uint currentTotalRatings = 100;
            double currentRating = 4.8;
            uint newRating = 5;

            // When
            var result = RatingCalculator.CalculateNewRating(currentTotalRatings, currentRating, newRating);

            // Then
            var expected = (4.8 * 100 + 5) / 101;
            Assert.Equal(expected, result, 10);
        }
    }
}