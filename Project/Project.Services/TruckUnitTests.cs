using Project.Abstractions;
using Project.Model;
using Xunit;

public class TruckTests
{
    [Fact]
    public void Constructor_Test()
    {
        var t = new Truck(1, "V", 2010, 7f, 300000, "MAN", "TGX", "REG3", 25000);
        Assert.Equal(25000, t.MaxPayloadKg);
    }

    [Fact]
    public void VType_Test()
    {
        var t = new Truck(1, "", 0, 0, 0, "", "", "", 0);
        Assert.Equal(VehicleType.Truck, t.VType);
    }

    [Fact]
    public void CalculateWearRate_Test()
    {
        var t = new Truck(1, "", 0, 0, 0, "", "", "", 0);
        Assert.Equal(0.25f, t.CalculateWearRate());
    }
}
