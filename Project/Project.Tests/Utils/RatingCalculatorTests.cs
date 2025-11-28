using Project.Utils;

namespace Project.Tests.Utils
{
    public class RatingCalculatorTests
    {
        [Theory]
        [InlineData(0, 0.0, 5, 5.0)] 
        [InlineData(1, 5.0, 1, 3.0)] 
        [InlineData(4, 4.0, 5, 4.2)] 
        [InlineData(10, 3.5, 2, 3.3636363636363638)] 
        [InlineData(0, 0.0, 0, 0.0)] 
        [InlineData(0, 0.0, 6, 6.0)]

        public void CalculateNewRating_VariousInputs_ReturnsCorrectRating(
            uint currentTotalRatings, double currentRating, uint newRating, double expected)
        {
            // Act
            var result = RatingCalculator.CalculateNewRating(currentTotalRatings, currentRating, newRating);

            // Assert
            Assert.Equal(expected, result, 10);
        }

        [Fact]
        public void CalculateNewRating_EdgeCase_LargeNumbers()
        {
            // Arrange
            uint currentTotalRatings = 1000000;
            double currentRating = 4.5;
            uint newRating = 5;

            // Act
            var result = RatingCalculator.CalculateNewRating(currentTotalRatings, currentRating, newRating);

            // Assert
            Assert.True(result > 4.5 && result < 4.500001);
        }
    }
}