using Project.ConsoleApp.Helpers;
using Project.Services.Common;
using Project.Services;
using Project.DAL;

namespace Project.ConsoleApp.Menues
{
    public static class ReservationMenu
    {
        public static void ShowReservationMenu(ApplicationDBContext context)
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
                    case "1": AddReservation(context); break;
                    case "2": ViewAllReservations(context); break;
                    case "3": FindReservationById(context); break;
                    case "4": UpdateReservation(context); break;
                    case "5": ShowReservationSortFilterMenu(context); break;
                    case "6": DeleteReservation(context); break;
                    case "0": return;
                    default:  Console.WriteLine("Invalid choice!"); 
                              ConsoleHelper.WaitForKey(); 
                              break;
                }
            }
        }

        static void ShowReservationSortFilterMenu(ApplicationDBContext context)
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
                        var newestReservations = BaseService.SortByTimeNewest(ReservationService.GetAll(context));
                        DisplayHelper.DisplayReservations(newestReservations, "Reservations (Newest First)");
                        break;
                    case "2":
                        var oldestReservations = BaseService.SortByTimeOldest(ReservationService.GetAll(context));
                        DisplayHelper.DisplayReservations(oldestReservations, "Reservations (Oldest First)");
                        break;
                    case "3":
                        Console.Write("Enter seance ID to filter: ");
                        string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

                        var seanceReservations = ReservationService.FilterBySeanceId(context, seanceId);
                        DisplayHelper.DisplayReservations(seanceReservations, $"Reservations for seance {seanceId}");
                        break;
                    case "4":
                        Console.Write("Enter payment method to filter: ");
                        string paymentMethod = ConsoleHelper.ReadRequiredString("Payment method");

                        var paymentReservations = ReservationService.FilterByPaymentMethod(context, paymentMethod);
                        DisplayHelper.DisplayReservations(paymentReservations, $"Reservations with payment method '{paymentMethod}'");
                        break;
                    case "0": return;
                    default:  Console.WriteLine("Invalid choice!"); 
                              ConsoleHelper.WaitForKey(); 
                              break;
                }
            }
        }

        static void AddReservation(ApplicationDBContext context)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD RESERVATION ===");

                Console.Write("Seance ID: ");
                string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

                var seance = SeanceService.GetById(context, seanceId);

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


                var reservation = ReservationService.Add(context, seanceId, customerFirstName, customerLastName, customerEmail, customerPhone, paymentMethod);

                Console.WriteLine($"\nReservation added successfully! ID: {reservation.Id}");
                Console.WriteLine($"Customer: {reservation.CustomerFullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void ViewAllReservations(ApplicationDBContext context)
        {
            var reservations = ReservationService.GetAll(context);
            DisplayHelper.DisplayReservations(reservations, "All Reservations");
        }

        static void FindReservationById(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== FIND RESERVATION BY ID ===");

            Console.Write("Enter reservation ID: ");
            string id = ConsoleHelper.ReadRequiredString("Reservation ID");

            var reservation = ReservationService.GetById(context, id);

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

        static void UpdateReservation(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE RESERVATION ===");

            Console.Write("Enter reservation ID to update: ");
            string id = ConsoleHelper.ReadRequiredString("Reservation ID");

            var reservation = ReservationService.GetById(context, id);

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
                ReservationService.Update(context, reservation);

                Console.WriteLine("Reservation information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void DeleteReservation(ApplicationDBContext context)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE RESERVATION ===");

            Console.Write("Enter reservation ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Reservation ID");

            var reservation = ReservationService.GetById(context, id);

            if (reservation != null)
            {
                Console.WriteLine($"\nReservation to delete: {reservation.CustomerFullName}");
                Console.WriteLine("This will also delete all tickets for this reservation.");
                Console.Write("Are you sure you want to delete this reservation? (yes/no): ");

                string? confirmation = Console.ReadLine()?.ToLower();

                if (confirmation == "yes" || confirmation == "y")
                {
                    ReservationService.Delete(context, id);
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