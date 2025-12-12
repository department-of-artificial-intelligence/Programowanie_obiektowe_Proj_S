
using Project.Model;
using Project.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Project.ConsoleApp
{
    public class Program
    {
        // Testing Entity Framework
        static async Task Main(string[] args) {

            // Setup and configure application database and migrations
            var context = DatabaseConfiguration.Configure(args);

            // Setup default 'mock' data for application test
            context.SeedDatabase(); // Method which generate mock data to DB
        }
    }
}
