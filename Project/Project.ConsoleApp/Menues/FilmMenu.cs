using Project.Models;
using Project.ConsoleApp.Helpers;
using Project.Logic.Handlers;
using Project.Logic.SortingFiltering;

namespace Project.ConsoleApp.Menues
{
    public static class FilmMenu
    {
        public static void ShowFilmMenu(List<Film> films, List<Actor> actors, List<Cinema> cinemas,
            List<Seance> seances, List<Reservation> reservations, List<Ticket> tickets)
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
                Console.WriteLine("8. Sort and Filter Films");
                Console.WriteLine("9. Delete Film");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddFilm(films); break;
                    case "2": ViewAllFilms(films); break;
                    case "3": FindFilmById(films); break;
                    case "4": UpdateFilm(films); break;
                    case "5": AddActorToFilm(films, actors); break;
                    case "6": RemoveActorFromFilm(films); break;
                    case "7": RateFilm(films); break;
                    case "8": ShowFilmSortFilterMenu(films, cinemas); break;
                    case "9": DeleteFilm(films, cinemas, seances, reservations, tickets); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void ShowFilmSortFilterMenu(List<Film> films, List<Cinema> cinemas)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== FILM SORTING & FILTERING ===");
                Console.WriteLine("1. Sort by Time (Newest First)");
                Console.WriteLine("2. Sort by Time (Oldest First)");
                Console.WriteLine("3. Sort by Rating");
                Console.WriteLine("4. Sort by Popularity");
                Console.WriteLine("5. Sort by Number of Actors");
                Console.WriteLine("6. Filter by Duration");
                Console.WriteLine("7. Filter by Genre");
                Console.WriteLine("8. Filter by Age Restriction");
                Console.WriteLine("9. Filter by Director");
                Console.WriteLine("10. Filter by Title");
                Console.WriteLine("11. Find Cinemas Where Film Available");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var newestFilms = GenericSorting.SortByTimeNewest(films);
                        DisplayHelper.DisplayFilms(newestFilms, "Films (Newest First)");
                        break;
                    case "2":
                        var oldestFilms = GenericSorting.SortByTimeOldest(films);
                        DisplayHelper.DisplayFilms(oldestFilms, "Films (Oldest First)");
                        break;
                    case "3":
                        var ratedFilms = RatableSorting.SortByRating(films);
                        DisplayHelper.DisplayFilms(ratedFilms, "Films by Rating");
                        break;
                    case "4":
                        var popularFilms = RatableSorting.SortByPopularity(films);
                        DisplayHelper.DisplayFilms(popularFilms, "Films by Popularity");
                        break;
                    case "5":
                        var actorCountFilms = FilmSortingFiltering.SortFilmsByNumberOfActors(films);
                        DisplayHelper.DisplayFilms(actorCountFilms, "Films by Number of Actors");
                        break;
                    case "6":
                        Console.Write("Enter minimum duration (minutes): ");
                        uint minDuration = ConsoleHelper.ReadUInt();
                        Console.Write("Enter maximum duration (minutes, optional): ");
                        uint maxDuration = ConsoleHelper.ReadUInt(uint.MaxValue);
                        var durationFilms = FilmSortingFiltering.FilterFilmsByDuration(films, minDuration, maxDuration);
                        DisplayHelper.DisplayFilms(durationFilms, $"Films with duration {minDuration}-{maxDuration} minutes");
                        break;
                    case "7":
                        Console.Write("Enter genre: ");
                        string genre = ConsoleHelper.ReadRequiredString("Genre");
                        var genreFilms = FilmSortingFiltering.FilterFilmsByGenre(films, genre);
                        DisplayHelper.DisplayFilms(genreFilms, $"Films in genre '{genre}'");
                        break;
                    case "8":
                        Console.Write("Filter by age restriction? (true/false): ");
                        bool ageRestriction = ConsoleHelper.ReadBoolean();
                        var ageFilms = FilmSortingFiltering.FilterFilmsByAgeRestriction(films, ageRestriction);
                        DisplayHelper.DisplayFilms(ageFilms, $"Films with age restriction: {ageRestriction}");
                        break;
                    case "9":
                        Console.Write("Enter director name: ");
                        string director = ConsoleHelper.ReadRequiredString("Director");
                        var directorFilms = FilmSortingFiltering.FilterFilmsByDirector(films, director);
                        DisplayHelper.DisplayFilms(directorFilms, $"Films by director '{director}'");
                        break;
                    case "10":
                        Console.Write("Enter title: ");
                        string title = ConsoleHelper.ReadRequiredString("Title");
                        var titleFilms = FilmSortingFiltering.FilterFilmsByTitle(films, title);
                        DisplayHelper.DisplayFilms(titleFilms, $"Films with title containing '{title}'");
                        break;
                    case "11":
                        Console.Write("Enter film ID: ");
                        string filmId = ConsoleHelper.ReadRequiredString("Film ID");
                        var cinemasWithFilm = CinemaSortingFiltering.FilterCinemasWhereFilmAvailable(cinemas, filmId);
                        DisplayHelper.DisplayCinemas(cinemasWithFilm, $"Cinemas showing film {filmId}");
                        break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void AddFilm(List<Film> films)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD FILM ===");

                Console.Write("Title: ");
                string title = ConsoleHelper.ReadRequiredString("Title");

                Console.Write("Description: ");
                string description = ConsoleHelper.ReadRequiredString("Description");

                Console.Write("Duration (minutes): ");
                uint duration = ConsoleHelper.ReadUInt();

                Console.Write("Director: ");
                string director = ConsoleHelper.ReadRequiredString("Director");

