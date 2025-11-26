namespace RatingSystem.DataAcces;
using Microsoft.EntityFrameworkCore;
using RatingSystem.Domain;
public class AplicationDbContext : DbContext
{
    public DbSet<Service> Services { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<User> Users { get; set; }

    public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=RatingSystem.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Rating>()
            .HasOne(s => s.Service)
            .WithMany()
            .HasForeignKey(r => r.ServiceId);
    }
}