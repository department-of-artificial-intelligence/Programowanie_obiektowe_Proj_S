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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Cars)
                      .WithOne(c => c.Branch)
                      .HasForeignKey(c => c.BranchId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Customers)
                      .WithOne(c => c.Branch)
                      .HasForeignKey(c => c.BranchId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Rentals)
                      .WithOne(r => r.Branch)
                      .HasForeignKey(r => r.BranchId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PricePerDay).HasColumnType("decimal(18,2)");
                entity.Property(e => e.EngineVolume).HasColumnType("decimal(5,1)");
                entity.Property(e => e.AvgConsumption).HasColumnType("decimal(5,1)");

                entity.HasMany(e => e.Rentals)
                      .WithOne(r => r.Car)
                      .HasForeignKey(r => r.CarId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(e => e.Rentals)
                      .WithOne(r => r.Customer)
                      .HasForeignKey(r => r.CustomerId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Cost).HasColumnType("decimal(18,2)");
            });
        }

    }
}