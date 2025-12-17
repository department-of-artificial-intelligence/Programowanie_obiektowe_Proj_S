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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasDiscriminator<string>("VehicleType")
                .HasValue<CompanyCar>("CompanyCar")
                .HasValue<Truck>("Truck")
                .HasValue<DeliveryVan>("DeliveryVan")
                .HasValue<SemiTrailer>("SemiTrailer");

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.AssignedVehicle)
                .WithOne(v => v.AssignedDriver)
                .HasForeignKey<Driver>("VehicleId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.AssignedDriver)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
