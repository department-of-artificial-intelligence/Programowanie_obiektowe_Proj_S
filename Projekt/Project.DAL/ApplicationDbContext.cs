using Microsoft.EntityFrameworkCore;
using Projekt.Model;

namespace Projekt.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja typów danych dla wartości pieniężnych.

            modelBuilder.Entity<Car>()
                .Property(c => c.DailyRate)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Rental>()
                .Property(r => r.TotalCost)
                .HasColumnType("decimal(18,2)");

            // Relacja: Wypożyczenie -> Samochód

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany()
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja: Wypożyczenie -> Klient

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.RentalHistory)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja: Wypożyczenie -> Oddział

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.PickupBranch)
                .WithMany()
                .HasForeignKey(r => r.PickupBranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}