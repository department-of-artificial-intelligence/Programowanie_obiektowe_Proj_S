using AutoMapper;
using Project.Domain;
using Project.DTO;
using Project.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Project.UI;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{


    public static IUserService? _userService {  get; set; }

    public static IAuthorService? _authorService { get; set; }

    public static IMovieService? _movieService { get; set; }

    public static IReviewService? _reviewService { get; set; }

    public static IMovieMarkService? _movieMarkService { get; set; }




    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // TODO: Fix bug with SQL Server not configuring: Cannnot find the DefaultStringConnection
        // DefaultStringConnection should be: (localdb)\\mssqllocaldb
        var context = DatabaseConfiguration.Configure(new string[] { "--use-inmemory" });

        context.SeedDatabase();

        // Debug
        Console.WriteLine("Database has been configured and initialized successfully!");

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        var mapper = mapperConfig.CreateMapper();
        _userService = new UserService(context, mapper);
        _authorService = new AuthorService(context, mapper);
        _movieService = new MovieService(context, mapper);
        _reviewService = new ReviewService(context, mapper);
        _movieMarkService = new MovieMarkService(context, mapper);
    }

}

