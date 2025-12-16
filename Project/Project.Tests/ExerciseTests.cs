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
            
            var exercise = new Exercise();

            
            Assert.Null(exercise.Name); 
            Assert.Null(exercise.MuscleGroup);
        }

        [Fact]
        public void KonstruktorParam_PrzypisujeDane()
        {
           
            string nazwa = "Deadlift";
            string grupa = "Back";

            
            var exercise = new Exercise(nazwa, grupa);

           
            Assert.Equal(nazwa, exercise.Name);
            Assert.Equal(grupa, exercise.MuscleGroup);
        }
    }
}