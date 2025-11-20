using System.Diagnostics.CodeAnalysis;
using Project.Model.Abstract;
using Project.Model.Utils;

namespace Project.Model
{
    public record Manager : IdentifiableEntity<ulong>
    {
        public required Person Person { get; set; }

        public Manager() : base(UlongIdGenerator.GenerateId()) { }

        [SetsRequiredMembers]
        public Manager(Person person)
            : base(UlongIdGenerator.GenerateId())
            => this.Person = person;

        [SetsRequiredMembers]
        public Manager(string firstName, string lastName, DateTime dateOfBirth)
            : base(UlongIdGenerator.GenerateId())
            => this.Person = new Person(firstName, lastName, dateOfBirth);
    }
}
