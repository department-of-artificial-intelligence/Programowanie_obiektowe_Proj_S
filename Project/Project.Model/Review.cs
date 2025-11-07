using System.Diagnostics.CodeAnalysis;

namespace Project.Model;
public class Review : BaseEntity<int>
{
    public User User { get; set; }
    public Movie Movie { get; set; }
    public float Rate { get; set; } // between 0 and 5 
    public string Comment { get; set; }

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
