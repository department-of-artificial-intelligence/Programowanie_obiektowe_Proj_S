using Project.ConsoleApp.Helpers;
using Project.Models;
using Project.Services;
using Project.Services.Common;

namespace Project.ConsoleApp.Menues
{
    public static class CinemaNetworkMenu
    {
        public static void ShowCinemaNetworkMenu(List<CinemaNetwork> cinemaNetworks)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CINEMA NETWORK MANAGEMENT ===");
                Console.WriteLine("1. Add Cinema Network");
                Console.WriteLine("2. View All Cinema Networks");
                Console.WriteLine("3. Find Cinema Network by Name");
                Console.WriteLine("4. Update Cinema Network Information");
                Console.WriteLine("5. Update Total Cinemas");
                Console.WriteLine("6. Sort and Filter Cinema Networks");
                Console.WriteLine("7. Delete Cinema Network");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddCinemaNetwork(cinemaNetworks); break;
                    case "2": ViewAllCinemaNetworks(cinemaNetworks); break;
                    case "3": FindCinemaNetworkByName(cinemaNetworks); break;
                    case "4": UpdateCinemaNetwork(cinemaNetworks); break;
                    case "5": UpdateTotalCinemas(cinemaNetworks); break;
                    case "6": ShowCinemaNetworkSortFilterMenu(cinemaNetworks); break;
                    case "7": DeleteCinemaNetwork(cinemaNetworks); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void ShowCinemaNetworkSortFilterMenu(List<CinemaNetwork> cinemaNetworks)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CINEMA NETWORK SORTING & FILTERING ===");
                Console.WriteLine("1. Sort by Time (Newest First)");
                Console.WriteLine("2. Sort by Time (Oldest First)");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var newestNetworks = BaseService.SortByTimeNewest(cinemaNetworks);
                        DisplayHelper.DisplayCinemaNetworks(newestNetworks, "Cinema Networks (Newest First)");
                        break;
                    case "2":
                        var oldestNetworks = BaseService.SortByTimeOldest(cinemaNetworks);
                        DisplayHelper.DisplayCinemaNetworks(oldestNetworks, "Cinema Networks (Oldest First)");
                        break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void AddCinemaNetwork(List<CinemaNetwork> cinemaNetworks)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD CINEMA NETWORK ===");

                Console.Write("Company Name: ");
                string companyName = ConsoleHelper.ReadRequiredString("Company name");

                Console.Write("Manager Name: ");
                string managerName = ConsoleHelper.ReadRequiredString("Manager name");

                var network = new CinemaNetwork(companyName, managerName);
                cinemaNetworks.Add(network);

                Console.WriteLine($"\nCinema network added successfully! ID: {network.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void ViewAllCinemaNetworks(List<CinemaNetwork> cinemaNetworks)
        {
            Console.Clear();
            Console.WriteLine("=== ALL CINEMA NETWORKS ===");

            if (cinemaNetworks.Count == 0)
            {
                Console.WriteLine("No cinema networks found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            foreach (var network in cinemaNetworks)
            {
                Console.WriteLine(network.ToString());
                Console.WriteLine("----------------------------------------");
            }
            ConsoleHelper.WaitForKey();
        }

        static void FindCinemaNetworkByName(List<CinemaNetwork> cinemaNetworks)
        {
            Console.Clear();
            Console.WriteLine("=== FIND CINEMA NETWORK BY NAME ===");

            Console.Write("Enter company name: ");
            string name = ConsoleHelper.ReadRequiredString("Company name");

            var network = cinemaNetworks.FirstOrDefault(cn =>
                cn.CompanyName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (network != null)
            {
                Console.WriteLine(network.ToString());
            }
            else
            {
                Console.WriteLine("Cinema network with this name not found.");
            }
            ConsoleHelper.WaitForKey();
        }

        static void UpdateCinemaNetwork(List<CinemaNetwork> cinemaNetworks)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE CINEMA NETWORK ===");

            Console.Write("Enter company name to update: ");
            string name = ConsoleHelper.ReadRequiredString("Company name");

            var network = cinemaNetworks.FirstOrDefault(cn =>
                cn.CompanyName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (network == null)
            {
                Console.WriteLine("Cinema network with this name not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            try
            {
                Console.Write($"New company name (current: {network.CompanyName}): ");
                string companyName = Console.ReadLine() ?? network.CompanyName;

                Console.Write($"New manager name (current: {network.ManagerName}): ");
                string managerName = Console.ReadLine() ?? network.ManagerName;

                network.UpdateInfo(companyName, managerName);
                Console.WriteLine("Cinema network information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void UpdateTotalCinemas(List<CinemaNetwork> cinemaNetworks)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE TOTAL CINEMAS ===");

            Console.Write("Enter company name: ");
            string name = ConsoleHelper.ReadRequiredString("Company name");

            var network = cinemaNetworks.FirstOrDefault(cn =>
                cn.CompanyName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (network == null)
            {
                Console.WriteLine("Cinema network with this name not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write($"Enter total cinemas count (current: {network.TotalCinemas}): ");
            int count = ConsoleHelper.ReadInt();

            try
            {
                network.SetTotalCinemas(count);
                Console.WriteLine("Total cinemas count updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void DeleteCinemaNetwork(List<CinemaNetwork> cinemaNetworks)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE CINEMA NETWORK ===");

            Console.Write("Enter cinema network ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Cinema Network ID");

            var network = cinemaNetworks.FirstOrDefault(cn => cn.Id == id);
            if (network != null)
            {
                Console.WriteLine($"\nCinema Network to delete: {network.CompanyName}");
                Console.WriteLine("Note: This will delete the cinema network record.");
                Console.Write("Are you sure you want to delete this cinema network? (yes/no): ");

                string? confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "yes" || confirmation == "y")
                {
                    CinemaNetworkService.DeleteCinemaNetwork(cinemaNetworks, id);
                    Console.WriteLine("Cinema network deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Cinema network with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }
    }
}