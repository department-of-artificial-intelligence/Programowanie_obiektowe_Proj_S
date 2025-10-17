namespace Project.Model
{
    public record RoomHistoryEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required IEnumerable<Person> HistoricResidents { get; set; }

        public required DateTime ResidentFrom { get; set; }

        public required DateTime ResidentTo { get; set; }
    }
}
