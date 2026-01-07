using Xunit;
using Project.Model;

namespace Project.Tests
{
    public class DeliveryTests
    {
        [Fact]
        public void CourierDelivery_ShouldHaveCorrectCostAndProperties()
        {
            var delivery = new CourierDelivery("Warszawa, Złota 44");

            Assert.Equal(19.99m, delivery.Cost);
            Assert.Equal(1, delivery.EstimatedDays);
            Assert.Equal("Warszawa, Złota 44", delivery.ShippingAddress);
            Assert.StartsWith("DPD-", delivery.TrackingId);
        }

        [Fact]
        public void ParcelLockerDelivery_ShouldHaveCorrectCostAndProperties()
        {
            var delivery = new ParcelLockerDelivery("WAW01A");

            Assert.Equal(12.99m, delivery.Cost);
            Assert.Equal(2, delivery.EstimatedDays);
            Assert.Equal("WAW01A", delivery.LockerCode);
            Assert.StartsWith("PACK-", delivery.TrackingId);
        }
    }
}