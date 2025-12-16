using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;

namespace RestaurantManagement.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Employee -> Restaurant - NoAction ondelete
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Restaurant)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);

            //Reservation -> Restaurant- NoAction ondelete
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Restaurant)
                .WithMany(rest => rest.Reservations)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);

            // MenuItem -> Restaurant - NoAction ondelete
            modelBuilder.Entity<MenuItem>()
                .HasOne(m => m.Restaurant)
                .WithMany(r => r.Menu)
                .HasForeignKey(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
