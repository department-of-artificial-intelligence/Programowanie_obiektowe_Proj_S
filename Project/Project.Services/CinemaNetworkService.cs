using Project.Models;

namespace Project.Services
{
    public class CinemaNetworkService
    {
        public static void DeleteCinemaNetwork(List<CinemaNetwork> cinemaNetworks, string networkId)
        {
            var network = cinemaNetworks.FirstOrDefault(cn => cn.Id == networkId);

            if (network != null)
            {
                cinemaNetworks.Remove(network);
            }
        }
    }
}
