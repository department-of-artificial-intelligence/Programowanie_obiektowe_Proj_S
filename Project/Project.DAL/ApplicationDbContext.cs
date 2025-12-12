using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Play> Plays { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<TheaterNetwork> TheaterNetworks { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Theater> Theaters { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Performance> Performances { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Precyzja
        modelBuilder.Entity<Actor>()
            .Property(a => a.Salary)
            .HasPrecision(18, 2);
        modelBuilder.Entity<Director>()
            .Property(d => d.Salary)
            .HasPrecision(18, 2);
        modelBuilder.Entity<Ticket>()
            .Property(t => t.Price)
            .HasPrecision(18, 2);

        base.OnModelCreating(modelBuilder);
    }
}