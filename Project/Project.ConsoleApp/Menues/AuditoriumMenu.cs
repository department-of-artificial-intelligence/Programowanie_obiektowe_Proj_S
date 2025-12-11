using Project.ConsoleApp.Helpers;
using Project.Services.Common;
using Project.Services;
using Project.DAL;

namespace Project.ConsoleApp.Menues
{
    public static class AuditoriumMenu
    {
        public static void ShowAuditoriumMenu(ApplicationDBContext context)
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
                    case "1": AddAuditorium(context); break;
                    case "2": ViewAllAuditoriums(context); break;
                    case "3": FindAuditoriumById(context); break;
                    case "4": UpdateAuditorium(context); break;
                    case "5": AddFeature(context); break;
                    case "6": RemoveFeature(context); break;
                    case "7": RateAuditorium(context); break;
                    case "8": ShowAuditoriumSortFilterMenu(context); break;
                    case "9": DeleteAuditorium(context); break;
                    case "0": return;
                    default:  Console.WriteLine("Invalid choice!"); 
                              ConsoleHelper.WaitForKey(); 
                              break;
                }
            }
        }

        static void ShowAuditoriumSortFilterMenu(ApplicationDBContext context)
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
                        var newestAuditoriums = BaseService.SortByTimeNewest(AuditoriumService.GetAll(context));
                        DisplayHelper.DisplayAuditoriums(newestAuditoriums, "Auditoriums (Newest First)");
                        break;
                    case "2":
                        var oldestAuditoriums = BaseService.SortByTimeOldest(AuditoriumService.GetAll(context));
                        DisplayHelper.DisplayAuditoriums(oldestAuditoriums, "Auditoriums (Oldest First)");
                        break;
                    case "3":
                        var ratedAuditoriums = RatableService.SortByRating(AuditoriumService.GetAll(context));
                        DisplayHelper.DisplayAuditoriums(ratedAuditoriums, "Auditoriums by Rating");
                        break;
                    case "4":
                        var popularAuditoriums = RatableService.SortByPopularity(AuditoriumService.GetAll(context));
                        DisplayHelper.DisplayAuditoriums(popularAuditoriums, "Auditoriums by Popularity");
                        break;
                    case "5":
                        var featureCountAuditoriums = AuditoriumService.SortByFeatures(context);
                        DisplayHelper.DisplayAuditoriums(featureCountAuditoriums, "Auditoriums by Features Count");
                        break;
                    case "6":
                        var capacityAuditoriums = AuditoriumService.SortByMaxCapacity(context);
                        DisplayHelper.DisplayAuditoriums(capacityAuditoriums, "Auditoriums by Max Capacity");
                        break;
                    case "7":
                        Console.Write("Enter auditorium name to filter: ");
                        string name = ConsoleHelper.ReadRequiredString("Auditorium name");

                        var filteredAuditoriums = AuditoriumService.FilterByName(context, name);
                        DisplayHelper.DisplayAuditoriums(filteredAuditoriums, $"Auditoriums with name containing '{name}'");
                        break;
                    case "8":
                        Console.Write("Enter feature to filter: ");
                        string feature = ConsoleHelper.ReadRequiredString("Feature");

                        var featureAuditoriums = AuditoriumService.FilterByFeature(context, feature);
                        DisplayHelper.DisplayAuditoriums(featureAuditoriums, $"Auditoriums with feature '{feature}'");
                        break;
                    case "0": return;
                    default:  Console.WriteLine("Invalid choice!"); 
                              ConsoleHelper.WaitForKey(); 
                              break;
                }
            }
        }

        static void AddAuditorium(ApplicationDBContext context)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD AUDITORIUM ===");

                Console.Write("Cinema ID: ");
                string cinemaId = ConsoleHelper.ReadRequiredString("Cinema ID");

                var cinema = CinemaService.GetById(context, cinemaId);

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

                
                var auditorium = AuditoriumService.Add(context, cinemaId, name, roomNumber, rows, seatsPerRow);

                Console.WriteLine($"\nAuditorium added successfully! ID: {auditorium.Id}, Capacity: {auditorium.Capacity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void ViewAllAuditoriums(ApplicationDBContext context)
        {
            var auditoriums = AuditoriumService.GetAll(context);
            DisplayHelper.DisplayAuditoriums(auditoriums, "All Auditoriums");
        }

        static void FindAuditoriumById(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== FIND AUDITORIUM BY ID ===");

            Console.Write("Enter auditorium ID: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = AuditoriumService.GetById(context, id);

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

        static void UpdateAuditorium(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE AUDITORIUM ===");

            Console.Write("Enter auditorium ID to update: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = AuditoriumService.GetById(context, id);

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
                AuditoriumService.Update(context, auditorium);

                Console.WriteLine("Auditorium information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void AddFeature(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== ADD FEATURE ===");

            Console.Write("Enter auditorium ID: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = AuditoriumService.GetById(context, id);

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
                AuditoriumService.Update(context, auditorium);
                Console.WriteLine("Feature added successfully!");
            }
            else
            {
                Console.WriteLine("Failed to add feature. Possibly reached limit (5 features) or it's already added.");
            }

            ConsoleHelper.WaitForKey();
        }

        static void RemoveFeature(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE FEATURE ===");

            Console.Write("Enter auditorium ID: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = AuditoriumService.GetById(context, id);

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
                AuditoriumService.Update(context, auditorium);
                Console.WriteLine("Feature removed successfully!");
            }
            else
            {
                Console.WriteLine("Failed to remove feature. Possibly it's not in the list.");
            }

            ConsoleHelper.WaitForKey();
        }

        static void RateAuditorium(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== RATE AUDITORIUM ===");

            Console.Write("Enter auditorium ID: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = AuditoriumService.GetById(context, id);

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
            AuditoriumService.Update(context, auditorium);

            Console.WriteLine($"Auditorium rated successfully! Current rating: {auditorium.Rating}");
            ConsoleHelper.WaitForKey();
        }

        static void DeleteAuditorium(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE AUDITORIUM ===");

            Console.Write("Enter auditorium ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Auditorium ID");

            var auditorium = AuditoriumService.GetById(context, id);

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
                    AuditoriumService.Delete(context, id);
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