using Xunit;
using Project.Model;
using System;

namespace Project.Tests
{
    public class ExerciseTesty
    {
        [Fact]
        public void KonstruktorDomyslny_InicjalizujeWartosci()
        {
            // Przygotowanie i Działanie
            var exercise = new Exercise();

            // Asercja
            Assert.Null(exercise.Name); // Domyślnie string jest null, chyba że inicjalizujemy inaczej
            Assert.Null(exercise.MuscleGroup);
        }

        [Fact]
        public void KonstruktorParam_PrzypisujeDane()
        {
            // Przygotowanie
            string nazwa = "Deadlift";
            string grupa = "Back";

            // Działanie
            var exercise = new Exercise(nazwa, grupa);

            // Asercja
            Assert.Equal(nazwa, exercise.Name);
            Assert.Equal(grupa, exercise.MuscleGroup);
        }
    }
}