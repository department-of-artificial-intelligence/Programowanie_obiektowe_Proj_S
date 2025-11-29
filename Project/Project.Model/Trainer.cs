public class Trainer : Person
{
    public string Specialization { get; set; }
    public decimal HourlyRate { get; set; }

    public Trainer(int id, string firstName, string lastName, string email, string specialization, decimal rate)
        : base(id, firstName, lastName, email)
    {
        Specialization = specialization;
        HourlyRate = rate;
    }
}