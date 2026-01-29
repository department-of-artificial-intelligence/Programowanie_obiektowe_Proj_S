﻿using Project.DTO;
using Project.Model;
using Project.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Project.UI.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IMovieService _movieService;
    private readonly IAuthorService _authorService;
    private readonly IServiceProvider _serviceProvider;
    private Genre? _appliedGenreFilter;
    private AuthorDisplayItem? _appliedAuthorFilter;
    private string _appliedMovieNameFilter = string.Empty;

    public ObservableCollection<MovieDto> Movies { get; set; }
    public ObservableCollection<AuthorDisplayItem> Authors { get; set; }

    public Genre? AppliedGenreFilter
    {
        get => _appliedGenreFilter;
        set
        {
            if (_appliedGenreFilter != value)
            {
                _appliedGenreFilter = value;
                OnPropertyChanged();
            }
        }
    }

    public AuthorDisplayItem? AppliedAuthorFilter
    {
        get => _appliedAuthorFilter;
        set
        {
            if (_appliedAuthorFilter != value)
            {
                _appliedAuthorFilter = value;
                OnPropertyChanged();
            }
        }
    }

    public string AppliedMovieNameFilter
    {
        get => _appliedMovieNameFilter;
        set
        {
            if (_appliedMovieNameFilter != value)
            {
                _appliedMovieNameFilter = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand ApplyFiltersCommand { get; }
    public ICommand AddMovieCommand { get; }
    public ICommand AddAuthorCommand { get; }
    public ICommand EditMovieCommand { get; }

    public MainViewModel(IMovieService movieService, IAuthorService authorService, IServiceProvider serviceProvider)
    {
        _movieService = movieService;
        _authorService = authorService;
        _serviceProvider = serviceProvider;
        Movies = new ObservableCollection<MovieDto>();
        Authors = new ObservableCollection<AuthorDisplayItem>();
        ApplyFiltersCommand = new AsyncRelayCommand(async _ => await ApplyFiltersAsync());
        AddMovieCommand = new RelayCommand(_ => OpenAddMovieWindow());
        AddAuthorCommand = new RelayCommand(_ => OpenAddAuthorWindow());
        EditMovieCommand = new RelayCommand(movie => OpenEditMovieWindow((MovieDto)movie!));
        // Load initial data
        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        await LoadAuthorsAsync();
        await GetAllMoviesAsync();
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
    
    public async Task GetAllMoviesAsync()
    {
        Movies.Clear();

        var movies = await _movieService.GetAllAsync();

        foreach (var movie in movies)
        {
            Movies.Add(movie);
        }
    }
    
    private async Task ApplyFiltersAsync()
    {
        Movies.Clear();
        IEnumerable<MovieDto> filteredMovies;

        // filters
        if (!string.IsNullOrWhiteSpace(AppliedMovieNameFilter))
        {
            // Search by title
            filteredMovies = await _movieService.SearchByTitleAsync(AppliedMovieNameFilter);
        }
        else if (AppliedGenreFilter.HasValue)
        {
            // Filter by genre
            filteredMovies = await _movieService.GetMoviesByGenreAsync(AppliedGenreFilter.Value);
        }
        else if (AppliedAuthorFilter != null)
        {
            // Filter by author
            filteredMovies = await _movieService.GetMoviesByAuthorAsync(AppliedAuthorFilter.Author.Id);
        }
        else
        {
            filteredMovies = await _movieService.GetAllAsync();
        }
        
        if (!string.IsNullOrWhiteSpace(AppliedMovieNameFilter) && AppliedGenreFilter.HasValue)
        {
            filteredMovies = filteredMovies.Where(m => m.Genre == AppliedGenreFilter.Value);
        }

        if (!string.IsNullOrWhiteSpace(AppliedMovieNameFilter) && AppliedAuthorFilter != null)
        {
            filteredMovies = filteredMovies.Where(m => m.AuthorId == AppliedAuthorFilter.Author.Id);
        }

        if (AppliedGenreFilter.HasValue && AppliedAuthorFilter != null)
        {
            filteredMovies = filteredMovies.Where(m => m.AuthorId == AppliedAuthorFilter.Author.Id);
        }

        foreach (var movie in filteredMovies)
        {
            Movies.Add(movie);
        }
    }

    private void OpenAddMovieWindow()
    {
        var movieViewModel = _serviceProvider.GetRequiredService<MovieViewModel>();
        var movieWindow = new Views.MovieWindow(movieViewModel);
        
        var result = movieWindow.ShowDialog();
        
        if (result == true)
        {
            _ = GetAllMoviesAsync();
        }
    }

    private void OpenEditMovieWindow(MovieDto movie)
    {
        var movieViewModel = new MovieViewModel(
            _serviceProvider.GetRequiredService<IMovieService>(),
            _serviceProvider.GetRequiredService<IAuthorService>(),
            movie);
        
        var movieWindow = new Views.MovieWindow(movieViewModel);
        
        var result = movieWindow.ShowDialog();
        
        if (result == true)
        {
            _ = GetAllMoviesAsync();
        }
    }

    private void OpenAddAuthorWindow()
    {
        var authorViewModel = _serviceProvider.GetRequiredService<AuthorViewModel>();
        var authorWindow = new Views.AuthorWindow(authorViewModel);
        
        var result = authorWindow.ShowDialog();
        
        if (result == true)
        {
            _ = LoadAuthorsAsync();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

