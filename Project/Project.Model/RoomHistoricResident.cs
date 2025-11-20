using Project.Model.Abstract;
using Project.Model.Utils;

namespace Project.Model
{
    public record RoomHistoricResident : IdentifiableEntity<ulong>, IResident
    {
        public required Person Person { get; set; }

        public required DateTime ResidentFrom { get; set; }

        public required DateTime ResidentTo { get; set; }

        public RoomHistoricResident() : base(UlongIdGenerator.GenerateId()) { }
        
        public RoomHistoricResident(Person person, DateTime residentFrom, DateTime residentTo)
            : base(UlongIdGenerator.GenerateId())
            => (this.Person, this.ResidentFrom, this.ResidentTo) = (person, residentFrom, residentTo);
        
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
