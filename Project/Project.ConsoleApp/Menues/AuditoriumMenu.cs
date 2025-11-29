using Project.Models;
using Project.ConsoleApp.Helpers;
using Project.Services;
using Project.Services.Common;

namespace Project.ConsoleApp.Menues
{
    public static class AuditoriumMenu
    {
        public static void ShowAuditoriumMenu(List<Auditorium> auditoriums, List<Cinema> cinemas,
            List<Seance> seances, List<Reservation> reservations, List<Ticket> tickets)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== AUDITORIUM MANAGEMENT ===");
                Console.WriteLine("1. Add Auditorium");
                Console.WriteLine("2. View All Auditoriums");
                Console.WriteLine("3. Find Auditorium by ID");
                Console.WriteLine("4. Update Auditorium Information");
                Console.WriteLine("5. Add Feature");
                Console.WriteLine("6. Remove Feature");
                Console.WriteLine("7. Rate Auditorium");
                Console.WriteLine("8. Sort and Filter Auditoriums");
                Console.WriteLine("9. Delete Auditorium");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddAuditorium(auditoriums, cinemas); break;
                    case "2": ViewAllAuditoriums(auditoriums); break;
                    case "3": FindAuditoriumById(auditoriums); break;
                    case "4": UpdateAuditorium(auditoriums); break;
                    case "5": AddFeature(auditoriums); break;
                    case "6": RemoveFeature(auditoriums); break;
                    case "7": RateAuditorium(auditoriums); break;
                    case "8": ShowAuditoriumSortFilterMenu(auditoriums); break;
                    case "9": DeleteAuditorium(auditoriums, seances, reservations, tickets); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void ShowAuditoriumSortFilterMenu(List<Auditorium> auditoriums)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== AUDITORIUM SORTING & FILTERING ===");
                Console.WriteLine("1. Sort by Time (Newest First)");
                Console.WriteLine("2. Sort by Time (Oldest First)");
                Console.WriteLine("3. Sort by Rating");
                Console.WriteLine("4. Sort by Popularity");
                Console.WriteLine("5. Sort by Features Count");
                Console.WriteLine("6. Sort by Max Capacity");
                Console.WriteLine("7. Filter by Name");
                Console.WriteLine("8. Filter by Feature");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var newestAuditoriums = BaseService.SortByTimeNewest(auditoriums);

                        DisplayHelper.DisplayAuditoriums(newestAuditoriums, "Auditoriums (Newest First)");
                        break;
                    case "2":
                        var oldestAuditoriums = BaseService.SortByTimeOldest(auditoriums);

                        DisplayHelper.DisplayAuditoriums(oldestAuditoriums, "Auditoriums (Oldest First)");
                        break;
                    case "3":
                        var ratedAuditoriums = RatableService.SortByRating(auditoriums);

                        DisplayHelper.DisplayAuditoriums(ratedAuditoriums, "Auditoriums by Rating");
                        break;
                    case "4":
                        var popularAuditoriums = RatableService.SortByPopularity(auditoriums);

                        DisplayHelper.DisplayAuditoriums(popularAuditoriums, "Auditoriums by Popularity");
                        break;
                    case "5":
                        var featureCountAuditoriums = AuditoriumService.SortAuditoriumsByFeatures(auditoriums);

                        DisplayHelper.DisplayAuditoriums(featureCountAuditoriums, "Auditoriums by Features Count");
                        break;
                    case "6":
                        var capacityAuditoriums = AuditoriumService.SortAuditoriumsByMaxCapacity(auditoriums);

                        DisplayHelper.DisplayAuditoriums(capacityAuditoriums, "Auditoriums by Max Capacity");
                        break;
                    case "7":
                        Console.Write("Enter auditorium name to filter: ");

                        string name = ConsoleHelper.ReadRequiredString("Auditorium name");
                        var filteredAuditoriums = AuditoriumService.FilterAuditoriumsByName(auditoriums, name);

                        DisplayHelper.DisplayAuditoriums(filteredAuditoriums, $"Auditoriums with name containing '{name}'");
                        break;
                    case "8":
                        Console.Write("Enter feature to filter: ");

                        string feature = ConsoleHelper.ReadRequiredString("Feature");
                        var featureAuditoriums = AuditoriumService.FilterAuditoriumsByFeature(auditoriums, feature);

                        DisplayHelper.DisplayAuditoriums(featureAuditoriums, $"Auditoriums with feature '{feature}'");
                        break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void AddAuditorium(List<Auditorium> auditoriums, List<Cinema> cinemas)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD AUDITORIUM ===");

                Console.Write("Cinema ID: ");
                string cinemaId = ConsoleHelper.ReadRequiredString("Cinema ID");

                var cinema = cinemas.FirstOrDefault(c => c.Id == cinemaId);

                if (cinema == null)
                {
                    Console.WriteLine("Cinema with this ID not found.");
                    ConsoleHelper.WaitForKey();

                    return;
                }

                Console.Write("Auditorium Name: ");
                string name = ConsoleHelper.ReadRequiredString("Auditorium name");

                Console.Write("Room Number: ");
                uint roomNumber = ConsoleHelper.ReadUInt();

