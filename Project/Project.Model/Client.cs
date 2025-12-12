using Project.Model;

public class Client : Person, IReportable
{
    public double Weight { get; set; }
    public double Height { get; set; }
    public string TrainingGoal { get; set; }
    public DateTime JoinDate { get; set; }

    public List<Workout> PlannedWorkouts { get; set; }
    public Client() : base(string.Empty, string.Empty, string.Empty)
    {
        // Pamiętaj o inicjalizacji kolekcji
        PlannedWorkouts = new List<Workout>();
    }

    public Client(string firstName, string lastName, string email, double weight, double height, string goal)
        : base(firstName, lastName, email)
    {
        Weight = weight;
        Height = height;
        TrainingGoal = goal;
        JoinDate = DateTime.Now.Date;
        PlannedWorkouts = new List<Workout>();
    }

    public string ReportDescription
    {
        get { return $"CLIENT (ID: {Id}): {FirstName} {LastName} | Goal: {TrainingGoal}"; }
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"  -> Joined: {JoinDate.ToShortDateString()}, Height: {Height}m, Weight: {Weight}kg");
    }
}