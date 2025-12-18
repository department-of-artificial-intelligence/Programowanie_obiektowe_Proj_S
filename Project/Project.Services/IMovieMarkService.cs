using Project.DTO;
using Project.Model;

namespace Project.Services;

public interface IMovieMarkService : IBaseService<MovieMark, int, MovieMarkDto>
{
    Task<IEnumerable<MovieMarkDto>> GetMovieMarksByUserAsync(int userId);
    Task<IEnumerable<MovieMarkDto>> GetMovieMarksByTypeAsync(int userId, MovieMarkType type);
    Task<MovieMarkDto?> GetUserMovieMarkAsync(int userId, int movieId);
}

