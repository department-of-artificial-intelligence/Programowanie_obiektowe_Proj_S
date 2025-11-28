
using System.Diagnostics.CodeAnalysis;

namespace Project.Model;

// Enum for MovieList.cs to identify Movie type for user as Favorite, Watched, WatchLater 
public class MovieMark : BaseEntity<int>
{
    public required User User { get; set; }
    public required Movie Movie { get; set; }
    public required MovieMarkType Type { get; set; }

    [SetsRequiredMembers]
    public MovieMark(int id, User user, Movie movie, MovieMarkType type) : base(id)
    {
        User = user;
        Movie = movie;
        Type = type;
    }

    [SetsRequiredMembers]
    public MovieMark () : base(0) { }

    public override string ToString()
    {
        return $"MovieMark: {Type} for film: {Movie.Title}";
    }
}
