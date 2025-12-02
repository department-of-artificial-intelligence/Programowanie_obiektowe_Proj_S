using Project.Abstractions;
using Project.Model;
using Xunit;

public class SemiTrailerTests
{
    [Fact]
    public void Constructor_Test()
    {
        var st = new SemiTrailer(1, "VIN", 2005, 0, 0, "Krone", "Coolliner", "R1", 40);
        Assert.Equal(40, st.MaxGrossWeightTons);
    }

    [Fact]
    public void VType_Test()
    {
        var st = new SemiTrailer(1, "", 0, 0, 0, "", "", "", 0);
        Assert.Equal(VehicleType.SemiTrailer, st.VType);
    }

    [Fact]
    public void CalculateWearRate_Test()
    {
        var st = new SemiTrailer(1, "", 0, 0, 0, "", "", "", 0);
        Assert.Equal(0.35f, st.CalculateWearRate());
    }
}
