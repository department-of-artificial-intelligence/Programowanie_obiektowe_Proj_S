using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{
    public record Manager
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required Person Person { get; set; }

        public Manager() { }

        [SetsRequiredMembers]
        public Manager(Person person) => this.Person = person;

        [SetsRequiredMembers]
        public Manager(string firstName, string lastName) => this.Person = new Person(firstName, lastName);
    }
}
