using Microsoft.EntityFrameworkCore;
using Project.Model;
using System.Collections.Generic;

namespace Project.DAL;

public class ApplicationDbContext: DbContext
{
    public DbSet<Bicycle> Bicycles { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<RentalRecord> Rentals { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Bicycle>()
            .Property(b => b.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Person>()
        .HasDiscriminator<string>("UserType")
        .HasValue<Employee>("Employee")
        .HasValue<Customer>("Customer");
    }
}

