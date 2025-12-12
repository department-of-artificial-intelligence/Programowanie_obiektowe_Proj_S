using Xunit;
using System;
using Project.Model;

namespace Project.Tests
{
    public class TimeSlotTests
    {
        [Fact]
        public void GetDuration_ObliczaPoprawnyCzasTrwania()
        {
            // Arrange
            DateTime start = new DateTime(2023, 10, 1, 10, 0, 0);
            DateTime end = new DateTime(2023, 10, 1, 11, 30, 0); // 1.5 godziny różnicy
            var slot = new TimeSlot(1, start, end);

            // Act
            TimeSpan duration = slot.GetDuration();

            // Assert
            Assert.Equal(90, duration.TotalMinutes);
        }

        [Fact]
        public void Domyslnie_IsBooked_PowinnoBycFalse()
        {
            // Arrange
            var slot = new TimeSlot();

            // Assert
            Assert.False(slot.IsBooked);
        }
    }
}