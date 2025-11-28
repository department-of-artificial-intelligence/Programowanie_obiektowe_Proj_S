using Project.Models;
using Project.ConsoleApp.Helpers;
using Project.Services.Handlers;
using Project.Services.SortingFiltering;

namespace Project.ConsoleApp.Menues
{
    public static class ActorMenu
    {
        public static void ShowActorMenu(List<Actor> actors, List<Film> films, List<Cinema> cinemas)
        {
            ArgumentNullException.ThrowIfNull(cinemas);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ACTOR MANAGEMENT ===");
                Console.WriteLine("1. Add Actor");
                Console.WriteLine("2. View All Actors");
                Console.WriteLine("3. Find Actor by ID");
                Console.WriteLine("4. Update Actor Information");
                Console.WriteLine("5. Delete Actor");
                Console.WriteLine("6. Sort and Filter Actors");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddActor(actors); break;
                    case "2": ViewAllActors(actors); break;
                    case "3": FindActorById(actors); break;
                    case "4": UpdateActor(actors); break;
                    case "5": DeleteActor(actors, films); break;
                    case "6": ShowActorSortFilterMenu(actors, films); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void ShowActorSortFilterMenu(List<Actor> actors, List<Film> films)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ACTOR SORTING & FILTERING ===");
                Console.WriteLine("1. Sort by Time (Newest First)");
                Console.WriteLine("2. Sort by Time (Oldest First)");
                Console.WriteLine("3. Sort by Popularity");
                Console.WriteLine("4. Filter by Last Name");
                Console.WriteLine("5. Filter Films Where Actor Appears");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var newestActors = GenericSorting.SortByTimeNewest(actors);

                        DisplayHelper.DisplayActors(newestActors, "Actors (Newest First)");
                        break;
                    case "2":
                        var oldestActors = GenericSorting.SortByTimeOldest(actors);

                        DisplayHelper.DisplayActors(oldestActors, "Actors (Oldest First)");
                        break;
                    case "3":
                        var popularActors = ActorSortingFiltering.SortActorsByPopularity(actors);

                        DisplayHelper.DisplayActors(popularActors, "Actors by Popularity");
                        break;
                    case "4":
                        Console.Write("Enter last name to filter: ");

                        string lastName = ConsoleHelper.ReadRequiredString("Last name");
                        var filteredActors = ActorSortingFiltering.FilterActorsByLastName(actors, lastName);

                        DisplayHelper.DisplayActors(filteredActors, $"Actors with Last Name containing '{lastName}'");
                        break;
                    case "5":
                        Console.Write("Enter actor ID: ");

                        string actorId = ConsoleHelper.ReadRequiredString("Actor ID");
                        var actorFilms = FilmSortingFiltering.FilterFilmsWhereActorIs(films, actorId);

                        DisplayHelper.DisplayFilms(actorFilms, $"Films featuring actor {actorId}");
                        break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice!"); ConsoleHelper.WaitForKey(); break;
                }
            }
        }

        static void AddActor(List<Actor> actors)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== ADD ACTOR ===");

                Console.Write("First Name: ");
                string firstName = ConsoleHelper.ReadRequiredString("First name");

                Console.Write("Last Name: ");
                string lastName = ConsoleHelper.ReadRequiredString("Last name");

                Console.Write("Nationality: ");
                string nationality = ConsoleHelper.ReadRequiredString("Nationality");

                Console.Write("Birth Date (yyyy-mm-dd): ");
                DateTime birthDate = ConsoleHelper.ReadDateTime();

                Console.Write("Profile Image URL: ");
                string profileImageUrl = ConsoleHelper.ReadRequiredString("Profile Image URL");

                Console.Write("Biography: ");
                string biography = ConsoleHelper.ReadRequiredString("Biography");

                Console.Write("Popularity (0-100): ");
                double popularity = ConsoleHelper.ReadDouble();

                var actor = new Actor(firstName, lastName, nationality, birthDate, profileImageUrl, biography, popularity);
                actors.Add(actor);

                Console.WriteLine($"\nActor added successfully! ID: {actor.Id}");
                Console.WriteLine($"Name: {actor.FullName}, Age: {actor.Age}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void ViewAllActors(List<Actor> actors)
        {
            Console.Clear();
            Console.WriteLine("=== ALL ACTORS ===");

            if (actors.Count == 0)
            {
                Console.WriteLine("No actors found.");
                ConsoleHelper.WaitForKey();

                return;
            }

            foreach (var actor in actors)
            {
                Console.WriteLine(actor.ToString());
                Console.WriteLine("----------------------------------------");
            }

            ConsoleHelper.WaitForKey();
        }

        static void FindActorById(List<Actor> actors)
        {
            Console.Clear();
            Console.WriteLine("=== FIND ACTOR BY ID ===");

            Console.Write("Enter actor ID: ");
            string id = ConsoleHelper.ReadRequiredString("Actor ID");

            var actor = actors.FirstOrDefault(a => a.Id == id);

            if (actor != null)
            {
                Console.WriteLine(actor.ToString());
            }
            else
            {
                Console.WriteLine("Actor with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }

        static void UpdateActor(List<Actor> actors)
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE ACTOR ===");

            Console.Write("Enter actor ID to update: ");
            string id = ConsoleHelper.ReadRequiredString("Actor ID");

            var actor = actors.FirstOrDefault(a => a.Id == id);

            if (actor == null)
            {
                Console.WriteLine("Actor with this ID not found.");
                ConsoleHelper.WaitForKey();
                return;
            }

            try
            {
                Console.Write($"New first name (current: {actor.FirstName}): ");
                string firstName = Console.ReadLine() ?? actor.FirstName;

                Console.Write($"New last name (current: {actor.LastName}): ");
                string lastName = Console.ReadLine() ?? actor.LastName;

                Console.Write($"New nationality (current: {actor.Nationality}): ");
                string nationality = Console.ReadLine() ?? actor.Nationality;

                Console.Write($"New biography (current: {actor.Biography}): ");
                string biography = Console.ReadLine() ?? actor.Biography;

                Console.Write($"New popularity (current: {actor.Popularity}): ");
                string? popularityInput = Console.ReadLine();
                double popularity = string.IsNullOrEmpty(popularityInput) ? actor.Popularity : double.Parse(popularityInput);

                actor.SetBiography(biography);
                actor.SetPopularity(popularity);
                actor.UpdatePersonalInfo(firstName, lastName, nationality, actor.BirthDate, actor.ProfileImageUrl);

                Console.WriteLine("Actor information updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            ConsoleHelper.WaitForKey();
        }

        static void DeleteActor(List<Actor> actors, List<Film> films)
        {
            Console.Clear();
            Console.WriteLine("=== DELETE ACTOR ===");

            Console.Write("Enter actor ID to delete: ");
            string id = ConsoleHelper.ReadRequiredString("Actor ID");

            var actor = actors.FirstOrDefault(a => a.Id == id);
            if (actor != null)
            {
                Console.WriteLine($"\nActor to delete: {actor.FullName}");
                Console.WriteLine("This will also remove this actor from all films.");
                Console.Write("Are you sure you want to delete this actor? (yes/no): ");

                string? confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "yes" || confirmation == "y")
                {
                    DeleteHandler.DeleteActor(actors, films, id);
                    Console.WriteLine("Actor deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Actor with this ID not found.");
            }

            ConsoleHelper.WaitForKey();
        }
    }
}