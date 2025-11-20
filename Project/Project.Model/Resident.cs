using Project.Model.Abstract;
using System.Diagnostics.CodeAnalysis;
using Project.Model.Utils;

namespace Project.Model
{
    public record Resident : IdentifiableEntity<ulong>, IResident
    {
        public required Person Person { get; set; }

        public required DateTime ResidentFrom { get; set; }

        public Resident() : base(UlongIdGenerator.GenerateId()) { }

        [SetsRequiredMembers]
        public Resident(Person person, DateTime residentFrom)
            : base(UlongIdGenerator.GenerateId())
            => (this.Person, this.ResidentFrom) = (person, residentFrom);

        [SetsRequiredMembers]
        public Resident(string firstName, string lastName, DateTime dateOfBirth, DateTime residentFrom)
            : base(UlongIdGenerator.GenerateId())
            => (this.Person, this.ResidentFrom) = (new Person(firstName, lastName, dateOfBirth), residentFrom);
    }
}
