using Project.DTO;
using Project.Model;

namespace Project.Services;

public interface IMovieService : IBaseService<Movie, int, MovieDto>
{
    Task<IEnumerable<MovieDto>> GetMoviesByGenreAsync(Genre genre);
    Task<IEnumerable<MovieDto>> GetMoviesByAuthorAsync(int authorId);
    Task<IEnumerable<MovieDto>> SearchByTitleAsync(string title);
}

