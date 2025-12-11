using Project.Models.Common;

namespace Project.Models
{
    public class CinemaNetwork : Base
    {
        public string CompanyName { get; private set; }
        public string ManagerName { get; private set; }
        public int TotalCinemas { get; private set; }


        protected CinemaNetwork()
        {
            CompanyName = string.Empty;
            ManagerName = string.Empty;
            TotalCinemas = 0;
        }

        public CinemaNetwork(string companyName, string managerName) : base()
        {
            ValidateNetworkData(companyName, managerName);

            CompanyName = companyName;
            ManagerName = managerName;
        }

        private static void ValidateNetworkData(string companyName, string managerName)
        {
            if (string.IsNullOrWhiteSpace(companyName)) throw new ArgumentException("Company name is required", nameof(companyName));
            if (string.IsNullOrWhiteSpace(managerName)) throw new ArgumentException("Manager name is required", nameof(managerName));
        }

        public void UpdateInfo(string companyName, string managerName)
        {
            ValidateNetworkData(companyName, managerName);

            CompanyName = companyName;
            ManagerName = managerName;

            MarkAsUpdated();
        }

        public void SetTotalCinemas(int count)
        {
            if (count < 0) throw new ArgumentException("Count cannot be negative");

            TotalCinemas = count;
            MarkAsUpdated();
        }

        public override string ToString()
        {
            return $"Cinema Network: {CompanyName}\n" +
                   $"Manager: {ManagerName}\n" +
                   $"Total Cinemas: {TotalCinemas}\n" +
                   $"ID: {Id}";
        }
    }
}