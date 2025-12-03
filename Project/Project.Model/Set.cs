using Project.Model;
public class Set
{
    public Exercise Exercise { get; set; }
    public int Repetitions { get; set; }
    public int SetCount { get; set; }
    public double WeightUsed { get; set; }

    public Set(Exercise exercise, int repetitions, int setCount, double weight)
    {
        Exercise = exercise;
        Repetitions = repetitions;
        SetCount = setCount;
        WeightUsed = weight;
    }//d
}