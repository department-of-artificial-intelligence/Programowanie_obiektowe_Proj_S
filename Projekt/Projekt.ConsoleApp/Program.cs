using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Project.DAL;
using Project.Model;


IPHostEntry _host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
{
    var cns = context.Configuration.GetConnectionString("DefaultConnection");
    serices.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cns));
}).Build();

var context = _host.Services.GetService<ApplicationExceptionDbContext>();
if(context != null)
{
    context.Database.Migrate();
    context.Database.EnsureCreated();
    Osoba osoba = new Osoba() { FirstName = "Marek", LastName = "Nowak", Age = 24 };
    context.Osoby.Add(osoba);
    context.SaveChanges;
}