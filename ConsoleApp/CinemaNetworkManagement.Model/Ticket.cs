using System;

namespace CinemaNetworkManagement.Model
{
    public abstract class Ticket
    {
        public int Id { get; }
        public Customer Customer { get; }
        public Screening Screening { get; }
        public Seat Seat { get; }
        public DateTime? PurchasedAt { get; private set; }
        public decimal Price { get; private set; }
        public bool IsCancelled { get; private set; }

        protected decimal BasePriceAtCreation { get; }

        public bool IsSold
        {
            get { return PurchasedAt != null; }
        }

        protected Ticket(int id, Customer customer, Screening screening, Seat seat)
        {
            Id = id;
            Customer = customer;
            Screening = screening;
            Seat = seat;
            BasePriceAtCreation = screening.BasePrice;
            PurchasedAt = null;
            Price = 0m;
            IsCancelled = false;
        }

        
        public abstract decimal CalculatePrice();

        
        
    }
}
