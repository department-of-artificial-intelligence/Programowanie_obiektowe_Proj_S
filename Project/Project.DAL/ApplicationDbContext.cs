using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
