using Project.WPFApp.ViewModels;
using Project.WPFApp.Windows;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Project.WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            new DatabaseConnectionWindow().ShowDialog();
        }
    }
}
