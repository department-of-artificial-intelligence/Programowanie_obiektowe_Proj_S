using Microsoft.Extensions.DependencyInjection;
using RatingSystem.BLL;
using RatingSystem.DAL;
using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var services = new ServiceCollection();
ConfigureServices(services);
var serviceProvider = services.BuildServiceProvider();

var userLogic = serviceProvider.GetRequiredService<IUserLogic>();
var serviceLogic = serviceProvider.GetRequiredService<IServiceLogic>();
var ratingLogic = serviceProvider.GetRequiredService<IRatingLogic>();
var analytics = serviceProvider.GetRequiredService<RatingAnalytics>();
var sorter = serviceProvider.GetRequiredService<RatingSorter>();
//await ClearDatabase(serviceLogic, ratingLogic, userLogic);
await SeedRichData(userLogic, serviceLogic, ratingLogic);


bool running = true;
while (running)
{
    Console.Clear();
    Console.WriteLine("========== RATING MANAGEMENT SYSTEM ==========");
    Console.WriteLine("1. [Users] Register or Delete");
    Console.WriteLine("2. [Services] Add or Remove");
    Console.WriteLine("3. [Ratings] Create or Delete");
    Console.WriteLine("4.  Service Ratings & Sorting ");
    Console.WriteLine("5.  Global Statistics ");
    Console.WriteLine("6. View all Services");
    Console.WriteLine("7. TOP 3 Services");
    Console.WriteLine("0. Exit");
    Console.WriteLine("==============================================");
    Console.Write("Select category: ");

    switch (Console.ReadLine())
    {
        case "1": await UserMenu(userLogic); break;
        case "2": await ServiceMenu(serviceLogic); break;
        case "3": await RatingMenu(ratingLogic); break;
        case "4": await ViewRatingsMenu(serviceLogic, sorter); break;
        case "5": Console.WriteLine(await analytics.GetComplexStatsReportAsync()); break;
        case "6": await ViewAllServices(serviceLogic);break;
        case "7": Console.WriteLine(await analytics.GetTop3ServicesReportAsync()); break;
        case "0": running = false; break;
        default: Console.WriteLine("Invalid choice."); break;
    }

    if (running) { Console.WriteLine("\nPress any key to continue..."); Console.ReadKey(); }
}


