using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; 
using Project.DAL;
using Project.Services;
using Project.Services.Interfaces;

namespace Project.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(System.IO.Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    
                    var cns = context.Configuration.GetConnectionString("DefaultConnection");
                    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));

                    
                    services.AddTransient<IBicycleService, BicycleService>();
                    services.AddTransient<IStationService, StationService>();
                    services.AddTransient<IPersonService, PersonService>();
                    services.AddTransient<IRentalService, RentalService>();

                    
                    services.AddTransient<ConsoleMenu>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Warning); 
                })
                .Build();

           
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.EnsureCreated(); 

                    var menu = services.GetRequiredService<ConsoleMenu>();
                    menu.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"CRITICAL ERROR: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }
    }
}