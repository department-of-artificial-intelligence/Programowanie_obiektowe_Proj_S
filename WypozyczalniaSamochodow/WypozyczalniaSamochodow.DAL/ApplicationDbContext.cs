using Microsoft.EntityFrameworkCore;
using WypozyczalniaSamochodow.Model;

namespace WypozyczalniaSamochodow.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
