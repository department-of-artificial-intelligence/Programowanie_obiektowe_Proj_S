using Project.Model;

namespace Project.DTO;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? TagLine { get; set; }
    public Genre Genre { get; set; }
    public AuthorDto? Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

