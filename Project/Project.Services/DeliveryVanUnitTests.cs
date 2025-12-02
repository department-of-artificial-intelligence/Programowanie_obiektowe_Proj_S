using Project.Abstractions;
using Project.Model;
using Xunit;

public class DeliveryVanTests
{
    [Fact]
    public void Constructor_Test()
    {
        var van = new DeliveryVan(1, "VIN2", 2020, 2.0f, 120000, "Ford", "Transit", "REG2", 10.5f);

        Assert.Equal(10.5f, van.MaxVolumeCubicMeters);
    }

    [Fact]
    public void VType_Test()
    {
        var van = new DeliveryVan(1, "", 0, 0, 0, "", "", "", 0);
        Assert.Equal(VehicleType.DeliveryVan, van.VType);
    }

    [Fact]
    public void CalculateWearRate_Test()
    {
        var van = new DeliveryVan(1, "", 0, 0, 0, "", "", "", 0);
        Assert.Equal(0.15f, van.CalculateWearRate());
    }
}
