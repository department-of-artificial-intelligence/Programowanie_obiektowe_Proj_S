using Project.Model;

namespace Project.Tests
{
    public class TreatmentTests
    {
        [Fact]
        public void Constructor_ShouldCreateTreatment()
        {
            var t = new Treatment("Vaccination", "Rabies injection", 99.99m);

            Assert.Equal("Vaccination", t.Name);
            Assert.Equal("Rabies injection", t.Description);
            Assert.Equal(99.99m, t.Cost);
        }

        [Fact]
        public void EmptyConstructor_ShouldInitializeDefaults()
        {
            var t = new Treatment();

            Assert.NotNull(t.Name);
            Assert.Equal(0m, t.Cost);
        }
    }
}
