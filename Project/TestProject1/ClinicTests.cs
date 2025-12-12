using Project.Model;

namespace Project.Tests
{
    public class ClinicTests
    {
        [Fact]
        public void Constructor_ShouldCreateClinic()
        {
            var c = new Clinic(1, "Vet Clinic", "Street 1", "clinic@mail.com", "777");

            Assert.Equal(1, c.Id);
            Assert.Equal("Vet Clinic", c.Name);
            Assert.Equal("Street 1", c.Address);
            Assert.Equal("clinic@mail.com", c.Email);
            Assert.Equal("777", c.Phone);
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
