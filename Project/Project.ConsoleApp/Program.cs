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
}