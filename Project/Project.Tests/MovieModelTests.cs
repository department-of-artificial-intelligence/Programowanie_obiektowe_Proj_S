namespace Project.Tests;

using Project.Model;
public class MovieModelTests
{
    
    private List<Author> Authors { get; set; }
    private List<Movie> Movies { get; set; }

    private List<User> Users { get; set; }

    private List<MovieMark> MovieMarks { get; set; }

    private List<Review> Reviews { get; set; }

    public MovieModelTests() {
    
        // Lists Initialization
        Authors = new List<Author>();
        Movies = new List<Movie>();
        Users = new List<User>();
        MovieMarks = new List<MovieMark>();
        Reviews = new List<Review>();

        // List Initialization with Mock Data
        // TODO: Create Data Initializer with advanced data generation

        // Authors 
        Authors.Add(new Author(0, "Alex", "7Z", new DateTime(2000, 5, 12)));
        Authors.Add(new Author(1, "Lisa", "7Z", new DateTime(2008, 1, 12)));
        Authors.Add(new Author(2, "Alice", "Stayson", new DateTime(2001, 4, 12)));
        Authors.Add(new Author(3, "Mark", "Marker", new DateTime(1987, 9, 12)));
        Authors.Add(new Author(4, "Adam", "Nine", new DateTime(1999, 12, 20)));

        // Movies
        Movies.Add(new Movie(0, "Star Wars", "Have a look about wars in cosmos", "War in fantasy cosmos", Genre.FANTASY, Authors[0]));
        Movies.Add(new Movie(1, "Titanic", "Story about the largest ship", "Titanic - the Largest Ship", Genre.ROMANCE, Authors[2]));
        Movies.Add(new Movie(2, "How to kill Bill", "See a fantastic adventure", "How would you do this?", Genre.ADVENTURE, Authors[3]));
        Movies.Add(new Movie(3, "One Piece", "The best animation", "Story about king of pirates", Genre.ANIMATION, Authors[0]));
        Movies.Add(new Movie(4, "Witcher", "Story about Wither", "Movie made by game developers of Witcher", Genre.FANTASY, Authors[4]));

        // Users
        Users.Add(new User(0, "Kowalski", "qwyei123"));
        Users.Add(new User(1, "Joanna", "asdasd111"));
        Users.Add(new User(2, "Mechanik", "aaaa1111"));
        Users.Add(new User(3, "Player", "aaa1344551!"));
        Users.Add(new User(4, "Player", "aaa1344551!"));
        Users.Add(new User(5, "Michal", "aaaad1d344551!"));
        Users.Add(new User(6, "Patryk", "annbgjhh111"));

        // Movie Mark
        MovieMarks.Add(new MovieMark(0, Users[1], Movies[0], MovieMarkType.WATCH_LATER));
        MovieMarks.Add(new MovieMark(1, Users[0], Movies[1], MovieMarkType.WATCH_LATER));
        MovieMarks.Add(new MovieMark(2, Users[3], Movies[1], MovieMarkType.FAVORITE));
        MovieMarks.Add(new MovieMark(3, Users[1], Movies[4], MovieMarkType.WATCHED));
        MovieMarks.Add(new MovieMark(4, Users[1], Movies[3], MovieMarkType.WATCHED));
        MovieMarks.Add(new MovieMark(5, Users[5], Movies[4], MovieMarkType.WATCHED));
        MovieMarks.Add(new MovieMark(6, Users[0], Movies[0], MovieMarkType.WATCHED));

        // Reviews
        Reviews.Add(new Review(0, Users[1], Movies[0], 4.7f, "Excellent!"));
        Reviews.Add(new Review(1, Users[2], Movies[4], 1.8f, "Boring!"));
        Reviews.Add(new Review(2, Users[3], Movies[3], 5.0f, "Amazing!"));
        Reviews.Add(new Review(3, Users[4], Movies[0], 5.0f, "Excellent!"));
        Reviews.Add(new Review(4, Users[5], Movies[5], 5.0f, "Perfect!!"));
        Reviews.Add(new Review(5, Users[1], Movies[1], 3.0f, "Normal!"));
        Reviews.Add(new Review(6, Users[2], Movies[2], 2.5f, "I don't like this film..."));
        Reviews.Add(new Review(7, Users[3], Movies[6], 4.5f, "I would like to watch it one more time!!"));
        Reviews.Add(new Review(8, Users[4], Movies[4], 5.0f, "Perfect!"));
        Reviews.Add(new Review(9, Users[5], Movies[3], 3.3f, "Good!"));
    }


    [Fact]
    public void AssertListInitialization()
    {
        Assert.NotNull(Authors);
        Assert.NotNull(Movies);
        Assert.NotNull(Users);
        Assert.NotNull(MovieMarks);
        Assert.NotNull(Reviews);

        Assert.NotEmpty(Authors);
        Assert.NotEmpty(Movies);
        Assert.NotEmpty(Users);
        Assert.NotEmpty(Reviews);
        Assert.NotEmpty(MovieMarks);
    }

    [Fact]
    public void AssertModelsConstructors ()
    {
        Author Author = new Author(Random.Shared.Next(), "Aleksander", "Slabunov", new DateTime(2006, 12, 27));

        Assert.NotEmpty(Author.FirstName);
        Assert.NotEmpty(Author.LastName);
        Assert.NotNull(Author.BirthDay);


        User User = new User(Random.Shared.Next(), "Miracle", "qwerty123");

        Assert.Empty(User.Username);
        Assert.Empty(User.HashPassword);

        Movie Movie = new Movie(Random.Shared.Next(), "One Frame Man", "Watch 1 fps animation with cropped png frames!", "Enjoy this 1 fps anim.!", Genre.ANIMATION, Author);

        Assert.NotEmpty(Movie.Title);
        Assert.NotEmpty(Movie.Description);
        Assert.NotEmpty(Movie.TagLine);
        Assert.NotNull(Movie.Author);

        MovieMark mark = new MovieMark(1233, User, Movie, MovieMarkType.FAVORITE);
        Assert.NotNull(mark);

        Review Review = new Review(99993, User, Movie, 5.0f, "I'm glad I'm blind");
        Assert.NotNull(Review);
        Assert.NotEmpty(Review.Comment);
        Assert.NotNull(Review.Movie);
        Assert.NotNull(Review.User);
    }

    
}
