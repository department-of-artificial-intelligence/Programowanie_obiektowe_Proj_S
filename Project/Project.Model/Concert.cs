namespace Project.Model;

public class Concert
{
    public int Id { get; set; }
    public Artist Artist { get; set; }
    public Venue Venue { get; set; }

    public override string ToString()
    {
        return $"{Artist} - {Venue}";
    }
}