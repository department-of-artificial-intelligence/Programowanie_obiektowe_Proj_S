namespace Projekt.Model
{  
    public class Car
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public string RegistrationNumber { get; set; }

        public decimal DailyRate { get; set; }
        public CarStatus Status { get; set; }
        public int CurrentBranchId { get; set; }
        public virtual Branch CurrentBranch { get; set; }

        public Car () { }
        public override string ToString()
        {
            return $"[{Id}] {Marka} {Model} ({RegistrationNumber}) - Status: {Status} - Oddział: {CurrentBranch?.Name ?? "Brak"}";
        }
    }
}

