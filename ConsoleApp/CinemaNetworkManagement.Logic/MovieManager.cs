using CinemaNetworkManagement.Model;

namespace CinemaNetworkManagement.Logic
{
    public class MovieManager
    {
        private readonly IMoviesSource _movies;

        public MovieManager(
            IMoviesSource movies)
        {
            _movies = movies;
        }

    }
}
