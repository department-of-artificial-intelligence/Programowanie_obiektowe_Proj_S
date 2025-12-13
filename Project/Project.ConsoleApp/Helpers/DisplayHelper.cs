using Project.Models;

namespace Project.ConsoleApp.Helpers
{
    public static class DisplayHelper
    {
        public static void DisplayActors(List<Actor> actorsToDisplay, string title)
        {
            Console.Clear();
            Console.WriteLine($"=== {title} ===");

            if (actorsToDisplay.Count == 0)
            {
                Console.WriteLine("No actors found.");
            }
            else
            {
                foreach (var actor in actorsToDisplay)
                {
                    Console.WriteLine(actor.ToString());
                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine($"\nTotal: {actorsToDisplay.Count} actors");
            }

            ConsoleHelper.WaitForKey();
        }

        public static void DisplayCinemas(List<Cinema> cinemasToDisplay, string title)
        {
            Console.Clear();
            Console.WriteLine($"=== {title} ===");

            if (cinemasToDisplay.Count == 0)
            {
                Console.WriteLine("No cinemas found.");
            }
            else
            {
                foreach (var cinema in cinemasToDisplay)
                {
                    Console.WriteLine(cinema.ToString());
                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine($"\nTotal: {cinemasToDisplay.Count} cinemas");
            }

            ConsoleHelper.WaitForKey();
        }

        public static void DisplayAuditoriums(List<Auditorium> auditoriumsToDisplay, string title)
        {
            Console.Clear();
            Console.WriteLine($"=== {title} ===");

            if (auditoriumsToDisplay.Count == 0)
            {
                Console.WriteLine("No auditoriums found.");
            }
            else
            {
                foreach (var auditorium in auditoriumsToDisplay)
                {
                    Console.WriteLine(auditorium.ToString());
                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine($"\nTotal: {auditoriumsToDisplay.Count} auditoriums");
            }

            ConsoleHelper.WaitForKey();
        }

        public static void DisplayFilms(List<Film> filmsToDisplay, string title)
        {
            Console.Clear();
            Console.WriteLine($"=== {title} ===");

            if (filmsToDisplay.Count == 0)
            {
                Console.WriteLine("No films found.");
            }
            else
            {
                foreach (var film in filmsToDisplay)
                {
                    Console.WriteLine(film.ToString());
                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine($"\nTotal: {filmsToDisplay.Count} films");
            }

            ConsoleHelper.WaitForKey();
        }

        public static void DisplaySeances(List<Seance> seancesToDisplay, string title)
        {
            Console.Clear();
            Console.WriteLine($"=== {title} ===");

            if (seancesToDisplay.Count == 0)
            {
                Console.WriteLine("No seances found.");
            }
            else
            {
                foreach (var seance in seancesToDisplay)
                {
                    Console.WriteLine(seance.ToString());
                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine($"\nTotal: {seancesToDisplay.Count} seances");
            }

            ConsoleHelper.WaitForKey();
        }

        public static void DisplayReservations(List<Reservation> reservationsToDisplay, string title)
        {
            Console.Clear();
            Console.WriteLine($"=== {title} ===");

            if (reservationsToDisplay.Count == 0)
            {
                Console.WriteLine("No reservations found.");
            }
            else
            {
                foreach (var reservation in reservationsToDisplay)
                {
                    Console.WriteLine(reservation.ToString());
                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine($"\nTotal: {reservationsToDisplay.Count} reservations");
            }

            ConsoleHelper.WaitForKey();
        }

        public static void DisplayTickets(List<Ticket> ticketsToDisplay, string title)
        {
            Console.Clear();
            Console.WriteLine($"=== {title} ===");

            if (ticketsToDisplay.Count == 0)
            {
                Console.WriteLine("No tickets found.");
            }
            else
            {
                foreach (var ticket in ticketsToDisplay)
                {
                    Console.WriteLine(ticket.ToString());
                    Console.WriteLine("----------------------------------------");
                }

                Console.WriteLine($"\nTotal: {ticketsToDisplay.Count} tickets");
            }

            ConsoleHelper.WaitForKey();
        }
    }
}