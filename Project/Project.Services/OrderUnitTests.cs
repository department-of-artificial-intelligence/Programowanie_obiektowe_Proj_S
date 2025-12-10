using Project.Abstractions;
using Project.Model;
using Xunit;

public class OrderTests
{
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
