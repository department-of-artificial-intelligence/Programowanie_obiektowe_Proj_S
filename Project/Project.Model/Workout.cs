using Project.Model;

public class Workout : IReportable
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Client Client { get; set; } = null!;
    public List<Set> Sets { get; set; }

    public Workout()
    {
        Sets = new List<Set>();
    }

    public Workout(DateTime date, Client client)
    {
        Date = date;
        Client = client;
        Sets = new List<Set>();
    }
    public string ReportDescription
    {
        get { return $"WORKOUT (ID: {Id}): {Date.ToString("yyyy-MM-dd")} | Client: {Client.LastName}"; }
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"  -> Client: {Client.FirstName} | Total Exercises: {Sets.Count}");
        foreach (var set in Sets)
        {
            Console.WriteLine($" - {set.Exercise.Name}: {set.SetCount}x{set.Repetitions} ({set.WeightUsed}kg)");
        }
    }
}




