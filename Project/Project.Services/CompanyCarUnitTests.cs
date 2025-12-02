using Project.Abstractions;
using Project.Model;
using Xunit;

public class CompanyCarTests
{
    [Fact]
    public void Constructor_Test()
    {
        var car = new CompanyCar(1, "VIN1", 2019, 1.8f, 50000, "BMW", "320i", "REG1", true);

        Assert.Equal(1, car.Id);
        Assert.Equal("VIN1", car.VinNumber);
        Assert.Equal(2019, car.ProductionYear);
        Assert.Equal(1.8f, car.EngineSize);
        Assert.Equal(50000, car.Mileage);
        Assert.Equal("BMW", car.Brand);
        Assert.Equal("320i", car.Model);
        Assert.Equal("REG1", car.RegistrationNumber);
        Assert.True(car.IsExecutive);
    }

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
