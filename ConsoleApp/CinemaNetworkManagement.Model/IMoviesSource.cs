using System.Collections.Generic;

namespace CinemaNetworkManagement.Model
{
    public interface IMoviesSource
    {
        List<Movie> AllMovies();
        bool AddMovie(Movie movie);
        bool UpdateMovie(Movie movie);
    }
}
