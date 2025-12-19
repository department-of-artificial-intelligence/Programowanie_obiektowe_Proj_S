namespace Project.Model;

public class Concert: Event
{
    
    public Artist Artist { get; set; }


    public override string ToString()
    {
        return $"{Artist.Name} - {Venue} - {Date}";
    }
}