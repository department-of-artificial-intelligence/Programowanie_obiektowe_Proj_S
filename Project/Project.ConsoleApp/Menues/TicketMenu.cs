using Project.Models;
using Project.ConsoleApp.Helpers;
using Project.Services;
using Project.Services.Common;


namespace Project.ConsoleApp.Menues
{
    public static class TicketMenu
    {
        public static void ShowTicketMenu(List<Ticket> tickets, List<Seance> seances, List<Reservation> reservations,
            List<Cinema> cinemas, List<Auditorium> auditoriums, List<Film> films)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== TICKET MANAGEMENT ===");
                Console.WriteLine("1. Add Ticket");
                Console.WriteLine("2. View All Tickets");
                Console.WriteLine("3. Find Ticket by ID");
                Console.WriteLine("4. Update Ticket Type");
                Console.WriteLine("5. Sort and Filter Tickets");
                Console.WriteLine("6. Delete Ticket");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddTicket(tickets, seances, reservations, cinemas, auditoriums, films); break;
                    case "2": ViewAllTickets(tickets); break;
                    case "3": FindTicketById(tickets); break;
                    case "4": UpdateTicketType(tickets); break;
                    case "5": ShowTicketSortFilterMenu(tickets); break;
                    case "6": DeleteTicket(tickets); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void ShowTicketSortFilterMenu(List<Ticket> tickets)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== TICKET SORTING & FILTERING ===");
                Console.WriteLine("1. Sort by Time (Newest First)");
                Console.WriteLine("2. Sort by Time (Oldest First)");
                Console.WriteLine("3. Sort by Final Price");
                Console.WriteLine("4. Filter by Reservation ID");
                Console.WriteLine("5. Filter by Cinema ID");
                Console.WriteLine("6. Filter by Seance ID");
                Console.WriteLine("7. Filter by Film ID");
                Console.WriteLine("8. Filter by Auditorium ID");
                Console.WriteLine("9. Filter by Ticket Type");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var newestTickets = BaseService.SortByTimeNewest(tickets);
                        DisplayHelper.DisplayTickets(newestTickets, "Tickets (Newest First)");
                        break;
                    case "2":
                        var oldestTickets = BaseService.SortByTimeOldest(tickets);
                        DisplayHelper.DisplayTickets(oldestTickets, "Tickets (Oldest First)");
                        break;
                    case "3":
                        var priceTickets = TicketService.SortTicketsByFinalPrice(tickets);
                        DisplayHelper.DisplayTickets(priceTickets, "Tickets by Final Price");
                        break;
                    case "4":
                        Console.Write("Enter reservation ID to filter: ");
                        string reservationId = ConsoleHelper.ReadRequiredString("Reservation ID");
                        var reservationTickets = TicketService.FilterTicketsByReservationId(tickets, reservationId);
                        DisplayHelper.DisplayTickets(reservationTickets, $"Tickets for reservation {reservationId}");
                        break;
                    case "5":
                        Console.Write("Enter cinema ID to filter: ");
                        string cinemaId = ConsoleHelper.ReadRequiredString("Cinema ID");
                        var cinemaTickets = TicketService.FilterTicketsByCinemaId(tickets, cinemaId);
                        DisplayHelper.DisplayTickets(cinemaTickets, $"Tickets for cinema {cinemaId}");
                        break;
                    case "6":
                        Console.Write("Enter seance ID to filter: ");
                        string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");
                        var seanceTickets = TicketService.FilterTicketsBySeanceId(tickets, seanceId);
                        DisplayHelper.DisplayTickets(seanceTickets, $"Tickets for seance {seanceId}");
                        break;
                    case "7":
                        Console.Write("Enter film ID to filter: ");
                        string filmId = ConsoleHelper.ReadRequiredString("Film ID");
                        var filmTickets = TicketService.FilterTicketsByFilmId(tickets, filmId);
                        DisplayHelper.DisplayTickets(filmTickets, $"Tickets for film {filmId}");
                        break;
                    case "8":
                        Console.Write("Enter auditorium ID to filter: ");
                        string auditoriumId = ConsoleHelper.ReadRequiredString("Auditorium ID");
                        var auditoriumTickets = TicketService.FilterTicketsByAuditoriumId(tickets, auditoriumId);
                        DisplayHelper.DisplayTickets(auditoriumTickets, $"Tickets for auditorium {auditoriumId}");
                        break;
                    case "9":
                        Console.WriteLine("Ticket types: Standard, Student, Senior, Child, VIP");
                        Console.Write("Enter ticket type to filter: ");
                        string ticketTypeInput = ConsoleHelper.ReadRequiredString("Ticket type");
                        if (!Enum.TryParse(ticketTypeInput, true, out TicketType ticketType))
                        {
                            Console.WriteLine("Invalid ticket type.");
                            ConsoleHelper.WaitForKey();
                            break;
                        }
                        var typeTickets = TicketService.FilterTicketsByTicketType(tickets, ticketType);
                        DisplayHelper.DisplayTickets(typeTickets, $"Tickets of type {ticketType}");
                        break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void AddTicket(List<Ticket> tickets, List<Seance> seances, List<Reservation> reservations,
            List<Cinema> cinemas, List<Auditorium> auditoriums, List<Film> films)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD TICKET ===");

