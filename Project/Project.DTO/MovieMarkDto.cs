using Project.Model;

namespace Project.DTO;

public class MovieMarkDto
{
    public int Id { get; set; }
    public UserDto? User { get; set; }
    public MovieDto? Movie { get; set; }
    public MovieMarkType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

