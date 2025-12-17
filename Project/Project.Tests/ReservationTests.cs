using Xunit;
using Project.Model;
using System;

namespace Project.Tests
{
    public class ReservationTests
    {
        [Fact]
        public void Koszt_Jest_Poprawnie_Obliczany()
        {
            decimal cenaZaDobe = 200m;
            var room = new Room { CenaZaDobe = cenaZaDobe };

            var reservation = new Reservation
            {
                Pokoj = room,
                DataOd = new DateTime(2024, 6, 1),
                DataDo = new DateTime(2024, 6, 6)
            };

            var koszt = reservation.Koszt;

            Assert.Equal(1000m, koszt);
        }

        [Fact]
        public void Koszt_Jest_Zero_Gdy_Brak_Pokoju()
        {
            var reservation = new Reservation
            {
                Pokoj = null!,
                DataOd = DateTime.Today,
                DataDo = DateTime.Today.AddDays(1)
            };

            Assert.Equal(0, reservation.Koszt);
        }
    }
}