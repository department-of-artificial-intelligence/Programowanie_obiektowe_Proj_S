using System;

namespace CinemaNetworkManagement.Model
{
    public class ReducedTicket : Ticket
    {
        public decimal DiscountPercent { get; }

        public ReducedTicket(int id, Customer customer, Screening screening, Seat seat, decimal discountPercent)
            : base(id, customer, screening, seat)
        {
            if (discountPercent < 0m || discountPercent > 100m)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(discountPercent), "Znizka musi nalezec do zakresu od 0 do 100 procent.");
            }

            DiscountPercent = discountPercent;
        }

        public override decimal CalculatePrice()
        {
            return BasePriceAtCreation * (1m - DiscountPercent / 100m);
        }
    }
}
