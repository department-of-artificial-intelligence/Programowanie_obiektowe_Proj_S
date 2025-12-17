namespace RatingSystem.Domain;

public class User
{
    public string? UserName { get; set; }
    public int UserId { get; set; }

    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}