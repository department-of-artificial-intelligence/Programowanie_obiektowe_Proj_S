using Project.Abstractions;
using Project.Model;
using Xunit;

public class DriverTests
{
    [Fact]
    public void AssignVehicle_Test()
    {
        var d = new Driver(1, "A", "B", "C");
        var car = new CompanyCar(1, "VIN", 2020, 1.5f, 0, "BMW", "3", "REG", false);

        d.AssignVehicle(car);

        Assert.Equal(car, d.AssignedVehicle);
        Assert.Equal(DriverStatus.Assigned, d.Status);
    }

    [Fact]
    public void MarkAsAvailable_Test()
    {
        var d = new Driver(1, "A", "B", "C");
        var van = new DeliveryVan(1, "X", 2020, 2, 0, "Ford", "Transit", "REG", 12);

        d.AssignVehicle(van);
        d.MarkAsAvailable();

        Assert.True(d.IsAvailable);
        Assert.Null(d.AssignedVehicle);
    }
}
