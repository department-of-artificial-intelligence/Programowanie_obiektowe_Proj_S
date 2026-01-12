using Project.Interfaces;
using Project.Utils;

namespace Project.Models.Common
{
    public abstract class Base : IEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

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