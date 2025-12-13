// W pliku Project.Model/Set.cs

using Project.Model;
using System.ComponentModel.DataAnnotations.Schema; // Potrzebne dla Column

public class Set
{
    public int Id { get; set; }

    // Klucze Obce dla relacji 1:W
    public int WorkoutId { get; set; } // Dodany jawny klucz obcy do Workout
    // public Workout Workout { get; set; } <-- Referencja nie jest konieczna, ale nie zaszkodzi

    public int ExerciseId { get; set; } // Klucz obcy do Exercise
    public Exercise Exercise { get; set; } // Publiczna referencja do Ćwiczenia

    public int Repetitions { get; set; }
    public int SetCount { get; set; }

    [Column(TypeName = "decimal(5, 2)")] // Dodanie atrybutu dla precyzji w bazie
    public double WeightUsed { get; set; }

    public Set() { }

    // W konstruktorze nie musimy przyjmować WorkoutId, bo jest ustawiane podczas dodawania do kolekcji Workout.Sets
    public Set(Exercise exercise, int repetitions, int setCount, double weight)
    {
        Exercise = exercise;
        ExerciseId = exercise.Id; // Zakładamy, że Exercise ma już ID z bazy
        Repetitions = repetitions;
        SetCount = setCount;
        WeightUsed = weight;
    }
}