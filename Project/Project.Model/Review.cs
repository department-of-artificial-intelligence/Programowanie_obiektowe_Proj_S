using System.Diagnostics.CodeAnalysis;

namespace Project.Model;
public class Review : BaseEntity<int>
{
    public required User User { get; set; }
    public required Movie Movie { get; set; }
    public required float Rate { get; set; } // between 0 and 5 
    public required string Comment { get; set; }

    [SetsRequiredMembers]
    public Review(int id, User user, Movie movie, float rate, string comment) : base(id)
    {
        User = user;
        Movie = movie;
        Rate = rate;
        Comment = comment;
    }

    public override string ToString()
    {
        return $"Review for film: {Movie.Title} by {User}: {Rate}/{Comment}";
    }
}
