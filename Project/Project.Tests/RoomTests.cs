using Xunit;
using Project.Model;
using System;

namespace Project.Tests
{
    public class RoomTests
    {
        [Fact]
        public void Zarezerwuj_DlaPoprawnychDat_UstawiaWartosci()
        {
            var room = new Room();
            var start = DateTime.Today;
            var end = DateTime.Today.AddDays(3);

            room.Zarezerwuj(start, end);

            Assert.Equal(start, room.Od);
            Assert.Equal(end, room.Do);
            Assert.False(room.Dostepny);
        }

        [Fact]
        public void Zarezerwuj_RzucaWyjatek_GdyDataKoncowaJestWczesniejsza()
        {
            var room = new Room();
            var start = DateTime.Today;
            var end = DateTime.Today.AddDays(-1);

            var ex = Assert.Throws<ArgumentException>(() => room.Zarezerwuj(start, end));
            Assert.Equal("Niepoprawny zakres dat.", ex.Message);
        }

        [Fact]
        public void Zarezerwuj_RzucaWyjatek_GdyDatySaTeSame()
        {
            var room = new Room();
            var now = DateTime.Today;

            Assert.Throws<ArgumentException>(() => room.Zarezerwuj(now, now));
        }
    }
}