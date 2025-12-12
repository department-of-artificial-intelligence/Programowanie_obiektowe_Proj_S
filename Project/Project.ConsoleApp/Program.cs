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
    Author author = new Author() { FirstName = "Jan", LastName = "Kowalski" };
    context.Authors.Add(author);
    context.SaveChanges();
}

Console.WriteLine("--------------------------------------------");
Console.WriteLine("System zarządzania siecią teatrów");
Console.WriteLine("--------------------------------------------");
