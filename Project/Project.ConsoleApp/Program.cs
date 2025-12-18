using Project.DTO;
using Project.Domain;
using Project.Services;
using Project.Model;

namespace Project.ConsoleApp
{
    public class Program
    {
        private static IUserService? _userService;
        private static IAuthorService? _authorService;
        private static IMovieService? _movieService;
        private static IReviewService? _reviewService;
        private static IMovieMarkService? _movieMarkService;

        // Testing Entity Framework
        // use --use-inmemory for in-memory database (DEBUG/TEST)
        static async Task Main(string[] args)
        {
            // Setup and configure application database and migrations
            var context = DatabaseConfiguration.Configure(args);

            // Setup default 'mock' data for application test
            context.SeedDatabase();
            
            Console.WriteLine("Database has been configured and seeded with mock data.\n");

            // Configure AutoMapper
            var mapper = AutoMapperConfiguration.Configure();
            
            // Initialize all services
            _userService = new UserService(context, mapper);
            _authorService = new AuthorService(context, mapper);
            _movieService = new MovieService(context, mapper);
            _reviewService = new ReviewService(context, mapper);
            _movieMarkService = new MovieMarkService(context, mapper);

            // Start the main menu loop
            await MainMenuAsync();
        }

        static async Task MainMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" ! ! ! MOVIE DATABASE MANAGEMENT SYSTEM ! ! ! ");
                Console.WriteLine();
                Console.WriteLine("1. Manage Users");
                Console.WriteLine("2. Manage Authors");
                Console.WriteLine("3. Manage Movies");
                Console.WriteLine("4. Manage Reviews");
                Console.WriteLine("5. Manage Movie Marks (Favorites/Watchlist)");
                Console.WriteLine("0. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ManageUsersAsync();
                        break;
                    case "2":
                        await ManageAuthorsAsync();
                        break;
                    case "3":
                        await ManageMoviesAsync();
                        break;
                    case "4":
                        await ManageReviewsAsync();
                        break;
                    case "5":
                        await ManageMovieMarksAsync();
                        break;
                    case "0":
                        Console.WriteLine("\nGoodbye!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        #region User Management
        static async Task ManageUsersAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("USER MANAGEMENT\n");
                Console.WriteLine("1. List All Users");
                Console.WriteLine("2. Get User by ID");
                Console.WriteLine("3. Search User by Username");
                Console.WriteLine("4. Create New User");
                Console.WriteLine("5. Update User");
                Console.WriteLine("6. Delete User");
                Console.WriteLine("0. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListAllUsersAsync();
                        break;
                    case "2":
                        await GetUserByIdAsync();
                        break;
                    case "3":
                        await SearchUserByUsernameAsync();
                        break;
                    case "4":
                        await CreateUserAsync();
                        break;
                    case "5":
                        await UpdateUserAsync();
                        break;
                    case "6":
                        await DeleteUserAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("\nInvalid option.");
                        break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static async Task ListAllUsersAsync()
        {
            var users = (await _userService!.GetAllAsync()).ToList();
            Console.WriteLine($"\n--- Total Users: {users.Count} ---");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id} | Username: {user.Username} | Created: {user.CreatedAt:yyyy-MM-dd}");
            }
        }

        static async Task GetUserByIdAsync()
        {
            Console.Write("\nEnter User ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = await _userService!.GetByIdAsync(id);
                if (user != null)
                {
                    Console.WriteLine($"\nUser Found:");
                    Console.WriteLine($"  ID: {user.Id}");
                    Console.WriteLine($"  Username: {user.Username}");
                    Console.WriteLine($"  Created: {user.CreatedAt:yyyy-MM-dd HH:mm}");
                    Console.WriteLine($"  Updated: {user.UpdatedAt:yyyy-MM-dd HH:mm}");
                }
                else
                {
                    Console.WriteLine("\nUser not found.");
                }
            }
        }

        static async Task SearchUserByUsernameAsync()
        {
            Console.Write("\nEnter Username: ");
            var username = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(username))
            {
                var user = await _userService!.GetByUsernameAsync(username);
                if (user != null)
                {
                    Console.WriteLine($"\nUser Found: ID {user.Id} | {user.Username}");
                }
                else
                {
                    Console.WriteLine("\nUser not found.");
                }
            }
        }

