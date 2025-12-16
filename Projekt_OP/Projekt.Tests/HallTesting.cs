using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt.Model;
using Xunit;
using Moq;

namespace Projekt.Tests
{

    public class HallTesting
    {
        [Fact]
        public void Kontruktor_DomsylnyHall()
        {
            var hall = new Hall();
            Assert.NotNull(hall);
            Assert.Equal(0, hall.ID);
            Assert.Equal(0, hall.Seats);
            Assert.NotNull(hall.Films);
            Assert.Empty(hall.Films);
            Assert.Null(hall.Cinema);
            Assert.Equal(0, hall.CinemaID);
        }

        [Fact]
        public void Kontruktor_ParametrycznyHall()
        {
            int expSeats = 67;
            var mockCinema = new Mock<Cinema>();
            var hall = new Hall(expSeats, mockCinema.Object);
            Assert.NotNull(hall);
            Assert.Equal(expSeats, hall.Seats);
            Assert.Equal(mockCinema.Object, hall.Cinema);
        }
    }
}
