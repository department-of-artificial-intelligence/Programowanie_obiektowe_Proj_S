using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Project.Domain;
public class DatabaseConfiguration {
    public static ApplicationDbContext Configure(string[] args)
    {
        bool useInMemory = args.Contains("--use-inmemory") || args.Contains("-m");

        IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            if (useInMemory)
            {
                // Use In-Memory Database for testing
                services.AddDbContext<ApplicationDbContext>(options => 
                    options.UseInMemoryDatabase("TestDatabase"));
                Console.WriteLine("DB CONFIG: Using In-Memory Database");
            }
            else
            {
                // Use SQL Server Database
                var cns = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
                Console.WriteLine("DB CONFIG: Using SQL Server Database");
            }
        }).Build();

        var context = _host.Services.GetService<ApplicationDbContext>();
        if (context != null)
        {
            if (!useInMemory)
            {
                // Migracje tylko dla bazy SQL Server
                context.Database.Migrate();
            }
            context.Database.EnsureCreated();
            Console.WriteLine("DB CONFIG: Successfully configured!");
            return context;
        }
        Console.WriteLine("DB CONFIG: Unexpected error!");
        return null;
    }
}
