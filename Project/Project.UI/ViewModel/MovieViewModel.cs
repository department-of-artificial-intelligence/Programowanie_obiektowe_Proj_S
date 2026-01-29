using Project.DTO;
using Project.Model;
using Project.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Project.UI.ViewModel;

public class MovieViewModel : INotifyPropertyChanged
{
    private readonly IMovieService _movieService;
    private readonly IAuthorService _authorService;
    private readonly MovieDto? _existingMovie;
    
    private string _title = string.Empty;
    private string? _tagLine;
    private string? _description;
    private Genre _selectedGenre;
    private AuthorDisplayItem? _selectedAuthor;
    public string WindowTitle => _existingMovie == null ? "Add New Movie" : "Edit Movie";
    public bool IsEditMode => _existingMovie != null;

    public string Title
    {
        get => _title;
        set
        {
            if (_title != value)
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }

    public string? TagLine
    {
        get => _tagLine;
        set
        {
            if (_tagLine != value)
            {
                _tagLine = value;
                OnPropertyChanged();
            }
        }
    }

    public string? Description
    {
        get => _description;
        set
        {
            if (_description != value)
            {
                _description = value;
                OnPropertyChanged();
            }
        }
    }

    public Genre SelectedGenre
    {
        get => _selectedGenre;
        set
        {
            if (_selectedGenre != value)
            {
                _selectedGenre = value;
                OnPropertyChanged();
            }
        }
    }

    public AuthorDisplayItem? SelectedAuthor
    {
        get => _selectedAuthor;
        set
        {
            if (_selectedAuthor != value)
            {
                _selectedAuthor = value;
                OnPropertyChanged();
            }
        }
    }
    

    public ObservableCollection<Genre> AvailableGenres { get; }
    public ObservableCollection<AuthorDisplayItem> Authors { get; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteCommand { get; }

    public event EventHandler<bool>? RequestClose;
    public event PropertyChangedEventHandler? PropertyChanged;

    public MovieViewModel(IMovieService movieService, IAuthorService authorService, MovieDto? existingMovie = null)
    {
        _movieService = movieService;
        _authorService = authorService;
        _existingMovie = existingMovie;

        AvailableGenres = new ObservableCollection<Genre>(Enum.GetValues<Genre>());
        Authors = new ObservableCollection<AuthorDisplayItem>();

        SaveCommand = new AsyncRelayCommand(async _ => await SaveAsync());
        CancelCommand = new RelayCommand(_ => Cancel());
        DeleteCommand = new AsyncRelayCommand(async _ => await DeleteAsync());

        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        await LoadAuthorsAsync();

        if (_existingMovie != null)
        {
            Title = _existingMovie.Title;
            TagLine = _existingMovie.TagLine;
            Description = _existingMovie.Description;
            SelectedGenre = _existingMovie.Genre;
            SelectedAuthor = Authors.FirstOrDefault(a => a.Author.Id == _existingMovie.AuthorId);
        }
        else
        {
            SelectedGenre = Genre.ACTION;
        }
    }

    private async Task LoadAuthorsAsync()
    {
        Authors.Clear();
        var authors = await _authorService.GetAllAsync();

        foreach (var author in authors)
        {
            Authors.Add(new AuthorDisplayItem(author));
        }
    }

    private async Task SaveAsync()
    {
        // Validation
        if (string.IsNullOrWhiteSpace(Title))
        {
            return;
        }

        if (SelectedAuthor == null)
        {
            return;
        }

        try
        {
            var movieDto = new MovieDto
            {
                Id = _existingMovie?.Id ?? 0,
                Title = Title,
                TagLine = TagLine,
                Description = Description,
                Genre = SelectedGenre,
                AuthorId = SelectedAuthor.Author.Id,
                Author = SelectedAuthor.Author
            };

            if (IsEditMode)
            {
                await _movieService.UpdateAsync(_existingMovie!.Id, movieDto);
            }
            else
            {
                await _movieService.CreateAsync(movieDto);
            }
            
            RequestClose?.Invoke(this, true);
        }
        catch (Exception ex)
        {
            
        }
    }

    private void Cancel()
    {
        RequestClose?.Invoke(this, false);
    }

    private async Task DeleteAsync()
    {
        if (_existingMovie == null) return;

        try
        {
            await _movieService.DeleteAsync(_existingMovie.Id);
            RequestClose?.Invoke(this, true);
        }
        catch (Exception ex)
        {
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
