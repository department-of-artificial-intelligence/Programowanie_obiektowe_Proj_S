using Project.Configuration;
using Project.ConsoleApp.ApplicationModes;
using Project.ConsoleApp.ApplicationModes.Interactive;
using Project.DAL;

namespace Project.ConsoleApp
{
    internal static class Program
    {
        private static readonly IApplicationMode PreferredMode = new InteractiveMode();
        
        public static async Task Main()
        {
            var configuration = new ApplicationConfigurationLoader<DatabaseConfiguration>(sectionName: "Database")
                .LoadConfiguration();

            var databaseContext = new ApplicationDbContextFactory(configuration)
                .CreateDbContext([]);

            await databaseContext.Prepare();
            
            await PreferredMode.Run(new ApplicationContext(databaseContext));
        }
    }
}
