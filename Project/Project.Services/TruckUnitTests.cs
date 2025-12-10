using Project.Abstractions;
using Project.Model;
using Xunit;

public class TruckTests
{
    [Fact]
    public void CalculateWearRate_Test()
    {
        var t = new Truck(1, "", 0, 0, 0, "", "", "", 0);
        Assert.Equal(0.25f, t.CalculateWearRate());
    }
}
