using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarsRental.DAL;
using CarsRental.Model;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
    {
        var cns = context.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
    }).Build();

var context = _host.Services.GetService<ApplicationDbContext>();
if(context != null)
{
    context.Database.Migrate();
    context.Database.EnsureCreated();
    Car car = new Car()
    {
        Id = 0,
        Brand = "BMW",
        Model = "M3",
        ProdYear = 2025,
        EngineVolume = 3.0,
        HorsePower = 450,
        Torque = 600,
        TimetoHundred = 3.8,
        DriveType = "Napęd na tył",
        GearboxType = "Automatyczna",
        Seats = 5,
        BasePrice = 500,
        IsAvailable = true,
        Department = null
    };
    context.Cars.Add(car);
    context.SaveChanges();
}