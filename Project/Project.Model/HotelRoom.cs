namespace Project.Model
{
    public class HotelRoom
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required Hotel Hotel { get; set; }

        public required int Number { get; set; }
    
        public required int Floor { get; set; }

        public IEnumerable<RoomHistoryEntry> History { get; set; }
    }
}
