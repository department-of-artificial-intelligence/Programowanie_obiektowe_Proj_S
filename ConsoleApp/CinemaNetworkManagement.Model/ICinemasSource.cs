using System.Collections.Generic;

namespace CinemaNetworkManagement.Model
{
    public interface ICinemasSource
    {
        List<Cinema> AllCinemas();
        bool AddCinema(Cinema cinema);


        bool UpdateCinema(Cinema cinema);
    }
}
