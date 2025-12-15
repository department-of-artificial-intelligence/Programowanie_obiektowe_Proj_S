using Xunit;
using Project.Model;

namespace Project.Tests
{
    public class StudentTests
    {
        [Fact]
        public void KonstruktorDomyslny_PowinienZainicjalizowacPusteWartosci()
        {
            var student = new Student();

            Assert.Equal(string.Empty, student.FirstName);
            Assert.Equal(string.Empty, student.LastName);

        }

        [Fact]
        public void KonstruktorParametryczny_PowinienPrzypisacDane()
        {
            string imie = "Anna";
            string nazwisko = "Nowak";
            string email = "anna.nowak@test.com";
            string poziom = "Liceum";

            var student = new Student(imie, nazwisko, email, poziom);

            Assert.Equal(imie, student.FirstName);
            Assert.Equal(nazwisko, student.LastName);
            Assert.Equal(email, student.Email);
            Assert.Equal(poziom, student.EducationalLevel);
        }

        [Fact]
        public void ToStringTest_PowinienZwrocicSformatowanyTekst()
        {
            var student = new Student("Anna", "Nowak", "a@b.com", "Studia");
            string oczekiwanyFragment = "Anna Nowak (a@b.com), Poziom: Studia";

            string wynik = student.ToString();

            Assert.Contains(oczekiwanyFragment, wynik);
        }
    }
}