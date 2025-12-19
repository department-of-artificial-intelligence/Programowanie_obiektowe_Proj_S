using Project.Model;
using Project.Logic;

namespace Project.Tests
{
    public class VeterinarianTests
    {
        [Fact]
        public void Constructor_ShouldCreateVeterinarian()
        {
            var v = new Veterinarian(
                "Piotr",
                "Lekarz",
                "ABC123",
                "Surgery",
                "vet@clinic.com",
                "555555555"
            );

            Assert.Equal("Piotr", v.FirstName);
            Assert.Equal("Lekarz", v.LastName);
            Assert.Equal("ABC123", v.LicenseNumber);
            Assert.Equal("Surgery", v.Specialty);
            Assert.Equal("vet@clinic.com", v.Email);
            Assert.Equal("555555555", v.Phone);
        }

        [Fact]
        public void EmptyConstructor_ShouldSetDefaults()
        {
            var v = new Veterinarian();

            Assert.NotNull(v.FirstName);
            Assert.NotNull(v.LastName);
            Assert.Null(v.LicenseNumber);
            Assert.Null(v.Specialty);
        }

    }
}
