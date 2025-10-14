namespace Project.Model;

public class Venue
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public int FloorCapacity { get; set; }
    public int SeatsCapacity { get; set; }

    public override string ToString()
    {
        return $"{Name} -  {City}, Pojemność: Płyta - {FloorCapacity}, Trybuny - {SeatsCapacity}";
    }
}