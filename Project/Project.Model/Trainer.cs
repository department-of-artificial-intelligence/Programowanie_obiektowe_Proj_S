public class Trainer : Person
{
    public string Specialization { get; set; }
    public decimal HourlyRate { get; set; }

    // *******************************************************************
    // ✅ POPRAWKA: Wymagany przez Entity Framework
    public Trainer() : base(string.Empty, string.Empty, string.Empty)
    {
        // Konstruktor bezargumentowy, aby EF mógł go użyć.
    }
    // *******************************************************************

    public Trainer(string firstName, string lastName, string email, string specialization, decimal rate)
        : base(firstName, lastName, email)
    {
        Specialization = specialization;
        HourlyRate = rate;
    }
}