
namespace Project.Model;

// Enum for MovieList.cs to identify Movie type for user as Favorite, Watched, WatchLater 
public class MovieMark : BaseEntity<int>
{
    public User User { get; set; }
    public Movie Movie { get; set; }
    public MovieMarkType Type { get; set; }

    public MovieMark(User user, Movie movie, MovieMarkType type)
    {
        User = user;
        Movie = movie;
        Type = type;
    }
}
