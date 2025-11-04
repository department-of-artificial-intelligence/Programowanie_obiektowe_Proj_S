namespace RestaurantManagement.Models
{
    public class Reservation
    {
        public required string CustomerName { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{CustomerName} - {NumberOfPeople} osób, {Date:g}";
        }
    }
}
