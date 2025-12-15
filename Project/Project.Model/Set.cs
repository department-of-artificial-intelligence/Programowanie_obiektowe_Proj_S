

using Project.Model;
using System.ComponentModel.DataAnnotations.Schema; 

public class Set
{
    public int Id { get; set; }

    // Klucze Obce dla relacji 1:W
    public int WorkoutId { get; set; } //jawny klucz obcy
  
    public int ExerciseId { get; set; } // Klucz obcy do Exercise
    public Exercise Exercise { get; set; } // referencja do Ćwiczenia

    public int Repetitions { get; set; }
    public int SetCount { get; set; }

    [Column(TypeName = "decimal(5, 2)")] // Dodanie atrybutu dla precyzji w bazie
    public double WeightUsed { get; set; }

    public Set() { }
    public Set(Exercise exercise, int repetitions, int setCount, double weight)
    {
        Exercise = exercise;
        ExerciseId = exercise.Id; 
        Repetitions = repetitions;
        SetCount = setCount;
        WeightUsed = weight;
    }
}