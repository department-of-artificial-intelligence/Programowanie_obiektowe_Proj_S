using Xunit;
using Project.Model;

namespace Project.Tests
{
    public class TutorTests
    {
        [Fact]
        public void KonstruktorParametryczny_PowinienPrzypisacDaneIStawke()
        {
            // Arrange
            string imie = "Jan";
            string nazwisko = "Kowalski";
            string email = "jan@korepetycje.pl";
            decimal stawka = 100m;

            // Act
            var tutor = new Tutor(imie, nazwisko, email, stawka);

            // Assert
            Assert.Equal(imie, tutor.FirstName);
            Assert.Equal(stawka, tutor.HourlyRate);
            Assert.NotNull(tutor.Specialties);
            Assert.NotNull(tutor.Availability);
        }

        [Fact]
        public void ToStringTest_PowinienZawieracStawke()
        {
            // Arrange
            var tutor = new Tutor("Jan", "Kowalski", "j@k.pl", 50.00m);

            // Act
            string wynik = tutor.ToString();

            // Assert
            Assert.Contains("Jan Kowalski", wynik);
            // Sprawdzamy czy stawka się pojawia (formatowanie zależy od kultury systemowej, np. przecinek lub kropka)
            Assert.True(wynik.Contains("50,00") || wynik.Contains("50.00"));
        }
    }
}