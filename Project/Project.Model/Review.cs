namespace Project.Model;
public class Review : BaseEntity<int>
{
    public User User { get; set; }
    public Movie Movie { get; set; }
    public float Rate { get; set; } // between 0 and 5 
    public string Comment { get; set; }

    public Review(User user, Movie movie, float rate, string comment)
    {
        User = user;
        Movie = movie;
        Rate = rate;
        Comment = comment;
    }
}
