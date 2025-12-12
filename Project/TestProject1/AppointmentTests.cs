using Project.Model;

namespace Project.Tests
{
    public class AppointmentTests
    {
        [Fact]
        public void Constructor_ShouldCreateAppointment()
        {
            var date = DateTime.Now;
            var ap = new Appointment(1, 2, 3, date, "Note", "Pending");

            Assert.Equal(1, ap.Id);
            Assert.Equal(2, ap.AnimalId);
            Assert.Equal(3, ap.VeterinarianId);
            Assert.Equal(date, ap.Date);
            Assert.Equal("Note", ap.Notes);
            Assert.Equal("Pending", ap.Status);
        }

        [Fact]
        public void EmptyConstructor_ShouldHaveDefaults()
        {
            var ap = new Appointment();

            Assert.NotEqual(default(DateTime), ap.Date);
        }
    }
}
