using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{
    public record Resident
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required Person Person { get; set; }

        public DateTime ResidentFrom { get; set; }

        public Resident() { }

        [SetsRequiredMembers]
        public Resident(Person person, DateTime residentFrom) => (this.Person, this.ResidentFrom) = (person, residentFrom);

        [SetsRequiredMembers]
        public Resident(string firstName, string lastName, DateTime residentFrom) => (this.Person, this.ResidentFrom) = (new Person(firstName, lastName), residentFrom);
    }
}
