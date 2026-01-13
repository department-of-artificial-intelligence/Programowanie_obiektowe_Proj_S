using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.DAL;
using Project.Model;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;


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

    
    if (!context.Stores.Any())
    {


        var adres1 = new Address { 
            City = "Warszawa", 
            Street = "ul. Marszałkowska 1", 
            ZipCode = "12-690", 
            Country = "Polska" 
        };
        
        
        var newStore = new Store
        {
            Name = "Nowe MediaExpert",
            Address = adres1,
            PhoneNumber = "+48863972431"
            
        };

        
        var newEmployee = new Employee
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            PhoneNumber = "+48732456823",
            Email = "jan.kowalski@firma.pl",
            WorkPlace = newStore,
            Position = EmployeePosition.Cashier,
            Salary = 4500m,
            
            
        };


        context.Stores.Add(newStore);       
        context.Employees.Add(newEmployee); 


        context.SaveChanges();

        System.Console.WriteLine("Dodano przykładowe dane (Sklep + Pracownik).");
    }
}