using Project.Model;

public class Trainer : Person, IReportable 
{
    public string Specialization { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }

    public List<Reservation> ScheduledReservations { get; set; } = null!;
    public Trainer() : base(string.Empty, string.Empty, string.Empty)
    {
        ScheduledReservations = new List<Reservation>();
    }

    public Trainer(string firstName, string lastName, string email, string specialization, decimal rate)
        : base(firstName, lastName, email)
    {
        Specialization = specialization;
        HourlyRate = rate;
        ScheduledReservations = new List<Reservation>();
    }
    public string ReportDescription
    {
        get { return $"TRAINER (ID: {Id}): {FirstName} {LastName} | Specialization: {Specialization}"; }
    }
    //wyświetlenie rezerwacji o ile one sąto jest moj plik trainer.cs
    
    public void DisplayDetails()
    {
        Console.WriteLine($"  -> Rate: {HourlyRate:C} | Specializes in: {Specialization}");
        if (ScheduledReservations.Count > 0)
        {
            Console.WriteLine($"  -> Scheduled Sessions: {ScheduledReservations.Count}");
            foreach (var r in ScheduledReservations) 
            {
                Console.WriteLine($"     - {r.ScheduledTime:yyyy-MM-dd HH:mm} z klient ID: {r.ClientId}");
            }
        }
    }
}