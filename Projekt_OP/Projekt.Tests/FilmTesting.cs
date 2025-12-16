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
    public class FilmTesting
    {
        [Fact]
        public void Konstruktor_DomyslnyFilm()
        {
            var film = new Film();
            Assert.NotNull(film);
            Assert.Equal(0, film.ID);
            Assert.Null(film.Title);
            Assert.Equal(0, film.TimeMin);
            Assert.Null(film.Genre);
            Assert.Null(film.Hall);
            Assert.Equal(0, film.HallID);
        }

        [Fact]
        public void Kontruktor_ParametrycznyFilms()
        {
            string expTitle = "DuszekKacperek";
            int expTimeMin = 90;
            string expGenre = "Animacja";
            var mockHall = new Mock<Hall>();
            var film = new Film(expTitle, expTimeMin, expGenre, mockHall.Object);
            Assert.NotNull(film);
            Assert.Equal(expTitle, film.Title);
            Assert.Equal(expTimeMin, film.TimeMin);
            Assert.Equal(expGenre, film.Genre);
            Assert.Equal(mockHall.Object, film.Hall);
        }

    }
}
