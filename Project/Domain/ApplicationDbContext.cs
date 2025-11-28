using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.Domain;
public class ApplicationDbContext : DbContext
{

    public DbSet<User> Users { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<MovieMark> MovieMarks { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public string DbPath { get; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    // - - - - - - - - - - - - - - - - - -
    // Temp code | will be deleted soon
    // - - - - - - - - - - - - - - - - - -

    // Default constructor for usage in Program.cs
    public ApplicationDbContext() {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        DbPath = System.IO.Path.Join(path, "application.db");
    }

    // Defining SQLite as Application Database | Use DatabaseConfiguration.cs instead
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder.UseSqlite($"Data Source={DbPath}");


}
