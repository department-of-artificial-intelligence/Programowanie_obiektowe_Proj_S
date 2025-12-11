using Project.ConsoleApp.Helpers;
using Project.Services.Common;
using Project.Services;
using Project.DAL;

namespace Project.ConsoleApp.Menues
{
    public static class SeanceMenu
    {
        public static void ShowSeanceMenu(ApplicationDBContext context)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SEANCE MANAGEMENT ===");
                Console.WriteLine("1. Add Seance");
                Console.WriteLine("2. View All Seances");
                Console.WriteLine("3. Find Seance by ID");
                Console.WriteLine("4. Add Occupied Seat");
                Console.WriteLine("5. Remove Occupied Seat");
                Console.WriteLine("6. Update Seance Time");
                Console.WriteLine("7. Update Seance Price");
                Console.WriteLine("8. Sort and Filter Seances");
                Console.WriteLine("9. Delete Seance");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddSeance(context); break;
                    case "2": ViewAllSeances(context); break;
                    case "3": FindSeanceById(context); break;
                    case "4": AddOccupiedSeat(context); break;
                    case "5": RemoveOccupiedSeat(context); break;
                    case "6": UpdateSeanceTime(context); break;
                    case "7": UpdateSeancePrice(context); break;
                    case "8": ShowSeanceSortFilterMenu(context); break;
                    case "9": DeleteSeance(context); break;
                    case "0": return;
                    default:  Console.WriteLine("Invalid choice!"); 
                              ConsoleHelper.WaitForKey(); 
                              break;
                }
            }
        }

        static void ShowSeanceSortFilterMenu(ApplicationDBContext context)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SEANCE SORTING & FILTERING ===");
                Console.WriteLine("1. Sort by Time (Newest First)");
                Console.WriteLine("2. Sort by Time (Oldest First)");
                Console.WriteLine("3. Sort by Start Time");
                Console.WriteLine("4. Sort by Price");
                Console.WriteLine("5. Sort by Occupied Seats");
                Console.WriteLine("6. Filter by Film ID");
                Console.WriteLine("7. Filter by Auditorium ID");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var newestSeances = BaseService.SortByTimeNewest(SeanceService.GetAll(context));
                        DisplayHelper.DisplaySeances(newestSeances, "Seances (Newest First)");
                        break;
                    case "2":
                        var oldestSeances = BaseService.SortByTimeOldest(SeanceService.GetAll(context));
                        DisplayHelper.DisplaySeances(oldestSeances, "Seances (Oldest First)");
                        break;
                    case "3":
                        var startTimeSeances = SeanceService.SortByStartTime(context);
                        DisplayHelper.DisplaySeances(startTimeSeances, "Seances by Start Time");
                        break;
                    case "4":
                        var priceSeances = SeanceService.SortByPrice(context);
                        DisplayHelper.DisplaySeances(priceSeances, "Seances by Price");
                        break;
                    case "5":
                        var occupiedSeances = SeanceService.SortByOccupiedSeats(context);
                        DisplayHelper.DisplaySeances(occupiedSeances, "Seances by Occupied Seats");
                        break;
                    case "6":
                        Console.Write("Enter film ID to filter: ");
                        string filmId = ConsoleHelper.ReadRequiredString("Film ID");

                        var filmSeances = SeanceService.FilterByFilmId(context, filmId);
                        DisplayHelper.DisplaySeances(filmSeances, $"Seances for film {filmId}");
                        break;
                    case "7":
                        Console.Write("Enter auditorium ID to filter: ");
                        string auditoriumId = ConsoleHelper.ReadRequiredString("Auditorium ID");

                        var auditoriumSeances = SeanceService.FilterByAuditoriumId(context, auditoriumId);
                        DisplayHelper.DisplaySeances(auditoriumSeances, $"Seances in auditorium {auditoriumId}");
                        break;
                    case "0": return;
                    default:  Console.WriteLine("Invalid choice!"); 
                              ConsoleHelper.WaitForKey(); 
                              break;
                }
            }
        }

        static void AddSeance(ApplicationDBContext context)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD SEANCE ===");

                Console.Write("Film ID: ");
                string filmId = ConsoleHelper.ReadRequiredString("Film ID");

                var film = FilmService.GetById(context, filmId);

                if (film == null)
                {
                    Console.WriteLine("Film with this ID not found.");
                    ConsoleHelper.WaitForKey();
                    return;
                }

                Console.Write("Auditorium ID: ");
                string auditoriumId = ConsoleHelper.ReadRequiredString("Auditorium ID");

                var auditorium = AuditoriumService.GetById(context, auditoriumId);

                if (auditorium == null)
                {
                    Console.WriteLine("Auditorium with this ID not found.");
                    ConsoleHelper.WaitForKey();
                    return;
                }

                Console.Write("Start date and time (yyyy-mm-dd hh:mm): ");
                DateTime startTime = ConsoleHelper.ReadDateTime();

                Console.Write("Price: ");
                decimal price = ConsoleHelper.ReadDecimal();

                
                var seance = SeanceService.Add(context, filmId, auditoriumId, startTime, price, film.DurationMinutes);

                Console.WriteLine($"\nSeance added successfully! ID: {seance.Id}");
                Console.WriteLine($"End Time: {seance.EndTime}");
                Console.WriteLine($"Available Seats: {seance.AvailableSeats(auditorium.Capacity)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void ViewAllSeances(ApplicationDBContext context)
        {
            var seances = SeanceService.GetAll(context);
            DisplayHelper.DisplaySeances(seances, "All Seances");
        }

        static void FindSeanceById(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== FIND SEANCE BY ID ===");

            Console.Write("Enter seance ID: ");
            string id = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = SeanceService.GetById(context, id);

            if (seance != null)
            {
                Console.WriteLine(seance.ToString());
            }
            else
            {
                Console.WriteLine("Seance with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }

        static void AddOccupiedSeat(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== ADD OCCUPIED SEAT ===");

            Console.Write("Enter seance ID: ");
            string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = SeanceService.GetById(context, seanceId);

            if (seance == null)
            {
                Console.WriteLine("Seance with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter seat ID: ");
            string seatId = ConsoleHelper.ReadRequiredString("Seat ID");

            var auditorium = AuditoriumService.GetById(context, seance.AuditoriumId);

            if (auditorium == null)
            {
                Console.WriteLine("Auditorium for this seance not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            if (seance.ReserveSeat(seatId, auditorium.Capacity))
            {
                SeanceService.Update(context, seance);
                Console.WriteLine("Seat successfully reserved!");
                Console.WriteLine($"Available seats remaining: {seance.AvailableSeats(auditorium.Capacity)}");
            }
            else
            {
                Console.WriteLine("Failed to reserve seat. Possibly reached capacity limit or seat already reserved.");
            }

            ConsoleHelper.WaitForKey();
        }

        static void RemoveOccupiedSeat(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE OCCUPIED SEAT ===");

            Console.Write("Enter seance ID: ");
            string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = SeanceService.GetById(context, seanceId);

            if (seance == null)
            {
                Console.WriteLine("Seance with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter seat ID to remove: ");
            string seatId = ConsoleHelper.ReadRequiredString("Seat ID");

            if (seance.CancelSeatReservation(seatId))
            {
                SeanceService.Update(context, seance);
                var auditorium = AuditoriumService.GetById(context, seance.AuditoriumId);

                Console.WriteLine("Seat reservation cancelled successfully!");

                if (auditorium != null)
                {
                    Console.WriteLine($"Available seats now: {seance.AvailableSeats(auditorium.Capacity)}");
                }
            }
            else
            {
                Console.WriteLine("Failed to cancel seat reservation. Possibly not in the list.");
            }

            ConsoleHelper.WaitForKey();
        }

        static void UpdateSeanceTime(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE SEANCE TIME ===");

            Console.Write("Enter seance ID: ");
            string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = SeanceService.GetById(context, seanceId);

            if (seance == null)
            {
                Console.WriteLine("Seance with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter new start time (yyyy-mm-dd hh:mm): ");
            DateTime newStartTime = ConsoleHelper.ReadDateTime();

            var film = FilmService.GetById(context, seance.FilmId);

            if (film == null)
            {
                Console.WriteLine("Film for this seance not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            try
            {
                seance.UpdateTime(newStartTime, film.DurationMinutes);
                SeanceService.Update(context, seance);

                Console.WriteLine($"Seance time updated successfully! New end time: {seance.EndTime}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void UpdateSeancePrice(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE SEANCE PRICE ===");

            Console.Write("Enter seance ID: ");
            string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = SeanceService.GetById(context, seanceId);

            if (seance == null)
            {
                Console.WriteLine("Seance with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write($"Enter new price (current: {seance.Price}): ");
            decimal newPrice = ConsoleHelper.ReadDecimal();

            try
            {
                seance.UpdatePrice(newPrice);
                SeanceService.Update(context, seance);

                Console.WriteLine($"Seance price updated successfully! New price: {seance.Price}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void DeleteSeance(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE SEANCE ===");

            Console.Write("Enter seance ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = SeanceService.GetById(context, id);

            if (seance != null)
            {
                Console.WriteLine($"\nSeance to delete: {seance.StartTime}");
                Console.WriteLine("This will also:");
                Console.WriteLine("- Delete all reservations for this seance");
                Console.WriteLine("- Delete all tickets for those reservations");
                Console.Write("Are you sure you want to delete this seance? (yes/no): ");

                string? confirmation = Console.ReadLine()?.ToLower();

                if (confirmation == "yes" || confirmation == "y")
                {
                    SeanceService.Delete(context, id);
                    Console.WriteLine("Seance and all related data deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Seance with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }
    }
}