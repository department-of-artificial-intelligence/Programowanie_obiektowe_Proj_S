using CinemaNetworkManagement.Model;

namespace CinemaNetworkManagement.Logic
{
    public class TicketManager
    {
        private readonly ICinemasSource _cinemas;
        private readonly IMoviesSource _movies;

        public TicketManager(
            ICinemasSource cinemas,
            IMoviesSource movies)
        {
            _cinemas = cinemas;
            _movies = movies;
        }

    }
}
