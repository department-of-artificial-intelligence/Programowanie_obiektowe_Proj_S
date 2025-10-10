namespace Project.Model
{
    public record Resident
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required Person Person { get; set; }

        public required HotelRoom HotelRoom { get; set; }

        public DateTime ResidentFrom { get; set; }
    }
}
