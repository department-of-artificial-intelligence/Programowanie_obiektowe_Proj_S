using Project.Model;
public class Set
{
    public int Id { get; set; }
    public Exercise Exercise { get; set; }
    public int Repetitions { get; set; }
    public int SetCount { get; set; }
    public double WeightUsed { get; set; }

    // *******************************************************************
    // ✅ POPRAWKA: Wymagany przez Entity Framework
    public Set() { }
    // *******************************************************************

    public Set(Exercise exercise, int repetitions, int setCount, double weight)
    {
        Exercise = exercise;
        Repetitions = repetitions;
        SetCount = setCount;
        WeightUsed = weight;
    }
}