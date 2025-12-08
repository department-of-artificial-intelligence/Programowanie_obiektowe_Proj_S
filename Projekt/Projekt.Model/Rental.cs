using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Rental
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
        public int PickupBranchId { get; set; }
        public virtual Branch PickupBranch { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public decimal TotalCost { get; set; }
        public RentalStatus Status { get; set; }

        public Rental() { }

        public override string ToString()
        {
            string status = (ActualReturnDate == null) ? "W TOKU" : "Zakończone";
            return $"[Wypożyczenie #{Id}] Pojazd: {CarId}, Klient: {CustomerId}, Data: {StartDate.ToShortDateString()} - {status}";
        }
    }
}

