namespace Project.Model
{
    public record RoomHistoryEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required Person HistoricResident { get; set; }

        public required DateTime ResidentFrom { get; set; }

        public required DateTime ResidentTo { get; set; }

        public static RoomHistoryEntry FromResident(Resident resident, DateTime? residentTo = null /* DateTime.UtcNow if null */)
        {
            return new RoomHistoryEntry()
            {
                HistoricResident = resident.Person,
                ResidentFrom = resident.ResidentFrom,
                ResidentTo = residentTo ?? DateTime.UtcNow
            };
        }
    }
}
