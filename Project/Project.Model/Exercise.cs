namespace Project.Model;
public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MuscleGroup { get; set; }

    public Exercise(int id, string name, string muscleGroup)
    {
        Id = id;
        Name = name;
        MuscleGroup = muscleGroup;
    }
}