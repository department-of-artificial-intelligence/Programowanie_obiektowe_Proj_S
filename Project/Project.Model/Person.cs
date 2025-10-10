namespace Project.Model
{
    public record Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string FirstName { get; set; }
        
        public required string LastName { get; set; }

        public required int Age { get; set; }
    }
}
