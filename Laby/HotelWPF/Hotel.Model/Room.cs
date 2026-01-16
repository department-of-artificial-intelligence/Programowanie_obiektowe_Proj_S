namespace Hotel.Model
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Number { get; set; }
        public int NumberOfPeople { get; set; }
        public decimal PricePerDay { get; set; }
        public string Status { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public Room() { }

        public Room(string number, int numberOfPeople, decimal pricePerDay)
        {
            Number = number;
            NumberOfPeople = numberOfPeople;
            PricePerDay = pricePerDay;
            Status = "available";
        }

        public override string ToString()
        {
            return $"Pokój {Number} (Liczba osób: {NumberOfPeople}, Cena: {PricePerDay:C}, Status: {Status})";
        }
    }
}