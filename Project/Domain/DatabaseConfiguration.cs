using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Project.Domain;
public class DatabaseConfiguration {


    public static ApplicationDbContext Configure(string[] args)
    {
        IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
        {
            var cns = context.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
        }).Build();

        var context = _host.Services.GetService<ApplicationDbContext>();
        if (context != null)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();
            Console.WriteLine("DB CONFIG: Successfully configured!");
            return context;
        }
        Console.WriteLine("DB CONFIG: Unexpected error!");
        return null;
    }
}
