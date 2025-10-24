namespace Project.Model;

public class User : BaseEntity<int>
{

    public required string Username { get; set; }
    public required string HashPassword { get; set; }

    public User(string username, string hashPassword)
    {
        Username = username;
        HashPassword = hashPassword;
    }
}