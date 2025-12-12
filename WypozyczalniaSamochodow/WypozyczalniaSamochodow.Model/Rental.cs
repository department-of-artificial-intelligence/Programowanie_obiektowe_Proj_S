namespace WypozyczalniaSamochodow.Model
{
    public class Rental
    {
        public int Id { get; set; }
        public Car? Car { get; set; }
        public Customer? Customer { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public double Cost { get; set; }

        public Rental() { }
        public Rental(int id, Car? car, Customer? customer, DateTime startDate, DateTime endDate, int days, double cost)
        {
            Id = id;
            Car = car;
            Customer = customer;
            StartDate = startDate;
            EndDate = endDate;
            Days = days;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"[{Id}]: {Car?.Brand} {Car?.Model}, Klient: {Customer?.FirstName} {Customer?.LastName}, " +
                   $"Okres: {StartDate.ToShortDateString()} - {EndDate.ToShortDateString()} ({Days} dni), Koszt: {Cost} zł";
        }
    }
}