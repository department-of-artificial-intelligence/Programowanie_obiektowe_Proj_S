using Project.Models;
using Project.DAL;

namespace Project.Services
{
    public static class CinemaNetworkService
    {
        public static List<CinemaNetwork> GetAll(ApplicationDBContext context)
        {
            return [.. context.CinemaNetworks];
        }

        public static CinemaNetwork? GetById(ApplicationDBContext context, string id)
        {
            return context.CinemaNetworks.FirstOrDefault(cn => cn.Id == id);
        }

        public static CinemaNetwork Add(ApplicationDBContext context, string companyName, string managerName)
        {
            var network = new CinemaNetwork(companyName, managerName);

            context.CinemaNetworks.Add(network);
            context.SaveChanges();

            return network;
        }

        public static void Update(ApplicationDBContext context, CinemaNetwork network)
        {
            context.CinemaNetworks.Update(network);
            context.SaveChanges();
        }

        public static void Delete(ApplicationDBContext context, string networkId)
        {
            var network = context.CinemaNetworks.FirstOrDefault(cn => cn.Id == networkId);

            if (network != null)
            {
                context.CinemaNetworks.Remove(network);
                context.SaveChanges();
            }
        }
    }
}