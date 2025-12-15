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
            DateTime start = new DateTime(2023, 10, 1, 10, 0, 0);
            DateTime end = new DateTime(2023, 10, 1, 11, 30, 0);
            var slot = new TimeSlot(1, start, end);

            TimeSpan duration = slot.GetDuration(); 
            Assert.Equal(90, duration.TotalMinutes);
        }

        [Fact]
        public void Domyslnie_IsBooked_PowinnoBycFalse()
        {
            var slot = new TimeSlot();
            Assert.False(slot.IsBooked);
        }
    }
}