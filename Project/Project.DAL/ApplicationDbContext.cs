using Microsoft.EntityFrameworkCore;
using Project.Model;

namespace Project.DAL;
public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
}
