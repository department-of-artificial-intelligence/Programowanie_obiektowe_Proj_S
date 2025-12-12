using Xunit;
using Project.Model;

namespace Project.Tests
{
    public class StudentTests
    {
        [Fact]
        public void KonstruktorDomyslny_PowinienZainicjalizowacPusteWartosci()
        {
            // Arrange & Act
            var student = new Student();

            // Assert
            Assert.Equal(string.Empty, student.FirstName);
            Assert.Equal(string.Empty, student.LastName);
            Assert.NotNull(student.Interests); // Lista powinna byc zainicjalizowana
            Assert.Empty(student.Interests);
        }

        [Fact]
        public void KonstruktorParametryczny_PowinienPrzypisacDane()
        {
            // Arrange
            string imie = "Anna";
            string nazwisko = "Nowak";
            string email = "anna.nowak@test.com";
            string poziom = "Liceum";

            // Act
            var student = new Student(imie, nazwisko, email, poziom);

            // Assert
            Assert.Equal(imie, student.FirstName);
            Assert.Equal(nazwisko, student.LastName);
            Assert.Equal(email, student.Email);
            Assert.Equal(poziom, student.EducationalLevel);
        }

        [Fact]
        public void ToStringTest_PowinienZwrocicSformatowanyTekst()
        {
            // Arrange
            var student = new Student("Anna", "Nowak", "a@b.com", "Studia");
            string oczekiwanyFragment = "Anna Nowak (a@b.com), Poziom: Studia";

            // Act
            string wynik = student.ToString();

            // Assert
            Assert.Contains(oczekiwanyFragment, wynik);
        }
    }
}