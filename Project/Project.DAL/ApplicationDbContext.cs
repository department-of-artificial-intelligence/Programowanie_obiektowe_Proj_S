using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class ApplicationDbContext : DbContext
    {
        // Define Tables
        public DbSet<Pizzeria> Pizzerias { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Configure Rules
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Inheritance Logic
            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("PersonType")
                .HasValue<Client>("Client")
                .HasValue<KitchenWorker>("KitchenWorker")
                .HasValue<HallWorker>("HallWorker");

            modelBuilder.Entity<Pizzeria>()
                .HasMany(p => p.Workers)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Set precision for money
            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Worker>()
                .Property(w => w.Salary)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
