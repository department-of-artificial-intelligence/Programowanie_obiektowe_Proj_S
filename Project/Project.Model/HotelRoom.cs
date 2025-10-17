namespace Project.Model
{
    public record HotelRoom
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required int Number { get; set; }
    
        public required int Floor { get; set; }

        public required IEnumerable<Resident> Residents { get; set; }

        public required IEnumerable<RoomHistoryEntry> History { get; set; }
    }
}