async Task UserMenu(IUserLogic uL)
{
    Console.WriteLine("\n1. Register New User | 2. Delete User");
    var choice = Console.ReadLine();
    try
    {
        if (choice == "1")
        {
            Console.Write("Name: "); await uL.RegisterUserAsync(Console.ReadLine());
            Console.WriteLine("User registered.");
        }
        else if (choice == "2")
        {
            Console.Write("User ID: "); await uL.DeleteUserAsync(int.Parse(Console.ReadLine()));
            Console.WriteLine("User deleted.");
        }
    }
    catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

async Task ServiceMenu(IServiceLogic sL)
{
    Console.WriteLine("\n1. Add Service | 2. Delete Service");
    var choice = Console.ReadLine();
    try
    {
        if (choice == "1")
        {
            Console.Write("Name: "); string n = Console.ReadLine();
            Console.Write("Type: "); string t = Console.ReadLine();
            await sL.AddServiceAsync(n, "Test Description", t);
            Console.WriteLine("Service added.");
        }
        else if (choice == "2")
        {
            Console.Write("Service ID: "); await sL.DeleteServiceAsync(int.Parse(Console.ReadLine()));
            Console.WriteLine("Service deleted.");
        }
    }
    catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
}

async Task RatingMenu(IRatingLogic rL)
{
    Console.WriteLine("\n1. Add Rating | 2. Delete Rating");
    var choice = Console.ReadLine();
    try
    {
        if (choice == "1")
        {
            Console.Write("User ID: "); int uId = int.Parse(Console.ReadLine());
            Console.Write("Service ID: "); int sId = int.Parse(Console.ReadLine());
            Console.Write("Score (1-5): "); int score = int.Parse(Console.ReadLine());
            Console.Write("Comment: "); string comm = Console.ReadLine();
            await rL.SubmitRatingAsync(uId, sId, score, comm);
            Console.WriteLine("Rating added.");
        }
        else if (choice == "2")
        {
            Console.Write("Rating ID: "); int rId = int.Parse(Console.ReadLine());
            Console.Write("Your User ID: "); int uId = int.Parse(Console.ReadLine());
            await rL.DeleteRatingAsync(rId, uId);
            Console.WriteLine( "Deleted.");
        }
    }
    catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
}
async Task ViewAllServices(IServiceLogic sL)
{
    Console.WriteLine("\n--- LIST OF ALL SERVICES ---");
    var servicesList = await sL.GetAllAsync();

    if (!servicesList.Any())
    {
        Console.WriteLine("No services found in the database.");
        return;
    }

    Console.WriteLine("{0,-5} | {1,-20} | {2,-15}", "ID", "Name", "Type");
    Console.WriteLine(new string('-', 45));

    foreach (var s in servicesList)
    {
        Console.WriteLine("{0,-5} | {1,-20} | {2,-15}", s.ServiceId, s.Name, s.ServiceType);
    }
}
async Task ViewRatingsMenu(IServiceLogic sL, RatingSorter sorter)
{
    Console.Write("\nEnter Service Name to search: ");
    string name = Console.ReadLine();
    try
    {
        int id = await sL.GetServiceIdByNameAsync(name);
        Console.WriteLine("Sort: 1. Stars (High) | 2. Stars (Low) | 3. Date (New)");
        string sort = Console.ReadLine();

        var list = sort switch
        {
            "1" => await sorter.GetServiceRatingsSortedByScoreAsync(id, true),
            "2" => await sorter.GetServiceRatingsSortedByScoreAsync(id, false),
            _ => await sorter.GetServiceRatingsSortedByDateAsync(id, true)
        };

        Console.WriteLine($"\n--- Ratings for {name.ToUpper()} ---");
        foreach (var r in list)
            Console.WriteLine($"[{r.Value.ToStars()}] {r.Comment} (Date: {r.Created:yyyy-MM-dd})");
    }
    catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
}
async Task ClearDatabase(IServiceLogic sL, IRatingLogic rL, IUserLogic uL)
{
    Console.WriteLine("Cleaning up duplicate data...");

    var services = await sL.GetAllAsync();
    foreach (var s in services)
    {
        await sL.DeleteServiceAsync(s.ServiceId);
    }

  
    Console.WriteLine("Database is clean.");
}


async Task SeedRichData(IUserLogic uL, IServiceLogic sL, IRatingLogic rL)
{
    Console.WriteLine("Generating extensive test data...");

 
    var users = new[] { "Admin", "User_Alpha", "Reviewer_Pro", "Customer_99", "Tester_X", "Foodie", "TechGuy" };
    foreach (var name in users)
    {
        try { await uL.RegisterUserAsync(name); } catch {  }
    }


    var serviceData = new[]
    {
        (Name: "Pizza Drive", Desc: "Italian fast food", Type: "Food"),
        (Name: "Master Fix", Desc: "Electronics repair", Type: "Tech"),
        (Name: "Clean Home", Desc: "House cleaning", Type: "Service"),
        (Name: "Turbo Taxi", Desc: "City transport", Type: "Transport"),
        (Name: "Eco Garden", Desc: "Landscaping", Type: "Service"),
        (Name: "Pet Care", Desc: "Dog walking and grooming", Type: "Service")
    };

    foreach (var s in serviceData)
    {
        try { await sL.AddServiceAsync(s.Name, s.Desc, s.Type); } catch {  }
    }



    var ratingsToSeed = new List<(int uId, int sId, int score, string comment)>
    {
        
        (1, 1, 5, "Best pizza in the neighborhood!"),
        (2, 1, 2, "Delivery took 2 hours. Cold crust."),
        (3, 1, 4, "Great toppings, but slightly expensive."),
        (6, 1, 1, "Found a hair in my pizza. Never again."),
        (7, 1, 5, "Standard of excellence. Fast delivery."),

        
        (1, 2, 5, "Fixed my laptop in 4 hours. Amazing!"),
        (4, 2, 5, "Professional service, original parts used."),
        (5, 2, 4, "A bit pricey, but they know what they are doing."),

        (2, 3, 3, "Cleaned well, but missed the balcony."),
        (6, 3, 5, "Sparkling clean! Highly recommend Alice."),
        (3, 3, 2, "Late for 30 minutes, didn't apologize."),

   
        (1, 4, 1, "Driver was extremely rude."),
        (7, 4, 5, "Clean car, smooth ride, 5 stars!"),
        (4, 4, 4, "Fair price, arrived on time."),

        (5, 5, 5, "Our backyard looks like a park now!"),
        (2, 5, 3, "Good work, but the communication was difficult."),

        (3, 6, 5, "My dog loves them!"),
        (6, 6, 4, "Great service, very responsible sitters.")
    };

    foreach (var r in ratingsToSeed)
    {
        try
        {
            await rL.SubmitRatingAsync(r.uId, r.sId, r.score, r.comment);
        }
        catch (Exception ex)
        {
          
            Console.WriteLine($"[Seed Warning]: Could not submit rating for User {r.uId} - {ex.Message}");
        }
}

Console.WriteLine("Rich test data successfully injected via SubmitRatingAsync.");
}

void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>();

    services.AddScoped<IUserDataLogic, UserDataLogic>();
    services.AddScoped<IServiceDataLogic, ServiceDataLogic>();
    services.AddScoped<IRatingDataLogic, RatingDataLogic>();

    services.AddScoped<IUserLogic, UserLogic>();
    services.AddScoped<IServiceLogic, ServiceLogic>();
    services.AddScoped<IRatingLogic, RatingLogic>();

    services.AddScoped<RatingAnalytics>();
    services.AddScoped<RatingSorter>();
}