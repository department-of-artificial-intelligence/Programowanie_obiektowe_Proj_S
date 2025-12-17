using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Project.DAL
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=HotelManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
            );
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
