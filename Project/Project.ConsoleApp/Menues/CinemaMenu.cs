using Project.Models;
using Project.ConsoleApp.Helpers;
using Project.Services;
using Project.Services.Common;

namespace Project.ConsoleApp.Menues
{
    public static class CinemaMenu
    {
        public static void ShowCinemaMenu(List<Cinema> cinemas, List<Film> films,
            List<Auditorium> auditoriums, List<Seance> seances, List<Reservation> reservations,
            List<Ticket> tickets)
        {
            ArgumentNullException.ThrowIfNull(films);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CINEMA MANAGEMENT ===");
                Console.WriteLine("1. Add Cinema");
                Console.WriteLine("2. View All Cinemas");
                Console.WriteLine("3. Find Cinema by ID");
                Console.WriteLine("4. Update Cinema Information");
                Console.WriteLine("5. Add Available Film");
                Console.WriteLine("6. Remove Available Film");
                Console.WriteLine("7. Rate Cinema");
                Console.WriteLine("8. Sort and Filter Cinemas");
                Console.WriteLine("9. Delete Cinema");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddCinema(cinemas); break;
                    case "2": ViewAllCinemas(cinemas); break;
                    case "3": FindCinemaById(cinemas); break;
                    case "4": UpdateCinema(cinemas); break;
                    case "5": AddAvailableFilm(cinemas); break;
                    case "6": RemoveAvailableFilm(cinemas); break;
                    case "7": RateCinema(cinemas); break;
                    case "8": ShowCinemaSortFilterMenu(cinemas); break;
                    case "9": DeleteCinema(cinemas, auditoriums, seances, reservations, tickets); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void ShowCinemaSortFilterMenu(List<Cinema> cinemas)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CINEMA SORTING & FILTERING ===");
                Console.WriteLine("1. Sort by Time (Newest First)");
                Console.WriteLine("2. Sort by Time (Oldest First)");
                Console.WriteLine("3. Sort by Rating");
                Console.WriteLine("4. Sort by Popularity");
                Console.WriteLine("5. Sort by Number of Available Films");
                Console.WriteLine("6. Filter by Name");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var newestCinemas = BaseService.SortByTimeNewest(cinemas);
                        DisplayHelper.DisplayCinemas(newestCinemas, "Cinemas (Newest First)");
                        break;
                    case "2":
                        var oldestCinemas = BaseService.SortByTimeOldest(cinemas);
                        DisplayHelper.DisplayCinemas(oldestCinemas, "Cinemas (Oldest First)");
                        break;
                    case "3":
                        var ratedCinemas = RatableService.SortByRating(cinemas);
                        DisplayHelper.DisplayCinemas(ratedCinemas, "Cinemas by Rating");
                        break;
                    case "4":
                        var popularCinemas = RatableService.SortByPopularity(cinemas);
                        DisplayHelper.DisplayCinemas(popularCinemas, "Cinemas by Popularity");
                        break;
                    case "5":
                        var filmCountCinemas = CinemaService.SortByNumberOfAvailableFilms(cinemas);
                        DisplayHelper.DisplayCinemas(filmCountCinemas, "Cinemas by Number of Available Films");
                        break;
                    case "6":
                        Console.Write("Enter cinema name to filter: ");
                        string name = ConsoleHelper.ReadRequiredString("Cinema name");
                        var filteredCinemas = CinemaService.FilterCinemasByName(cinemas, name);
                        DisplayHelper.DisplayCinemas(filteredCinemas, $"Cinemas with name containing '{name}'");
                        break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void AddCinema(List<Cinema> cinemas)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD CINEMA ===");

                Console.Write("Cinema Name: ");
                string name = ConsoleHelper.ReadRequiredString("Cinema name");

                Console.Write("Address: ");
                string address = ConsoleHelper.ReadRequiredString("Address");

                Console.Write("Contact Phone: ");
                string contactPhone = ConsoleHelper.ReadRequiredString("Contact phone");

                Console.Write("Contact Email: ");
                string contactEmail = ConsoleHelper.ReadRequiredString("Contact email");

                Console.Write("Manager Name: ");
                string managerName = ConsoleHelper.ReadRequiredString("Manager name");

                var cinema = new Cinema(name, address, contactPhone, contactEmail, managerName);
                cinemas.Add(cinema);

                Console.WriteLine($"\nCinema added successfully! ID: {cinema.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void ViewAllCinemas(List<Cinema> cinemas)
        {
            Console.Clear();
            Console.WriteLine("=== ALL CINEMAS ===");

            if (cinemas.Count == 0)
            {
                Console.WriteLine("No cinemas found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            foreach (var cinema in cinemas)
            {
                Console.WriteLine(cinema.ToString());
                Console.WriteLine("----------------------------------------");
            }
            ConsoleHelper.WaitForKey();
        }

        static void FindCinemaById(List<Cinema> cinemas)
        {
            Console.Clear();
            Console.WriteLine("=== FIND CINEMA BY ID ===");

            Console.Write("Enter cinema ID: ");
            string id = ConsoleHelper.ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema != null)
            {
                Console.WriteLine(cinema.ToString());
            }
            else
            {
                Console.WriteLine("Cinema with this ID not found.");
            }
            ConsoleHelper.WaitForKey();
        }

        static void UpdateCinema(List<Cinema> cinemas)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE CINEMA ===");

            Console.Write("Enter cinema ID to update: ");
            string id = ConsoleHelper.ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema == null)
            {
                Console.WriteLine("Cinema with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            try
            {
                Console.Write($"New name (current: {cinema.Name}): ");
                string name = Console.ReadLine() ?? cinema.Name;

                Console.Write($"New address (current: {cinema.Address}): ");
                string address = Console.ReadLine() ?? cinema.Address;

                Console.Write($"New contact phone (current: {cinema.ContactPhone}): ");
                string contactPhone = Console.ReadLine() ?? cinema.ContactPhone;

                Console.Write($"New contact email (current: {cinema.ContactEmail}): ");
                string contactEmail = Console.ReadLine() ?? cinema.ContactEmail;

                Console.Write($"New manager name (current: {cinema.ManagerName}): ");
                string managerName = Console.ReadLine() ?? cinema.ManagerName;

                cinema.UpdateInfo(name, address, contactPhone, contactEmail, managerName);
                Console.WriteLine("Cinema information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void AddAvailableFilm(List<Cinema> cinemas)
        {
            Console.Clear();
            Console.WriteLine("=== ADD AVAILABLE FILM ===");

            Console.Write("Enter cinema ID: ");
            string cinemaId = ConsoleHelper.ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == cinemaId);
            if (cinema == null)
            {
                Console.WriteLine("Cinema with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter film ID: ");
            string filmId = ConsoleHelper.ReadRequiredString("Film ID");

            if (cinema.AddItem(filmId))
            {
                Console.WriteLine("Film successfully added to available films!");
            }
            else
            {
                Console.WriteLine("Failed to add film. Possibly reached limit (10 films) or film already added.");
            }
            ConsoleHelper.WaitForKey();
        }

        static void RemoveAvailableFilm(List<Cinema> cinemas)
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE AVAILABLE FILM ===");

            Console.Write("Enter cinema ID: ");
            string cinemaId = ConsoleHelper.ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == cinemaId);
            if (cinema == null)
            {
                Console.WriteLine("Cinema with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter film ID to remove: ");
            string filmId = ConsoleHelper.ReadRequiredString("Film ID");

            if (cinema.RemoveItem(filmId))
            {
                Console.WriteLine("Film successfully removed from available films!");
            }
            else
            {
                Console.WriteLine("Failed to remove film. Possibly it's not in the available list.");
            }
            ConsoleHelper.WaitForKey();
        }

        static void RateCinema(List<Cinema> cinemas)
        {
            Console.Clear();
            Console.WriteLine("=== RATE CINEMA ===");

            Console.Write("Enter cinema ID: ");
            string id = ConsoleHelper.ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema == null)
            {
                Console.WriteLine("Cinema with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter rating (1-5): ");
            uint rating = ConsoleHelper.ReadUInt();

            if (rating < 1 || rating > 5)
            {
                Console.WriteLine("Rating must be between 1 and 5.");
                ConsoleHelper.WaitForKey();
                return;
            }

            cinema.AddRating(rating);
            Console.WriteLine($"Cinema rated successfully! Current rating: {cinema.Rating}");
            ConsoleHelper.WaitForKey();
        }

        static void DeleteCinema(List<Cinema> cinemas, List<Auditorium> auditoriums,
            List<Seance> seances, List<Reservation> reservations, List<Ticket> tickets)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE CINEMA ===");

            Console.Write("Enter cinema ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema != null)
            {
                Console.WriteLine($"\nCinema to delete: {cinema.Name}");
                Console.WriteLine("This will also:");
                Console.WriteLine("- Delete all auditoriums in this cinema");
                Console.WriteLine("- Delete all seances in those auditoriums");
                Console.WriteLine("- Delete all reservations for those seances");
                Console.WriteLine("- Delete all tickets for those reservations");
                Console.Write("Are you sure you want to delete this cinema? (yes/no): ");

                string? confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "yes" || confirmation == "y")
                {
                    CinemaService.DeleteCinema(cinemas, auditoriums, seances, reservations, tickets, id);
                    Console.WriteLine("Cinema and all related data deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Cinema with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }
    }
}