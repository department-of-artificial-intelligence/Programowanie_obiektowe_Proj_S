using System.Diagnostics.CodeAnalysis;
using Project.Model.Abstract;
using Project.Model.Utils;

namespace Project.Model
{
    public record Person : IdentifiableEntity<ulong>
    {
        public required string FirstName { get; set; }
        
        public required string LastName { get; set; }
        
        public required DateTime DateOfBirth { get; set; }

        public Person() : base(UlongIdGenerator.GenerateId()) { }
        
        [SetsRequiredMembers]
        public Person(string firstName, string lastName, DateTime dateOfBirth)
            : base(UlongIdGenerator.GenerateId())
            => (this.FirstName, this.LastName, this.DateOfBirth) = (firstName, lastName, dateOfBirth);
    }
}
