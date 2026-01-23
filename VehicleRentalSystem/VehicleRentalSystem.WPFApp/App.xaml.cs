using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using VehicleRentalSystem.View;
using VehicleRentalSystem.View.Abstractions;

namespace VehicleRentalSystem.WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<IMainWindow, MainWindow>();
            services.AddTransient<IAddDepartmentWindow, AddDepartmentWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetRequiredService<IMainWindow>();
            mainWindow.DataContext = new MainViewModel(mainWindow, _serviceProvider);
            mainWindow.Show();
        }
    }
}
