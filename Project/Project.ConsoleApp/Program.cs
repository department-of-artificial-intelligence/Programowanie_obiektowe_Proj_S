using System;
using System.Collections.Generic;
using System.Linq;
using Project.Models;
using Project.Entities;
using Project.Interfaces;
using Project.Utils;

namespace Project.ConsoleApp
{
    class Program
    {
        private readonly static List<Actor> actors = [];
        private readonly static List<Auditorium> auditoriums = [];
        private readonly static List<Cinema> cinemas = [];
        private readonly static List<CinemaNetwork> cinemaNetworks = [];
        private readonly static List<Film> films = [];
        private readonly static List<Reservation> reservations = [];
        private readonly static List<Seance> seances = [];
        private readonly static List<Ticket> tickets = [];

        static void Main()
        {
            SeedSampleData();
            ShowMainMenu();
        }

        static void ShowMainMenu()
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
                    case "1": ShowActorMenu(); break;
                    case "2": ShowCinemaMenu(); break;
                    case "3": ShowAuditoriumMenu(); break;
                    case "4": ShowFilmMenu(); break;
                    case "5": ShowSeanceMenu(); break;
                    case "6": ShowReservationMenu(); break;
                    case "7": ShowTicketMenu(); break;
                    case "8": ShowCinemaNetworkMenu(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); WaitForKey(); break;
                }
            }
        }
        static void ShowActorMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ACTOR MANAGEMENT ===");
                Console.WriteLine("1. Add Actor");
                Console.WriteLine("2. View All Actors");
                Console.WriteLine("3. Find Actor by ID");
                Console.WriteLine("4. Update Actor Information");
                Console.WriteLine("5. Delete Actor");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddActor(); break;
                    case "2": ViewAllActors(); break;
                    case "3": FindActorById(); break;
                    case "4": UpdateActor(); break;
                    case "5": DeleteActor(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); WaitForKey(); break;
                }
            }
        }

        static void AddActor()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD ACTOR ===");

                Console.Write("First Name: ");
                string firstName = ReadRequiredString("First name");

                Console.Write("Last Name: ");
                string lastName = ReadRequiredString("Last name");

                Console.Write("Nationality: ");
                string nationality = ReadRequiredString("Nationality");

                Console.Write("Birth Date (yyyy-mm-dd): ");
                DateTime birthDate = ReadDateTime();

                Console.Write("Image URL: ");
                string previewImgUrl = ReadRequiredString("Image URL");

                Console.Write("Biography: ");
                string biography = ReadRequiredString("Biography");

                Console.Write("Popularity: ");
                double popularity = ReadDouble();

                var actor = new Actor(firstName, lastName, nationality, birthDate, previewImgUrl, biography, popularity);
                actors.Add(actor);

                Console.WriteLine($"\nActor added successfully! ID: {actor.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void ViewAllActors()
        {
            Console.Clear();
            Console.WriteLine("=== ALL ACTORS ===");

            if (actors.Count == 0)
            {
                Console.WriteLine("No actors found.");
                WaitForKey();
                return;
            }

            foreach (var actor in actors)
            {
                Console.WriteLine(actor.ToString());
                Console.WriteLine("----------------------------------------");
            }
            WaitForKey();
        }

        static void FindActorById()
        {
            Console.Clear();
            Console.WriteLine("=== FIND ACTOR BY ID ===");

            Console.Write("Enter actor ID: ");
            string id = ReadRequiredString("Actor ID");

            var actor = actors.FirstOrDefault(a => a.Id == id);
            if (actor != null)
            {
                Console.WriteLine(actor.ToString());
            }
            else
            {
                Console.WriteLine("Actor with this ID not found.");
            }
            WaitForKey();
        }

        static void UpdateActor()
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE ACTOR ===");

            Console.Write("Enter actor ID to update: ");
            string id = ReadRequiredString("Actor ID");

            var actor = actors.FirstOrDefault(a => a.Id == id);
            if (actor == null)
            {
                Console.WriteLine("Actor with this ID not found.");
                WaitForKey();
                return;
            }

            try
            {
                Console.Write("New first name (current: {0}): ", actor.FirstName);
                string firstName = Console.ReadLine() ?? "";

                Console.Write("New biography (current: {0}): ", actor.Biography);
                string biography = Console.ReadLine() ?? "";

                Console.WriteLine("Actor information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void DeleteActor()
        {
            Console.Clear();
            Console.WriteLine("=== DELETE ACTOR ===");

            Console.Write("Enter actor ID to delete: ");
            string id = ReadRequiredString("Actor ID");

            var actor = actors.FirstOrDefault(a => a.Id == id);
            if (actor != null)
            {
                actors.Remove(actor);
                Console.WriteLine("Actor deleted successfully!");
            }
            else
            {
                Console.WriteLine("Actor with this ID not found.");
            }
            WaitForKey();
        }
        
        static void ShowCinemaMenu()
        {
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
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddCinema(); break;
                    case "2": ViewAllCinemas(); break;
                    case "3": FindCinemaById(); break;
                    case "4": UpdateCinema(); break;
                    case "5": AddAvailableFilm(); break;
                    case "6": RemoveAvailableFilm(); break;
                    case "7": RateCinema(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); WaitForKey(); break;
                }
            }
        }

        static void AddCinema()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD CINEMA ===");

                Console.Write("Cinema Name: ");
                string name = ReadRequiredString("Cinema name");

                Console.Write("Address: ");
                string address = ReadRequiredString("Address");

                Console.Write("Contact Number: ");
                string contactNumber = ReadRequiredString("Contact number");

                Console.Write("Contact Email: ");
                string contactEmail = ReadRequiredString("Contact email");

                Console.Write("Manager Name: ");
                string managerName = ReadRequiredString("Manager name");

                var cinema = new Cinema(name, address, contactNumber, contactEmail, managerName);
                cinemas.Add(cinema);

                Console.WriteLine($"\nCinema added successfully! ID: {cinema.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void ViewAllCinemas()
        {
            Console.Clear();
            Console.WriteLine("=== ALL CINEMAS ===");

            if (cinemas.Count == 0)
            {
                Console.WriteLine("No cinemas found.");
                WaitForKey();
                return;
            }

            foreach (var cinema in cinemas)
            {
                Console.WriteLine(cinema.ToString());
                Console.WriteLine("----------------------------------------");
            }
            WaitForKey();
        }

        static void FindCinemaById()
        {
            Console.Clear();
            Console.WriteLine("=== FIND CINEMA BY ID ===");

            Console.Write("Enter cinema ID: ");
            string id = ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema != null)
            {
                Console.WriteLine(cinema.ToString());
            }
            else
            {
                Console.WriteLine("Cinema with this ID not found.");
            }
            WaitForKey();
        }

        static void UpdateCinema()
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE CINEMA ===");

            Console.Write("Enter cinema ID to update: ");
            string id = ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema == null)
            {
                Console.WriteLine("Cinema with this ID not found.");
                WaitForKey();
                return;
            }

            try
            {
                Console.Write("New name (current: {0}): ", cinema.CinemaName);
                string name = Console.ReadLine() ?? cinema.CinemaName;

                Console.Write("New address (current: {0}): ", cinema.Adress);
                string address = Console.ReadLine() ?? cinema.Adress;

                cinema.UpdateGlobalInfo(name, address, cinema.ContactNumber, cinema.ContactEmail, cinema.ManagerName);
                Console.WriteLine("Cinema information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void AddAvailableFilm()
        {
            Console.Clear();
            Console.WriteLine("=== ADD AVAILABLE FILM ===");

            Console.Write("Enter cinema ID: ");
            string cinemaId = ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == cinemaId);
            if (cinema == null)
            {
                Console.WriteLine("Cinema with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter film ID: ");
            string filmId = ReadRequiredString("Film ID");

            if (cinema.AddAvalibleFilmId(filmId))
            {
                Console.WriteLine("Film successfully added to available films!");
            }
            else
            {
                Console.WriteLine("Failed to add film. Possibly reached limit (5 films) or film already added.");
            }
            WaitForKey();
        }

        static void RemoveAvailableFilm()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE AVAILABLE FILM ===");

            Console.Write("Enter cinema ID: ");
            string cinemaId = ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == cinemaId);
            if (cinema == null)
            {
                Console.WriteLine("Cinema with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter film ID to remove: ");
            string filmId = ReadRequiredString("Film ID");

            if (cinema.DeleteAvalibleFilmId(filmId))
            {
                Console.WriteLine("Film successfully removed from available films!");
            }
            else
            {
                Console.WriteLine("Failed to remove film. Possibly it's not in the available list.");
            }
            WaitForKey();
        }

        static void RateCinema()
        {
            Console.Clear();
            Console.WriteLine("=== RATE CINEMA ===");

            Console.Write("Enter cinema ID: ");
            string id = ReadRequiredString("Cinema ID");

            var cinema = cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema == null)
            {
                Console.WriteLine("Cinema with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter rating (1-5): ");
            uint rating = ReadUInt();

            if (rating < 1 || rating > 5)
            {
                Console.WriteLine("Rating must be between 1 and 5.");
                WaitForKey();
                return;
            }

            cinema.UpdateRating(rating);
            Console.WriteLine($"Cinema rated successfully! Current rating: {cinema.Rating:F2}");
            WaitForKey();
        }
        static void ShowAuditoriumMenu()
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
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddAuditorium(); break;
                    case "2": ViewAllAuditoriums(); break;
                    case "3": FindAuditoriumById(); break;
                    case "4": UpdateAuditorium(); break;
                    case "5": AddFeature(); break;
                    case "6": RemoveFeature(); break;
                    case "7": RateAuditorium(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); WaitForKey(); break;
                }
            }
        }

        static void AddAuditorium()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD AUDITORIUM ===");

                Console.Write("Cinema ID: ");
                string cinemaId = ReadRequiredString("Cinema ID");

                Console.Write("Auditorium Name: ");
                string name = ReadRequiredString("Auditorium name");

                Console.Write("Room Number: ");
                uint roomNumber = ReadUInt();

                Console.Write("Number of Rows: ");
                uint rows = ReadUInt();

                Console.Write("Seats per Row: ");
                uint seatsPerRow = ReadUInt();

                var auditorium = new Auditorium(cinemaId, name, roomNumber, rows, seatsPerRow);
                auditoriums.Add(auditorium);

                Console.WriteLine($"\nAuditorium added successfully! ID: {auditorium.Id}, Capacity: {auditorium.MaxCapacity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void ViewAllAuditoriums()
        {
            Console.Clear();
            Console.WriteLine("=== ALL AUDITORIUMS ===");

            if (auditoriums.Count == 0)
            {
                Console.WriteLine("No auditoriums found.");
                WaitForKey();
                return;
            }

            foreach (var auditorium in auditoriums)
            {
                Console.WriteLine(auditorium.ToString());
                Console.WriteLine("----------------------------------------");
            }
            WaitForKey();
        }

        static void FindAuditoriumById()
        {
            Console.Clear();
            Console.WriteLine("=== FIND AUDITORIUM BY ID ===");

            Console.Write("Enter auditorium ID: ");
            string id = ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);
            if (auditorium != null)
            {
                Console.WriteLine(auditorium.ToString());
            }
            else
            {
                Console.WriteLine("Auditorium with this ID not found.");
            }
            WaitForKey();
        }

        static void UpdateAuditorium()
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE AUDITORIUM ===");

            Console.Write("Enter auditorium ID to update: ");
            string id = ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);
            if (auditorium == null)
            {
                Console.WriteLine("Auditorium with this ID not found.");
                WaitForKey();
                return;
            }

            try
            {
                Console.Write("New name (current: {0}): ", auditorium.AuditoriumName);
                string name = Console.ReadLine() ?? auditorium.AuditoriumName;

                Console.Write("New room number (current: {0}): ", auditorium.RoomNumber);
                uint roomNumber = ReadUInt(auditorium.RoomNumber);

                Console.Write("New number of rows (current: {0}): ", auditorium.Rows);
                uint rows = ReadUInt(auditorium.Rows);

                Console.Write("New number of seats per row (current: {0}): ", auditorium.SeatsPerRow);
                uint seatsPerRow = ReadUInt(auditorium.SeatsPerRow);

                auditorium.UpdateGlobalInfo(name, roomNumber, rows, seatsPerRow);
                Console.WriteLine("Auditorium information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void AddFeature()
        {
            Console.Clear();
            Console.WriteLine("=== ADD FEATURE ===");

            Console.Write("Enter auditorium ID: ");
            string id = ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);
            if (auditorium == null)
            {
                Console.WriteLine("Auditorium with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter feature: ");
            string feature = ReadRequiredString("Feature");

            if (auditorium.AddFeature(feature))
            {
                Console.WriteLine("Feature added successfully!");
            }
            else
            {
                Console.WriteLine("Failed to add feature. Possibly reached limit (5 features) or it's already added.");
            }
            WaitForKey();
        }

        static void RemoveFeature()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE FEATURE ===");

            Console.Write("Enter auditorium ID: ");
            string id = ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);
            if (auditorium == null)
            {
                Console.WriteLine("Auditorium with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter feature to remove: ");
            string feature = ReadRequiredString("Feature");

            if (auditorium.DeleteFeature(feature))
            {
                Console.WriteLine("Feature removed successfully!");
            }
            else
            {
                Console.WriteLine("Failed to remove feature. Possibly it's not in the list.");
            }
            WaitForKey();
        }

        static void RateAuditorium()
        {
            Console.Clear();
            Console.WriteLine("=== RATE AUDITORIUM ===");

            Console.Write("Enter auditorium ID: ");
            string id = ReadRequiredString("Auditorium ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == id);
            if (auditorium == null)
            {
                Console.WriteLine("Auditorium with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter rating (1-5): ");
            uint rating = ReadUInt();

            if (rating < 1 || rating > 5)
            {
                Console.WriteLine("Rating must be between 1 and 5.");
                WaitForKey();
                return;
            }

            auditorium.UpdateRating(rating);
            Console.WriteLine($"Auditorium rated successfully! Current rating: {auditorium.Rating:F2}");
            WaitForKey();
        }
        static void ShowFilmMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== FILM MANAGEMENT ===");
                Console.WriteLine("1. Add Film");
                Console.WriteLine("2. View All Films");
                Console.WriteLine("3. Find Film by ID");
                Console.WriteLine("4. Update Film Information");
                Console.WriteLine("5. Add Actor to Film");
                Console.WriteLine("6. Remove Actor from Film");
                Console.WriteLine("7. Rate Film");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddFilm(); break;
                    case "2": ViewAllFilms(); break;
                    case "3": FindFilmById(); break;
                    case "4": UpdateFilm(); break;
                    case "5": AddActorToFilm(); break;
                    case "6": RemoveActorFromFilm(); break;
                    case "7": RateFilm(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); WaitForKey(); break;
                }
            }
        }

        static void AddFilm()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD FILM ===");

                Console.Write("Title: ");
                string title = ReadRequiredString("Title");

                Console.Write("Description: ");
                string description = ReadRequiredString("Description");

                Console.Write("Duration (minutes): ");
                uint duration = ReadUInt();

                Console.Write("Director: ");
                string director = ReadRequiredString("Director");

                Console.Write("Genre: ");
                string genre = ReadRequiredString("Genre");

                Console.Write("Age Restriction (true/false): ");
                bool ageRestriction = ReadBoolean();

                Console.Write("Image URL: ");
                string imageUrl = ReadRequiredString("Image URL");

                Console.Write("Trailer URL: ");
                string trailerUrl = ReadRequiredString("Trailer URL");

                var film = new Film(title, description, duration, director, genre, ageRestriction, imageUrl, trailerUrl);
                films.Add(film);

                Console.WriteLine($"\nFilm added successfully! ID: {film.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void ViewAllFilms()
        {
            Console.Clear();
            Console.WriteLine("=== ALL FILMS ===");

            if (films.Count == 0)
            {
                Console.WriteLine("No films found.");
                WaitForKey();
                return;
            }

            foreach (var film in films)
            {
                Console.WriteLine(film.ToString());
                Console.WriteLine("----------------------------------------");
            }
            WaitForKey();
        }

        static void FindFilmById()
        {
            Console.Clear();
            Console.WriteLine("=== FIND FILM BY ID ===");

            Console.Write("Enter film ID: ");
            string id = ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == id);
            if (film != null)
            {
                Console.WriteLine(film.ToString());
            }
            else
            {
                Console.WriteLine("Film with this ID not found.");
            }
            WaitForKey();
        }

        static void UpdateFilm()
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE FILM ===");

            Console.Write("Enter film ID to update: ");
            string id = ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == id);
            if (film == null)
            {
                Console.WriteLine("Film with this ID not found.");
                WaitForKey();
                return;
            }

            try
            {
                Console.Write("New title (current: {0}): ", film.Title);
                string title = Console.ReadLine() ?? film.Title;

                Console.Write("New description (current: {0}): ", film.Description);
                string description = Console.ReadLine() ?? film.Description;

                film.UpdateGlobalInfo(title, description, film.DurationInMinutes, film.Director,
                                    film.Genre, film.AgeRestriction, film.PreviewImgUrl, film.TrailerUrl);
                Console.WriteLine("Film information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void AddActorToFilm()
        {
            Console.Clear();
            Console.WriteLine("=== ADD ACTOR TO FILM ===");

            Console.Write("Enter film ID: ");
            string filmId = ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == filmId);
            if (film == null)
            {
                Console.WriteLine("Film with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter actor ID: ");
            string actorId = ReadRequiredString("Actor ID");

            if (film.AddActorId(actorId))
            {
                Console.WriteLine("Actor successfully added to film!");
            }
            else
            {
                Console.WriteLine("Failed to add actor. Possibly reached limit (5 actors) or actor already added.");
            }
            WaitForKey();
        }

        static void RemoveActorFromFilm()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE ACTOR FROM FILM ===");

            Console.Write("Enter film ID: ");
            string filmId = ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == filmId);
            if (film == null)
            {
                Console.WriteLine("Film with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter actor ID to remove: ");
            string actorId = ReadRequiredString("Actor ID");

            if (film.DeleteActorId(actorId))
            {
                Console.WriteLine("Actor successfully removed from film!");
            }
            else
            {
                Console.WriteLine("Failed to remove actor. Possibly not in the list.");
            }
            WaitForKey();
        }

        static void RateFilm()
        {
            Console.Clear();
            Console.WriteLine("=== RATE FILM ===");

            Console.Write("Enter film ID: ");
            string id = ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == id);
            if (film == null)
            {
                Console.WriteLine("Film with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter rating (1-5): ");
            uint rating = ReadUInt();

            if (rating < 1 || rating > 5)
            {
                Console.WriteLine("Rating must be between 1 and 5.");
                WaitForKey();
                return;
            }

            film.UpdateRating(rating);
            Console.WriteLine($"Film rated successfully! Current rating: {film.Rating:F2}");
            WaitForKey();
        }
        static void ShowSeanceMenu()
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
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddSeance(); break;
                    case "2": ViewAllSeances(); break;
                    case "3": FindSeanceById(); break;
                    case "4": AddOccupiedSeat(); break;
                    case "5": RemoveOccupiedSeat(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); WaitForKey(); break;
                }
            }
        }

        static void AddSeance()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD SEANCE ===");

                Console.Write("Film ID: ");
                string filmId = ReadRequiredString("Film ID");

                var film = films.FirstOrDefault(f => f.Id == filmId);
                if (film == null)
                {
                    Console.WriteLine("Film with this ID not found.");
                    WaitForKey();
                    return;
                }

                Console.Write("Auditorium ID: ");
                string auditoriumId = ReadRequiredString("Auditorium ID");

                var auditorium = auditoriums.FirstOrDefault(a => a.Id == auditoriumId);
                if (auditorium == null)
                {
                    Console.WriteLine("Auditorium with this ID not found.");
                    WaitForKey();
                    return;
                }

                Console.Write("Start date and time (yyyy-mm-dd hh:mm:ss): ");
                DateTime startTime = ReadDateTime();

                Console.Write("Price: ");
                double price = ReadDouble();

                var seance = new Seance(filmId, auditoriumId, startTime, price, film);
                seances.Add(seance);

                Console.WriteLine($"\nSeance added successfully! ID: {seance.Id}, End: {seance.EndTime}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void ViewAllSeances()
        {
            Console.Clear();
            Console.WriteLine("=== ALL SEANCES ===");

            if (seances.Count == 0)
            {
                Console.WriteLine("No seances found.");
                WaitForKey();
                return;
            }

            foreach (var seance in seances)
            {
                Console.WriteLine(seance.ToString());
                Console.WriteLine("----------------------------------------");
            }
            WaitForKey();
        }

        static void FindSeanceById()
        {
            Console.Clear();
            Console.WriteLine("=== FIND SEANCE BY ID ===");

            Console.Write("Enter seance ID: ");
            string id = ReadRequiredString("Seance ID");

            var seance = seances.FirstOrDefault(s => s.Id == id);
            if (seance != null)
            {
                Console.WriteLine(seance.ToString());
            }
            else
            {
                Console.WriteLine("Seance with this ID not found.");
            }
            WaitForKey();
        }

        static void AddOccupiedSeat()
        {
            Console.Clear();
            Console.WriteLine("=== ADD OCCUPIED SEAT ===");

            Console.Write("Enter seance ID: ");
            string seanceId = ReadRequiredString("Seance ID");

            var seance = seances.FirstOrDefault(s => s.Id == seanceId);
            if (seance == null)
            {
                Console.WriteLine("Seance with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter seat ID: ");
            string seatId = ReadRequiredString("Seat ID");

            var auditorium = auditoriums.FirstOrDefault(a => a.Id == seance.AuditoriumId);
            if (auditorium == null)
            {
                Console.WriteLine("Auditorium for this seance not found.");
                WaitForKey();
                return;
            }

            if (seance.AddOccupiedSeatId(seatId, auditorium))
            {
                Console.WriteLine("Seat successfully added to occupied seats!");
            }
            else
            {
                Console.WriteLine("Failed to add seat. Possibly reached capacity limit or seat already added.");
            }
            WaitForKey();
        }

        static void RemoveOccupiedSeat()
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE OCCUPIED SEAT ===");

            Console.Write("Enter seance ID: ");
            string seanceId = ReadRequiredString("Seance ID");

            var seance = seances.FirstOrDefault(s => s.Id == seanceId);
            if (seance == null)
            {
                Console.WriteLine("Seance with this ID not found.");
                WaitForKey();
                return;
            }

            Console.Write("Enter seat ID to remove: ");
            string seatId = ReadRequiredString("Seat ID");

            if (seance.DeleteOccupiedSeatId(seatId))
            {
                Console.WriteLine("Seat successfully removed from occupied seats!");
            }
            else
            {
                Console.WriteLine("Failed to remove seat. Possibly not in the list.");
            }
            WaitForKey();
        }
        static void ShowReservationMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== RESERVATION MANAGEMENT ===");
                Console.WriteLine("1. Add Reservation");
                Console.WriteLine("2. View All Reservations");
                Console.WriteLine("3. Find Reservation by ID");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddReservation(); break;
                    case "2": ViewAllReservations(); break;
                    case "3": FindReservationById(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); WaitForKey(); break;
                }
            }
        }

        static void AddReservation()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD RESERVATION ===");

                Console.Write("Seance ID: ");
                string seanceId = ReadRequiredString("Seance ID");

                Console.Write("Customer Name: ");
                string customerName = ReadRequiredString("Customer name");

                Console.Write("Customer Surname: ");
                string customerSurname = ReadRequiredString("Customer surname");

                Console.Write("Customer Email: ");
                string customerEmail = ReadRequiredString("Customer email");

                Console.Write("Customer Phone: ");
                string customerPhone = ReadRequiredString("Customer phone");

                Console.Write("Payment Method: ");
                string paymentMethod = ReadRequiredString("Payment method");

                var reservation = new Reservation(seanceId, customerName, customerSurname, customerEmail, customerPhone, paymentMethod);
                reservations.Add(reservation);

                Console.WriteLine($"\nReservation added successfully! ID: {reservation.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void ViewAllReservations()
        {
            Console.Clear();
            Console.WriteLine("=== ALL RESERVATIONS ===");

            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found.");
                WaitForKey();
                return;
            }

            foreach (var reservation in reservations)
            {
                Console.WriteLine(reservation.ToString());
                Console.WriteLine("----------------------------------------");
            }
            WaitForKey();
        }

        static void FindReservationById()
        {
            Console.Clear();
            Console.WriteLine("=== FIND RESERVATION BY ID ===");

            Console.Write("Enter reservation ID: ");
            string id = ReadRequiredString("Reservation ID");

            var reservation = reservations.FirstOrDefault(r => r.Id == id);
            if (reservation != null)
            {
                Console.WriteLine(reservation.ToString());
            }
            else
            {
                Console.WriteLine("Reservation with this ID not found.");
            }
            WaitForKey();
        }
        static void ShowTicketMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== TICKET MANAGEMENT ===");
                Console.WriteLine("1. Add Ticket");
                Console.WriteLine("2. View All Tickets");
                Console.WriteLine("3. Find Ticket by ID");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddTicket(); break;
                    case "2": ViewAllTickets(); break;
                    case "3": FindTicketById(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); WaitForKey(); break;
                }
            }
        }

        static void AddTicket()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD TICKET ===");

                Console.Write("Reservation ID: ");
                string reservationId = ReadRequiredString("Reservation ID");

                Console.Write("Cinema ID: ");
                string cinemaId = ReadRequiredString("Cinema ID");

                Console.Write("Auditorium ID: ");
                string auditoriumId = ReadRequiredString("Auditorium ID");

                Console.Write("Seance ID: ");
                string seanceId = ReadRequiredString("Seance ID");

                var seance = seances.FirstOrDefault(s => s.Id == seanceId);
                if (seance == null)
                {
                    Console.WriteLine("Seance with this ID not found.");
                    WaitForKey();
                    return;
                }

                Console.Write("Film ID: ");
                string filmId = ReadRequiredString("Film ID");

                Console.Write("Seat ID: ");
                string seatId = ReadRequiredString("Seat ID");

                Console.Write("Ticket type (standard/student/senior/child/vip): ");
                string ticketType = ReadRequiredString("Ticket type");

                var ticket = new Ticket(reservationId, cinemaId, auditoriumId, seanceId, filmId, seatId, ticketType, seance);
                tickets.Add(ticket);

                Console.WriteLine($"\nTicket added successfully! ID: {ticket.Id}, Price: {ticket.Price:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void ViewAllTickets()
        {
            Console.Clear();
            Console.WriteLine("=== ALL TICKETS ===");

            if (tickets.Count == 0)
            {
                Console.WriteLine("No tickets found.");
                WaitForKey();
                return;
            }

            foreach (var ticket in tickets)
            {
                Console.WriteLine(ticket.ToString());
                Console.WriteLine("----------------------------------------");
            }
            WaitForKey();
        }

        static void FindTicketById()
        {
            Console.Clear();
            Console.WriteLine("=== FIND TICKET BY ID ===");

            Console.Write("Enter ticket ID: ");
            string id = ReadRequiredString("Ticket ID");

            var ticket = tickets.FirstOrDefault(t => t.Id == id);
            if (ticket != null)
            {
                Console.WriteLine(ticket.ToString());
            }
            else
            {
                Console.WriteLine("Ticket with this ID not found.");
            }
            WaitForKey();
        }
        static void ShowCinemaNetworkMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== CINEMA NETWORK MANAGEMENT ===");
                Console.WriteLine("1. Add Cinema Network");
                Console.WriteLine("2. View All Cinema Networks");
                Console.WriteLine("3. Find Cinema Network by Name");
                Console.WriteLine("4. Update Cinema Network Information");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddCinemaNetwork(); break;
                    case "2": ViewAllCinemaNetworks(); break;
                    case "3": FindCinemaNetworkByName(); break;
                    case "4": UpdateCinemaNetwork(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); WaitForKey(); break;
                }
            }
        }

        static void AddCinemaNetwork()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD CINEMA NETWORK ===");

                Console.Write("Company Name: ");
                string companyName = ReadRequiredString("Company name");

                Console.Write("Manager Name: ");
                string managerName = ReadRequiredString("Manager name");

                var network = new CinemaNetwork(companyName, managerName);
                cinemaNetworks.Add(network);

                Console.WriteLine($"\nCinema network added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }

        static void ViewAllCinemaNetworks()
        {
            Console.Clear();
            Console.WriteLine("=== ALL CINEMA NETWORKS ===");

            if (cinemaNetworks.Count == 0)
            {
                Console.WriteLine("No cinema networks found.");
                WaitForKey();
                return;
            }

            foreach (var network in cinemaNetworks)
            {
                Console.WriteLine(network.ToString());
                Console.WriteLine("----------------------------------------");
            }
            WaitForKey();
        }

        static void FindCinemaNetworkByName()
        {
            Console.Clear();
            Console.WriteLine("=== FIND CINEMA NETWORK BY NAME ===");

            Console.Write("Enter company name: ");
            string name = ReadRequiredString("Company name");

            var network = cinemaNetworks.FirstOrDefault(cn =>
                cn.CompanyName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (network != null)
            {
                Console.WriteLine(network.ToString());
            }
            else
            {
                Console.WriteLine("Cinema network with this name not found.");
            }
            WaitForKey();
        }

        static void UpdateCinemaNetwork()
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE CINEMA NETWORK ===");

            Console.Write("Enter company name to update: ");
            string name = ReadRequiredString("Company name");

            var network = cinemaNetworks.FirstOrDefault(cn =>
                cn.CompanyName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (network == null)
            {
                Console.WriteLine("Cinema network with this name not found.");
                WaitForKey();
                return;
            }

            try
            {
                Console.Write("New company name (current: {0}): ", network.CompanyName);
                string companyName = Console.ReadLine() ?? network.CompanyName;

                Console.Write("New manager name (current: {0}): ", network.ManagerName);
                string managerName = Console.ReadLine() ?? network.ManagerName;

                network.updateGlobalInfo(companyName, managerName);
                Console.WriteLine("Cinema network information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            WaitForKey();
        }
        static void WaitForKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static string ReadRequiredString(string fieldName)
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.Write($"{fieldName} cannot be empty. Please enter {fieldName.ToLower()}: ");
            }
        }

        static uint ReadUInt(uint defaultValue = 0)
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) && defaultValue != 0)
                {
                    return defaultValue;
                }
                if (uint.TryParse(input, out uint result))
                {
                    return result;
                }
                Console.Write("Please enter a valid positive number: ");
            }
        }

        static double ReadDouble()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (double.TryParse(input, out double result))
                {
                    return result;
                }
                Console.Write("Please enter a valid number: ");
            }
        }

        static DateTime ReadDateTime()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (DateTime.TryParse(input, out DateTime result))
                {
                    return result;
                }
                Console.Write("Please enter a valid date (yyyy-mm-dd): ");
            }
        }

        static bool ReadBoolean()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (bool.TryParse(input, out bool result))
                {
                    return result;
                }
                if (input?.ToLower() == "true" || input == "1")
                    return true;
                if (input?.ToLower() == "false" || input == "0")
                    return false;
                Console.Write("Please enter 'true' or 'false': ");
            }
        }

        static void SeedSampleData()
        {
            try
            {
                var actor1 = new Actor("Tom", "Hanks", "American", new DateTime(1956, 7, 9),
                    "https://example.com/tom_hanks.jpg", "Famous American actor", 9.5);
                var actor2 = new Actor("Meryl", "Streep", "American", new DateTime(1949, 6, 22),
                    "https://example.com/meryl_streep.jpg", "Legendary American actress", 9.8);
                actors.AddRange([actor1, actor2]);

                var cinema = new Cinema("Multiplex", "123 Main Street", "+380441234567", "info@multiplex.ua", "Ivan Petrenko");
                cinemas.Add(cinema);

                var auditorium = new Auditorium(cinema.Id, "Hall 1", 1, 10, 15);
                auditoriums.Add(auditorium);

                var film = new Film("Forrest Gump", "Story of a man with low IQ", 142, "Robert Zemeckis",
                    "Drama", false, "https://example.com/forrest_gump.jpg", "https://example.com/forrest_trailer");
                films.Add(film);

                var seance = new Seance(film.Id, auditorium.Id, DateTime.Now.AddHours(2), 150.0, film);
                seances.Add(seance);

                var network = new CinemaNetwork("CinemaMax", "Olena Sydorenko");
                cinemaNetworks.Add(network);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating test data: {ex.Message}");
            }
        }
    }
}