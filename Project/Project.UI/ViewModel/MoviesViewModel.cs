using Project.DTO;
using Project.Model;
using Project.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.UI.ViewModel;
public class MoviesViewModel : INotifyPropertyChanged
{

    private readonly IMovieService _movieService;
    private readonly IServiceProvider _serviceProvider;
    public Genre _appliedGenreFilter;
    public AuthorDto _appliedAuthorFilter;
    public String _appliedMovieNameFilter;

    public ObservableCollection<MovieDto> Movies;
   
    public MoviesViewModel(IMovieService _service, IServiceProvider provider)
    {
        _movieService = _service;
        _serviceProvider = provider;

        Movies = new ObservableCollection<MovieDto>();
    }

    public ICommand applyFiltersCommand { get; set; }


    // TODO: Implement with Filtrations
    public async Task GetAllMovies(object? parameter)
    {
        Movies.Clear();

        var movies = await _movieService.GetAllAsync();

        foreach (var movie in movies) {
            Movies.Add(movie);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}

