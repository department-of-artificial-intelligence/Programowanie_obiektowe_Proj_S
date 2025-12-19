using Project.Model;
using Project.Logic;

namespace Project.Tests
{
    public class AnimalTests
    {
        [Fact]
        public void Constructor_ShouldCreateAnimal()
        {
            
            var a = new Animal(
                name: "Reksio",
                species: "Dog",
                breed: "Beagle",
                age: 5,
                weightKg: 12.5,
                ownerId: 10
            );

            
            Assert.Equal("Reksio", a.Name);
            Assert.Equal("Dog", a.Species);
            Assert.Equal("Beagle", a.Breed);
            Assert.Equal(5, a.Age);
            Assert.Equal(12.5, a.WeightKg);
            Assert.Equal(10, a.OwnerId);
        }

        [Fact]
        public void EmptyConstructor_ShouldInitializeDefaults()
        {
            var a = new Animal();

            Assert.NotNull(a.Name);
            Assert.Equal(string.Empty, a.Name);
        }
    }
}
