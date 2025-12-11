using Project.Utils;
using System.Globalization;

namespace Project.Tests.Utils
{
    public class IdHandlerTests
    {
        [Fact]
        public void Generate_ShouldReturnNonEmptyString()
        {
            // Given
            // When
            var result = IdGenerator.Generate();

            // Then
            Assert.False(string.IsNullOrEmpty(result));
            Assert.True(result.Length > 0);
        }

        [Fact]
        public void Generate_ShouldReturnStringWithCorrectLength()
        {
            // Given
            const int expectedLength = 21;

            // When
            var result = IdGenerator.Generate();

            // Then
            Assert.Equal(expectedLength, result.Length);
        }

        [Fact]
        public void Generate_ShouldReturnStringContainingOnlyDigits()
        {
            // Given
            // When
            var result = IdGenerator.Generate();

            // Then
            Assert.Matches(@"^\d+$", result);
        }

        [Fact]
        public void Generate_WhenCalledMultipleTimes_ShouldReturnDifferentIds()
        {
            // Given
            // When
            var id1 = IdGenerator.Generate();
            var id2 = IdGenerator.Generate();
            var id3 = IdGenerator.Generate();

            // Then
            Assert.NotEqual(id1, id2);
            Assert.NotEqual(id2, id3);
            Assert.NotEqual(id1, id3);
        }

        [Fact]
        public void Generate_ShouldReturnIdWithCurrentYear()
        {
            // Given
            var currentYear = DateTime.Now.Year.ToString();

            // When
            var result = IdGenerator.Generate();

            // Then
            Assert.StartsWith(currentYear, result);
        }

        [Fact]
        public void Generate_ShouldReturnIdWithValidDateFormat()
        {
            // Given
            var expectedFormat = "yyyyMMddHHmmssFFFFFFF";
            var expectedDatePartLength = 14;

            // When
            var result = IdGenerator.Generate();

            // Then
            Assert.Equal(expectedFormat.Length, result.Length);

            var datePart = result.Substring(0, expectedDatePartLength);

            Assert.True(DateTime.TryParseExact(datePart, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
        }


        [Fact]
        public void Generate_ShouldHaveUniqueMillisecondPart()
        {
            // Given
            // When
            var ids = new HashSet<string>();
            for (int i = 0; i < 100; i++)
            {
                ids.Add(IdGenerator.Generate());
                Thread.Sleep(1);
            }

            // Then
            Assert.Equal(100, ids.Count);
        }
    }
}