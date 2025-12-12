using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Project.Configuration;

namespace Project.DAL
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly Action<DbContextOptionsBuilder> _action;

        public ApplicationDbContextFactory()
        {
            this._action = optionsBuilder =>
            {
                var configuration = new ApplicationConfigurationLoader<ApplicationConfiguration>().LoadConfiguration();
                
                optionsBuilder.UseSqlServer(configuration.ConnectionString);
            };
        }
        
        public ApplicationDbContextFactory(Action<DbContextOptionsBuilder> action)
        {
            this._action = action;
        }
        
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            this._action(optionsBuilder);
            
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}