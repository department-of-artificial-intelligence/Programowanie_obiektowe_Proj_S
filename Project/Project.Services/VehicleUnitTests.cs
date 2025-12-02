using Project.Abstractions;
using Project.Model;
using Xunit;

public class VehicleTests
{
    [Fact]
    public void AssignDriver_Test()
    {
        var driver = new Driver(1, "A", "B", "C");
        var truck = new Truck(1, "V", 2000, 5, 0, "MAN", "TGX", "R1", 1000);

        truck.AssignDriver(driver);

        Assert.Equal(driver, truck.AssignedDriver);
        Assert.Equal(VehicleStatus.InTransit, truck.VStatus);
    }

    [Fact]
    public void MarkAsAvailable_Test()
    {
        var driver = new Driver(1, "A", "B", "C");
        var van = new DeliveryVan(1, "V", 2000, 5, 0, "Ford", "T", "R2", 10);

        van.AssignDriver(driver);
        van.MarkAsAvailable();

        Assert.Null(van.AssignedDriver);
        Assert.Equal(VehicleStatus.Available, van.VStatus);
        Assert.True(van.IsAvailable);
    }
}
