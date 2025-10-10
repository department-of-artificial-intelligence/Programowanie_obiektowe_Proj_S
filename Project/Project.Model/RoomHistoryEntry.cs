namespace Project.Model
{
    public record RoomHistoryEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required Person Resident { get; set; }

        public required DateTime ResidentFrom { get; set; }

        public required DateTime ResidentTo { get; set; }
    }
}
