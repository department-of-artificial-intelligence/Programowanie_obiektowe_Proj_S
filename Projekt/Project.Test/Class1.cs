using Projekt.Model;
namespace Project.Test
{
    public class RentalTests
    {
        [Fact]
        public void TestBezDaty()
        {
            var rental = new Rental
            {
                Id = 1,
                CarId = 100,
                CustomerId = 50,
                StartDate = new DateTime(2023, 10, 1),
                ActualReturnDate = null
            };

            var result = rental.ToString();
            Assert.Contains("W TOKU", result);
            Assert.DoesNotContain("Zakończone", result);
        }

        [Fact]
        public void TestZData()
        {
            var rental = new Rental
            {
                Id = 2,
                CarId = 100,
                CustomerId = 50,
                StartDate = new DateTime(2023, 10, 1),
                ActualReturnDate = DateTime.Now 
            };

            var result = rental.ToString();

            Assert.Contains("Zakończone", result);
            Assert.DoesNotContain("W TOKU", result);
        }

        [Fact]
        public void TestPoprawnnosciDanych()
        {

            var date = new DateTime(2023, 5, 20);
            var rental = new Rental
            {
                Id = 123,
                CarId = 999,
                CustomerId = 888,
                StartDate = date,
                ActualReturnDate = null
            };

            var result = rental.ToString();

            Assert.Contains("#123", result);
            Assert.Contains("Pojazd: 999", result);
            Assert.Contains("Klient: 888", result);
            Assert.Contains(date.ToShortDateString(), result);
        }
    }
}