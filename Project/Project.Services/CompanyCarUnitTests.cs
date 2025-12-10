using Project.Abstractions;
using Project.Model;
using Xunit;

public class CompanyCarTests
{
    [Fact]
    public void VType_Test()
    {
        var car = new CompanyCar(1, "", 0, 0, 0, "", "", "", false);
        Assert.Equal(VehicleType.CompanyCar, car.VType);
    }

    [Fact]
    public void CalculateWearRate_Test()
    {
        var car = new CompanyCar(1, "", 0, 0, 0, "", "", "", false);
        Assert.Equal(0.05f, car.CalculateWearRate());
    }
}
