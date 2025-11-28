using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Project.Domain;
public class DatabaseConfiguration {

    public static void Configure(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        var app = builder.Build();

        using (var scope = app.Services.CreateScope()) {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
            db.Database.EnsureCreated();
            db.SeedDatabase();
        }

        Console.WriteLine("Running!");

        var context = app.Services.GetRequiredService<ApplicationDbContext>();
        foreach( var user in context.Users)
        {
            Console.WriteLine(user);
        }

    }
}
