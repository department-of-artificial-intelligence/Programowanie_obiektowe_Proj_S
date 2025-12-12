using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Project.Configuration
{
    public class ApplicationConfigurationLoader<T>
    {
        private readonly string _jsonFileName;

        private readonly string _sectionName;

        private readonly Assembly? _assembly;

        public ApplicationConfigurationLoader(string jsonFileName = "appsettings.json", string sectionName = "Application", Assembly? assembly = null)
        {
            this._jsonFileName = jsonFileName;
            this._sectionName = sectionName;
            this._assembly = assembly;
        }

        public T LoadConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(this._jsonFileName, optional: true, reloadOnChange: false);

            if (this._assembly is not null)
            {
                configurationBuilder.AddUserSecrets(this._assembly);
            }    
                
            configurationBuilder.AddEnvironmentVariables();
            
            return configurationBuilder.Build()
                .GetSection(this._sectionName)
                .Get<T>()!;
        }
    }
}