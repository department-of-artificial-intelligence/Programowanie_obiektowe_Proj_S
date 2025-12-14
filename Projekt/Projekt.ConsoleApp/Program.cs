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
    context.Database.EnsureDeleted();
    context.Database.Migrate();
    context.Database.EnsureCreated();
    Car car = new Car("Toyota", "Corolla", 50000, 1.8, 2018, "Petrol", "XYZ1234", 4, "Sedan");
    Motorbike motorbike = new Motorbike("Honda", "CBR600RR", 15000, 0.6, 2020, "Petrol", "MOTO5678", 600,"naked");
    Driver driver = new Driver("John", "Doe", "D1234567"); 
    context.Add(driver);
    car.PrzypisanyKierowca = driver;
    context.Add(car);
    context.Add(motorbike);
    motorbike.PrzypisanyKierowca=driver;
    context.SaveChanges();
}
