using Xunit;
using Project.Model;
using System;
using System.Linq;

namespace Project.Tests
{
    public class TrainerTesty
    {
        [Fact]
        public void KonstruktorDomyslny_InicjalizujeKolekcje()
        {
            // Przygotowanie i Działanie (Arrange & Act)
            var trainer = new Trainer();

            // Asercja (Assert)
            // Właściwości Person
            Assert.Equal(string.Empty, trainer.FirstName);
            Assert.Equal(0, trainer.HourlyRate); // decimal domyślnie 0

            // Kolekcja Rezerwacji
            Assert.NotNull(trainer.ScheduledReservations);
            Assert.Empty(trainer.ScheduledReservations);
        }

        [Fact]
        public void KonstruktorParam_PrzypisujeDane()
        {
            // Przygotowanie (Arrange)
            string imie = "Robert";
            string specjalizacja = "Rehabilitacja";
            decimal stawka = 180.50m;

            // Działanie (Act)
            var trainer = new Trainer(imie, "Kowalski", "r@k.pl", specjalizacja, stawka);

            // Asercja (Assert)
            Assert.Equal(imie, trainer.FirstName);
            Assert.Equal(stawka, trainer.HourlyRate);
            Assert.Equal(specjalizacja, trainer.Specialization);
        }

        [Fact]
        public void OpisRaportu_ZwrociPoprawnyFormat()
        {
            // Przygotowanie
            var trainer = new Trainer("Alicja", "Nowak", "a@n.com", "Pilates", 110.00m);
            trainer.Id = 22;
            string oczekiwanyFragment = "TRAINER (ID: 22): Alicja Nowak | Specialization: Pilates";

            // Działanie
            string wynik = trainer.ReportDescription;

            // Asercja
            Assert.Contains(oczekiwanyFragment, wynik);
        }
    }
}