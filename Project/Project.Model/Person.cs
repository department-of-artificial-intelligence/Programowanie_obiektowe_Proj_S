using System.Diagnostics.CodeAnalysis;

namespace Project.Model
{
    public record Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public required string FirstName { get; set; }
        
        public required string LastName { get; set; }
    
        public Person() { }

        [SetsRequiredMembers]
        public Person(string firstName, string lastName) => (this.FirstName, this.LastName) = (firstName, lastName);
    }
}
