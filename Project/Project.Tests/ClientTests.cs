using Xunit;
using Project.Model;
using System;

namespace Project.Tests
{
    public class KlientTesty
    {
        [Fact]
        public void KonstruktorDomyslny_InicjalizujeWartosci()
        {
            // Przygotowanie i Działanie (Arrange & Act)
            var klient = new Client();

            // Asercja (Assert)
            // Właściwości Person
            Assert.Equal(string.Empty, klient.FirstName);
            Assert.Equal(string.Empty, klient.LastName);

            // Kolekcja Workouts
            Assert.NotNull(klient.PlannedWorkouts);
            Assert.Empty(klient.PlannedWorkouts);
        }

        [Fact]
        public void KonstruktorParam_PrzypisujeDane()
        {
            // Przygotowanie (Arrange)
            string imie = "Marta";
            string nazwisko = "Zając";
            double waga = 62.5;
            string cel = "Fat Loss";

            // Działanie (Act)
            var klient = new Client(imie, nazwisko, "marta@gym.pl", waga, 1.68, cel);

            // Asercja (Assert)
            Assert.Equal(imie, klient.FirstName);
            Assert.Equal(cel, klient.TrainingGoal);
            Assert.Equal(DateTime.Now.Date, klient.JoinDate);
        }

        [Fact]
        public void OpisRaportu_ZwrociPoprawnyFormat()
        {
            // Przygotowanie
            var klient = new Client("Piotr", "Lis", "p@l.com", 80, 1.80, "Mass Gain");
            klient.Id = 15;
            string oczekiwanyFragment = "CLIENT (ID: 15): Piotr Lis | Goal: Mass Gain";

            // Działanie
            string wynik = klient.ReportDescription;

            // Asercja
            Assert.Contains(oczekiwanyFragment, wynik);
        }
    }
}