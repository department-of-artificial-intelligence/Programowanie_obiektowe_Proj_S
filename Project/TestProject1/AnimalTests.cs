using Project.Model;

namespace Project.Tests
{
    public class AnimalTests
    {
        [Fact]
        public void Constructor_ShouldCreateAnimal()
        {
            var a = new Animal(1, "Reksio", "Dog", "Beagle", 5, 12.5, 10);

            Assert.Equal(1, a.Id);
            Assert.Equal("Reksio", a.Name);
            Assert.Equal("Dog", a.Species);
            Assert.Equal("Beagle", a.Breed);
            Assert.Equal(5, a.Age);
            Assert.Equal(12.5, a.WeightKg);
            Assert.Equal(10, a.OwnerId);
        }

        [Fact]
        public void EmptyConstructor_ShouldInitDefaults()
        {
            var a = new Animal();

            Assert.NotNull(a.Name);
        }
    }
}