        static async Task CreateUserAsync()
        {
            Console.WriteLine("\n--- Create New User ---");
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                var userDto = new UserDto
                {
                    Username = username,
                    PasswordHash = password // In real app, hash this!
                };

                var created = await _userService!.CreateAsync(userDto);
                Console.WriteLine($"\n✓ User created successfully! ID: {created.Id}");
            }
            else
            {
                Console.WriteLine("\n✗ Invalid input.");
            }
        }

        static async Task UpdateUserAsync()
        {
            Console.Write("\nEnter User ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = await _userService!.GetByIdAsync(id);
                if (user != null)
                {
                    Console.WriteLine($"Current Username: {user.Username}");
                    Console.Write("New Username (or press Enter to keep): ");
                    var newUsername = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(newUsername))
                    {
                        user.Username = newUsername;
                        var updated = await _userService.UpdateAsync(id, user);
                        if (updated != null)
                        {
                            Console.WriteLine("\n✓ User updated successfully!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nUser not found.");
                }
            }
        }

        static async Task DeleteUserAsync()
        {
            Console.Write("\nEnter User ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Are you sure? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    var deleted = await _userService!.DeleteAsync(id);
                    Console.WriteLine(deleted ? "\n✓ User deleted successfully!" : "\n✗ User not found.");
                }
            }
        }
        #endregion

        #region Author Management
        static async Task ManageAuthorsAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("AUTHOR MANAGEMENT\n");
                Console.WriteLine("1. List All Authors");
                Console.WriteLine("2. Get Author by ID");
                Console.WriteLine("3. Search Authors by Last Name");
                Console.WriteLine("4. Create New Author");
                Console.WriteLine("5. Update Author");
                Console.WriteLine("6. Delete Author");
                Console.WriteLine("0. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListAllAuthorsAsync();
                        break;
                    case "2":
                        await GetAuthorByIdAsync();
                        break;
                    case "3":
                        await SearchAuthorsByLastNameAsync();
                        break;
                    case "4":
                        await CreateAuthorAsync();
                        break;
                    case "5":
                        await UpdateAuthorAsync();
                        break;
                    case "6":
                        await DeleteAuthorAsync();
                        break;
                    case "0":
                        return;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static async Task ListAllAuthorsAsync()
        {
            var authors = (await _authorService!.GetAllAsync()).ToList();
            Console.WriteLine($"\n--- Total Authors: {authors.Count} ---");
            foreach (var author in authors)
            {
                Console.WriteLine($"ID: {author.Id} | {author.FirstName} {author.LastName} | Born: {author.BirthDay:yyyy-MM-dd}");
            }
        }

        static async Task GetAuthorByIdAsync()
        {
            Console.Write("\nEnter Author ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var author = await _authorService!.GetByIdAsync(id);
                if (author != null)
                {
                    Console.WriteLine($"\nAuthor Found:");
                    Console.WriteLine($"  ID: {author.Id}");
                    Console.WriteLine($"  Name: {author.FirstName} {author.LastName}");
                    Console.WriteLine($"  Birthday: {author.BirthDay:yyyy-MM-dd}");
                }
                else
                {
                    Console.WriteLine("\nAuthor not found.");
                }
            }
        }

        static async Task SearchAuthorsByLastNameAsync()
        {
            Console.Write("\nEnter Last Name: ");
            var lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                var authors = (await _authorService!.SearchByLastNameAsync(lastName)).ToList();
                Console.WriteLine($"\n--- Found {authors.Count} author(s) ---");
                foreach (var author in authors)
                {
                    Console.WriteLine($"ID: {author.Id} | {author.FirstName} {author.LastName}");
                }
            }
        }

        static async Task CreateAuthorAsync()
        {
            Console.WriteLine("\n--- Create New Author ---");
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            Console.Write("Birth Date (yyyy-mm-dd): ");
            
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName) 
                && DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
            {
                var authorDto = new AuthorDto
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDay = birthDate
                };

                var created = await _authorService!.CreateAsync(authorDto);
                Console.WriteLine($"\n✓ Author created successfully! ID: {created.Id}");
            }
            else
            {
                Console.WriteLine("\n✗ Invalid input.");
            }
        }

        static async Task UpdateAuthorAsync()
        {
            Console.Write("\nEnter Author ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var author = await _authorService!.GetByIdAsync(id);
                if (author != null)
                {
                    Console.WriteLine($"Current: {author.FirstName} {author.LastName}");
                    Console.Write("New First Name (or press Enter to keep): ");
                    var newFirstName = Console.ReadLine();
                    Console.Write("New Last Name (or press Enter to keep): ");
                    var newLastName = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(newFirstName))
                        author.FirstName = newFirstName;
                    if (!string.IsNullOrWhiteSpace(newLastName))
                        author.LastName = newLastName;

                    var updated = await _authorService.UpdateAsync(id, author);
                    Console.WriteLine(updated != null ? "\n✓ Author updated successfully!" : "\n✗ Update failed.");
                }
                else
                {
                    Console.WriteLine("\nAuthor not found.");
                }
            }
        }

        static async Task DeleteAuthorAsync()
        {
            Console.Write("\nEnter Author ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Are you sure? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    var deleted = await _authorService!.DeleteAsync(id);
                    Console.WriteLine(deleted ? "\n✓ Author deleted successfully!" : "\n✗ Author not found.");
                }
            }
        }
        #endregion

        #region Movie Management
        static async Task ManageMoviesAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MOVIE MANAGEMENT\n");
                Console.WriteLine("1. List All Movies");
                Console.WriteLine("2. Get Movie by ID");
                Console.WriteLine("3. Search Movies by Title");
                Console.WriteLine("4. Get Movies by Genre");
                Console.WriteLine("5. Get Movies by Author");
                Console.WriteLine("6. Create New Movie");
                Console.WriteLine("7. Update Movie");
                Console.WriteLine("8. Delete Movie");
                Console.WriteLine("0. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListAllMoviesAsync();
                        break;
                    case "2":
                        await GetMovieByIdAsync();
                        break;
                    case "3":
                        await SearchMoviesByTitleAsync();
                        break;
                    case "4":
                        await GetMoviesByGenreAsync();
                        break;
                    case "5":
                        await GetMoviesByAuthorAsync();
                        break;
                    case "6":
                        await CreateMovieAsync();
                        break;
                    case "7":
                        await UpdateMovieAsync();
                        break;
                    case "8":
                        await DeleteMovieAsync();
                        break;
                    case "0":
                        return;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static async Task ListAllMoviesAsync()
        {
            var movies = (await _movieService!.GetAllAsync()).ToList();
            Console.WriteLine($"\n--- Total Movies: {movies.Count} ---");
            foreach (var movie in movies)
            {
                var authorName = movie.Author != null ? $"{movie.Author.FirstName} {movie.Author.LastName}" : "Unknown";
                Console.WriteLine($"ID: {movie.Id} | {movie.Title} | {movie.Genre} | By: {authorName}");
            }
        }

        static async Task GetMovieByIdAsync()
        {
            Console.Write("\nEnter Movie ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var movie = await _movieService!.GetByIdAsync(id);
                if (movie != null)
                {
                    Console.WriteLine($"\n--- Movie Details ---");
                    Console.WriteLine($"  ID: {movie.Id}");
                    Console.WriteLine($"  Title: {movie.Title}");
                    Console.WriteLine($"  Genre: {movie.Genre}");
                    Console.WriteLine($"  Tagline: {movie.TagLine}");
                    Console.WriteLine($"  Description: {movie.Description}");
                    if (movie.Author != null)
                        Console.WriteLine($"  Director: {movie.Author.FirstName} {movie.Author.LastName}");
                }
                else
                {
                    Console.WriteLine("\nMovie not found.");
                }
            }
        }

        static async Task SearchMoviesByTitleAsync()
        {
            Console.Write("\nEnter Title: ");
            var title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                var movies = (await _movieService!.SearchByTitleAsync(title)).ToList();
                Console.WriteLine($"\n--- Found {movies.Count} movie(s) ---");
                foreach (var movie in movies)
                {
                    Console.WriteLine($"ID: {movie.Id} | {movie.Title} | {movie.Genre}");
                }
            }
        }

        static async Task GetMoviesByGenreAsync()
        {
            Console.WriteLine("\n--- Available Genres ---");
            var genres = Enum.GetValues<Genre>();
            for (int i = 0; i < genres.Length; i++)
            {
                Console.WriteLine($"{(int)genres[i]}. {genres[i]}");
            }
            Console.Write("\nSelect Genre Number: ");
            if (int.TryParse(Console.ReadLine(), out int genreNum) && Enum.IsDefined(typeof(Genre), genreNum))
            {
                var genre = (Genre)genreNum;
                var movies = (await _movieService!.GetMoviesByGenreAsync(genre)).ToList();
                Console.WriteLine($"\n--- {genre} Movies ({movies.Count}) ---");
                foreach (var movie in movies)
                {
                    Console.WriteLine($"ID: {movie.Id} | {movie.Title}");
                }
            }
        }

        static async Task GetMoviesByAuthorAsync()
        {
            Console.Write("\nEnter Author ID: ");
            if (int.TryParse(Console.ReadLine(), out int authorId))
            {
                var movies = (await _movieService!.GetMoviesByAuthorAsync(authorId)).ToList();
                Console.WriteLine($"\n--- Found {movies.Count} movie(s) ---");
                foreach (var movie in movies)
                {
                    Console.WriteLine($"ID: {movie.Id} | {movie.Title} | {movie.Genre}");
                }
            }
        }

        static async Task CreateMovieAsync()
        {
            Console.WriteLine("\n--- Create New Movie ---");
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Description: ");
            var description = Console.ReadLine();
            Console.Write("Tagline: ");
            var tagline = Console.ReadLine();
            
            Console.WriteLine("\n--- Available Genres ---");
            var genres = Enum.GetValues<Genre>();
            for (int i = 0; i < genres.Length; i++)
            {
                Console.WriteLine($"{(int)genres[i]}. {genres[i]}");
            }
            Console.Write("Select Genre Number: ");
            if (!int.TryParse(Console.ReadLine(), out int genreNum) || !Enum.IsDefined(typeof(Genre), genreNum))
            {
                Console.WriteLine("\n✗ Invalid genre.");
                return;
            }
            
            Console.Write("Enter Director (Author) ID: ");
            if (!int.TryParse(Console.ReadLine(), out int authorId))
            {
                Console.WriteLine("\n✗ Invalid Author ID.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(title))
            {
                var movieDto = new MovieDto
                {
                    Title = title,
                    Description = description,
                    TagLine = tagline,
                    Genre = (Genre)genreNum,
                    AuthorId = authorId
                };

                try
                {
                    var created = await _movieService!.CreateAsync(movieDto);
                    Console.WriteLine($"\n✓ Movie created successfully! ID: {created.Id}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"\n✗ Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("\n✗ Invalid input.");
            }
        }

        static async Task UpdateMovieAsync()
        {
            Console.Write("\nEnter Movie ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var movie = await _movieService!.GetByIdAsync(id);
                if (movie != null)
                {
                    Console.WriteLine($"Current Title: {movie.Title}");
                    Console.Write("New Title (or press Enter to keep): ");
                    var newTitle = Console.ReadLine();
                    Console.Write("New Description (or press Enter to keep): ");
                    var newDesc = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(newTitle))
                        movie.Title = newTitle;
                    if (!string.IsNullOrWhiteSpace(newDesc))
                        movie.Description = newDesc;

                    var updated = await _movieService.UpdateAsync(id, movie);
                    Console.WriteLine(updated != null ? "\n✓ Movie updated successfully!" : "\n✗ Update failed.");
                }
                else
                {
                    Console.WriteLine("\nMovie not found.");
                }
            }
        }

        static async Task DeleteMovieAsync()
        {
            Console.Write("\nEnter Movie ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Are you sure? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    var deleted = await _movieService!.DeleteAsync(id);
                    Console.WriteLine(deleted ? "\n✓ Movie deleted successfully!" : "\n✗ Movie not found.");
                }
            }
        }
        #endregion

        #region Review Management
        static async Task ManageReviewsAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("REVIEW MANAGEMENT\n");
                Console.WriteLine("1. List All Reviews");
                Console.WriteLine("2. Get Reviews by Movie");
                Console.WriteLine("3. Get Reviews by User");
                Console.WriteLine("4. Get Average Rating for Movie");
                Console.WriteLine("5. Create New Review");
                Console.WriteLine("6. Update Review");
                Console.WriteLine("7. Delete Review");
                Console.WriteLine("0. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListAllReviewsAsync();
                        break;
                    case "2":
                        await GetReviewsByMovieAsync();
                        break;
                    case "3":
                        await GetReviewsByUserAsync();
                        break;
                    case "4":
                        await GetAverageRatingAsync();
                        break;
                    case "5":
                        await CreateReviewAsync();
                        break;
                    case "6":
                        await UpdateReviewAsync();
                        break;
                    case "7":
                        await DeleteReviewAsync();
                        break;
                    case "0":
                        return;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static async Task ListAllReviewsAsync()
        {
            var reviews = (await _reviewService!.GetAllAsync()).ToList();
            Console.WriteLine($"\n--- Total Reviews: {reviews.Count} ---");
            foreach (var review in reviews)
            {
                var movieTitle = review.Movie?.Title ?? "Unknown";
                var username = review.User?.Username ?? "Unknown";
                Console.WriteLine($"ID: {review.Id} | {movieTitle} | By: {username} | Rating: {review.Rate}/5");
            }
        }

        static async Task GetReviewsByMovieAsync()
        {
            Console.Write("\nEnter Movie ID: ");
            if (int.TryParse(Console.ReadLine(), out int movieId))
            {
                var reviews = (await _reviewService!.GetReviewsByMovieAsync(movieId)).ToList();
                Console.WriteLine($"\n--- Found {reviews.Count} review(s) ---");
                foreach (var review in reviews)
                {
                    Console.WriteLine($"Rating: {review.Rate}/5 | By: {review.User?.Username} | Comment: {review.Comment}");
                }
            }
        }

        static async Task GetReviewsByUserAsync()
        {
            Console.Write("\nEnter User ID: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                var reviews = (await _reviewService!.GetReviewsByUserAsync(userId)).ToList();
                Console.WriteLine($"\n--- Found {reviews.Count} review(s) ---");
                foreach (var review in reviews)
                {
                    Console.WriteLine($"Movie: {review.Movie?.Title} | Rating: {review.Rate}/5 | Comment: {review.Comment}");
                }
            }
        }

        static async Task GetAverageRatingAsync()
        {
            Console.Write("\nEnter Movie ID: ");
            if (int.TryParse(Console.ReadLine(), out int movieId))
            {
                var avgRating = await _reviewService!.GetAverageRatingForMovieAsync(movieId);
                Console.WriteLine($"\nAverage Rating: {avgRating:F2}/5");
            }
        }

        static async Task CreateReviewAsync()
        {
            Console.WriteLine("\n--- Create New Review ---");
            Console.Write("User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId)) return;
            
            Console.Write("Movie ID: ");
            if (!int.TryParse(Console.ReadLine(), out int movieId)) return;
            
            Console.Write("Rating (0-5): ");
            if (!float.TryParse(Console.ReadLine(), out float rating) || rating < 0 || rating > 5)
            {
                Console.WriteLine("Invalid rating.");
                return;
            }
            
            Console.Write("Comment: ");
            var comment = Console.ReadLine();

            var reviewDto = new ReviewDto
            {
                UserId = userId,
                MovieId = movieId,
                Rate = rating,
                Comment = comment ?? ""
            };

            try
            {
                var created = await _reviewService!.CreateAsync(reviewDto);
                Console.WriteLine($"\n✓ Review created successfully! ID: {created.Id}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }
        }

        static async Task UpdateReviewAsync()
        {
            Console.Write("\nEnter Review ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var review = await _reviewService!.GetByIdAsync(id);
                if (review != null)
                {
                    Console.WriteLine($"Current Rating: {review.Rate}");
                    Console.Write("New Rating (0-5, or press Enter to keep): ");
                    var ratingInput = Console.ReadLine();
                    if (float.TryParse(ratingInput, out float newRating) && newRating >= 0 && newRating <= 5)
                    {
                        review.Rate = newRating;
                    }
                    
                    Console.Write("New Comment (or press Enter to keep): ");
                    var newComment = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newComment))
                    {
                        review.Comment = newComment;
                    }

                    var updated = await _reviewService.UpdateAsync(id, review);
                    Console.WriteLine(updated != null ? "\n✓ Review updated successfully!" : "\n✗ Update failed.");
                }
                else
                {
                    Console.WriteLine("\nReview not found.");
                }
            }
        }

        static async Task DeleteReviewAsync()
        {
            Console.Write("\nEnter Review ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Are you sure? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    var deleted = await _reviewService!.DeleteAsync(id);
                    Console.WriteLine(deleted ? "\n✓ Review deleted successfully!" : "\n✗ Review not found.");
                }
            }
        }
        #endregion

        #region MovieMark Management
        static async Task ManageMovieMarksAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("MOVIE MARKS MANAGEMENT\n");
                Console.WriteLine("1. List All Movie Marks");
                Console.WriteLine("2. Get Movie Marks by User");
                Console.WriteLine("3. Get User's Favorites");
                Console.WriteLine("4. Get User's Watch Later List");
                Console.WriteLine("5. Get User's Watched Movies");
                Console.WriteLine("6. Create New Movie Mark");
                Console.WriteLine("7. Update Movie Mark");
                Console.WriteLine("8. Delete Movie Mark");
                Console.WriteLine("0. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListAllMovieMarksAsync();
                        break;
                    case "2":
                        await GetMovieMarksByUserAsync();
                        break;
                    case "3":
                        await GetUserFavoritesAsync();
                        break;
                    case "4":
                        await GetUserWatchLaterAsync();
                        break;
                    case "5":
                        await GetUserWatchedAsync();
                        break;
                    case "6":
                        await CreateMovieMarkAsync();
                        break;
                    case "7":
                        await UpdateMovieMarkAsync();
                        break;
                    case "8":
                        await DeleteMovieMarkAsync();
                        break;
                    case "0":
                        return;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static async Task ListAllMovieMarksAsync()
        {
            var marks = (await _movieMarkService!.GetAllAsync()).ToList();
            Console.WriteLine($"\n--- Total Movie Marks: {marks.Count} ---");
            foreach (var mark in marks)
            {
                var movieTitle = mark.Movie?.Title ?? "Unknown";
                var username = mark.User?.Username ?? "Unknown";
                Console.WriteLine($"ID: {mark.Id} | {movieTitle} | User: {username} | Type: {mark.Type}");
            }
        }

        static async Task GetMovieMarksByUserAsync()
        {
            Console.Write("\nEnter User ID: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                var marks = (await _movieMarkService!.GetMovieMarksByUserAsync(userId)).ToList();
                Console.WriteLine($"\n--- Found {marks.Count} mark(s) ---");
                foreach (var mark in marks)
                {
                    Console.WriteLine($"{mark.Type}: {mark.Movie?.Title}");
                }
            }
        }

        static async Task GetUserFavoritesAsync()
        {
            Console.Write("\nEnter User ID: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                var marks = (await _movieMarkService!.GetMovieMarksByTypeAsync(userId, MovieMarkType.FAVORITE)).ToList();
                Console.WriteLine($"\n--- Favorite Movies ({marks.Count}) ---");
                foreach (var mark in marks)
                {
                    Console.WriteLine($"- {mark.Movie?.Title}");
                }
            }
        }

        static async Task GetUserWatchLaterAsync()
        {
            Console.Write("\nEnter User ID: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                var marks = (await _movieMarkService!.GetMovieMarksByTypeAsync(userId, MovieMarkType.WATCH_LATER)).ToList();
                Console.WriteLine($"\n--- Watch Later List ({marks.Count}) ---");
                foreach (var mark in marks)
                {
                    Console.WriteLine($"- {mark.Movie?.Title}");
                }
            }
        }

        static async Task GetUserWatchedAsync()
        {
            Console.Write("\nEnter User ID: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                var marks = (await _movieMarkService!.GetMovieMarksByTypeAsync(userId, MovieMarkType.WATCHED)).ToList();
                Console.WriteLine($"\n--- Watched Movies ({marks.Count}) ---");
                foreach (var mark in marks)
                {
                    Console.WriteLine($"- {mark.Movie?.Title}");
                }
            }
        }

        static async Task CreateMovieMarkAsync()
        {
            Console.WriteLine("\n--- Create New Movie Mark ---");
            Console.Write("User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId)) return;
            
            Console.Write("Movie ID: ");
            if (!int.TryParse(Console.ReadLine(), out int movieId)) return;
            
            Console.WriteLine("\n1. FAVORITE");
            Console.WriteLine("2. WATCH_LATER");
            Console.WriteLine("3. WATCHED");
            Console.Write("Select Mark Type: ");
            if (!int.TryParse(Console.ReadLine(), out int typeNum) || !Enum.IsDefined(typeof(MovieMarkType), typeNum))
            {
                Console.WriteLine("Invalid type.");
                return;
            }

            var markDto = new MovieMarkDto
            {
                UserId = userId,
                MovieId = movieId,
                Type = (MovieMarkType)typeNum
            };

            try
            {
                var created = await _movieMarkService!.CreateAsync(markDto);
                Console.WriteLine($"\n✓ Movie mark created successfully! ID: {created.Id}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }
        }

        static async Task UpdateMovieMarkAsync()
        {
            Console.Write("\nEnter Movie Mark ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var mark = await _movieMarkService!.GetByIdAsync(id);
                if (mark != null)
                {
                    Console.WriteLine($"Current Type: {mark.Type}");
                    Console.WriteLine("\n1. FAVORITE");
                    Console.WriteLine("2. WATCH_LATER");
                    Console.WriteLine("3. WATCHED");
                    Console.Write("New Type: ");
                    if (int.TryParse(Console.ReadLine(), out int typeNum) && Enum.IsDefined(typeof(MovieMarkType), typeNum))
                    {
                        mark.Type = (MovieMarkType)typeNum;
                        var updated = await _movieMarkService.UpdateAsync(id, mark);
                        Console.WriteLine(updated != null ? "\n✓ Movie mark updated successfully!" : "\n✗ Update failed.");
                    }
                }
                else
                {
                    Console.WriteLine("\nMovie mark not found.");
                }
            }
        }

        static async Task DeleteMovieMarkAsync()
        {
            Console.Write("\nEnter Movie Mark ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Are you sure? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    var deleted = await _movieMarkService!.DeleteAsync(id);
                    Console.WriteLine(deleted ? "\n✓ Movie mark deleted successfully!" : "\n✗ Movie mark not found.");
                }
            }
        }
        #endregion
    }
}
