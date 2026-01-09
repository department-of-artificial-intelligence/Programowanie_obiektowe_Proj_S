using Microsoft.EntityFrameworkCore;
using VehicleRentalSystem.Model;

namespace VehicleRentalSystem.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Vehicle)
                .WithMany()
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Department)
                .WithMany()
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}