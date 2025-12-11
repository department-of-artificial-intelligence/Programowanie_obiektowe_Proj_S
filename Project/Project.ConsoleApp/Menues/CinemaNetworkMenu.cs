using Project.ConsoleApp.Helpers;
using Project.Services.Common;
using Project.Services;
using Project.DAL;

namespace Project.ConsoleApp.Menues
{
    public static class CinemaNetworkMenu
    {
        public static void ShowCinemaNetworkMenu(ApplicationDBContext context)
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
                    case "1": AddCinemaNetwork(context); break;
                    case "2": ViewAllCinemaNetworks(context); break;
                    case "3": FindCinemaNetworkByName(context); break;
                    case "4": UpdateCinemaNetwork(context); break;
                    case "5": UpdateTotalCinemas(context); break;
                    case "6": ShowCinemaNetworkSortFilterMenu(context); break;
                    case "7": DeleteCinemaNetwork(context); break;
                    case "0": return;
                    default:  Console.WriteLine("Invalid choice!"); 
                              ConsoleHelper.WaitForKey(); 
                              break;
                }
            }
        }

        static void ShowCinemaNetworkSortFilterMenu(ApplicationDBContext context)
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
                        var newestNetworks = BaseService.SortByTimeNewest(CinemaNetworkService.GetAll(context));
                        DisplayHelper.DisplayCinemaNetworks(newestNetworks, "Cinema Networks (Newest First)");
                        break;
                    case "2":
                        var oldestNetworks = BaseService.SortByTimeOldest(CinemaNetworkService.GetAll(context));
                        DisplayHelper.DisplayCinemaNetworks(oldestNetworks, "Cinema Networks (Oldest First)");
                        break;
                    case "0": return;
                    default:  Console.WriteLine("Invalid choice!"); 
                              ConsoleHelper.WaitForKey(); 
                              break;
                }
            }
        }

        static void AddCinemaNetwork(ApplicationDBContext context)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD CINEMA NETWORK ===");

                Console.Write("Company Name: ");
                string companyName = ConsoleHelper.ReadRequiredString("Company name");

                Console.Write("Manager Name: ");
                string managerName = ConsoleHelper.ReadRequiredString("Manager name");

                var network = CinemaNetworkService.Add(context, companyName, managerName);

                Console.WriteLine($"\nCinema network added successfully! ID: {network.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void ViewAllCinemaNetworks(ApplicationDBContext context)
        {
            var networks = CinemaNetworkService.GetAll(context);
            DisplayHelper.DisplayCinemaNetworks(networks, "All Cinema Networks");
        }

        static void FindCinemaNetworkByName(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== FIND CINEMA NETWORK BY NAME ===");

            Console.Write("Enter company name: ");
            string name = ConsoleHelper.ReadRequiredString("Company name");

            var network = CinemaNetworkService.GetAll(context).FirstOrDefault(cn => cn.CompanyName.Equals(name, StringComparison.OrdinalIgnoreCase));

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

        static void UpdateCinemaNetwork(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE CINEMA NETWORK ===");

            Console.Write("Enter company name to update: ");
            string name = ConsoleHelper.ReadRequiredString("Company name");

            var network = CinemaNetworkService.GetAll(context).FirstOrDefault(cn => cn.CompanyName.Equals(name, StringComparison.OrdinalIgnoreCase));

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
                CinemaNetworkService.Update(context, network);

                Console.WriteLine("Cinema network information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void UpdateTotalCinemas(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE TOTAL CINEMAS ===");

            Console.Write("Enter company name: ");
            string name = ConsoleHelper.ReadRequiredString("Company name");

            var network = CinemaNetworkService.GetAll(context).FirstOrDefault(cn => cn.CompanyName.Equals(name, StringComparison.OrdinalIgnoreCase));

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
                CinemaNetworkService.Update(context, network);

                Console.WriteLine("Total cinemas count updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void DeleteCinemaNetwork(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE CINEMA NETWORK ===");

            Console.Write("Enter cinema network ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Cinema Network ID");

            var network = CinemaNetworkService.GetById(context, id);

            if (network != null)
            {
                Console.WriteLine($"\nCinema Network to delete: {network.CompanyName}");
                Console.WriteLine("Note: This will delete the cinema network record.");
                Console.Write("Are you sure you want to delete this cinema network? (yes/no): ");

                string? confirmation = Console.ReadLine()?.ToLower();

                if (confirmation == "yes" || confirmation == "y")
                {
                    CinemaNetworkService.Delete(context, id);
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