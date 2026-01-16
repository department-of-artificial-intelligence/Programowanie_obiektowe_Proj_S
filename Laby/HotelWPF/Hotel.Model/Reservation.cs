namespace Hotel.Model
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public int GuestId { get; set; }
        public virtual Client Client { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalCost { get; set; }

        public Reservation() { }

        public Reservation(Client client, Room room, DateTime start, DateTime end, decimal cost)
        {
            Client = client;
            Room = room;
            StartTime = start;
            EndTime = end;
            TotalCost = cost;
        }

        public override string ToString()
        {
            return $"Rezerwacja: {Client.FirstName} {Client.LastName}, Pokój: {Room?.Number}, Koszt: {TotalCost:C}, Od: {StartTime.ToShortDateString()} Do: {EndTime.ToShortDateString()}";
        }
    }
}