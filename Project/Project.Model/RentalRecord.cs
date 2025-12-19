using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class RentalRecord
    {
        [Key]
        public int Id { get; set; }

        
        public int BicycleId { get; set; }
        public virtual Bicycle Bicycle { get; set; }

        
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; } 

        
        public decimal? TotalCost { get; set; } 
    }
}