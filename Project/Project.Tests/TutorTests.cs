using Xunit;
using Project.Model;

namespace Project.Tests
{
    public class TutorTests
    {
        [Fact]
        public void KonstruktorParametryczny_PowinienPrzypisacDaneIStawke()
        {
            string imie = "Jan";
            string nazwisko = "Kowalski";
            string email = "jan@korepetycje.pl";
            decimal stawka = 100m;

            var tutor = new Tutor(imie, nazwisko, email, stawka);

            Assert.Equal(imie, tutor.FirstName);
            Assert.Equal(stawka, tutor.HourlyRate);
            Assert.NotNull(tutor.Specialties);
            Assert.NotNull(tutor.Availability);
        }

        [Fact]
        public void ToStringTest_PowinienZawieracStawke()
        {
            var tutor = new Tutor("Jan", "Kowalski", "j@k.pl", 50.00m);

            string wynik = tutor.ToString();

            Assert.Contains("Jan Kowalski", wynik);
            // czy stawka się pojawia
            Assert.True(wynik.Contains("50,00") || wynik.Contains("50.00"));
        }
    }
}