using Microsoft.EntityFrameworkCore;
using Projekt.Model;

namespace Projekt.DATABASE
{
    public class AppDbContext:DbContext
    {
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
        }

    }
}
