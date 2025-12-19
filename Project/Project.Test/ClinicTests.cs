using Project.Model;

namespace Project.Tests
{
    public class ClinicTests
    {
        [Fact]
        public void Constructor_ShouldCreateClinic()
        {
            var c = new Clinic("Vet Clinic", "Street 1", "clinic@mail.com", "777777777");

            Assert.Equal("Vet Clinic", c.Name);
            Assert.Equal("Street 1", c.Address);
            Assert.Equal("clinic@mail.com", c.Email);
            Assert.Equal("777777777", c.Phone);
        }

        [Fact]
        public void Clinic_ShouldHaveEmptyVeterinariansList_ByDefault()
        {
            var c = new Clinic();

            Assert.NotNull(c.Veterinarians);
            Assert.Empty(c.Veterinarians);
        }
    }
}
