using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
{
    var cns = context.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
}).Build();

var context = _host.Services.GetService<ApplicationDbContext>();
if (context != null)
{
    //context.Database.Migrate();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    // Seed()
    Driver driver1 = new Driver() { FirstName = "John", LastName = "Smith", LicenseNumber = "A123" };
    Driver driver2 = new Driver() { FirstName = "Jane", LastName = "Doe", LicenseNumber = "A124" };
    Driver driver3 = new Driver() { FirstName = "Anna", LastName = "Taylor", LicenseNumber = "A125" };
    Driver driver4 = new Driver() { FirstName = "Adam", LastName = "Williams", LicenseNumber = "A126" };
    context.Drivers.Add(driver1);
    context.Drivers.Add(driver2);
    context.Drivers.Add(driver3);
    context.Drivers.Add(driver4);
    context.SaveChanges();



    CompanyCar car1 = new CompanyCar(0, "VIN_CC_001", 2022, 1.6f, 15000, "Skoda", "Octavia", "W0 12345", 5);
    Truck truck1 = new Truck(0, "VIN_TR_002", 2020, 12.0f, 250000, "Volvo", "FH16", "W0 99887", 24000);
    DeliveryVan van1 = new DeliveryVan(0, "VIN_DV_003", 2021, 2.3f, 80000, "Renault", "Master", "W0 55443", 12.5f);
    SemiTrailer trailer1 = new SemiTrailer(0, "VIN_ST_004", 2019, 0, 120000, "Schmitz", "Cargobull", "W0 11223", 35.0f);

    context.CompanyCars.Add(car1);
    context.Trucks.Add(truck1);
    context.DeliveryVans.Add(van1);
    context.SemiTrailers.Add(trailer1);
    context.SaveChanges();

    driver1.AssignVehicle(car1);
    driver2.AssignVehicle(truck1);
    driver3.AssignVehicle(van1);
}