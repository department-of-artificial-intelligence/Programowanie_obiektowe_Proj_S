using Project.Interfaces;
using Project.Utils;

namespace Project.Models.Common
{
    public abstract class Base : IEntity
    {
        public string Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected Base()
        {
            Id = IdGenerator.Generate();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        protected Base(string id, DateTime createdAt, DateTime updatedAt)
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