using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Project.Domain;
using Project.DTO;
using Project.Services;
using System.Windows;

namespace Project.UI;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        ServiceCollection serviceDescriptors = new ServiceCollection();
        ConfigureServices(serviceDescriptors);
        _serviceProvider = serviceDescriptors.BuildServiceProvider();
    }

    public void ConfigureServices(ServiceCollection services)
    {
        // Configure Database here
        services.AddSingleton(DatabaseConfiguration.Configure(new[] { "-e" }));
        //services.AddSingleton<MapperConfiguration>();
        var mapCfg = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        services.AddSingleton(mapCfg.CreateMapper());
        services.AddTransient<IMovieService, MovieService>();
        services.AddTransient<IAuthorService, AuthorService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IReviewService, ReviewService>();
        services.AddTransient<IMovieMarkService, MovieMarkService>();
        
        services.AddTransient<ViewModel.MainViewModel>();
        services.AddTransient<ViewModel.MovieViewModel>();
        services.AddTransient<ViewModel.AuthorViewModel>();
        services.AddTransient<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

}

// OLD CODE | DEPRECATED
// OLD CODE | DEPRECATED
// OLD CODE | DEPRECATED
// TODO: Fix bug with SQL Server not configuring: Cannnot find the DefaultStringConnection
// DefaultStringConnection should be: (localdb)\\mssqllocaldb
//var context = DatabaseConfiguration.Configure(new string[] { "--use-inmemory" });

//context.SeedDatabase();

//// Debug
//Console.WriteLine("Database has been configured and initialized successfully!");

//var mapperConfig = new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile<MappingProfile>();
//});

//var mapper = mapperConfig.CreateMapper();
//_userService = new UserService(context, mapper);
//_authorService = new AuthorService(context, mapper);
//_movieService = new MovieService(context, mapper);
//_reviewService = new ReviewService(context, mapper);
//_movieMarkService = new MovieMarkService(context, mapper);
