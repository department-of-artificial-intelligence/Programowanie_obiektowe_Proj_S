
using Project.Model;
using Project.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Project.ConsoleApp
{
    public class Program
    {
        // Testing Entity Framework
        static async Task Main(string[] args) {
            IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                var cns = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options=> options.UseSqlServer(cns));
            }).Build();

            var context = _host.Services.GetService<ApplicationDbContext>();
            if (context != null) {
                context.Database.Migrate();
                context.Database.EnsureCreated();

            }
        }


        //static void Main(string[] args)
        //{

        //    Console.WriteLine("- - - - - Testowanie Programu - - - - -");

        //    List<Author> authors = new List<Author>();
        //    authors.Add(new Author(0, "Alex", "7Z", new DateTime(2000, 5, 12)));
        //    authors.Add(new Author(1, "Lisa", "7Z", new DateTime(2008, 1, 12)));
        //    authors.Add(new Author(2, "Alice", "Stayson", new DateTime(2001, 4, 12)));
        //    authors.Add(new Author(3, "Mark", "Marker", new DateTime(1987, 9, 12)));
        //    authors.Add(new Author(4, "Adam", "Nine", new DateTime(1999, 12, 20)));

        //    Console.WriteLine(" Lista Authors:\n");
        //    foreach (Author author in authors)
        //    {
        //        Console.WriteLine(author);
        //    }

        //    Console.WriteLine(" - - - Operacjii LINQ nad autorami - - - \n");

        //    var SevenZAuthors = authors.Where(n => n.LastName.Equals("7Z")).ToList();
        //    Console.WriteLine("Autory z nazwiskiem 7Z:");
        //    foreach (Author author in SevenZAuthors) { Console.WriteLine("\t" + author); }

        //    Console.WriteLine("\n\nAuthory które mają >= 18 lat:");
        //    var authors2 = authors.Where(a => DateTime.Today.Year - a.BirthDay.Year >= 18).ToList();
        //    foreach (Author a in authors2) { Console.WriteLine("\t" + a); }

        //    List<Movie> movies = new List<Movie>();
        //    movies.Add(new Movie(0, "Star Wars", "Have a look about wars in cosmos", "War in fantasy cosmos", Genre.FANTASY, authors[0]));
        //    movies.Add(new Movie(0, "Titanic", "Story about the largest ship", "Titanic - the Largest Ship", Genre.ROMANCE, authors[2]));
        //    movies.Add(new Movie(0, "How to kill Bill", "See a fantastic adventure", "How would you do this?", Genre.ADVENTURE, authors[3]));
        //    movies.Add(new Movie(0, "One Piece", "The best animation", "Story about king of pirates", Genre.ANIMATION, authors[0]));
        //    movies.Add(new Movie(0, "Witcher", "Story about Wither", "Movie made by game developers of Witcher", Genre.FANTASY, authors[4]));

        //    Console.WriteLine("\n\n\nLista Movie");
        //    foreach (Movie m in movies)
        //    {
        //        Console.WriteLine("\t" + m);
        //    }

        //    Console.WriteLine("\n - - - Operacji LINQ nad filmami - - - - ");
        //    var authorsWithFantasyFilms = movies.Where(m => m.Genre == Genre.FANTASY).Select(a => a.Author).ToList();

        //    Console.WriteLine("\n Autory które publikowali filmy z gatunkiem FANTASY");
        //    foreach(Author m in authorsWithFantasyFilms) Console.WriteLine("\t" + m);

        //    var authorsWithoutMoviePublished = authors.Except(movies.Select(a => a.Author)).ToList();
        //    Console.WriteLine("\nAutory które nie publikowali żadnego filma: ");
        //    foreach(Author author in authorsWithoutMoviePublished) Console.WriteLine("\t" + author);


        //}
    }
}
