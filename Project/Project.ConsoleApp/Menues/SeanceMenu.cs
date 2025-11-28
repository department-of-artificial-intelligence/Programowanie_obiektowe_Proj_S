using Project.Models;
using Project.ConsoleApp.Helpers;
using Project.Services.Handlers;
using Project.Services.SortingFiltering;

namespace Project.ConsoleApp.Menues
{
    public static class SeanceMenu
    {
        public static void ShowSeanceMenu(List<Seance> seances, List<Film> films, List<Auditorium> auditoriums,
            List<Reservation> reservations, List<Ticket> tickets)
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
                    case "1": AddSeance(seances, films, auditoriums); break;
                    case "2": ViewAllSeances(seances); break;
                    case "3": FindSeanceById(seances); break;
                    case "4": AddOccupiedSeat(seances, auditoriums); break;
                    case "5": RemoveOccupiedSeat(seances, auditoriums); break;
                    case "6": UpdateSeanceTime(seances, films); break;
                    case "7": UpdateSeancePrice(seances); break;
                    case "8": ShowSeanceSortFilterMenu(seances, auditoriums); break;
                    case "9": DeleteSeance(seances, reservations, tickets); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void ShowSeanceSortFilterMenu(List<Seance> seances, List<Auditorium> auditoriums)
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
                        var newestSeances = GenericSorting.SortByTimeNewest(seances);
                        DisplayHelper.DisplaySeances(newestSeances, "Seances (Newest First)");
                        break;
                    case "2":
                        var oldestSeances = GenericSorting.SortByTimeOldest(seances);
                        DisplayHelper.DisplaySeances(oldestSeances, "Seances (Oldest First)");
                        break;
                    case "3":
                        var startTimeSeances = SeanceSortingFiltering.SortSeancesByStartTime(seances);
                        DisplayHelper.DisplaySeances(startTimeSeances, "Seances by Start Time");
                        break;
                    case "4":
                        var priceSeances = SeanceSortingFiltering.SortSeancesByPrice(seances);
                        DisplayHelper.DisplaySeances(priceSeances, "Seances by Price");
                        break;
                    case "5":
                        var occupiedSeances = SeanceSortingFiltering.SortSeancesByOccupiedSeats(seances, auditoriums);
                        DisplayHelper.DisplaySeances(occupiedSeances, "Seances by Occupied Seats");
                        break;
                    case "6":
                        Console.Write("Enter film ID to filter: ");
                        string filmId = ConsoleHelper.ReadRequiredString("Film ID");
                        var filmSeances = SeanceSortingFiltering.FilterSeancesByFilmId(seances, filmId);
                        DisplayHelper.DisplaySeances(filmSeances, $"Seances for film {filmId}");
                        break;
                    case "7":
                        Console.Write("Enter auditorium ID to filter: ");
                        string auditoriumId = ConsoleHelper.ReadRequiredString("Auditorium ID");
                        var auditoriumSeances = SeanceSortingFiltering.FilterSeancesByAuditoriumId(seances, auditoriumId);
                        DisplayHelper.DisplaySeances(auditoriumSeances, $"Seances in auditorium {auditoriumId}");
                        break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void AddSeance(List<Seance> seances, List<Film> films, List<Auditorium> auditoriums)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD SEANCE ===");

                Console.Write("Film ID: ");
                string filmId = ConsoleHelper.ReadRequiredString("Film ID");

                var film = films.FirstOrDefault(f => f.Id == filmId);
                if (film == null)
                {
                    Console.WriteLine("Film with this ID not found.");
                    ConsoleHelper.WaitForKey();
                    return;
                }

                Console.Write("Auditorium ID: ");
                string auditoriumId = ConsoleHelper.ReadRequiredString("Auditorium ID");

                var auditorium = auditoriums.FirstOrDefault(a => a.Id == auditoriumId);
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

                var seance = new Seance(filmId, auditoriumId, startTime, price, film.DurationMinutes);
                seances.Add(seance);

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

        static void ViewAllSeances(List<Seance> seances)
        {
            Console.Clear();
            Console.WriteLine("=== ALL SEANCES ===");

            if (seances.Count == 0)
            {
                Console.WriteLine("No seances found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            foreach (var seance in seances)
            {
                Console.WriteLine(seance.ToString());
                Console.WriteLine("----------------------------------------");
            }
            ConsoleHelper.WaitForKey();
        }

        static void FindSeanceById(List<Seance> seances)
        {
            Console.Clear();
            Console.WriteLine("=== FIND SEANCE BY ID ===");

            Console.Write("Enter seance ID: ");
            string id = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = seances.FirstOrDefault(s => s.Id == id);
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

        static void AddOccupiedSeat(List<Seance> seances, List<Auditorium> auditoriums)
        {
            Console.Clear();
            Console.WriteLine("=== ADD OCCUPIED SEAT ===");

            Console.Write("Enter seance ID: ");
            string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = seances.FirstOrDefault(s => s.Id == seanceId);
            if (seance == null)
            {
                Console.WriteLine("Seance with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter seat ID: ");
            string seatId = ConsoleHelper.ReadRequiredString("Seat ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == seance.AuditoriumId);
            if (auditorium == null)
            {
                Console.WriteLine("Auditorium for this seance not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            if (seance.ReserveSeat(seatId, auditorium.Capacity))
            {
                Console.WriteLine("Seat successfully reserved!");
                Console.WriteLine($"Available seats remaining: {seance.AvailableSeats(auditorium.Capacity)}");
            }
            else
            {
                Console.WriteLine("Failed to reserve seat. Possibly reached capacity limit or seat already reserved.");
            }
            ConsoleHelper.WaitForKey();
        }

        static void RemoveOccupiedSeat(List<Seance> seances, List<Auditorium> auditoriums)
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE OCCUPIED SEAT ===");

            Console.Write("Enter seance ID: ");
            string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = seances.FirstOrDefault(s => s.Id == seanceId);
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
                var auditorium = auditoriums.FirstOrDefault(a => a.Id == seance.AuditoriumId);
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

        static void UpdateSeanceTime(List<Seance> seances, List<Film> films)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE SEANCE TIME ===");

            Console.Write("Enter seance ID: ");
            string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = seances.FirstOrDefault(s => s.Id == seanceId);
            if (seance == null)
            {
                Console.WriteLine("Seance with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter new start time (yyyy-mm-dd hh:mm): ");
            DateTime newStartTime = ConsoleHelper.ReadDateTime();

            var film = films.FirstOrDefault(f => f.Id == seance.FilmId);
            if (film == null)
            {
                Console.WriteLine("Film for this seance not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            try
            {
                seance.UpdateTime(newStartTime, film.DurationMinutes);
                Console.WriteLine($"Seance time updated successfully! New end time: {seance.EndTime}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void UpdateSeancePrice(List<Seance> seances)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE SEANCE PRICE ===");

            Console.Write("Enter seance ID: ");
            string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = seances.FirstOrDefault(s => s.Id == seanceId);
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
                Console.WriteLine($"Seance price updated successfully! New price: {seance.Price}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void DeleteSeance(List<Seance> seances, List<Reservation> reservations, List<Ticket> tickets)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE SEANCE ===");

            Console.Write("Enter seance ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Seance ID");

            var seance = seances.FirstOrDefault(s => s.Id == id);
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
                    DeleteHandler.DeleteSeance(seances, reservations, tickets, id);
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