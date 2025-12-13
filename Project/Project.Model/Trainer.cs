using Project.Model;

public class Trainer : Person, IReportable // Dodanie IReportable
{
    public string Specialization { get; set; }
    public decimal HourlyRate { get; set; }

    public List<Reservation> ScheduledReservations { get; set; }

    // ... (Konstruktory bez zmian) ...
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

    // Dodanie implementacji IReportable
    public string ReportDescription
    {
        get { return $"TRAINER (ID: {Id}): {FirstName} {LastName} | Specialization: {Specialization}"; }
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"  -> Rate: {HourlyRate:C} | Specializes in: {Specialization}");
        // Dodanie wyświetlania rezerwacji, jeśli są
        if (ScheduledReservations.Count > 0)
        {
            Console.WriteLine($"  -> Scheduled Sessions: {ScheduledReservations.Count}");
            foreach (var r in ScheduledReservations.Take(3)) // Ograniczenie do 3, by nie zaśmiecać konsoli
            {
                Console.WriteLine($"     - {r.ScheduledTime:yyyy-MM-dd HH:mm} with Client ID: {r.ClientId}");
            }
        }
    }
}