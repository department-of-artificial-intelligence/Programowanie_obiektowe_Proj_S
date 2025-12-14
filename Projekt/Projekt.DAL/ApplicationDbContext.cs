using Microsoft.EntityFrameworkCore;
using Projekt.Model;

namespace Projekt.DAL
{
    public class ApplicationDbContext : DbContext
    {  
        public DbSet<Car> Car { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


    }
}
