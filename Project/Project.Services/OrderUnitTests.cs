using Project.Abstractions;
using Project.Model;
using Xunit;

public class OrderTests
{
    [Fact]
    public void Constructor_Test()
    {
        var o = new Order(5, "Steel", "Warsaw", "Berlin");

        Assert.Equal(5, o.Id);
        Assert.Equal("Steel", o.LoadingDescription);
        Assert.Equal("Warsaw", o.LoadingAddress);
        Assert.Equal("Berlin", o.UnloadingAddress);
        Assert.Equal(OrderStatus.Pending, o.Status);
    }

    [Fact]
    public void AssignOrder_WhenDriverUnavailable_Test()
    {
        var o = new Order(1, "Desc", "A", "B");
        var d = new Driver(1, "Jan", "Nowak", "X") { Status = DriverStatus.Unavailable };

        o.AssignOrder(d);

        Assert.Null(o.AssignedDriver);
        Assert.Equal(OrderStatus.Pending, o.Status);
    }
}
