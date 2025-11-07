using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Project.Model;
public class BaseEntity<T>
{
    public required T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [SetsRequiredMembers]
    public BaseEntity(T id, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    [SetsRequiredMembers]
    public BaseEntity(T id) {
        Id = id;
    }
}
