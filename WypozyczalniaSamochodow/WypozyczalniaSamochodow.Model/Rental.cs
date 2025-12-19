namespace WypozyczalniaSamochodow.Model
{
    public class Rental
    {
        public int Id { get; set; }

        public int? CarId { get; set; }
        public int? CustomerId { get; set; }
        public int BranchId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Days { get; set; }
        public decimal Cost { get; set; }

        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedDate { get; set; }

        public bool IsCancelledBeforeStart { get; set; }

        public Car? Car { get; set; }
        public Customer? Customer { get; set; }
        public Branch? Branch { get; set; }

        public Rental() { }
        public Rental(int id, int? carId, int? customerId, int branchId, DateTime startDate, DateTime endDate, int days, decimal cost, bool isCompleted, DateTime? completedDate, bool isCancelledBeforeStart, Car? car, Customer? customer, Branch? branch)
        {
            Id = id;
            CarId = carId;
            CustomerId = customerId;
            BranchId = branchId;
            StartDate = startDate;
            EndDate = endDate;
            Days = days;
            Cost = cost;
            IsCompleted = isCompleted;
            CompletedDate = completedDate;
            IsCancelledBeforeStart = isCancelledBeforeStart;
            Car = car;
            Customer = customer;
            Branch = branch;
        }

        public override string ToString()
        {
            string rentalInfo =
                $"  [{Id}] 🚗 {Car?.Brand} {Car?.Model}\n" +
                $"      👤 {Customer?.FirstName} {Customer?.LastName}\n" +
                $"      📅 {StartDate:dd.MM.yyyy} - {EndDate:dd.MM.yyyy} ({Days} dni)\n" +
                $"      💰 {Cost:C}";

            if (IsCompleted && CompletedDate.HasValue && !IsCancelledBeforeStart)
            {
                rentalInfo += $" | ✅ Zakończono: {CompletedDate:dd.MM.yyyy HH:mm}";
            }

            if (IsCancelledBeforeStart && IsCompleted)
            {
                rentalInfo += $" | ❌ Anulowano: {CompletedDate:dd.MM.yyyy HH:mm}";
            }

            return rentalInfo;
        }
    }
}