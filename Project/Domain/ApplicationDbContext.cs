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
}
