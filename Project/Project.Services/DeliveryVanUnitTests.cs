using Project.Abstractions;
using Project.Model;
using Xunit;

public class DeliveryVanTests
{
    [Fact]
    public void CalculateWearRate_Test()
    {
        var van = new DeliveryVan(1, "", 0, 0, 0, "", "", "", 0);
        Assert.Equal(0.15f, van.CalculateWearRate());
    }
}
