using CinemaNetworkManagement.Model;

namespace CinemaNetworkManagement.Logic
{
    public class CinemaNetwork
    {
        private readonly ICinemasSource _cinemas;
        private readonly ISourceEmployee _employees;

        public string Name { get; set; }

        public CinemaNetwork(
            string name,
            ICinemasSource cinemas,
            ISourceEmployee employees)
        {
            Name = name;
            _cinemas = cinemas;
            _employees = employees;
        }

    }
}
