using Project.Model;

namespace Project.Tests
{
    public class SemiTrailerTests
    {
        [Fact]
        public void VTypeTest() // Returns SemiTrailer
        {
            var st = new SemiTrailer(1, "V", 2000, 10, 0, "MAN", "X", "R3", 20);
            Assert.Equal(Project.Abstractions.VehicleType.SemiTrailer, st.VType);
        }

        [Fact]
        public void CalculateWearRateTest() // Returns positive value
        {
            var st = new SemiTrailer(1, "V", DateTime.Now.Year - 8, 10, 100000, "MAN", "X", "R3", 20);
            float wear = st.CalculateWearRate();

            Assert.True(wear > 0);
        }
    }
}