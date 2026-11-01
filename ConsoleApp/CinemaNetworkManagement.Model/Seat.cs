namespace CinemaNetworkManagement.Model
{
    public class Seat
    {
        public int Id { get; }
        public int Row { get; }
        public int Number { get; }
        public ScreeningRoom Room { get; }

        public Seat(int id, int row, int number, ScreeningRoom room)
        {
            Id = id;
            Row = row;
            Number = number;
            Room = room;
        }
    }
}
