using Projekt.Model;
using Xunit;
using System.Collections.Generic;
using Moq;

namespace Projekt.Tests
{
    public class HallTest
    {
    }
    public class CinemaAddressTest
    {
        public override string ToString()
        {
            return "TEST ADDRESS MOQ";
        }
    } 
    public class EmployeeTest
    {
    }

    public class CinemaTesting
    {
        [Fact]
        public void Konstruktor_DomyslnyCinema()
        {
           
            var cinema = new Cinema();
            
            Assert.NotNull(cinema);
            Assert.Null(cinema.CinemaName);
            Assert.Null(cinema.Address);
            Assert.NotNull(cinema.Hall);
            Assert.NotNull(cinema.Employees);
        }

        [Fact]
        public void KonstruktorParametrycznyCinema()
        {

            string ExpName = "Test Cinema";
            var mockAddress = new Mock<CinemaAddress>();

            var cinema = new Cinema(ExpName, mockAddress.Object);
            Assert.Equal(ExpName,cinema.CinemaName);
            Assert.Equal(mockAddress.Object, cinema.Address);
            Assert.NotNull(cinema.Hall);
            Assert.NotNull(cinema.Employees);
        }

        [Fact]
        public void TestToStringCinema()
        {
            string ExpName = "Test Cinema";
            var mockAddress = new Mock<CinemaAddress>();
            var cinema = new Cinema(ExpName, mockAddress.Object);
            string expectedString = $"Kino: {ExpName} | IDKina:{cinema.CinemaID}|\n";
            Assert.Equal(expectedString, cinema.ToString());
        }
    }
}