                Console.Write("Number of Rows: ");
                uint rows = ConsoleHelper.ReadUInt();

                Console.Write("Seats per Row: ");
                uint seatsPerRow = ConsoleHelper.ReadUInt();

                var auditorium = new Auditorium(cinemaId, name, roomNumber, rows, seatsPerRow);
                auditoriums.Add(auditorium);

                Console.WriteLine($"\nAuditorium added successfully! ID: {auditorium.Id}, Capacity: {auditorium.Capacity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void ViewAllAuditoriums(List<Auditorium> auditoriums)
        {
            Console.Clear();
            Console.WriteLine("=== ALL AUDITORIUMS ===");

            if (auditoriums.Count == 0)
            {
                Console.WriteLine("No auditoriums found.");
                ConsoleHelper.WaitForKey();

                return;
            }

            foreach (var auditorium in auditoriums)
            {
                Console.WriteLine(auditorium.ToString());
                Console.WriteLine("----------------------------------------");
            }

            ConsoleHelper.WaitForKey();
        }

        static void FindAuditoriumById(List<Auditorium> auditoriums)
        {
            Console.Clear();
            Console.WriteLine("=== FIND AUDITORIUM BY ID ===");

            Console.Write("Enter auditorium ID: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);

            if (auditorium != null)
            {
                Console.WriteLine(auditorium.ToString());
            }
            else
            {
                Console.WriteLine("Auditorium with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }

        static void UpdateAuditorium(List<Auditorium> auditoriums)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE AUDITORIUM ===");

            Console.Write("Enter auditorium ID to update: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);

            if (auditorium == null)
            {
                Console.WriteLine("Auditorium with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            try
            {
                Console.Write($"New name (current: {auditorium.Name}): ");
                string name = Console.ReadLine() ?? auditorium.Name;

                Console.Write($"New room number (current: {auditorium.RoomNumber}): ");
                uint roomNumber = ConsoleHelper.ReadUInt(auditorium.RoomNumber);

                Console.Write($"New number of rows (current: {auditorium.Rows}): ");
                uint rows = ConsoleHelper.ReadUInt(auditorium.Rows);

                Console.Write($"New number of seats per row (current: {auditorium.SeatsPerRow}): ");
                uint seatsPerRow = ConsoleHelper.ReadUInt(auditorium.SeatsPerRow);

                auditorium.UpdateLayout(name, roomNumber, rows, seatsPerRow);
                Console.WriteLine("Auditorium information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void AddFeature(List<Auditorium> auditoriums)
        {
            Console.Clear();
            Console.WriteLine("=== ADD FEATURE ===");

            Console.Write("Enter auditorium ID: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);

            if (auditorium == null)
            {
                Console.WriteLine("Auditorium with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter feature: ");
            string feature = ConsoleHelper.ReadRequiredString("Feature");

            if (auditorium.AddItem(feature))
            {
                Console.WriteLine("Feature added successfully!");
            }
            else
            {
                Console.WriteLine("Failed to add feature. Possibly reached limit (5 features) or it's already added.");
            }

            ConsoleHelper.WaitForKey();
        }

        static void RemoveFeature(List<Auditorium> auditoriums)
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE FEATURE ===");

            Console.Write("Enter auditorium ID: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);

            if (auditorium == null)
            {
                Console.WriteLine("Auditorium with this ID not found.");
                ConsoleHelper.WaitForKey();

                return;
            }

            Console.Write("Enter feature to remove: ");
            string feature = ConsoleHelper.ReadRequiredString("Feature");

            if (auditorium.RemoveItem(feature))
            {
                Console.WriteLine("Feature removed successfully!");
            }
            else
            {
                Console.WriteLine("Failed to remove feature. Possibly it's not in the list.");
            }

            ConsoleHelper.WaitForKey();
        }

        static void RateAuditorium(List<Auditorium> auditoriums)
        {
            Console.Clear();
            Console.WriteLine("=== RATE AUDITORIUM ===");

            Console.Write("Enter auditorium ID: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);

            if (auditorium == null)
            {
                Console.WriteLine("Auditorium with this ID not found.");
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

            auditorium.AddRating(rating);
            Console.WriteLine($"Auditorium rated successfully! Current rating: {auditorium.Rating}");
            ConsoleHelper.WaitForKey();
        }

        static void DeleteAuditorium(List<Auditorium> auditoriums, List<Seance> seances,
            List<Reservation> reservations, List<Ticket> tickets)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE AUDITORIUM ===");

            Console.Write("Enter auditorium ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);
            if (auditorium != null)
            {
                Console.WriteLine($"\nAuditorium to delete: {auditorium.Name}");
                Console.WriteLine("This will also:");
                Console.WriteLine("- Delete all seances in this auditorium");
                Console.WriteLine("- Delete all reservations for those seances");
                Console.WriteLine("- Delete all tickets for those reservations");
                Console.Write("Are you sure you want to delete this auditorium? (yes/no): ");

                string? confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "yes" || confirmation == "y")
                {
                    AuditoriumService.DeleteAuditorium(auditoriums, seances, reservations, tickets, id);
                    Console.WriteLine("Auditorium and all related data deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Auditorium with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }
    }
}