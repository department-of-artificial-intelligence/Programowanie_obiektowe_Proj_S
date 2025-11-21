using Project.Models;
using Project.ConsoleApp.Helpers;
using Project.Logic.Handlers;
using Project.Logic.SortingFiltering;

namespace Project.ConsoleApp.Menues
{
    public static class ReservationMenu
    {
        public static void ShowReservationMenu(List<Reservation> reservations, List<Seance> seances, List<Ticket> tickets)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== RESERVATION MANAGEMENT ===");
                Console.WriteLine("1. Add Reservation");
                Console.WriteLine("2. View All Reservations");
                Console.WriteLine("3. Find Reservation by ID");
                Console.WriteLine("4. Update Reservation Info");
                Console.WriteLine("5. Sort and Filter Reservations");
                Console.WriteLine("6. Delete Reservation");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddReservation(reservations, seances); break;
                    case "2": ViewAllReservations(reservations); break;
                    case "3": FindReservationById(reservations); break;
                    case "4": UpdateReservation(reservations); break;
                    case "5": ShowReservationSortFilterMenu(reservations); break;
                    case "6": DeleteReservation(reservations, tickets); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void ShowReservationSortFilterMenu(List<Reservation> reservations)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== RESERVATION SORTING & FILTERING ===");
                Console.WriteLine("1. Sort by Time (Newest First)");
                Console.WriteLine("2. Sort by Time (Oldest First)");
                Console.WriteLine("3. Filter by Seance ID");
                Console.WriteLine("4. Filter by Payment Method");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var newestReservations = GenericSorting.SortByTimeNewest(reservations);
                        DisplayHelper.DisplayReservations(newestReservations, "Reservations (Newest First)");
                        break;
                    case "2":
                        var oldestReservations = GenericSorting.SortByTimeOldest(reservations);
                        DisplayHelper.DisplayReservations(oldestReservations, "Reservations (Oldest First)");
                        break;
                    case "3":
                        Console.Write("Enter seance ID to filter: ");
                        string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");
                        var seanceReservations = ReservationSortingFiltering.FilterReservationsBySeanceId(reservations, seanceId);
                        DisplayHelper.DisplayReservations(seanceReservations, $"Reservations for seance {seanceId}");
                        break;
                    case "4":
                        Console.Write("Enter payment method to filter: ");
                        string paymentMethod = ConsoleHelper.ReadRequiredString("Payment method");
                        var paymentReservations = ReservationSortingFiltering.FilterReservationsByPaymentMethod(reservations, paymentMethod);
                        DisplayHelper.DisplayReservations(paymentReservations, $"Reservations with payment method '{paymentMethod}'");
                        break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void AddReservation(List<Reservation> reservations, List<Seance> seances)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD RESERVATION ===");

                Console.Write("Seance ID: ");
                string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

                var seance = seances.FirstOrDefault(s => s.Id == seanceId);
                if (seance == null)
                {
                    Console.WriteLine("Seance with this ID not found.");
                    ConsoleHelper.WaitForKey();
                    return;
                }

                Console.Write("Customer First Name: ");
                string customerFirstName = ConsoleHelper.ReadRequiredString("Customer first name");

                Console.Write("Customer Last Name: ");
                string customerLastName = ConsoleHelper.ReadRequiredString("Customer last name");

                Console.Write("Customer Email: ");
                string customerEmail = ConsoleHelper.ReadRequiredString("Customer email");

                Console.Write("Customer Phone: ");
                string customerPhone = ConsoleHelper.ReadRequiredString("Customer phone");

                Console.Write("Payment Method: ");
                string paymentMethod = ConsoleHelper.ReadRequiredString("Payment method");

                var reservation = new Reservation(seanceId, customerFirstName, customerLastName, customerEmail, customerPhone, paymentMethod);
                reservations.Add(reservation);

                Console.WriteLine($"\nReservation added successfully! ID: {reservation.Id}");
                Console.WriteLine($"Customer: {reservation.CustomerFullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void ViewAllReservations(List<Reservation> reservations)
        {
            Console.Clear();
            Console.WriteLine("=== ALL RESERVATIONS ===");

            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            foreach (var reservation in reservations)
            {
                Console.WriteLine(reservation.ToString());
                Console.WriteLine("----------------------------------------");
            }
            ConsoleHelper.WaitForKey();
        }

        static void FindReservationById(List<Reservation> reservations)
        {
            Console.Clear();
            Console.WriteLine("=== FIND RESERVATION BY ID ===");

            Console.Write("Enter reservation ID: ");
            string id = ConsoleHelper.ReadRequiredString("Reservation ID");

            var reservation = reservations.FirstOrDefault(r => r.Id == id);
            if (reservation != null)
            {
                Console.WriteLine(reservation.ToString());
            }
            else
            {
                Console.WriteLine("Reservation with this ID not found.");
            }
            ConsoleHelper.WaitForKey();
        }

        static void UpdateReservation(List<Reservation> reservations)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE RESERVATION ===");

            Console.Write("Enter reservation ID to update: ");
            string id = ConsoleHelper.ReadRequiredString("Reservation ID");

            var reservation = reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                Console.WriteLine("Reservation with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            try
            {
                Console.Write($"New first name (current: {reservation.CustomerFirstName}): ");
                string firstName = Console.ReadLine() ?? reservation.CustomerFirstName;

                Console.Write($"New last name (current: {reservation.CustomerLastName}): ");
                string lastName = Console.ReadLine() ?? reservation.CustomerLastName;

                Console.Write($"New email (current: {reservation.CustomerEmail}): ");
                string email = Console.ReadLine() ?? reservation.CustomerEmail;

                Console.Write($"New phone (current: {reservation.CustomerPhone}): ");
                string phone = Console.ReadLine() ?? reservation.CustomerPhone;

                reservation.UpdateCustomerInfo(firstName, lastName, email, phone);
                Console.WriteLine("Reservation information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void DeleteReservation(List<Reservation> reservations, List<Ticket> tickets)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE RESERVATION ===");

            Console.Write("Enter reservation ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Reservation ID");

            var reservation = reservations.FirstOrDefault(r => r.Id == id);
            if (reservation != null)
            {
                Console.WriteLine($"\nReservation to delete: {reservation.CustomerFullName}");
                Console.WriteLine("This will also delete all tickets for this reservation.");
                Console.Write("Are you sure you want to delete this reservation? (yes/no): ");

                string? confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "yes" || confirmation == "y")
                {
                    DeleteHandler.DeleteReservation(reservations, tickets, id);
                    Console.WriteLine("Reservation and all related tickets deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Reservation with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }
    }
}