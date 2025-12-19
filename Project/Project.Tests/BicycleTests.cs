using Xunit;
using Project.Model;
using System;

namespace Project.Tests
{
    public class BicycleTests
    {
        [Fact]
        public void Rent_ShouldChangeStatus_WhenAvailable()
        {
            var bike = new Bicycle { Id = 1, Type = BicycleType.City, Price = 15 };
            bike.Rent();

            Assert.Equal(BicycleStatus.Rented, bike.Status);
            Assert.Null(bike.CurrentStation);
            Assert.Null(bike.CurrentStationId);
        }

        [Fact]
        public void Rent_ShouldThrowException_WhenAlreadyRented()
        {
            var bike = new Bicycle { Id = 1, Type = BicycleType.City, Price = 15 };
            bike.Rent();

            Assert.Throws<InvalidOperationException>(() => bike.Rent());
        }
    }
}