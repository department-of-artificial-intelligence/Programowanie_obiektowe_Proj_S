using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CompanyCar> CompanyCars { get; set; }
        public DbSet<DeliveryVan> DeliveryVans { get; set; }
        public DbSet<SemiTrailer> SemiTrailers { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