                Console.Write("Reservation ID: ");
                string reservationId = ConsoleHelper.ReadRequiredString("Reservation ID");

                var reservation = reservations.FirstOrDefault(r => r.Id == reservationId);
                if (reservation == null)
                {
                    Console.WriteLine("Reservation with this ID not found.");
                    ConsoleHelper.WaitForKey();
                    return;
                }

                Console.Write("Cinema ID: ");
                string cinemaId = ConsoleHelper.ReadRequiredString("Cinema ID");

                var cinema = cinemas.FirstOrDefault(c => c.Id == cinemaId);
                if (cinema == null)
                {
                    Console.WriteLine("Cinema with this ID not found.");
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

                Console.Write("Seance ID: ");
                string seanceId = ConsoleHelper.ReadRequiredString("Seance ID");

                var seance = seances.FirstOrDefault(s => s.Id == seanceId);
                if (seance == null)
                {
                    Console.WriteLine("Seance with this ID not found.");
                    ConsoleHelper.WaitForKey();
                    return;
                }

                Console.Write("Film ID: ");
                string filmId = ConsoleHelper.ReadRequiredString("Film ID");

                var film = films.FirstOrDefault(f => f.Id == filmId);
                if (film == null)
                {
                    Console.WriteLine("Film with this ID not found.");
                    ConsoleHelper.WaitForKey();
                    return;
                }

                Console.Write("Seat ID: ");
                string seatId = ConsoleHelper.ReadRequiredString("Seat ID");

                Console.WriteLine("Ticket types: Standard, Student, Senior, Child, VIP");
                Console.Write("Ticket type: ");
                string ticketTypeInput = ConsoleHelper.ReadRequiredString("Ticket type");

                if (!Enum.TryParse(ticketTypeInput, true, out TicketType ticketType))
                {
                    Console.WriteLine("Invalid ticket type. Using Standard.");
                    ticketType = TicketType.Standard;
                }

                var ticket = new Ticket(reservationId, cinemaId, auditoriumId, seanceId, filmId, seatId, seance.Price, ticketType);
                tickets.Add(ticket);

                Console.WriteLine($"\nTicket added successfully! ID: {ticket.Id}");
                Console.WriteLine($"Final Price: {ticket.FinalPrice} (Discount: {ticket.Discount})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void ViewAllTickets(List<Ticket> tickets)
        {
            Console.Clear();
            Console.WriteLine("=== ALL TICKETS ===");

            if (tickets.Count == 0)
            {
                Console.WriteLine("No tickets found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            foreach (var ticket in tickets)
            {
                Console.WriteLine(ticket.ToString());
                Console.WriteLine("----------------------------------------");
            }
            ConsoleHelper.WaitForKey();
        }

        static void FindTicketById(List<Ticket> tickets)
        {
            Console.Clear();
            Console.WriteLine("=== FIND TICKET BY ID ===");

            Console.Write("Enter ticket ID: ");
            string id = ConsoleHelper.ReadRequiredString("Ticket ID");

            var ticket = tickets.FirstOrDefault(t => t.Id == id);
            if (ticket != null)
            {
                Console.WriteLine(ticket.ToString());
            }
            else
            {
                Console.WriteLine("Ticket with this ID not found.");
            }
            ConsoleHelper.WaitForKey();
        }

        static void UpdateTicketType(List<Ticket> tickets)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE TICKET TYPE ===");

            Console.Write("Enter ticket ID: ");
            string id = ConsoleHelper.ReadRequiredString("Ticket ID");

            var ticket = tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                Console.WriteLine("Ticket with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.WriteLine($"Current ticket type: {ticket.Type}");
            Console.WriteLine("Available types: Standard, Student, Senior, Child, VIP");
            Console.Write("New ticket type: ");
            string ticketTypeInput = ConsoleHelper.ReadRequiredString("Ticket type");

            if (!Enum.TryParse(ticketTypeInput, true, out TicketType newType))
            {
                Console.WriteLine("Invalid ticket type.");
                ConsoleHelper.WaitForKey();
                return;
            }

            try
            {
                ticket.UpdateTicketType(newType);
                Console.WriteLine($"Ticket type updated successfully! New price: {ticket.FinalPrice}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void DeleteTicket(List<Ticket> tickets)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE TICKET ===");

            Console.Write("Enter ticket ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Ticket ID");

            var ticket = tickets.FirstOrDefault(t => t.Id == id);
            if (ticket != null)
            {
                Console.WriteLine($"\nTicket to delete: {ticket.Type} ticket for seat {ticket.SeatId}");
                Console.Write("Are you sure you want to delete this ticket? (yes/no): ");

                string? confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "yes" || confirmation == "y")
                {
                    TicketService.DeleteTicket(tickets, id);
                    Console.WriteLine("Ticket deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Ticket with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }
    }
}