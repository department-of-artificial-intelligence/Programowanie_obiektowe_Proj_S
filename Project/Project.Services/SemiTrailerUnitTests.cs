using Project.Abstractions;
using Project.Model;
using Xunit;

public class SemiTrailerTests
{

    [Fact]
    public void CalculateWearRate_Test()
    {
        var st = new SemiTrailer(1, "", 0, 0, 0, "", "", "", 0);
        Assert.Equal(0.35f, st.CalculateWearRate());
    }
}