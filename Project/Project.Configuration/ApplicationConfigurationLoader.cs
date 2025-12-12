using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Project.Configuration
{
    public class ApplicationConfigurationLoader<T>
    {
        public T LoadConfiguration(string section = "Application", Assembly? assembly = null)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            if (assembly is not null)
            {
                configurationBuilder.AddUserSecrets(assembly);
            }    
                
            configurationBuilder
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            
            return configurationBuilder.Build()
                .GetSection(section)
                .Get<T>()!;
        }
    }
}