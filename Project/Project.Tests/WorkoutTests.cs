using Xunit;
using Project.Model;
using System;
using System.Linq;

namespace Project.Tests
{
    public class WorkoutTesty
    {
        [Fact]
        public void KonstruktorDomyslny_InicjalizujeKolekcje()
        {
            var workout = new Workout();

            Assert.Null(workout.Client);

            Assert.NotNull(workout.Sets);
            Assert.Empty(workout.Sets);
        }

        [Fact]
        public void KonstruktorParam_PrzypisujeDane()
        {
            var client = new Client("Jan", "Kowalski", "j@k.pl", 80, 1.80, "siła");
            var date = DateTime.Today.AddDays(-5);

            var workout = new Workout(date, client);

            Assert.Equal(client, workout.Client);
            Assert.Equal(date, workout.Date);
        }

        [Fact]
        public void OpisRaportu_ZwrociPoprawnyFormat()
        {
            var client = new Client("Anna", "Lewa", "a@l.pl", 60, 1.75, "kondycja");
            var date = new DateTime(2025, 12, 10);
            var workout = new Workout(date, client);
            workout.Id = 5;
            string oczekiwanyFragment = $"WORKOUT (ID: 5): 2025-12-10 | Client: Lewa";
            string wynik = workout.ReportDescription;
            Assert.Contains(oczekiwanyFragment, wynik);
        }
    }
}