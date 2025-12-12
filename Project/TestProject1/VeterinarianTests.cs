using Project.Model;

namespace Project.Tests
{
    public class VeterinarianTests
    {
        [Fact]
        public void Constructor_ShouldCreateVeterinarian()
        {
            var v = new Veterinarian(1, "Piotr", "Lekarz", "ABC123", "Surgery", "vet@clinic.com", "555");

            Assert.Equal(1, v.Id);
            Assert.Equal("Piotr", v.FirstName);
            Assert.Equal("Lekarz", v.LastName);
            Assert.Equal("ABC123", v.LicenseNumber);
            Assert.Equal("Surgery", v.Specialty);
            Assert.Equal("vet@clinic.com", v.Email);
            Assert.Equal("555", v.Phone);
        }

        [Fact]
        public void EmptyConstructor_ShouldSetDefaults()
        {
            var v = new Veterinarian();

            Assert.Null(v.LicenseNumber);
            Assert.Null(v.Specialty);
        }
    }
}
