using CinemaNetworkManagement.Model;

namespace CinemaNetworkManagement.Logic
{
    public class ScreeningManager
    {
        private readonly ICinemasSource _cinemas;
        private readonly IMoviesSource _movies;

        public ScreeningManager(
            ICinemasSource cinemas,
            IMoviesSource movies)
        {
            _cinemas = cinemas;
            _movies = movies;
        }

    }
}
