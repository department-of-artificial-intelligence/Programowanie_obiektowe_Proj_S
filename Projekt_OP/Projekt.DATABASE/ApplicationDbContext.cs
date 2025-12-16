using Microsoft.EntityFrameworkCore;
using Projekt.Model;

namespace Projekt.DATABASE
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Hall> Halls { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }
    }
}
