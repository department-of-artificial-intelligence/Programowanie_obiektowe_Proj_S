using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projekt.DAL;
using Projekt.Model;

IHost _host =Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
    {
        var cns=context.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
    }).Build();
var context=_host.Services.GetRequiredService<ApplicationDbContext>();
if (context != null) 
{
    context.Database.Migrate();
    context.Database.EnsureCreated();
    Car car = new Car("Toyota", "Corolla", 50000, 1.8, 2018, "Petrol", "XYZ1234", 4, "Sedan");
    context.Car.Add(car);
    context.SaveChanges();
}


