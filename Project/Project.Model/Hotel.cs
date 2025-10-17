namespace Project.Model
{
    public record Hotel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }

        public required string Address { get; set; }

        public required Manager Manager { get; set; }

        public required IEnumerable<HotelRoom> Rooms { get; set; }
    }
}
