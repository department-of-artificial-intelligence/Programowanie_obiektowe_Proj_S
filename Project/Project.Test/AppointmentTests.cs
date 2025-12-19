using Project.Model;
using Project.Logic;

namespace Project.Tests
{
    public class AppointmentTests
    {
        [Fact]
        public void Constructor_ShouldCreateAppointment()
        {
            
            var date = new DateTime(2025, 12, 19);

            
            var ap = new Appointment(
                animalId: 1,
                veterinarianId: 2,
                date: date,
                notes: "Note",
                status: "Pending"
            );

            
            Assert.Equal(1, ap.AnimalId);
            Assert.Equal(2, ap.VeterinarianId);
            Assert.Equal(date, ap.Date);
            Assert.Equal("Note", ap.Notes);
            Assert.Equal("Pending", ap.Status);
        }

        [Fact]
        public void EmptyConstructor_ShouldInitializeCollections()
        {
            var ap = new Appointment();

            Assert.NotNull(ap.Treatments);
            Assert.Empty(ap.Treatments);
        }
    }
}
