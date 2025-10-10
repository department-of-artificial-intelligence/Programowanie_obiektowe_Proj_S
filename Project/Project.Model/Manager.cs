namespace Project.Model
{
    public record Manager
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required Person Person { get; set; }

        public required Hotel Hotel { get; set; }
    }
}
