namespace Project.DTO;

public class ReviewDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public UserDto? User { get; set; }
    public MovieDto? Movie { get; set; }
    public float Rate { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

