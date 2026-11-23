namespace Gemini.dataModel
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual string GetSummary()
        {
            return $"Entity ID: {Id}";
        }
    }
}