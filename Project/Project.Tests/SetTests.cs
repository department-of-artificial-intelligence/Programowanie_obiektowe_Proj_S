using Xunit;
using Project.Model;
using System;

namespace Project.Tests
{
    public class SetTesty
    {
        [Fact]
        public void KonstruktorDomyslny_InicjalizujeWartosci()
        {
            // Przygotowanie i Działanie
            var set = new Set();

            // Asercja
            Assert.Null(set.Exercise);
            Assert.Equal(0, set.Repetitions);
            Assert.Equal(0, set.WeightUsed);
        }

        [Fact]
        public void KonstruktorParam_PrzypisujeDane()
        {
            // Przygotowanie
            var ex = new Exercise("Push Up", "Chest");
            int reps = 15;
            int count = 4;
            double weight = 0.0; // Waga ciała

            // Działanie
            var set = new Set(ex, reps, count, weight);

            // Asercja
            Assert.Equal(ex, set.Exercise);
            Assert.Equal(reps, set.Repetitions);
            Assert.Equal(count, set.SetCount);
            Assert.Equal(weight, set.WeightUsed);
        }
    }
}