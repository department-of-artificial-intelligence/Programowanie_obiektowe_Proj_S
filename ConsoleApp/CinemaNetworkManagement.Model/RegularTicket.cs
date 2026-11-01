namespace CinemaNetworkManagement.Model
{
    public class RegularTicket : Ticket
    {
        public RegularTicket(int id, Customer customer, Screening screening, Seat seat)
            : base(id, customer, screening, seat)
        {
        }

        public override decimal CalculatePrice()
        {
            return BasePriceAtCreation;
        }
    }
}
