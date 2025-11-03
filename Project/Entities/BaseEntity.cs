using Project.Interfaces;
using Project.Utils;

namespace Project.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public string Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            Id = IdGenerator.Generate();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        protected BaseEntity(string id, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public void MarkAsUpdated()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}