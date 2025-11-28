using System.Diagnostics.CodeAnalysis;

namespace Project.Model;

public class User : BaseEntity<int>
{

    public required string Username { get; set; }
    public required string HashPassword { get; set; }

    [SetsRequiredMembers]
    public User(int id, string username, string hashPassword) : base(id)
    {
        Username = username;
        HashPassword = hashPassword;
    }

    [SetsRequiredMembers]
    public User() : base(0) { }

    public override string ToString()
    {
        return $"User: {Username}";
    }
}