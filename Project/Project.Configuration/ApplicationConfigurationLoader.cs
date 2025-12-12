using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Project.Configuration
{
    public class ApplicationConfigurationLoader<T>
    {
        public T LoadConfiguration(string section = "Application", Assembly? assembly = null)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

            if (assembly is not null)
            {
                configurationBuilder.AddUserSecrets(assembly);
            }    
                
            configurationBuilder.AddEnvironmentVariables();
            
            return configurationBuilder.Build()
                .GetSection(section)
                .Get<T>()!;
        }
    }
}