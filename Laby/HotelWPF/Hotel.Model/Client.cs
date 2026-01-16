namespace Hotel.Model
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public Client() { }

        public Client(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Email})";
        }
    }
}
