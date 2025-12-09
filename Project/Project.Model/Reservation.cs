using System;

namespace RestaurantManagement.Models
{
    public class Reservation
    {
        public int Id { get; set; } // PK
        public int RestaurantId { get; set; } // FK
        public Restaurant Restaurant { get; set; } = null!; // nawigacja

        public string CustomerName { get; set; } = null!;
        public int NumberOfPeople { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeOnly Time { get; set; }

        public override string ToString()
        {
            return $"{CustomerName} - {NumberOfPeople} osób, {Date:g}";
        }
    }
}
