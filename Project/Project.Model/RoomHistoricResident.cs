using Project.Model.Abstract;

namespace Project.Model
{
    public record RoomHistoricResident : IResident
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required Person Person { get; set; }

        public required DateTime ResidentFrom { get; set; }

        public required DateTime ResidentTo { get; set; }

        public static RoomHistoricResident FromResident(Resident resident, DateTime? residentTo = null /* DateTime.UtcNow if null */)
        {
            return new RoomHistoricResident()
            {
                Person = resident.Person,
                ResidentFrom = resident.ResidentFrom,
                ResidentTo = residentTo ?? DateTime.UtcNow
            };
        }
    }
}
