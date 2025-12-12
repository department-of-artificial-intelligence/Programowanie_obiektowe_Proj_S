namespace Project.Model;
public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MuscleGroup { get; set; }

    // *******************************************************************
    // ✅ POPRAWKA: Wymagany przez Entity Framework
    public Exercise() { }
    // *******************************************************************

    public Exercise(string name, string muscleGroup)
    {
        Name = name;
        MuscleGroup = muscleGroup;
    }
}