using Xunit;
using Project.DAL;
using Project.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Project.Tests
{
    public class DatabaseLogicTests
    {
        private ApplicationDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void Wykrywanie_Kolizji_Terminow_Dziala()
        {
            using var db = GetInMemoryContext();

            var hotel = new Hotel { Nazwa = "TestHotel", Gwiazdki = 3 };
            db.Hotels.Add(hotel);
            db.SaveChanges();

            var room = new Room { Numer = 101, HotelId = hotel.Id, LiczbaMiejsc = 2 };
            db.Rooms.Add(room);
            db.SaveChanges();

            db.Reservations.Add(new Reservation
            {
                HotelId = hotel.Id,
                PokojId = room.Id,
                DataOd = new DateTime(2024, 5, 1),
                DataDo = new DateTime(2024, 5, 5),
                Gosc = new Guest { Imie = "Jan", Nazwisko = "Testowy" }
            });
            db.SaveChanges();

            var startChciany = new DateTime(2024, 5, 3);
            var koniecChciany = new DateTime(2024, 5, 6);

            var zajeteId = db.Reservations
                .Where(r => r.HotelId == hotel.Id)
                .Where(r => r.DataOd < koniecChciany && r.DataDo > startChciany)
                .Select(r => r.PokojId)
                .ToList();

            Assert.Contains(room.Id, zajeteId);
        }

        [Fact]
        public void Rezerwacja_W_Innym_Terminie_Jest_Mozliwa()
        {
            using var db = GetInMemoryContext();
            var hotel = new Hotel { Nazwa = "TestHotel" };
            db.Hotels.Add(hotel);

            var room = new Room { Numer = 101, HotelId = hotel.Id };
            db.Rooms.Add(room);
            db.SaveChanges();

            db.Reservations.Add(new Reservation
            {
                HotelId = hotel.Id,
                PokojId = room.Id,
                DataOd = new DateTime(2024, 5, 1),
                DataDo = new DateTime(2024, 5, 5),
                Gosc = new Guest { Imie = "A", Nazwisko = "B" }
            });
            db.SaveChanges();

            var startChciany = new DateTime(2024, 12, 1);
            var koniecChciany = new DateTime(2024, 12, 5);

            var zajeteId = db.Reservations
                .Where(r => r.HotelId == hotel.Id)
                .Where(r => r.DataOd < koniecChciany && r.DataDo > startChciany)
                .Select(r => r.PokojId)
                .ToList();

            Assert.Empty(zajeteId);
        }

        [Fact]
        public void Filtrowanie_Po_Liczbie_Osob_Dziala()
        {
            using var db = GetInMemoryContext();
            var hotel = new Hotel { Nazwa = "TestHotel" };
            db.Hotels.Add(hotel);
            db.SaveChanges();

            var r1 = new Room { Numer = 1, HotelId = hotel.Id, LiczbaMiejsc = 2 };
            var r2 = new Room { Numer = 2, HotelId = hotel.Id, LiczbaMiejsc = 4 };

            db.Rooms.AddRange(r1, r2);
            db.SaveChanges();

            int liczbaOsob = 3;

            var pasujacePokoje = db.Rooms
                .Where(r => r.HotelId == hotel.Id)
                .Where(r => r.LiczbaMiejsc >= liczbaOsob)
                .ToList();

            Assert.Single(pasujacePokoje);
            Assert.Equal(2, pasujacePokoje.First().Numer);
        }
    }
}