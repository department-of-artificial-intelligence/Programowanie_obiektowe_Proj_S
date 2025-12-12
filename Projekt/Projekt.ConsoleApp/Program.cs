using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;


IHost _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
{
    var cns = context.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<ApplicationDbcontext>(options => options.UseSqlServer(cns));
}).Build();

var context = _host.Services.GetService<ApplicationDbcontext>();
if(context != null)
{
    context.Database.Migrate();
    context.Database.EnsureCreated();
    Pracownik pracownik = new Pracownik() { FirstName = "Marek", LastName = "Nowak", Age = 24 };
    context.Pracownicy.Add(pracownik);
    context.SaveChanges();
}