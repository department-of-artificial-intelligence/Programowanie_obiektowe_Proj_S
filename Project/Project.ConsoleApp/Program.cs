using Microsoft.EntityFrameworkCore;
using Project.ConsoleApp.Helpers;
using Project.ConsoleApp.Menues;
using Project.DAL;
using Project.Models;

namespace Project.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            ApplicationDBContext context = DBManager.BuildDB();
            DBManager.ClearDB();

            SeedSampleData(context);
            ShowMainMenu(context);
        }

        static void ShowMainMenu(ApplicationDBContext context)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CINEMA MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Manage Actors");
                Console.WriteLine("2. Manage Cinemas");
                Console.WriteLine("3. Manage Auditoriums");
                Console.WriteLine("4. Manage Films");
                Console.WriteLine("5. Manage Seances");
                Console.WriteLine("6. Manage Reservations");
                Console.WriteLine("7. Manage Tickets");
                Console.WriteLine("8. Manage Cinema Networks");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ActorMenu.ShowActorMenu(context); break;
                    case "2": CinemaMenu.ShowCinemaMenu(context); break;
                    case "3": AuditoriumMenu.ShowAuditoriumMenu(context); break;
                    case "4": FilmMenu.ShowFilmMenu(context); break;
                    case "5": SeanceMenu.ShowSeanceMenu(context); break;
                    case "6": ReservationMenu.ShowReservationMenu(context); break;
                    case "7": TicketMenu.ShowTicketMenu(context); break;
                    case "8": CinemaNetworkMenu.ShowCinemaNetworkMenu(context); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void SeedSampleData(ApplicationDBContext context)
        {
            try
            {
                Console.WriteLine("Creating sample data...");

                // Actors
                var actor1 = new Actor("Tom", "Hanks", "American", new DateTime(1956, 7, 9),
                    "https://example.com/tom_hanks.jpg", "Famous American actor known for Forrest Gump and Cast Away.", 95.5);
                var actor2 = new Actor("Meryl", "Streep", "American", new DateTime(1949, 6, 22),
                    "https://example.com/meryl_streep.jpg", "Legendary American actress with multiple Academy Awards.", 98.2);
                var actor3 = new Actor("Leonardo", "DiCaprio", "American", new DateTime(1974, 11, 11),
                    "https://example.com/leo.jpg", "Academy Award winner known for Titanic and The Revenant.", 97.8);
                var actor4 = new Actor("Scarlett", "Johansson", "American", new DateTime(1984, 11, 22),
                    "https://example.com/scarlett.jpg", "Highest-grossing actress known for Marvel films.", 96.3);
                var actor5 = new Actor("Denzel", "Washington", "American", new DateTime(1954, 12, 28),
                    "https://example.com/denzel.jpg", "Two-time Academy Award winner and renowned dramatic actor.", 94.7);
                var actor6 = new Actor("Cate", "Blanchett", "Australian", new DateTime(1969, 5, 14),
                    "https://example.com/cate.jpg", "Two-time Academy Award winner known for versatile roles.", 93.2);

                // Cinemas
                var cinema1 = new Cinema("Multiplex Cinema", "123 Main Street, Kyiv", "+380441234567", "info@multiplex.ua", "Ivan Petrenko");
                var cinema2 = new Cinema("Star Cinema", "456 Central Avenue, Kyiv", "+380441234568", "info@starcinema.ua", "Olena Kovalenko");
                var cinema3 = new Cinema("City Lights", "789 Broadway, Kyiv", "+380441234569", "info@citylights.ua", "Petro Sydorenko");

                // Auditoriums
                var auditorium1 = new Auditorium(cinema1.Id, "IMAX Hall", 1, 12, 20);
                auditorium1.AddItem("Dolby Atmos");
                auditorium1.AddItem("3D Projection");
                auditorium1.AddItem("Laser Projection");

                var auditorium2 = new Auditorium(cinema1.Id, "VIP Hall", 2, 8, 15);
                auditorium2.AddItem("Recliner Seats");
                auditorium2.AddItem("Food Service");
                auditorium2.AddItem("Dolby 7.1");

                var auditorium3 = new Auditorium(cinema2.Id, "Main Hall", 1, 10, 18);
                auditorium3.AddItem("Dolby Digital");
                auditorium3.AddItem("3D Ready");

                var auditorium4 = new Auditorium(cinema3.Id, "Premium Hall", 1, 6, 12);
                auditorium4.AddItem("4K Projection");
                auditorium4.AddItem("Atmos Sound");
                auditorium4.AddItem("Butler Service");

                // Auditorium Ratings
                auditorium1.AddRating(5);
                auditorium1.AddRating(4);
                auditorium2.AddRating(5);
                auditorium2.AddRating(5);
                auditorium3.AddRating(4);
                auditorium3.AddRating(3);

                // Films
                var film1 = new Film("Forrest Gump", "The story of a man with low IQ who accomplished great things in his life",
                    142, "Robert Zemeckis", "Drama", false,
                    "https://example.com/forrest_gump.jpg", "https://example.com/forrest_trailer");

                var film2 = new Film("Inception", "A thief who steals corporate secrets through dream-sharing technology",
                    148, "Christopher Nolan", "Sci-Fi", false,
                    "https://example.com/inception.jpg", "https://example.com/inception_trailer");

                var film3 = new Film("The Dark Knight", "Batman faces the Joker, a criminal mastermind seeking to create chaos",
                    152, "Christopher Nolan", "Action", true,
                    "https://example.com/dark_knight.jpg", "https://example.com/dark_knight_trailer");

                var film4 = new Film("The Shawshank Redemption", "Two imprisoned men bond over a number of years finding solace",
                    142, "Frank Darabont", "Drama", false,
                    "https://example.com/shawshank.jpg", "https://example.com/shawshank_trailer");

                var film5 = new Film("Avengers: Endgame", "The Avengers take one final stand against Thanos",
                    181, "Anthony Russo", "Action", false,
                    "https://example.com/endgame.jpg", "https://example.com/endgame_trailer");

                var film6 = new Film("La La Land", "A jazz pianist and an aspiring actress pursue their dreams in Los Angeles",
                    128, "Damien Chazelle", "Musical", false,
                    "https://example.com/lalaland.jpg", "https://example.com/lalaland_trailer");

                // Adding Actors To Films
                film1.AddItem(actor1.Id);
                film2.AddItem(actor3.Id);
                film2.AddItem(actor4.Id);
                film3.AddItem(actor3.Id);
                film3.AddItem(actor5.Id);
                film4.AddItem(actor1.Id);
                film5.AddItem(actor4.Id);
                film6.AddItem(actor3.Id);
                film6.AddItem(actor6.Id);

                // Adding Films To Cinemas
                cinema1.AddItem(film1.Id);
                cinema1.AddItem(film2.Id);
                cinema1.AddItem(film3.Id);
                cinema1.AddItem(film4.Id);

                cinema2.AddItem(film2.Id);
                cinema2.AddItem(film3.Id);
                cinema2.AddItem(film5.Id);

                cinema3.AddItem(film1.Id);
                cinema3.AddItem(film4.Id);
                cinema3.AddItem(film6.Id);

                // Film Ratings
                film1.AddRating(5);
                film1.AddRating(4);
                film1.AddRating(5);
                film2.AddRating(5);
                film2.AddRating(5);
                film2.AddRating(4);
                film3.AddRating(5);
                film3.AddRating(5);
                film3.AddRating(5);
                film4.AddRating(5);
                film4.AddRating(5);
                film4.AddRating(4);
                film5.AddRating(4);
                film5.AddRating(4);
                film5.AddRating(3);
                film6.AddRating(4);
                film6.AddRating(5);

                // Cinema Ratings
                cinema1.AddRating(5);
                cinema1.AddRating(4);
                cinema1.AddRating(5);
                cinema2.AddRating(4);
                cinema2.AddRating(4);
                cinema2.AddRating(3);
                cinema3.AddRating(5);
                cinema3.AddRating(5);

                // Seances
                var seance1 = new Seance(film1.Id, auditorium1.Id, DateTime.Now.AddSeconds(4), 250.0m, film1.DurationMinutes);
                var seance2 = new Seance(film2.Id, auditorium1.Id, DateTime.Now.AddHours(5), 280.0m, film2.DurationMinutes);
                var seance3 = new Seance(film3.Id, auditorium2.Id, DateTime.Now.AddHours(6), 350.0m, film3.DurationMinutes);
                var seance4 = new Seance(film4.Id, auditorium3.Id, DateTime.Now.AddHours(8), 200.0m, film4.DurationMinutes);
                var seance5 = new Seance(film5.Id, auditorium3.Id, DateTime.Now.AddHours(10), 300.0m, film5.DurationMinutes);
                var seance6 = new Seance(film6.Id, auditorium4.Id, DateTime.Now.AddHours(25), 400.0m, film6.DurationMinutes);

                // Adding Reservated Seats
                seance1.ReserveSeat("A1", auditorium1.Capacity);
                seance1.ReserveSeat("A2", auditorium1.Capacity);
                seance1.ReserveSeat("B5", auditorium1.Capacity);

                seance3.ReserveSeat("C3", auditorium2.Capacity);
                seance3.ReserveSeat("C4", auditorium2.Capacity);
                seance3.ReserveSeat("D1", auditorium2.Capacity);
                seance3.ReserveSeat("D2", auditorium2.Capacity);

                seance6.ReserveSeat("A1", auditorium4.Capacity);
                seance6.ReserveSeat("A2", auditorium4.Capacity);

                // Reservations
                var reservation1 = new Reservation(seance1.Id, "John", "Doe", "john.doe@email.com", "+380501234567", "Credit Card");
                var reservation2 = new Reservation(seance3.Id, "Jane", "Smith", "jane.smith@email.com", "+380502345678", "Cash");
                var reservation3 = new Reservation(seance6.Id, "Bob", "Johnson", "bob.johnson@email.com", "+380503456789", "Online Payment");

                // Tickets
                var ticket1 = new Ticket(reservation1.Id, cinema1.Id, auditorium1.Id, seance1.Id, film1.Id, "A3", seance1.Price, TicketType.Standard);
                var ticket2 = new Ticket(reservation1.Id, cinema1.Id, auditorium1.Id, seance1.Id, film1.Id, "A4", seance1.Price, TicketType.Student);
                var ticket3 = new Ticket(reservation2.Id, cinema1.Id, auditorium2.Id, seance3.Id, film3.Id, "C5", seance3.Price, TicketType.VIP);
                var ticket4 = new Ticket(reservation3.Id, cinema3.Id, auditorium4.Id, seance6.Id, film6.Id, "A3", seance6.Price, TicketType.VIP);
                var ticket5 = new Ticket(reservation3.Id, cinema3.Id, auditorium4.Id, seance6.Id, film6.Id, "A4", seance6.Price, TicketType.Standard);

                // Cinema Networks
                var network1 = new CinemaNetwork("CinemaMax Ukraine", "Olena Sydorenko");
                network1.SetTotalCinemas(15);

                var network2 = new CinemaNetwork("MovieStar Group", "Mykola Ivanov");
                network2.SetTotalCinemas(8);

                var network3 = new CinemaNetwork("Film Paradise", "Svitlana Petrenko");
                network3.SetTotalCinemas(12);

                // Saving To DB
                context.Actors.AddRange([actor1, actor2, actor3, actor4, actor5, actor6]);
                context.Cinemas.AddRange([cinema1, cinema2, cinema3]);
                context.Auditoriums.AddRange([auditorium1, auditorium2, auditorium3, auditorium4]);
                context.Films.AddRange([film1, film2, film3, film4, film5, film6]);
                context.Seances.AddRange([seance1, seance2, seance3, seance4, seance5, seance6]);
                context.Reservations.AddRange([reservation1, reservation2, reservation3]);
                context.Tickets.AddRange([ticket1, ticket2, ticket3, ticket4, ticket5]);
                context.CinemaNetworks.AddRange([network1, network2, network3]);

                context.SaveChanges();

                Console.WriteLine("Sample data created successfully!");
                Thread.Sleep(2500);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating sample data: {ex.Message}");
                ConsoleHelper.WaitForKey();
            }
        }
    }
}