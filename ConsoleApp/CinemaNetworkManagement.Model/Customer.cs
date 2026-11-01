namespace CinemaNetworkManagement.Model
{
    public class Customer : Person
    {
        public string Email { get; set; }
        public int LoyaltyPoints { get; set; }

        public Customer(int id, string firstName, string lastName, string email, int loyaltyPoints)
            : base(id, firstName, lastName)
        {
            Email = email;
            LoyaltyPoints = loyaltyPoints;
        }

        public override string GenerateDescription()
        {
            return $"Klient: {base.GenerateDescription()}, e-mail: {Email}";
        }
    }
}
