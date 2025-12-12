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
    context.Database.Migrate();
    context.Database.EnsureCreated();
    Driver driver = new Driver() { FirstName = "John", LastName = "Smith", LicenseNumber = "A123" };
    context.Drivers.Add(driver);
    context.SaveChanges();
}