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
        services.AddSingleton(DatabaseConfiguration.Configure(new[] { "--use-inmemory" }));
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