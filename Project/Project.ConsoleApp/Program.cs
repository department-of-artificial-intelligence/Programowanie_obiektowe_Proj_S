namespace Project.ConsoleApp
{

    using Project.Model;

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("- - - APPLICATION TEST - - -");

            // Create first user
            User user1 = new User(1, "User1", "qwewqe123");
            Console.WriteLine(user1);

            // Create first Author
            Author author1 = new(1, "author1", "mm", new DateTime());
            Console.WriteLine(author1);

            // Now, create first movie
            Movie mov1 = new Movie(1, "Star Wars", "Some description", "About wars in cosmos", Genre.ADVENTURE, author1, new DateTime());
            Console.WriteLine(mov1);

            // Mark mov1 as Favorite for user1
            MovieMark mark = new(1, user1, mov1, MovieMarkType.FAVORITE);

            // Add feedback to mov1 as user1
            Review mov1Review = new Review(1, user1, mov1, 4.5f, "I would like to watch it one more time!!");
            Console.WriteLine(mov1Review);

            Console.WriteLine("- - - APPLICATION TEST PASSED SUCCESSFULLY - - -");
        }
    }
}
