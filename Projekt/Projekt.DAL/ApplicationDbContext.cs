using Microsoft.EntityFrameworkCore;
using Projekt.Model;

namespace Projekt.DAL
{
    public class ApplicationDbContext : DbContext
    {  
        public DbSet<Car> Car { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Motorbike> Motorbikes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Service> Services { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