                Console.Write("Genre: ");
                string genre = ConsoleHelper.ReadRequiredString("Genre");

                Console.Write("Age Restriction (true/false): ");
                bool ageRestriction = ConsoleHelper.ReadBoolean();

                Console.Write("Poster URL: ");
                string posterUrl = ConsoleHelper.ReadRequiredString("Poster URL");

                Console.Write("Trailer URL: ");
                string trailerUrl = ConsoleHelper.ReadRequiredString("Trailer URL");

                var film = new Film(title, description, duration, director, genre, ageRestriction, posterUrl, trailerUrl);
                films.Add(film);

                Console.WriteLine($"\nFilm added successfully! ID: {film.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void ViewAllFilms(List<Film> films)
        {
            Console.Clear();
            Console.WriteLine("=== ALL FILMS ===");

            if (films.Count == 0)
            {
                Console.WriteLine("No films found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            foreach (var film in films)
            {
                Console.WriteLine(film.ToString());
                Console.WriteLine("----------------------------------------");
            }
            ConsoleHelper.WaitForKey();
        }

        static void FindFilmById(List<Film> films)
        {
            Console.Clear();
            Console.WriteLine("=== FIND FILM BY ID ===");

            Console.Write("Enter film ID: ");
            string id = ConsoleHelper.ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == id);
            if (film != null)
            {
                Console.WriteLine(film.ToString());
            }
            else
            {
                Console.WriteLine("Film with this ID not found.");
            }
            ConsoleHelper.WaitForKey();
        }

        static void UpdateFilm(List<Film> films)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE FILM ===");

            Console.Write("Enter film ID to update: ");
            string id = ConsoleHelper.ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == id);
            if (film == null)
            {
                Console.WriteLine("Film with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            try
            {
                Console.Write($"New title (current: {film.Title}): ");
                string title = Console.ReadLine() ?? film.Title;

                Console.Write($"New description (current: {film.Description}): ");
                string description = Console.ReadLine() ?? film.Description;

                Console.Write($"New duration (current: {film.DurationMinutes}): ");
                string? durationInput = Console.ReadLine();
                uint duration = string.IsNullOrEmpty(durationInput) ? film.DurationMinutes : uint.Parse(durationInput);

                Console.Write($"New director (current: {film.Director}): ");
                string director = Console.ReadLine() ?? film.Director;

                Console.Write($"New genre (current: {film.Genre}): ");
                string genre = Console.ReadLine() ?? film.Genre;

                film.UpdateInfo(title, description, duration, director, genre, film.HasAgeRestriction, film.PosterUrl, film.TrailerUrl);
                Console.WriteLine("Film information updated!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            ConsoleHelper.WaitForKey();
        }

        static void AddActorToFilm(List<Film> films, List<Actor> actors)
        {
            ArgumentNullException.ThrowIfNull(actors);
            Console.Clear();
            Console.WriteLine("=== ADD ACTOR TO FILM ===");

            Console.Write("Enter film ID: ");
            string filmId = ConsoleHelper.ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == filmId);
            if (film == null)
            {
                Console.WriteLine("Film with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter actor ID: ");
            string actorId = ConsoleHelper.ReadRequiredString("Actor ID");

            if (film.AddItem(actorId))
            {
                Console.WriteLine("Actor successfully added to film!");
            }
            else
            {
                Console.WriteLine("Failed to add actor. Possibly reached limit (10 actors) or actor already added.");
            }
            ConsoleHelper.WaitForKey();
        }

        static void RemoveActorFromFilm(List<Film> films)
        {
            Console.Clear();
            Console.WriteLine("=== REMOVE ACTOR FROM FILM ===");

            Console.Write("Enter film ID: ");
            string filmId = ConsoleHelper.ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == filmId);
            if (film == null)
            {
                Console.WriteLine("Film with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Console.Write("Enter actor ID to remove: ");
            string actorId = ConsoleHelper.ReadRequiredString("Actor ID");

            if (film.RemoveItem(actorId))
            {
                Console.WriteLine("Actor successfully removed from film!");
            }
            else
            {
                Console.WriteLine("Failed to remove actor. Possibly not in the list.");
            }
            ConsoleHelper.WaitForKey();
        }

        static void RateFilm(List<Film> films)
        {
            Console.Clear();
            Console.WriteLine("=== RATE FILM ===");

            Console.Write("Enter film ID: ");
            string id = ConsoleHelper.ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == id);
            if (film == null)
            {
                Console.WriteLine("Film with this ID not found.");
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

            film.AddRating(rating);
            Console.WriteLine($"Film rated successfully! Current rating: {film.Rating}");
            ConsoleHelper.WaitForKey();
        }

        static void DeleteFilm(List<Film> films, List<Cinema> cinemas, List<Seance> seances,
            List<Reservation> reservations, List<Ticket> tickets)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE FILM ===");

            Console.Write("Enter film ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Film ID");

            var film = films.FirstOrDefault(f => f.Id == id);
            if (film != null)
            {
                Console.WriteLine($"\nFilm to delete: {film.Title}");
                Console.WriteLine("This will also:");
                Console.WriteLine("- Remove film from all cinemas' available films");
                Console.WriteLine("- Delete all seances with this film");
                Console.WriteLine("- Delete all reservations for those seances");
                Console.WriteLine("- Delete all tickets for those reservations");
                Console.Write("Are you sure you want to delete this film? (yes/no): ");

                string? confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "yes" || confirmation == "y")
                {
                    DeleteHandler.DeleteFilm(films, cinemas, seances, reservations, tickets, id);
                    Console.WriteLine("Film and all related data deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Film with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }
    }
}